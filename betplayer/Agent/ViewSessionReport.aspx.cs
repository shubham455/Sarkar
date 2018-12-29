using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace betplayer.Agent
{
    public partial class ViewSessionReport : System.Web.UI.Page
    {
        private DataTable dt;
        private DataTable runTable;
        public DataTable MatchesDataTable { get { return dt; } }
        public DataTable runTable1 { get { return runTable; } }



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int MatchID = Convert.ToInt32(Request.QueryString["MatchID"]);
                string CN = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                using (MySqlConnection cn = new MySqlConnection(CN))
                {
                    cn.Open();
                    string Client = "Select * From ClientMaster where CreatedBy = '" + Session["Agentcode"] + "'";
                    MySqlCommand Clientcmd = new MySqlCommand(Client, cn);
                    MySqlDataAdapter Clientadp = new MySqlDataAdapter(Clientcmd);
                    DataTable Clientdt = new DataTable();
                    Clientadp.Fill(Clientdt);
                    DropDownClient.DataSource = Clientdt;
                    DropDownClient.DataTextField = "Name";
                    DropDownClient.DataValueField = "ClientID";
                    DropDownClient.DataBind();

                    string s1 = "Select SessionID,Session From Session where MatchID = '" + MatchID + "' group by session";
                    MySqlCommand cmd1 = new MySqlCommand(s1, cn);
                    MySqlDataAdapter adp1 = new MySqlDataAdapter(cmd1);
                    DataTable dt1 = new DataTable();
                    adp1.Fill(dt1);
                    DropDownSession.DataSource = dt1;
                    DropDownSession.DataBind();
                    DropDownSession.DataTextField = "Session";
                    DropDownSession.DataValueField = "Session";
                    DropDownSession.DataBind();



                    string s2 = "select Session.sessionID,Session.session,Session.Runs,Session.Amount,Session.rate,Session.Mode,Session.DateTime,Session.Team,Session.clientID,clientmaster.Name from Session inner join clientmaster on Session.ClientID = clientmaster.ClientID where clientmaster.mode = 'Agent' && clientmaster.CreatedBy = '" + Session["Agentcode"] + "' && Session.MatchID = '0'";
                    MySqlCommand cmd2 = new MySqlCommand(s2, cn);
                    MySqlDataAdapter adp2 = new MySqlDataAdapter(cmd2);
                    dt = new DataTable();
                    adp2.Fill(dt);

                    runTable = new DataTable();
                    runTable.Columns.Add(new DataColumn("RUNS"));
                    runTable.Columns.Add(new DataColumn("AMOUNT"));
                    if (dt.Rows.Count > 0)
                    {
                        int runs = Convert.ToInt16(dt.Rows[0]["Runs"]);
                        int Amount = Convert.ToInt32(dt.Rows[0]["Amount"]);
                        for (int i = runs - 10; i <= runs + 10; i++)
                        {

                            DataRow row = runTable.NewRow();
                            row["RUNS"] = i.ToString();
                            if (i < runs)
                            {
                                int Amount1 = Amount * -1;
                                row["Amount"] =
                                    System.Drawing.Color.Red;
                                row["AMOUNT"] = Amount1;

                            }
                            if (i >= runs)
                            {
                                int Amount1 = Amount;
                                row["Amount"] = System.Drawing.Color.Blue;
                                row["AMOUNT"] = Amount1;
                            }


                            runTable.Rows.Add(row.ItemArray);

                        }
                    }

                }
            }
        }

        protected void btnview_Click(object sender, EventArgs e)
        {
            int MatchID = Convert.ToInt32(Request.QueryString["MatchID"]);
            string CN = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            using (MySqlConnection cn = new MySqlConnection(CN))
            {
                cn.Open();

                string declaresession = "Select * From DeclaredSession where Session = '" + DropDownSession.SelectedItem.Text + "' && MatchID = '"+MatchID+"'";
                MySqlCommand declaresessioncmd = new MySqlCommand(declaresession, cn);
                MySqlDataAdapter declaresessionadp = new MySqlDataAdapter(declaresessioncmd);
                DataTable declaresessiondt = new DataTable();
                declaresessionadp.Fill(declaresessiondt);
                if (declaresessiondt.Rows.Count > 0)
                {
                    lblrate.Text = declaresessiondt.Rows[0]["DeclareRun"].ToString();
                }
                else
                {
                    lblrate.Text = "";
                }

                if (DropDownClient.Text == "All Client")
                {
                    showsessions("All Client");
                }
                else
                {
                    showsessions(DropDownClient.SelectedItem.Value);
                }


            }
        }


        public void showsessions(string Client)
        {
            int MatchID = Convert.ToInt32(Request.QueryString["MatchID"]);
            string CN = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            using (MySqlConnection cn = new MySqlConnection(CN))
            {
                cn.Open();
                string s2 = "";
                if (Client == "All Client")
                {
                    s2 = "select Session.sessionID,Session.session,Session.Runs,Session.Amount,Session.rate,Session.Mode,Session.DateTime,Session.Team,Session.clientID,clientmaster.Name from Session inner join clientmaster on Session.ClientID = clientmaster.ClientID where clientmaster.mode = 'Agent' && clientmaster.CreatedBy = '" + Session["Agentcode"] + "'  && Session.MatchID = '" + MatchID + "' && Session.Session = '" + DropDownSession.SelectedItem.Text + "' order by Session.DateTime DESC";

                }
                else
                {
                    s2 = "select Session.sessionID,Session.session,Session.Runs,Session.Amount,Session.rate,Session.Mode,Session.DateTime,Session.Team,Session.clientID,clientmaster.Name from Session inner join clientmaster on Session.ClientID = clientmaster.ClientID where clientmaster.mode = 'Agent' && clientmaster.CreatedBy = '" + Session["Agentcode"] + "' && Session.ClientID = '" + DropDownClient.SelectedItem.Value + "' && Session.MatchID = '" + MatchID + "' && Session.Session = '" + DropDownSession.SelectedItem.Text + "' order by Session.DateTime DESC";
                }
                MySqlCommand cmd2 = new MySqlCommand(s2, cn);
                MySqlDataAdapter adp2 = new MySqlDataAdapter(cmd2);
                dt = new DataTable();
                adp2.Fill(dt);

                runTable = new DataTable();
                runTable.Columns.Add(new DataColumn("RUNS"));
                runTable.Columns.Add(new DataColumn("AMOUNT"));

                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    if (j == 0)
                    {

                        int runs = Convert.ToInt16(dt.Rows[j]["Runs"]);
                        int Amount = Convert.ToInt32(dt.Rows[j]["Amount"]);
                        Decimal Rate = Convert.ToDecimal(dt.Rows[j]["Rate"]);
                        string Mode = dt.Rows[j]["Mode"].ToString();
                        string ClientID = dt.Rows[j]["ClientID"].ToString();

                        string selectAgentshare = "select Agent_share From ClientMaster where ClientID = '" + ClientID + "'";
                        MySqlCommand selectAgentsharecmd = new MySqlCommand(selectAgentshare, cn);
                        MySqlDataAdapter selectAgentshareadp = new MySqlDataAdapter(selectAgentsharecmd);
                        DataTable selectAgentsharedt = new DataTable();
                        selectAgentshareadp.Fill(selectAgentsharedt);


                        Decimal AgentShare = Convert.ToDecimal(selectAgentsharedt.Rows[j]["Agent_Share"]);
                        Decimal AgentShare1 = AgentShare / 100;



                        for (int i = runs + 5; i >= runs - 5; i--)
                        {

                            DataRow row = runTable.NewRow();
                            row["RUNS"] = i.ToString();
                            row["Amount"] = CalculateAmount(Mode,
                                i, 0,
                                Rate,
                                runs, Amount,
                                AgentShare1).ToString();
                            runTable.Rows.Add(row.ItemArray);
                        }

                    }
                    else
                    {

                        int runs = Convert.ToInt32(dt.Rows[j]["Runs"]);
                        int Amount = Convert.ToInt32(dt.Rows[j]["Amount"]);
                        Decimal Rate = Convert.ToDecimal(dt.Rows[j]["Rate"]);
                        string Mode = dt.Rows[j]["Mode"].ToString();
                        string ClientID = dt.Rows[j]["ClientID"].ToString();


                        string selectAgentshare = "select Agent_share From ClientMaster where ClientID = '" + ClientID + "'";
                        MySqlCommand selectAgentsharecmd = new MySqlCommand(selectAgentshare, cn);
                        MySqlDataAdapter selectAgentshareadp = new MySqlDataAdapter(selectAgentsharecmd);
                        DataTable selectAgentsharedt = new DataTable();
                        selectAgentshareadp.Fill(selectAgentsharedt);


                        Decimal AgentShare = Convert.ToDecimal(selectAgentsharedt.Rows[0]["Agent_Share"]);
                        Decimal AgentShare1 = AgentShare / 100;
                        int highVal = Convert.ToInt32(runTable.Rows[0]["Runs"]);
                        int lowVal = Convert.ToInt32(runTable.Rows[runTable.Rows.Count - 1]["Runs"]);
                        if (runs > highVal)
                        {
                            for (int i = runs + 5; i > highVal; i--)
                            {
                                DataRow row = runTable.NewRow();
                                row["RUNS"] = i.ToString();
                                row["Amount"] = runTable.Rows[0]["Amount"];

                                runTable.Rows.InsertAt(row, (runs + 5 - i));
                            }
                        }
                        else if ((runs - 5) < lowVal)
                        {
                            for (int i = lowVal - 1; i >= runs - 5; i--)
                            {
                                DataRow row = runTable.NewRow();
                                row["RUNS"] = i.ToString();
                                row["Amount"] = runTable.Rows[runTable.Rows.Count - 1]["Amount"];

                                runTable.Rows.InsertAt(row, ((highVal - lowVal) + (lowVal - i)));
                            }
                        }

                        for (int i = 0; i < runTable.Rows.Count; i++)
                        {
                            DataRow row = runTable.Rows[i];
                            if (Convert.ToInt16(runTable.Rows[i]["Runs"]) >= Convert.ToInt16(dt.Rows[0]["Runs"]) &&
                                Convert.ToInt16(runTable.Rows[i]["Runs"]) < Convert.ToInt16(dt.Rows[j]["Runs"]))
                            {


                                row["AMOUNT"] = CalculateAmount(Mode,
                                    Convert.ToInt16(runTable.Rows[i]["Runs"]),
                                    Convert.ToDecimal(runTable.Rows[i]["Amount"]),
                                    Rate,
                                    runs,
                                    Amount,
                                    AgentShare1).ToString();
                            }
                            else
                            {
                                row["AMOUNT"] = CalculateAmount(Mode,
                                    Convert.ToInt16(runTable.Rows[i]["Runs"]),
                                    Convert.ToDecimal(runTable.Rows[i]["Amount"]),
                                    Rate,
                                    runs,
                                    Amount,
                                    AgentShare1).ToString();
                            }
                        }
                    }
                }

            }
        }
        public Decimal CalculateAmount(string Mode, int Initruns, Decimal InitAmount, Decimal Rate, int runs, int Amount, Decimal AgentShare1)
        {
            Decimal Difference = 0;

            if (Initruns < runs)
            {

                if (Mode == "Y")
                {
                    Difference = Amount * AgentShare1 + InitAmount;
                }
                else if (Mode == "N")
                {
                    Difference = Amount * -1 * AgentShare1 + InitAmount;
                }


            }
            if (Initruns >= runs)
            {
                if (Mode == "Y")
                {
                    Difference = Amount * Rate * -1 * AgentShare1 + InitAmount;
                }
                else if (Mode == "N")
                {
                    Difference = Amount * Rate * AgentShare1 + InitAmount;
                }


            }
            return Difference;
        }
    }
}