using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace betplayer.superagent
{
    public partial class ViewSessionReport : System.Web.UI.Page
    {
        private DataTable dt;
        private DataTable runTable;
        private DataTable ClientTable = new DataTable();
        private DataTable Clientname;
        public DataTable MatchesDataTable { get { return ClientTable; } }
        public DataTable runTable1 { get { return runTable; } }



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                Clientname = new DataTable();
                Clientname.Columns.Add(new DataColumn("ClientID"));
                Clientname.Columns.Add(new DataColumn("Name"));

                DataRow Clientrow = Clientname.NewRow();

                int MatchID = Convert.ToInt32(Request.QueryString["MatchID"]);
                string CN = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                using (MySqlConnection cn = new MySqlConnection(CN))
                {
                    cn.Open();

                    string Agent = "Select Code,Name From AgentMaster where Createdby = '" + Session["SuperAgentCode"] + "'";
                    MySqlCommand Agentcmd = new MySqlCommand(Agent, cn);
                    MySqlDataAdapter Agentadp = new MySqlDataAdapter(Agentcmd);
                    DataTable Agentdt = new DataTable();
                    Agentadp.Fill(Agentdt);
                    for (int a = 0; a < Agentdt.Rows.Count; a++)
                    {
                        string Agentcode = Agentdt.Rows[a]["code"].ToString();
                        string AgentName = Agentdt.Rows[a]["Name"].ToString();


                        string s = "Select * From ClientMaster where CreatedBy = '" + Agentcode + "'";
                        MySqlCommand cmd = new MySqlCommand(s, cn);
                        MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                        DataTable Clientdt = new DataTable();
                        adp.Fill(Clientdt);


                        for (int j = 0; j < Clientdt.Rows.Count; j++)
                        {
                            int ClientID = Convert.ToInt32(Clientdt.Rows[j]["ClientID"]);
                            string Name = "C"+ ClientID+Clientdt.Rows[j]["Name"].ToString();

                            Clientrow["ClientID"] = ClientID;
                            Clientrow["Name"] = Name;
                            Clientname.Rows.Add(Clientrow.ItemArray);
                        }
                        DropDownClient.DataSource = Clientname;
                        DropDownClient.DataTextField = "Name";
                        DropDownClient.DataValueField = "ClientID";
                        DropDownClient.DataBind();

                        string s1 = "Select SessionID,Session From Session where MatchID = '" + MatchID + "' group by Session";
                        MySqlCommand cmd1 = new MySqlCommand(s1, cn);
                        MySqlDataAdapter adp1 = new MySqlDataAdapter(cmd1);
                        DataTable dt1 = new DataTable();
                        adp1.Fill(dt1);
                        DropDownSession.DataSource = dt1;
                        DropDownSession.DataBind();
                        DropDownSession.DataTextField = "Session";
                        DropDownSession.DataValueField = "Session";
                        DropDownSession.DataBind();

                        string s2 = "select Session.sessionID,Session.session,Session.Runs,Session.Amount,Session.rate,Session.Mode,Session.DateTime,Session.Team,Session.clientID,clientmaster.Name,Clientmaster.createdBy from Session inner join clientmaster on Session.ClientID = clientmaster.ClientID where clientmaster.mode = 'Agent' && clientmaster.CreatedBy = '" + Agentcode + "' && Session.MatchID = '0'";
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
        }


        protected void btnview_Click(object sender, EventArgs e)
        {

            ClientTable.Columns.Add(new DataColumn("SessionID"));
            ClientTable.Columns.Add(new DataColumn("Session"));
            ClientTable.Columns.Add(new DataColumn("Runs"));
            ClientTable.Columns.Add(new DataColumn("Amount"));
            ClientTable.Columns.Add(new DataColumn("rate"));
            ClientTable.Columns.Add(new DataColumn("Mode"));
            ClientTable.Columns.Add(new DataColumn("dateTime"));
            ClientTable.Columns.Add(new DataColumn("Team"));
            ClientTable.Columns.Add(new DataColumn("clientID"));
            ClientTable.Columns.Add(new DataColumn("Name"));
            ClientTable.Columns.Add(new DataColumn("CreatedBy"));
            DataRow Clientrow = ClientTable.NewRow();


            runTable = new DataTable();
            runTable.Columns.Add(new DataColumn("RUNS"));
            runTable.Columns.Add(new DataColumn("AMOUNT"));
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



                string s11 = "Select AgentID,code From AgentMaster where CreatedBy = '" + Session["SuperAgentcode"] + "'";
                MySqlCommand cmd11 = new MySqlCommand(s11, cn);
                MySqlDataAdapter adp11 = new MySqlDataAdapter(cmd11);
                DataTable dt11 = new DataTable();
                adp11.Fill(dt11);

                for (int o = 0; o < dt11.Rows.Count; o++)
                {
                    int AgentID = Convert.ToInt16(dt11.Rows[o]["AgentID"]);
                    string Agentcode = (dt11.Rows[o]["code"]).ToString();
                    string s = "";
                    if (DropDownClient.SelectedItem.Text == "All Client")
                    {
                         s= "select Session.sessionID,Session.session,Session.Runs,Session.Amount,Session.rate,Session.Mode,Session.DateTime,Session.Team,Session.clientID,clientmaster.Name,clientmaster.createdby from Session inner join clientmaster on Session.ClientID = clientmaster.ClientID where clientmaster.mode = 'Agent' && clientmaster.CreatedBy = '" + Agentcode + "' && Session.MatchID = '" + MatchID + "' && Session.Session = '" + DropDownSession.SelectedItem.Text + "' order by Session.DateTime DESC";

                    }
                    else
                    {
                        s = "select Session.sessionID,Session.session,Session.Runs,Session.Amount,Session.rate,Session.Mode,Session.DateTime,Session.Team,Session.clientID,clientmaster.Name,clientmaster.createdby from Session inner join clientmaster on Session.ClientID = clientmaster.ClientID where Session.ClientID = '"+DropDownClient.SelectedItem.Value+"' && Session.MatchID = '" + MatchID + "' && Session.Session = '" + DropDownSession.SelectedItem.Text + "' order by Session.DateTime DESC";

                    }
                    MySqlCommand cmd1 = new MySqlCommand(s, cn);
                    MySqlDataAdapter adp1 = new MySqlDataAdapter(cmd1);
                    dt = new DataTable();

                    adp1.Fill(dt);


                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        string selectAgentshare = "select CreatedBy,Agent_share From ClientMaster where Createdby = '" + Agentcode + "'";
                        MySqlCommand selectAgentsharecmd = new MySqlCommand(selectAgentshare, cn);
                        MySqlDataAdapter selectAgentshareadp = new MySqlDataAdapter(selectAgentsharecmd);
                        DataTable selectAgentsharedt = new DataTable();
                        selectAgentshareadp.Fill(selectAgentsharedt);
                        string CreatedBy = "";
                        Decimal AgentShare1 = 0, AgentShare2 = 0;
                        if (selectAgentsharedt.Rows.Count > 0)
                        {
                            Decimal ClientAgentShare = Convert.ToDecimal(selectAgentsharedt.Rows[0]["Agent_Share"]);
                            AgentShare1 = ClientAgentShare / 100;

                            CreatedBy = selectAgentsharedt.Rows[0]["CreatedBy"].ToString();
                        }
                        else
                        {
                            AgentShare1 = 0;
                        }

                        string selectAgentshare1 = "select Agentshare From AgentMaster where Code = '" + CreatedBy + "'";
                        MySqlCommand selectAgentshare1cmd = new MySqlCommand(selectAgentshare1, cn);
                        MySqlDataAdapter selectAgentshare1adp = new MySqlDataAdapter(selectAgentshare1cmd);
                        DataTable selectAgentshare1dt = new DataTable();
                        selectAgentshare1adp.Fill(selectAgentshare1dt);
                        if (selectAgentshare1dt.Rows.Count > 0)
                        {
                            Decimal Agentshare = Convert.ToInt16(selectAgentshare1dt.Rows[0]["Agentshare"]);
                            AgentShare2 = Agentshare / 100;
                        }
                        else
                        {
                            AgentShare2 = 0;
                        }

                        if (j == 0)
                        {

                            int runs = Convert.ToInt16(dt.Rows[j]["Runs"]);
                            int Amount = Convert.ToInt32(dt.Rows[j]["Amount"]);
                            Decimal Rate = Convert.ToDecimal(dt.Rows[j]["Rate"]);
                            string Mode = dt.Rows[j]["Mode"].ToString();
                            string ClientID = dt.Rows[j]["ClientID"].ToString();





                            for (int i = runs + 5; i >= runs - 5; i--)
                            {

                                DataRow row = runTable.NewRow();
                                row["RUNS"] = i.ToString();
                                row["Amount"] = CalculateAmount(Mode,
                                    i, 0,
                                    Rate,
                                    runs, Amount,
                                    AgentShare1, AgentShare2).ToString();
                                runTable.Rows.Add(row.ItemArray);
                            }
                            if (DropDownClient.SelectedItem.Text == "All Client")
                            {
                                if (dt.Rows.Count > 0)
                                {
                                    IEnumerable<DataRow> orderedRows = dt.AsEnumerable();
                                    DataTable TempClientTable = orderedRows.CopyToDataTable();
                                    foreach (DataRow row in TempClientTable.Rows)
                                    {
                                        ClientTable.Rows.Add(row.ItemArray);
                                    }
                                }
                            }
                            else
                            {
                                ClientTable = dt; 
                            }

                        }
                        else
                        {

                            int runs = Convert.ToInt32(dt.Rows[j]["Runs"]);
                            int Amount = Convert.ToInt32(dt.Rows[j]["Amount"]);
                            Decimal Rate = Convert.ToDecimal(dt.Rows[j]["Rate"]);
                            string Mode = dt.Rows[j]["Mode"].ToString();
                            string ClientID = dt.Rows[j]["ClientID"].ToString();



                            int highVal = Convert.ToInt32(runTable.Rows[0]["Runs"]);
                            int lowVal = Convert.ToInt32(runTable.Rows[runTable.Rows.Count - 1]["Runs"]);
                            if ((runs + 5) > highVal)
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
                                        AgentShare1, AgentShare2).ToString();
                                }
                                else
                                {
                                    row["AMOUNT"] = CalculateAmount(Mode,
                                        Convert.ToInt16(runTable.Rows[i]["Runs"]),
                                        Convert.ToDecimal(runTable.Rows[i]["Amount"]),
                                        Rate,
                                        runs,
                                        Amount,
                                        AgentShare1, AgentShare2).ToString();
                                }


                            }
                        }
                    }
                }
            }
        }
        public Decimal CalculateAmount(string Mode, int Initruns, Decimal InitAmount, Decimal Rate, int runs, int Amount, Decimal AgentShare1, Decimal AgentShare2)
        {
            Decimal Difference = 0;

            if (Initruns < runs)
            {

                if (Mode == "Y")
                {
                    Difference = Amount * AgentShare1 * AgentShare2 + InitAmount;
                }
                else if (Mode == "N")
                {
                    Difference = Amount * -1 * AgentShare1 * AgentShare2 + InitAmount;
                }


            }
            if (Initruns >= runs)
            {
                if (Mode == "Y")
                {
                    Difference = Amount * Rate * -1 * AgentShare1 * AgentShare2 + InitAmount;
                }
                else if (Mode == "N")
                {
                    Difference = Amount * Rate * AgentShare1 * AgentShare2 + InitAmount;
                }


            }
            return Difference;
        }

    }
}

