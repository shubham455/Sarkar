using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace betplayer.Superagent
{
    public partial class UpdateClientLimit : System.Web.UI.Page
    {
        private DataTable UpdateTable;
        public string Agentcode = "";
        public DataTable UpdateDataTable { get { return UpdateTable; } }


        protected void Page_Load(object sender, EventArgs e)
        {
            UpdateTable = new DataTable();
            UpdateTable.Columns.Add(new DataColumn("ClientID"));
            UpdateTable.Columns.Add(new DataColumn("Name"));
            UpdateTable.Columns.Add(new DataColumn("FixLimit"));
            UpdateTable.Columns.Add(new DataColumn("CurrentLimit"));
            UpdateTable.Columns.Add(new DataColumn("UsedLimit"));
            DataRow row = UpdateTable.NewRow();


            string CN = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            using (MySqlConnection cn = new MySqlConnection(CN))
            {
                cn.Open();
                Agentcode = Request.QueryString["AgentID"];
                string Agentlimit = "Select * From AgentMaster where code = '" + Agentcode + "'";
                MySqlCommand Agentlimitcmd = new MySqlCommand(Agentlimit, cn);
                MySqlDataAdapter Agentlimitadp = new MySqlDataAdapter(Agentlimitcmd);
                DataTable Agentlimitdt = new DataTable();
                Agentlimitadp.Fill(Agentlimitdt);

                agentlimit.Value = Agentlimitdt.Rows[0]["Currentlimit"].ToString();


                string Select = "Select * From ClientMaster where  CreatedBy = '" + Agentcode + "'";
                MySqlCommand cmd = new MySqlCommand(Select, cn);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                Decimal Total = 0;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int ClientID = Convert.ToInt16(dt.Rows[i]["ClientID"]);
                    String Name = dt.Rows[i]["Name"].ToString();
                    string FixLimit = dt.Rows[i]["Fixlimit"].ToString();

                    Decimal ClientCurrentLimit = Convert.ToDecimal(dt.Rows[i]["CurrentLimit"]);
                    Total = Total + ClientCurrentLimit;
                    row["ClientID"] = ClientID;
                    row["Name"] = Name;
                    row["Fixlimit"] = FixLimit;
                    row["Currentlimit"] = ClientCurrentLimit;

                    string undeclarematches = "Select * From Matches where declear = '0' and Active = '1'";
                    MySqlCommand undeclarematchescmd = new MySqlCommand(undeclarematches, cn);

                    MySqlDataAdapter undeclarematchesadp = new MySqlDataAdapter(undeclarematchescmd);
                    DataTable undeclarematchesdt = new DataTable();
                    undeclarematchesadp.Fill(undeclarematchesdt);
                    int finalTotalPosition = 0;
                    for (int k = 0; k < undeclarematchesdt.Rows.Count; k++)
                    {
                        int MatchID = Convert.ToInt32(undeclarematchesdt.Rows[k]["apiID"]);

                        string Runner = "Select Position1,Position2 From Runner where ClientID = '" + ClientID + "' and MatchID = '" + MatchID + "' order by DateTime DESC ";
                        MySqlCommand Runnercmd = new MySqlCommand(Runner, cn);
                        MySqlDataAdapter Runneradp = new MySqlDataAdapter(Runnercmd);
                        DataTable Runnerdt = new DataTable();
                        Runneradp.Fill(Runnerdt);

                        if (Runnerdt.Rows.Count > 0)
                        {
                            int Position1 = Convert.ToInt32(Runnerdt.Rows[0]["Position1"]);
                            int Position2 = Convert.ToInt32(Runnerdt.Rows[0]["Position2"]);

                            int TotalPosition = 0;
                            if (Position1 < 0 && Position2 < 0)
                            {
                                if (Position1 > Position2)
                                {
                                    TotalPosition = Position2;
                                    
                                }
                                else if (Position2 > Position1)
                                {
                                    TotalPosition = Position1;
                                }
                            }
                            else if (Position1 > 0 && Position2 > 0)
                            {
                                if (Position1 < Position2)
                                {
                                    TotalPosition = Position1;
                                }
                                else if (Position2 < Position1)
                                {
                                    TotalPosition = Position2;
                                }
                            }

                            else if (Position1 < 0)
                            {
                                TotalPosition = Position1;
                            }
                            else if (Position2 < 0)
                            {
                                TotalPosition = Position2;
                            }

                            finalTotalPosition = finalTotalPosition + TotalPosition;
                            decimal finalsessionamount= SessionCalculation(ClientID, MatchID);
                            decimal finaldeclaresessionamount = declareSessionAmount(ClientID, MatchID);
                            decimal finalusedlimit = finalTotalPosition - finalsessionamount + finaldeclaresessionamount;
                            row["Usedlimit"] = finalusedlimit;
                        }

                        else
                        {
                            decimal finalsessionamount = SessionCalculation(ClientID, MatchID);
                            decimal finaldeclaresessionamount = declareSessionAmount(ClientID, MatchID);
                            decimal finalusedlimit = finalsessionamount - finaldeclaresessionamount;
                            row["Usedlimit"] = finalusedlimit;

                        }

                    }
                    UpdateTable.Rows.Add(row.ItemArray);
                    TotalLimit.Value = String.Format("{0:C0}", Total);
                }
            }
        }
        public decimal SessionCalculation(Decimal ClientID, int apiID)
        {
            string CN = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            using (MySqlConnection cn = new MySqlConnection(CN))
            {
                cn.Open();

                string CheckSession = "Select * From Session where  ClientID = '" + ClientID + "' && MatchID  = '" + apiID + "' Group By Session";
                MySqlCommand Sessioncmd = new MySqlCommand(CheckSession, cn);
                MySqlDataAdapter Sessionadp = new MySqlDataAdapter(Sessioncmd);
                DataTable Sessiondt = new DataTable();
                Sessionadp.Fill(Sessiondt);
                decimal Amount1 = 0;
                if (Sessiondt.Rows.Count > 0)
                {
                    for (int j = 0; j < Sessiondt.Rows.Count; j++)
                    {
                        string session = (Sessiondt.Rows[j]["Session"]).ToString();

                        string CheckSession1 = "Select * From Session where  ClientID = '" + ClientID + "' && MatchID = '"+apiID+"' && Session = '" + session + "' order by  DATETIME DESC";
                        MySqlCommand Sessioncmd1 = new MySqlCommand(CheckSession1, cn);
                        MySqlDataAdapter Sessionadp1 = new MySqlDataAdapter(Sessioncmd1);
                        DataTable Sessiondt1 = new DataTable();
                        Sessionadp1.Fill(Sessiondt1);
                        if (Sessiondt1.Rows.Count > 0)
                        {
                            decimal lastAmount = Convert.ToDecimal(Sessiondt1.Rows[0]["Position"]);
                            Amount1 = Amount1 + lastAmount;

                        }
                    }
                }
                else
                {
                    Amount1 = 0;
                }
                return Amount1;
            }

        }
        public Decimal declareSessionAmount(Decimal ClientID, int apiID)
        {
            Decimal TotalAmount1 = 0;
            string CN = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            using (MySqlConnection cn = new MySqlConnection(CN))
            {
                cn.Open();


                decimal finaltotalamount = 0;

                string Amount = "Select TotalAmount from SessionCalculation where ClientID = '" + ClientID + "' && MatchID = '" + apiID + "'";
                MySqlCommand Amountcmd = new MySqlCommand(Amount, cn);
                MySqlDataAdapter Amountadp = new MySqlDataAdapter(Amountcmd);
                DataTable Amountdt = new DataTable();
                Amountadp.Fill(Amountdt);
                if (Amountdt.Rows.Count > 0)
                {
                    Decimal TotalAmount = 0;

                    for (int j = 0; j < Amountdt.Rows.Count; j++)
                    {
                        TotalAmount = Convert.ToDecimal(Amountdt.Rows[j]["TotalAmount"]);
                        TotalAmount1 = TotalAmount1 + TotalAmount;
                    }
                    TotalAmount = 0;
                    finaltotalamount = finaltotalamount + TotalAmount1;


                }

                return finaltotalamount;
            }
        }




        protected void btnUpdate_Click(object sender, EventArgs e)
        {

        }
    }
}