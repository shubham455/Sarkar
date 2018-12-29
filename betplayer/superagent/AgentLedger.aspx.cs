using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace betplayer.Super_Agent
{
    public partial class Agent_Ledger : System.Web.UI.Page
    {
        private DataTable dt1;
        private DataTable runtable;
        private DataTable runtable1;
        public DataTable LedgerTableOrdered;
        public DataTable runTable { get { return runtable; } }
        public DataTable runTable1 { get { return runtable1; } }
        public Boolean emptyLedgerTable = false;


        protected void Page_Load(object sender, EventArgs e)
        {


            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            if (!IsPostBack)
            {
                string CN = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                using (MySqlConnection cn = new MySqlConnection(CN))
                {
                    cn.Open();


                    string s = "Select * From AgentMaster where CreatedBy = '" + Session["SuperAgentcode"] + "' And  Mode = 'SuperAgent'";
                    MySqlCommand cmd = new MySqlCommand(s, cn);
                    MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);
                    dropdownAgent.DataSource = dt;
                    dropdownAgent.DataTextField = "Name";
                    dropdownAgent.DataValueField = "AgentID";
                    dropdownAgent.DataBind();


                    string ClientCollection = "Select * From AgentCollectionName where CreatedBy = '" + Session["SuperAgentcode"] + "'";
                    MySqlCommand ClientCollectioncmd = new MySqlCommand(ClientCollection, cn);
                    MySqlDataAdapter ClientCollectionadp = new MySqlDataAdapter(ClientCollectioncmd);
                    DataTable ClientCollectiondt = new DataTable();
                    ClientCollectionadp.Fill(ClientCollectiondt);
                    dropdowncollection.DataSource = ClientCollectiondt;
                    dropdowncollection.DataTextField = "Name";
                    dropdowncollection.DataValueField = "AgentCollectionnameID";
                    dropdowncollection.DataBind();



                    string s1 = "Select * From Agentledger where AgentID = '" + dropdownAgent.SelectedValue + "'";
                    MySqlCommand cmd1 = new MySqlCommand(s1, cn);
                    MySqlDataAdapter adp1 = new MySqlDataAdapter(cmd1);
                    runtable1 = new DataTable();
                    runtable = new DataTable();
                    LedgerTableOrdered = new DataTable();
                    adp1.Fill(runtable1);
                    adp1.Fill(runtable);
                    adp1.Fill(LedgerTableOrdered);
                }
            }
        }


        public void dropdownAgent_SelectedIndexChanged(object sender, EventArgs e)
        {


            runtable = new DataTable();
            runtable.Columns.Add(new DataColumn("ID"));
            DataColumn colDateTime = new DataColumn("Date");
            colDateTime.DataType = System.Type.GetType("System.DateTime");
            runtable.Columns.Add(colDateTime);

            runtable.Columns.Add(new DataColumn("CollectionID"));
            runtable.Columns.Add(new DataColumn("CollectionName"));
            runtable.Columns.Add(new DataColumn("Dabit"));
            runtable.Columns.Add(new DataColumn("Credit"));
            runtable.Columns.Add(new DataColumn("Balance"));
            runtable.Columns.Add(new DataColumn("PaynmentDescription"));
            runtable.Columns.Add(new DataColumn("Remark"));

            runtable1 = new DataTable();
            runtable1.Columns.Add(new DataColumn("TotalDabitAmount"));
            runtable1.Columns.Add(new DataColumn("TotalCreditAmount"));
            runtable1.Columns.Add(new DataColumn("TotalBalanceAmount"));

            DataRow row = runTable.NewRow();
            DataRow row1 = runtable1.NewRow();



            string CN = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            using (MySqlConnection cn = new MySqlConnection(CN))
            {
                cn.Open();
                string s = "select matches.TeamA,matches.teamB,matches.DateTime,agentledger.agentledgerID,agentledger.Dabit,agentledger.Credit from agentLedger inner join matches on agentledger.MatchID = matches.apiID where agentID = '" + dropdownAgent.SelectedValue + "' order by  matches.DateTime ASC";
                MySqlCommand cmd = new MySqlCommand(s, cn);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                dt1 = new DataTable();
                adp.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {

                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        int ID = Convert.ToInt32(dt1.Rows[i]["agentledgerID"]);
                        string DateFromDB = dt1.Rows[i]["DateTime"].ToString();
                        DateTime oDate = DateTime.Parse(DateFromDB);
                        string datetime = oDate.Date.ToString().Substring(0, 10);

                        string TeamA = dt1.Rows[i]["TeamA"].ToString();
                        string TeamB = dt1.Rows[i]["TeamB"].ToString();
                        decimal Dabit = Convert.ToDecimal(dt1.Rows[i]["Dabit"]);
                        decimal Credit = Convert.ToDecimal(dt1.Rows[i]["Credit"]);

                        row["CollectionID"] = ID;
                        row["Date"] = oDate;     //row["Date"] = datetime;
                        row["PaynmentDescription"] = TeamA + "VS" + TeamB;
                        row["Dabit"] = Dabit * -1;
                        row["Credit"] = Credit;

                        decimal Balance = 0, Balance1 = 0;
                        if (Dabit != 0)
                        {
                            Balance = Dabit * -1;
                        }
                        else if (Credit != 0)
                        {
                            Balance = Credit;
                        }
                        if (runtable.Rows.Count > 0)
                        {

                            Balance1 = Convert.ToDecimal(runtable.Rows[runtable.Rows.Count - 1]["Balance"]);
                            Balance1 = Balance1 + Balance;

                            row["Balance"] = Balance1;
                        }

                        else
                        {
                            row["Balance"] = Balance;
                        }

                        runTable.Rows.Add(row.ItemArray);
                    }
                }
                string s1 = "Select * From AgentCollectionMaster where agentID = '" + dropdownAgent.SelectedValue + "'";
                MySqlCommand cmd1 = new MySqlCommand(s1, cn);
                MySqlDataAdapter adp1 = new MySqlDataAdapter(cmd1);
                DataTable dt = new DataTable();
                adp1.Fill(dt);

                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    int CollectionID = Convert.ToInt32(dt.Rows[j]["CollectionID"]);
                    string CollectionDate = dt.Rows[j]["Date"].ToString();
                    DateTime date = DateTime.Parse(CollectionDate);
                    string Date1 = date.Date.ToString().Substring(0, 10);


                    string CollectionName = dt.Rows[j]["CollectionType"].ToString();
                    decimal Amount = Convert.ToDecimal(dt.Rows[j]["Amount"]);
                    string PaynmentDescription = dt.Rows[j]["PaynmentType"].ToString();
                    string Remark = dt.Rows[j]["Remark"].ToString();

                    row["CollectionID"] = CollectionID;
                    row["Date"] = Date1;        //row["Date"] = Date1;
                    row["CollectionName"] = CollectionName;
                    row["Remark"] = Remark;
                    if (PaynmentDescription == "Payment Received")
                    {
                        row["Dabit"] = Amount * -1;
                        row["Credit"] = 0;
                        row["Balance"] = Amount * -1;
                    }
                    else if (PaynmentDescription == "Payment Paid")
                    {
                        row["Credit"] = Amount;
                        row["Dabit"] = 0;
                        row["Balance"] = Amount;
                    }



                    row["PaynmentDescription"] = PaynmentDescription;

                    runTable.Rows.Add(row.ItemArray);

                }
            }
            if (runtable.Rows.Count > 0)
            {
                IEnumerable<DataRow> orderedRows = runTable.AsEnumerable()
                .OrderBy(r => r.Field<DateTime>("Date"));

                LedgerTableOrdered = orderedRows.CopyToDataTable();

                for (int l = 0; l < LedgerTableOrdered.Rows.Count; l++)
                {
                    DateTime date = DateTime.Parse(LedgerTableOrdered.Rows[0]["Date"].ToString());
                    LedgerTableOrdered.Rows[0]["Date"] = date.Date.ToString().Substring(0, 10);
                    if (l > 0)
                    {
                        LedgerTableOrdered.Rows[l]["Balance"] = Convert.ToDecimal(LedgerTableOrdered.Rows[l - 1]["Balance"]) + Convert.ToDecimal(LedgerTableOrdered.Rows[l]["Dabit"]) + Convert.ToDecimal(LedgerTableOrdered.Rows[l]["Credit"]);


                    }

                }


                decimal TotalDabitAmount1 = 0;
                for (int l = 0; l < LedgerTableOrdered.Rows.Count; l++)
                {
                    decimal DabitAmount = Convert.ToDecimal(LedgerTableOrdered.Rows[l]["Dabit"]);
                    TotalDabitAmount1 = TotalDabitAmount1 + DabitAmount;
                }

                row1["TotalDabitAmount"] = TotalDabitAmount1;

                decimal TotalCreditAmount1 = 0;
                for (int l = 0; l < LedgerTableOrdered.Rows.Count; l++)
                {
                    decimal MatchAmount = Convert.ToDecimal(LedgerTableOrdered.Rows[l]["Credit"]);
                    TotalCreditAmount1 = TotalCreditAmount1 + MatchAmount;
                }

                row1["TotalCreditAmount"] = TotalCreditAmount1;

                decimal TotalbalanceAmount1 = 0;
                for (int l = 0; l < LedgerTableOrdered.Rows.Count; l++)
                {
                    decimal TotalAmount = Convert.ToDecimal(LedgerTableOrdered.Rows[l]["Balance"]);
                    TotalbalanceAmount1 = TotalbalanceAmount1 + TotalAmount;
                }

                row1["TotalBalanceAmount"] = TotalbalanceAmount1;


                runTable1.Rows.Add(row1.ItemArray);
            }
            else
            {
                emptyLedgerTable = true;



                row1["TotalDabitAmount"] = 0;
                row1["TotalCreditAmount"] = 0;
                row1["TotalBalanceAmount"] = 0;
                runTable1.Rows.Add(row1.ItemArray);
                DataTable LedgerTableOrdered1 = runtable1;

            }
        }


        protected void btnSave_ServerClick(object sender, EventArgs e)
        {

            string CN = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            using (MySqlConnection cn = new MySqlConnection(CN))
            {
                cn.Open();
                string s = "Insert Into AgentCollectionMaster (AgentID,CollectionType,Date,Amount,PaynmentType,Remark,SuperAgentID) values(@ClientID,@CollectionType,@Date,@Amount,@PaynmentType,@Remark,@AgentID)";
                MySqlCommand cmd = new MySqlCommand(s, cn);
                cmd.Parameters.AddWithValue("@ClientID", dropdownAgent.Text);
                cmd.Parameters.AddWithValue("@CollectionType", dropdowncollection.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Date", BillDate.Text);
                cmd.Parameters.AddWithValue("@Amount", Amount.Text);
                cmd.Parameters.AddWithValue("@PaynmentType", PaymentType.Value);
                cmd.Parameters.AddWithValue("@Remark", Remark.Text);
                cmd.Parameters.AddWithValue("@AgentID", Session["SuperAgentID"]);
                cmd.ExecuteNonQuery();

                Amount.Text = "";
                Remark.Text = "";
                BillDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                dropdownAgent_SelectedIndexChanged(sender, e);



            }
        }
    }
}