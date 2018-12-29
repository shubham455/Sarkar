using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace betplayer.superagent
{
    public partial class Match_SessionPosition : System.Web.UI.Page
    {
        private DataTable dt;
        private DataTable dt1;
        private DataTable ClientTable = new DataTable();
        private DataTable ClientTable1 = new DataTable();
        public DataTable ClientTableOrdered;
        public DataTable MatchesDataTable { get { return dt; } }
        public DataTable MatchesDataTable1 { get { return dt1; } }
        public DataTable ClientDataTable1 { get { return ClientTableOrdered; } }
        public Boolean emptyLedgerTable = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            string type = Request.QueryString["Type"];
            if (type == "Test" || type == "test")
            {
                emptyLedgerTable = true;
            }

            ClientTable.Columns.Add(new DataColumn("runnerID"));
            ClientTable.Columns.Add(new DataColumn("Amount"));
            ClientTable.Columns.Add(new DataColumn("Rate"));
            ClientTable.Columns.Add(new DataColumn("Mode"));
            //ClientTable.Columns.Add(new DataColumn("DateTime"));
            DataColumn colDateTime = new DataColumn("DateTime");
            colDateTime.DataType = System.Type.GetType("System.DateTime");
            ClientTable.Columns.Add(colDateTime);
            ClientTable.Columns.Add(new DataColumn("Team"));
            ClientTable.Columns.Add(new DataColumn("ClientID"));
            ClientTable.Columns.Add(new DataColumn("Name"));
            ClientTable.Columns.Add(new DataColumn("Createdby"));
            ClientTable.Columns.Add(new DataColumn("Position1"));
            ClientTable.Columns.Add(new DataColumn("Position2"));
            DataRow Clientrow = ClientTable.NewRow();



            apiID.Value = Request.QueryString["MatchID"];
            firebasekey.Value = Request.QueryString["fk"];
            string CN = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            using (MySqlConnection cn = new MySqlConnection(CN))
            {
                cn.Open();

                string SELECT = "Select * from Matches Where apiID = '" + apiID.Value + "'";
                MySqlCommand cmd = new MySqlCommand(SELECT, cn);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adp.Fill(dt);
                string TeamA1 = dt.Rows[0]["TeamA"].ToString();
                string TeamB1 = dt.Rows[0]["TeamB"].ToString();
                lblTeamA.Text = TeamA1;
                lblTeamB.Text = TeamB1;




                int ID = Convert.ToInt32(Session["SuperAgentID"]);
                string s11 = "Select code From AgentMaster where CreatedBy = '" + Session["SuperAgentcode"] + "'";
                MySqlCommand cmd11 = new MySqlCommand(s11, cn);
                MySqlDataAdapter adp11 = new MySqlDataAdapter(cmd11);
                DataTable dt11 = new DataTable();
                adp11.Fill(dt11);

                for (int i = 0; i < dt11.Rows.Count; i++)
                {
                    DataRow row1 = ClientTable.NewRow();
                    string Agentcode = (dt11.Rows[i]["code"]).ToString();

                    string s = "select runner.runnerID,runner.Amount,runner.rate,runner.Mode,runner.DateTime,runner.Team,runner.clientID,clientmaster.Name,clientmaster.CreatedBy,runner.Position1,runner.Position2 from Runner inner join clientmaster on runner.ClientID = clientmaster.ClientID where clientmaster.mode = 'Agent' && clientmaster.CreatedBy = '" + Agentcode + "' && runner.MatchID = '" + apiID.Value + "' order by DateTime DESC";
                    MySqlCommand cmd1 = new MySqlCommand(s, cn);
                    MySqlDataAdapter adp1 = new MySqlDataAdapter(cmd1);
                    dt1 = new DataTable();
                    adp1.Fill(dt1);

                    int l = 0;
                    foreach (DataRow row in dt1.Rows)
                    {
                        string Mode1 = "";
                        string Mode = dt1.Rows[l]["Mode"].ToString();
                        if (Mode == "K")
                        {
                            Mode1 = "L";

                        }
                        else if (Mode == "L")
                        {
                            Mode1 = "K";

                        }
                        row["Mode"] = Mode1;
                        row["Mode"] = Mode1;
                        l++;
                    }
                    int j = 0;
                    Decimal TeamAposition = 0;
                    Decimal TeamAposition1 = 0;
                    Decimal Teampos = 0;
                    foreach (DataRow row in dt1.Rows)
                    {

                        string selectteam = "select TeamA,TeamB From Matches where apiID = '" + apiID.Value + "'";
                        MySqlCommand selectteamcmd = new MySqlCommand(selectteam, cn);
                        MySqlDataAdapter selectteamadp = new MySqlDataAdapter(selectteamcmd);
                        DataTable selectteamdt = new DataTable();
                        selectteamadp.Fill(selectteamdt);
                        string TeamA = selectteamdt.Rows[0]["TeamA"].ToString();
                        string TeamB = selectteamdt.Rows[0]["TeamB"].ToString();
                        lblTeamA.Text = TeamA;
                        lblTeamB.Text = TeamB;

                        string team = dt1.Rows[j]["Team"].ToString();

                        Decimal Position = 0;
                        Decimal Amount = Convert.ToDecimal(dt1.Rows[j]["Amount"]);
                        Decimal Rate = Convert.ToDecimal(dt1.Rows[j]["Rate"]);
                        int clientID = Convert.ToInt32(dt1.Rows[j]["ClientID"]);
                        string Mode1 = "";
                        string Mode = dt1.Rows[j]["Mode"].ToString();
                        if (Mode == "K")
                        {
                            Mode1 = "L";
                        }
                        else if (Mode == "L")
                        {
                            Mode1 = "K";
                        }


                        if (TeamA == team && Mode1 == "K")
                        {
                            Position = Amount * Rate;
                        }
                        else if (TeamA == team && Mode1 == "L")
                        {
                            Position = Amount * Rate * -1;
                        }
                        else if (TeamB == team && Mode1 == "K")
                        {
                            Position = Amount * -1;
                        }
                        else if (TeamB == team && Mode1 == "L")
                        {
                            Position = Amount;
                        }

                        row["Position1"] = Position;

                        TeamAposition = TeamAposition + Position;

                        string selectAgentshare = "select Agent_share From ClientMaster where ClientID = '" + clientID + "'";
                        MySqlCommand selectAgentsharecmd = new MySqlCommand(selectAgentshare, cn);
                        MySqlDataAdapter selectAgentshareadp = new MySqlDataAdapter(selectAgentsharecmd);
                        DataTable selectAgentsharedt = new DataTable();
                        selectAgentshareadp.Fill(selectAgentsharedt);
                        TeamAposition1 = 0;

                        Decimal AgentShare1 = Convert.ToDecimal(selectAgentsharedt.Rows[0]["Agent_Share"]);

                        TeamAposition1 = (Position * AgentShare1) / 100;

                        Teampos = Teampos + TeamAposition1;

                        j++;


                    }


                    int k = 0;
                    Decimal TeamBposition = 0;
                    Decimal TeamBposition2 = 0;
                    Decimal TeamposB = 0;
                    foreach (DataRow row in dt1.Rows)
                    {
                        string selectteam = "select TeamA,TeamB From Matches where apiID = '" + apiID.Value + "'";
                        MySqlCommand selectteamcmd = new MySqlCommand(selectteam, cn);
                        MySqlDataAdapter selectteamadp = new MySqlDataAdapter(selectteamcmd);
                        DataTable selectteamdt = new DataTable();
                        selectteamadp.Fill(selectteamdt);
                        string TeamA = selectteamdt.Rows[0]["TeamA"].ToString();
                        string TeamB = selectteamdt.Rows[0]["TeamB"].ToString();


                        string team = dt1.Rows[k]["Team"].ToString();

                        string Mode1 = "";
                        Decimal Position = 0;
                        Decimal Amount = Convert.ToDecimal(dt1.Rows[k]["Amount"]);
                        Decimal Rate = Convert.ToDecimal(dt1.Rows[k]["Rate"]);
                        string Mode = dt1.Rows[k]["Mode"].ToString();
                        int clientID = Convert.ToInt32(dt1.Rows[k]["ClientID"]);

                        if (Mode == "K")
                        {
                            Mode1 = "L";
                        }
                        else if (Mode == "L")
                        {
                            Mode1 = "K";
                        }


                        if (TeamA == team && Mode1 == "K")
                        {
                            Position = Amount * -1;
                        }
                        else if (TeamA == team && Mode1 == "L")
                        {
                            Position = Amount;
                        }
                        else if (TeamB == team && Mode1 == "K")
                        {
                            Position = Amount * Rate;
                        }
                        else if (TeamB == team && Mode1 == "L")
                        {
                            Position = Amount * Rate * -1;
                        }
                        row["Position2"] = Position;
                        k++;

                        TeamBposition = TeamBposition + Position;


                        string selectAgentshare1 = "select Agent_share From ClientMaster where ClientID = '" + clientID + "'";
                        MySqlCommand selectAgentsharecmd1 = new MySqlCommand(selectAgentshare1, cn);
                        MySqlDataAdapter selectAgentshareadp1 = new MySqlDataAdapter(selectAgentsharecmd1);
                        DataTable selectAgentsharedt1 = new DataTable();
                        selectAgentshareadp1.Fill(selectAgentsharedt1);


                        Decimal AgentShare = Convert.ToDecimal(selectAgentsharedt1.Rows[0]["Agent_Share"]);

                        TeamBposition2 = (Position * AgentShare) / 100;
                        TeamposB = TeamposB + TeamBposition2;



                    }


                    if (dt1.Rows.Count > 0)
                    {
                        IEnumerable<DataRow> orderedRows = dt1.AsEnumerable();
                        DataTable TempClientTable = orderedRows.CopyToDataTable();
                        foreach (DataRow row in TempClientTable.Rows)
                        {
                            ClientTable.Rows.Add(row.ItemArray);
                        }
                    }
                }
                if (ClientTable.Rows.Count > 0)
                {
                    IEnumerable<DataRow> orderedRows = from row in ClientTable.AsEnumerable()
                                                       orderby row.Field<DateTime>("DateTime") descending
                                                       select row;

                    ClientTableOrdered = orderedRows.CopyToDataTable();

                }
                else
                {
                    ClientTableOrdered = ClientTable;
                }
                decimal totalCalculation1 = 0, totalCalculation2 = 0;
                for (int d = 0; d < ClientTable.Rows.Count; d++)
                {
                    decimal total1 = Convert.ToDecimal(ClientTable.Rows[d]["Position1"]);
                    decimal total2 = Convert.ToDecimal(ClientTable.Rows[d]["Position2"]);
                    totalCalculation1 = totalCalculation1 + total1;
                    totalCalculation2 = totalCalculation2 + total2;

                }
                finalposition1.Text = totalCalculation1.ToString();
                finalposition2.Text = totalCalculation2.ToString();
                calculation();

            }

        }



        public void calculation()
        {

            ClientTable1.Columns.Add(new DataColumn("runnerID"));
            ClientTable1.Columns.Add(new DataColumn("Amount"));
            ClientTable1.Columns.Add(new DataColumn("Rate"));
            ClientTable1.Columns.Add(new DataColumn("Mode"));
            ClientTable1.Columns.Add(new DataColumn("DateTime"));
            ClientTable1.Columns.Add(new DataColumn("Team"));
            ClientTable1.Columns.Add(new DataColumn("ClientID"));
            ClientTable1.Columns.Add(new DataColumn("Name"));
            ClientTable1.Columns.Add(new DataColumn("Createdby"));
            ClientTable1.Columns.Add(new DataColumn("Position1"));
            ClientTable1.Columns.Add(new DataColumn("Position2"));
            DataRow Clientrow1 = ClientTable1.NewRow();

            apiID.Value = Request.QueryString["MatchID"];
            string CN = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            using (MySqlConnection cn = new MySqlConnection(CN))
            {
                cn.Open();
                string share = "Select myshare From SuperAgentMaster where SuperagentID = '" + Session["SuperAgentID"] + "'";
                MySqlCommand sharecmd = new MySqlCommand(share, cn);
                MySqlDataAdapter shareadp = new MySqlDataAdapter(sharecmd);
                DataTable sharedt = new DataTable();
                shareadp.Fill(sharedt);

                decimal myshare = Convert.ToDecimal(sharedt.Rows[0]["myshare"]);
                decimal myshare1 = myshare / 100;

                string s11 = "Select code,agentshare From AgentMaster where CreatedBy = '" + Session["SuperAgentcode"] + "'";
                MySqlCommand cmd11 = new MySqlCommand(s11, cn);
                MySqlDataAdapter adp11 = new MySqlDataAdapter(cmd11);
                DataTable dt11 = new DataTable();
                adp11.Fill(dt11);
                Decimal TeamAposition = 0;
                Decimal TeamBposition = 0;
                for (int i = 0; i < dt11.Rows.Count; i++)
                {

                    string Agentcode = (dt11.Rows[i]["code"]).ToString();


                    string s1 = "select runner.runnerID,runner.Amount,runner.rate,runner.Mode,runner.DateTime,runner.Team,runner.clientID,clientmaster.Name,clientmaster.CreatedBy,runner.Position1,runner.Position2 from Runner inner join clientmaster on runner.ClientID = clientmaster.ClientID where clientmaster.mode = 'Agent' && clientmaster.CreatedBy = '" + Agentcode + "' && runner.MatchID = '" + apiID.Value + "' order by DateTime DESC";
                    MySqlCommand cmd1 = new MySqlCommand(s1, cn);
                    MySqlDataAdapter adp1 = new MySqlDataAdapter(cmd1);
                    DataTable dt5 = new DataTable();
                    adp1.Fill(dt5);
                    int j = 0;
                    if (dt5.Rows.Count > 0)
                    {
                        int l = 0;
                        foreach (DataRow row in dt5.Rows)
                        {
                            string Mode1 = "";
                            string Mode = dt5.Rows[l]["Mode"].ToString();
                            if (Mode == "K")
                            {
                                Mode1 = "L";

                            }
                            else if (Mode == "L")
                            {
                                Mode1 = "K";

                            }
                            row["Mode"] = Mode1;
                            row["Mode"] = Mode1;
                            l++;
                        }
                        foreach (DataRow row in dt5.Rows)
                        {

                            string selectteam = "select TeamA,TeamB From Matches where apiID = '" + apiID.Value + "'";
                            MySqlCommand selectteamcmd = new MySqlCommand(selectteam, cn);
                            MySqlDataAdapter selectteamadp = new MySqlDataAdapter(selectteamcmd);
                            DataTable selectteamdt = new DataTable();
                            selectteamadp.Fill(selectteamdt);
                            string TeamA = selectteamdt.Rows[0]["TeamA"].ToString();
                            string TeamB = selectteamdt.Rows[0]["TeamB"].ToString();
                            lblTeamA.Text = TeamA;
                            lblTeamB.Text = TeamB;

                            string team = dt5.Rows[j]["Team"].ToString();

                            Decimal Position = 0;
                            Decimal Amount = Convert.ToDecimal(dt5.Rows[j]["Amount"]);
                            Decimal Rate = Convert.ToDecimal(dt5.Rows[j]["Rate"]);
                            int clientID = Convert.ToInt32(dt5.Rows[j]["ClientID"]);
                            string Mode1 = "";
                            string Mode = dt5.Rows[j]["Mode"].ToString();
                            if (Mode == "K")
                            {
                                Mode1 = "L";
                            }
                            else if (Mode == "L")
                            {
                                Mode1 = "K";
                            }


                            if (TeamA == team && Mode1 == "K")
                            {
                                Position = Amount * Rate;
                            }
                            else if (TeamA == team && Mode1 == "L")
                            {
                                Position = Amount * Rate * -1;
                            }
                            else if (TeamB == team && Mode1 == "K")
                            {
                                Position = Amount * -1;
                            }
                            else if (TeamB == team && Mode1 == "L")
                            {
                                Position = Amount;
                            }


                            string Client1 = "Select code,Agent_Share,ClientID From ClientMaster where ClientID  = '" + clientID + "'";
                            MySqlCommand Clientsharecmd1 = new MySqlCommand(Client1, cn);
                            MySqlDataAdapter Clientshareadp1 = new MySqlDataAdapter(Clientsharecmd1);
                            DataTable Clientsharedt1 = new DataTable();
                            Clientshareadp1.Fill(Clientsharedt1);

                            decimal clientshare = Convert.ToDecimal(Clientsharedt1.Rows[0]["Agent_Share"]);


                            decimal finalClientshare = clientshare / 100;
                            Decimal FinalShare = myshare1 - finalClientshare;
                            decimal Team1Amt1 = Convert.ToDecimal(Position * FinalShare);

                            row["Position1"] = Position;

                            TeamAposition = TeamAposition + Team1Amt1;

                            j++;
                        }

                        int k = 0;
                        foreach (DataRow row in dt5.Rows)
                        {
                            string selectteam = "select TeamA,TeamB From Matches where apiID = '" + apiID.Value + "'";
                            MySqlCommand selectteamcmd = new MySqlCommand(selectteam, cn);
                            MySqlDataAdapter selectteamadp = new MySqlDataAdapter(selectteamcmd);
                            DataTable selectteamdt = new DataTable();
                            selectteamadp.Fill(selectteamdt);
                            string TeamA = selectteamdt.Rows[0]["TeamA"].ToString();
                            string TeamB = selectteamdt.Rows[0]["TeamB"].ToString();

                            string team = dt5.Rows[k]["Team"].ToString();

                            string Mode1 = "";
                            Decimal Position = 0;
                            Decimal Amount = Convert.ToDecimal(dt5.Rows[k]["Amount"]);
                            Decimal Rate = Convert.ToDecimal(dt5.Rows[k]["Rate"]);
                            string Mode = dt5.Rows[k]["Mode"].ToString();
                            int clientID = Convert.ToInt32(dt5.Rows[k]["ClientID"]);

                            if (Mode == "K")
                            {
                                Mode1 = "L";
                            }
                            else if (Mode == "L")
                            {
                                Mode1 = "K";
                            }


                            if (TeamA == team && Mode1 == "K")
                            {
                                Position = Amount * -1;
                            }
                            else if (TeamA == team && Mode1 == "L")
                            {
                                Position = Amount;
                            }
                            else if (TeamB == team && Mode1 == "K")
                            {
                                Position = Amount * Rate;
                            }
                            else if (TeamB == team && Mode1 == "L")
                            {
                                Position = Amount * Rate * -1;
                            }

                            string Client1 = "Select code,Agent_Share,ClientID From ClientMaster where ClientID  = '" + clientID + "'";
                            MySqlCommand Clientsharecmd1 = new MySqlCommand(Client1, cn);
                            MySqlDataAdapter Clientshareadp1 = new MySqlDataAdapter(Clientsharecmd1);
                            DataTable Clientsharedt1 = new DataTable();
                            Clientshareadp1.Fill(Clientsharedt1);

                            decimal clientshare = Convert.ToDecimal(Clientsharedt1.Rows[0]["Agent_Share"]);


                            decimal finalClientshare = clientshare / 100;
                            Decimal FinalShare = myshare1 - finalClientshare;

                            decimal Team2Amt1 = Convert.ToDecimal(Position * FinalShare);

                            row["Position2"] = Position;

                            TeamBposition = TeamBposition + Team2Amt1;

                            k++;
                        }
                    }

                    if (dt5.Rows.Count > 0)
                    {
                        IEnumerable<DataRow> orderedRows = dt5.AsEnumerable();
                        DataTable TempClientTable = orderedRows.CopyToDataTable();
                        foreach (DataRow row11 in TempClientTable.Rows)
                        {
                            ClientTable1.Rows.Add(row11.ItemArray);
                        }
                    }

                    double dValue1 = double.Parse(TeamAposition.ToString());
                    double dValue2 = double.Parse(TeamBposition.ToString());
                    Team1Amt.Text = dValue1.ToString();
                    Team2Amt.Text = dValue2.ToString();
                    if (TeamAposition > 0)
                    {
                        Team1Amt.ForeColor = System.Drawing.Color.Blue;

                    }
                    else if (TeamAposition < 0)
                    {
                        Team1Amt.ForeColor = System.Drawing.Color.Red;

                    }
                }
                if (TeamBposition > 0)
                {
                    Team2Amt.ForeColor = System.Drawing.Color.Blue;

                }
                else if (TeamBposition < 0)
                {
                    Team2Amt.ForeColor = System.Drawing.Color.Red;

                }
            }
        }
    }
}



