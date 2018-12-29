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
    public partial class ViewMatchReport : System.Web.UI.Page
    {
        private DataTable dt;
        private DataTable dt1;
        private DataTable ClientTable = new DataTable();
        private DataTable ClientTable1 = new DataTable();
        public DataTable MatchesDataTable { get { return dt; } }
        public DataTable MatchesDataTable1 { get { return dt1; } }
        public DataTable ClientDataTable1 { get { return ClientTable; } }


        private DataTable runtable;


        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
                Bets();


                runtable = new DataTable();
                runtable.Columns.Add(new DataColumn("ClientID"));
                runtable.Columns.Add(new DataColumn("Name"));
                DataRow row = runtable.NewRow();

                string MatchID = Request.QueryString["MatchID"];
                string CN = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                using (MySqlConnection cn = new MySqlConnection(CN))
                {
                    cn.Open();

                    string SELECT = "Select * from Matches Where apiID = '" + MatchID + "'";
                    MySqlCommand cmd = new MySqlCommand(SELECT, cn);
                    MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);
                    string TeamA = dt.Rows[0]["TeamA"].ToString();
                    string TeamB = dt.Rows[0]["TeamB"].ToString();
                    lblTeamA.Text = TeamA;
                    lblTeamB.Text = TeamB;
                    lblTeamA1.Text = TeamA;
                    lblTeamB1.Text = TeamB;



                    string s1 = "Select * From AgentMaster where CreatedBy = '" + Session["SuperAgentCode"] + "'";
                    MySqlCommand cmd1 = new MySqlCommand(s1, cn);
                    MySqlDataAdapter adp1 = new MySqlDataAdapter(cmd1);
                    DataTable dt1 = new DataTable();
                    adp1.Fill(dt1);

                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        string Agentcode = dt1.Rows[i]["code"].ToString();
                        string s2 = "Select * From ClientMaster where CreatedBy = '" + Agentcode + "' && Mode = 'Agent'";
                        MySqlCommand cmd2 = new MySqlCommand(s2, cn);
                        MySqlDataAdapter adp2 = new MySqlDataAdapter(cmd2);
                        DataTable dt2 = new DataTable();
                        adp2.Fill(dt2);

                        for (int j = 0; j < dt2.Rows.Count; j++)
                        {
                            int ClientID = Convert.ToInt32(dt2.Rows[j]["ClientID"]);
                            string Name = "C"+ClientID + dt2.Rows[j]["Name"].ToString();

                            row["ClientID"] = ClientID;
                            row["Name"] = Name;
                            runtable.Rows.Add(row.ItemArray);
                        }
                    }
                    Dropdownclient.DataSource = runtable;
                    Dropdownclient.DataTextField = "Name";
                    Dropdownclient.DataValueField = "ClientID";
                    Dropdownclient.DataBind();


                }
            }
        }

        protected void Dropdownclient_SelectedIndexChanged(object sender, EventArgs e)
        {


            int MatchID = Convert.ToInt32(Request.QueryString["MatchID"]);
            string CN = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            using (MySqlConnection cn = new MySqlConnection(CN))
            {
                cn.Open();

                if (Dropdownclient.SelectedItem.Text == "All Client")
                {
                    Bets();

                }
                else
                {
                    ClientTable.Columns.Add(new DataColumn("runnerID"));
                    ClientTable.Columns.Add(new DataColumn("Amount"));
                    ClientTable.Columns.Add(new DataColumn("Rate"));
                    ClientTable.Columns.Add(new DataColumn("Mode"));
                    ClientTable.Columns.Add(new DataColumn("DateTime"));
                    ClientTable.Columns.Add(new DataColumn("Team"));
                    ClientTable.Columns.Add(new DataColumn("ClientID"));
                    ClientTable.Columns.Add(new DataColumn("Name"));
                    ClientTable.Columns.Add(new DataColumn("Createdby"));
                    ClientTable.Columns.Add(new DataColumn("Position1"));
                    ClientTable.Columns.Add(new DataColumn("Position2"));
                    DataRow Clientrow = ClientTable.NewRow();

                    string SELECT = "Select * from Matches Where apiID = '" + MatchID + "'";
                    MySqlCommand cmd = new MySqlCommand(SELECT, cn);
                    MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                    dt = new DataTable();
                    adp.Fill(dt);
                    string TeamA1 = dt.Rows[0]["TeamA"].ToString();
                    string TeamB1 = dt.Rows[0]["TeamB"].ToString();
                    lblTeamA.Text = TeamA1;
                    lblTeamB.Text = TeamB1;



                    string s = "select runner.runnerID,runner.Amount,runner.rate,runner.Mode,runner.DateTime,runner.Team,runner.clientID,clientmaster.Name,clientmaster.createdby,runner.Position1,runner.Position2 from Runner inner join clientmaster on runner.ClientID = clientmaster.ClientID where  runner.MatchID = '" + MatchID + "' && runner.ClientID = '" + Dropdownclient.SelectedValue + "'  order by DateTime DESC";
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

                    foreach (DataRow row in dt1.Rows)
                    {

                        string selectteam = "select TeamA,TeamB From Matches where apiID = '" + MatchID + "'";
                        MySqlCommand selectteamcmd = new MySqlCommand(selectteam, cn);
                        MySqlDataAdapter selectteamadp = new MySqlDataAdapter(selectteamcmd);
                        DataTable selectteamdt = new DataTable();
                        selectteamadp.Fill(selectteamdt);
                        string TeamA = selectteamdt.Rows[0]["TeamA"].ToString();
                        string TeamB = selectteamdt.Rows[0]["TeamB"].ToString();
                        lblTeamA.Text = TeamA;
                        lblTeamB.Text = TeamB;
                        string selectteam1 = "select Team From runner where MatchID = '" + MatchID + "' && ClientID = '" + Dropdownclient.SelectedValue + "' order by DateTime DESC";
                        MySqlCommand selectteamcmd1 = new MySqlCommand(selectteam1, cn);
                        MySqlDataAdapter selectteamadp1 = new MySqlDataAdapter(selectteamcmd1);
                        DataTable selectteamdt1 = new DataTable();
                        selectteamadp1.Fill(selectteamdt1);
                        if (selectteamdt1.Rows.Count > 0)
                        {
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

                        }
                        j++;
                    }

                    int k = 0;
                    foreach (DataRow row in dt1.Rows)
                    {
                        string selectteam = "select TeamA,TeamB From Matches where apiID = '" + MatchID + "'";
                        MySqlCommand selectteamcmd = new MySqlCommand(selectteam, cn);
                        MySqlDataAdapter selectteamadp = new MySqlDataAdapter(selectteamcmd);
                        DataTable selectteamdt = new DataTable();
                        selectteamadp.Fill(selectteamdt);
                        string TeamA = selectteamdt.Rows[0]["TeamA"].ToString();
                        string TeamB = selectteamdt.Rows[0]["TeamB"].ToString();

                        string selectteam1 = "select Team From runner where MatchID = '" + MatchID + "'&& ClientID = '" + Dropdownclient.SelectedValue + "' order by DateTime DESC";
                        MySqlCommand selectteamcmd1 = new MySqlCommand(selectteam1, cn);
                        MySqlDataAdapter selectteamadp1 = new MySqlDataAdapter(selectteamcmd1);
                        DataTable selectteamdt1 = new DataTable();
                        selectteamadp1.Fill(selectteamdt1);
                        if (selectteamdt1.Rows.Count > 0)
                        {
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
                        }
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

                    decimal totalPosition1 = 0, totalPosition2 = 0;
                    for (int a = 0; a < ClientTable.Rows.Count; a++)
                    {
                        decimal Position1 = Convert.ToDecimal(ClientTable.Rows[a]["Position1"]);
                        decimal Position2 = Convert.ToDecimal(ClientTable.Rows[a]["Position2"]);
                        totalPosition1 = totalPosition1 + Position1;
                        totalPosition2 = totalPosition2 + Position2;
                    }
                    lblTotalPosition1.Text = totalPosition1.ToString();
                    lblTotalPosition2.Text = totalPosition2.ToString();

                    ClientCalculation();
                }
            }

        }



        public void Bets()
        {
            ClientTable.Columns.Add(new DataColumn("runnerID"));
            ClientTable.Columns.Add(new DataColumn("Amount"));
            ClientTable.Columns.Add(new DataColumn("Rate"));
            ClientTable.Columns.Add(new DataColumn("Mode"));
            ClientTable.Columns.Add(new DataColumn("DateTime"));
            ClientTable.Columns.Add(new DataColumn("Team"));
            ClientTable.Columns.Add(new DataColumn("ClientID"));
            ClientTable.Columns.Add(new DataColumn("Name"));
            ClientTable.Columns.Add(new DataColumn("CreatedBy"));
            ClientTable.Columns.Add(new DataColumn("Position1"));
            ClientTable.Columns.Add(new DataColumn("Position2"));
            DataRow Clientrow = ClientTable.NewRow();



            string MatchID = Request.QueryString["MatchID"];
            string CN = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            using (MySqlConnection cn = new MySqlConnection(CN))
            {
                cn.Open();

                string SELECT = "Select * from Matches Where apiID = '" + MatchID + "'";
                MySqlCommand cmd = new MySqlCommand(SELECT, cn);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adp.Fill(dt);
                string TeamA1 = dt.Rows[0]["TeamA"].ToString();
                string TeamB1 = dt.Rows[0]["TeamB"].ToString();
                lblTeamA.Text = TeamA1;
                lblTeamB.Text = TeamB1;



                string share = "Select myshare From SuperAgentMaster where SuperagentID = '" + Session["SuperAgentID"] + "'";
                MySqlCommand sharecmd = new MySqlCommand(share, cn);
                MySqlDataAdapter shareadp = new MySqlDataAdapter(sharecmd);
                DataTable sharedt = new DataTable();
                shareadp.Fill(sharedt);

                decimal myshare = Convert.ToDecimal(sharedt.Rows[0]["myshare"]);
                decimal myshare1 = myshare / 100;


                string s11 = "Select code From AgentMaster where CreatedBy = '" + Session["SuperAgentcode"] + "'";
                MySqlCommand cmd11 = new MySqlCommand(s11, cn);
                MySqlDataAdapter adp11 = new MySqlDataAdapter(cmd11);
                DataTable dt11 = new DataTable();
                adp11.Fill(dt11);

                for (int i = 0; i < dt11.Rows.Count; i++)
                {
                    DataRow row1 = ClientTable.NewRow();
                    string Agentcode = (dt11.Rows[i]["code"]).ToString();

                    string s = "select runner.runnerID,runner.Amount,runner.rate,runner.Mode,runner.DateTime,runner.Team,runner.clientID,clientmaster.Name,clientmaster.createdby,runner.Position1,runner.Position2 from Runner inner join clientmaster on runner.ClientID = clientmaster.ClientID where clientmaster.mode = 'Agent' && clientmaster.CreatedBy = '" + Agentcode + "' && runner.MatchID = '" + MatchID + "' order by DateTime DESC";
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

                    foreach (DataRow row in dt1.Rows)
                    {

                        string selectteam = "select TeamA,TeamB From Matches where apiID = '" + MatchID + "'";
                        MySqlCommand selectteamcmd = new MySqlCommand(selectteam, cn);
                        MySqlDataAdapter selectteamadp = new MySqlDataAdapter(selectteamcmd);
                        DataTable selectteamdt = new DataTable();
                        selectteamadp.Fill(selectteamdt);
                        string TeamA = selectteamdt.Rows[0]["TeamA"].ToString();
                        string TeamB = selectteamdt.Rows[0]["TeamB"].ToString();
                        lblTeamA.Text = TeamA;
                        lblTeamB.Text = TeamB;
                        int clientID = Convert.ToInt32(dt1.Rows[j]["ClientID"]);
                        string selectteam1 = "select Team From runner where MatchID = '" + MatchID + "' order by DateTime DESC";
                        MySqlCommand selectteamcmd1 = new MySqlCommand(selectteam1, cn);
                        MySqlDataAdapter selectteamadp1 = new MySqlDataAdapter(selectteamcmd1);
                        DataTable selectteamdt1 = new DataTable();
                        selectteamadp1.Fill(selectteamdt1);
                        string team = "";
                        if (selectteamdt1.Rows.Count > 0)
                        {
                            team = dt1.Rows[j]["Team"].ToString();
                        }

                        Decimal Position = 0;
                        Decimal Amount = Convert.ToDecimal(dt1.Rows[j]["Amount"]);
                        Decimal Rate = Convert.ToDecimal(dt1.Rows[j]["Rate"]);
                        clientID = Convert.ToInt32(dt1.Rows[j]["ClientID"]);
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


                        j++;
                    }



                    int k = 0;
                    Decimal TeamBposition = 0;

                    foreach (DataRow row in dt1.Rows)
                    {
                        string selectteam = "select TeamA,TeamB From Matches where apiID = '" + MatchID + "'";
                        MySqlCommand selectteamcmd = new MySqlCommand(selectteam, cn);
                        MySqlDataAdapter selectteamadp = new MySqlDataAdapter(selectteamcmd);
                        DataTable selectteamdt = new DataTable();
                        selectteamadp.Fill(selectteamdt);
                        string TeamA = selectteamdt.Rows[0]["TeamA"].ToString();
                        string TeamB = selectteamdt.Rows[0]["TeamB"].ToString();
                        int clientID = Convert.ToInt32(dt1.Rows[k]["ClientID"]);
                        string selectteam1 = "select Team From runner where MatchID = '" + MatchID + "' order by DateTime DESC";
                        MySqlCommand selectteamcmd1 = new MySqlCommand(selectteam1, cn);
                        MySqlDataAdapter selectteamadp1 = new MySqlDataAdapter(selectteamcmd1);
                        DataTable selectteamdt1 = new DataTable();
                        selectteamadp1.Fill(selectteamdt1);
                        string team = "";
                        if (selectteamdt1.Rows.Count > 0)
                        {
                            team = dt1.Rows[k]["Team"].ToString();
                        }


                        string Mode1 = "";
                        Decimal Position = 0;
                        Decimal Amount = Convert.ToDecimal(dt1.Rows[k]["Amount"]);
                        Decimal Rate = Convert.ToDecimal(dt1.Rows[k]["Rate"]);
                        string Mode = dt1.Rows[k]["Mode"].ToString();
                        clientID = Convert.ToInt32(dt1.Rows[k]["ClientID"]);

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
                decimal totalCalculation1 = 0, totalCalculation2 = 0;
                for (int d = 0; d < ClientTable.Rows.Count; d++)
                {
                    decimal total1 = Convert.ToDecimal(ClientTable.Rows[d]["Position1"]);
                    decimal total2 = Convert.ToDecimal(ClientTable.Rows[d]["Position2"]);
                    totalCalculation1 = totalCalculation1 + total1;
                    totalCalculation2 = totalCalculation2 + total2;

                }
                lblTotalPosition1.Text = totalCalculation1.ToString();
                lblTotalPosition2.Text = totalCalculation2.ToString();

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
            ClientTable1.Columns.Add(new DataColumn("CreatedBy"));
            ClientTable1.Columns.Add(new DataColumn("Position1"));
            ClientTable1.Columns.Add(new DataColumn("Position2"));
            DataRow Clientrow1 = ClientTable1.NewRow();

            string MatchID = Request.QueryString["MatchID"];
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
                    decimal Agentshare = Convert.ToDecimal(dt11.Rows[i]["Agentshare"]);


                    string s = "select runner.runnerID,runner.Amount,runner.rate,runner.Mode,runner.DateTime,runner.Team,runner.clientID,clientmaster.Name,clientmaster.createdby,runner.Position1,runner.Position2 from Runner inner join clientmaster on runner.ClientID = clientmaster.ClientID where clientmaster.mode = 'Agent' && clientmaster.CreatedBy = '" + Agentcode + "' && runner.MatchID = '" + MatchID + "' order by DateTime DESC";
                    MySqlCommand cmd1 = new MySqlCommand(s, cn);
                    MySqlDataAdapter adp1 = new MySqlDataAdapter(cmd1);
                    DataTable dt5 = new DataTable();
                    adp1.Fill(dt5);
                    int j = 0;

                    Decimal TeamAposition1 = 0;
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

                        string selectteam = "select TeamA,TeamB From Matches where apiID = '" + MatchID + "'";
                        MySqlCommand selectteamcmd = new MySqlCommand(selectteam, cn);
                        MySqlDataAdapter selectteamadp = new MySqlDataAdapter(selectteamcmd);
                        DataTable selectteamdt = new DataTable();
                        selectteamadp.Fill(selectteamdt);
                        string TeamA = selectteamdt.Rows[0]["TeamA"].ToString();
                        string TeamB = selectteamdt.Rows[0]["TeamB"].ToString();
                        lblTeamA.Text = TeamA;
                        lblTeamB.Text = TeamB;

                        string selectteam1 = "select Team From runner where MatchID = '" + MatchID + "' order by DateTime DESC";
                        MySqlCommand selectteamcmd1 = new MySqlCommand(selectteam1, cn);
                        MySqlDataAdapter selectteamadp1 = new MySqlDataAdapter(selectteamcmd1);
                        DataTable selectteamdt1 = new DataTable();
                        selectteamadp1.Fill(selectteamdt1);
                        if (selectteamdt1.Rows.Count > 0)
                        {
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


                            string selectAgentshare = "select Agent_share,createdby From ClientMaster where ClientID = '" + clientID + "'";
                            MySqlCommand selectAgentsharecmd = new MySqlCommand(selectAgentshare, cn);
                            MySqlDataAdapter selectAgentshareadp = new MySqlDataAdapter(selectAgentsharecmd);
                            DataTable selectAgentsharedt = new DataTable();
                            selectAgentshareadp.Fill(selectAgentsharedt);
                            TeamAposition1 = 0;

                            Decimal Clientshare = Convert.ToDecimal(selectAgentsharedt.Rows[0]["Agent_Share"]);
                            string Agent = selectAgentsharedt.Rows[0]["Createdby"].ToString();


                            decimal finalClientshare = Clientshare / 100;
                            Decimal FinalShare = myshare1 - finalClientshare;
                            TeamAposition1 = Convert.ToDecimal(Position * FinalShare);

                            row["Position1"] = TeamAposition1;

                            TeamAposition = TeamAposition + TeamAposition1;


                        }
                        j++;
                    }

                    int k = 0;

                    Decimal TeamBposition2 = 0;

                    foreach (DataRow row in dt5.Rows)
                    {
                        string selectteam = "select TeamA,TeamB From Matches where apiID = '" + MatchID + "'";
                        MySqlCommand selectteamcmd = new MySqlCommand(selectteam, cn);
                        MySqlDataAdapter selectteamadp = new MySqlDataAdapter(selectteamcmd);
                        DataTable selectteamdt = new DataTable();
                        selectteamadp.Fill(selectteamdt);
                        string TeamA = selectteamdt.Rows[0]["TeamA"].ToString();
                        string TeamB = selectteamdt.Rows[0]["TeamB"].ToString();

                        string selectteam1 = "select Team From runner where MatchID = '" + MatchID + "' order by DateTime DESC";
                        MySqlCommand selectteamcmd1 = new MySqlCommand(selectteam1, cn);
                        MySqlDataAdapter selectteamadp1 = new MySqlDataAdapter(selectteamcmd1);
                        DataTable selectteamdt1 = new DataTable();
                        selectteamadp1.Fill(selectteamdt1);
                        if (selectteamdt1.Rows.Count > 0)
                        {
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


                            string selectAgentshare = "select Agent_share,createdby From ClientMaster where ClientID = '" + clientID + "'";
                            MySqlCommand selectAgentsharecmd = new MySqlCommand(selectAgentshare, cn);
                            MySqlDataAdapter selectAgentshareadp = new MySqlDataAdapter(selectAgentsharecmd);
                            DataTable selectAgentsharedt = new DataTable();
                            selectAgentshareadp.Fill(selectAgentsharedt);
                            TeamBposition2 = 0;

                            Decimal Clientshare = Convert.ToDecimal(selectAgentsharedt.Rows[0]["Agent_Share"]);
                            string Agent = selectAgentsharedt.Rows[0]["Createdby"].ToString();


                            decimal finalClientshare = Clientshare / 100;
                            Decimal FinalShare = myshare1 - finalClientshare;
                            TeamBposition2 = Convert.ToDecimal(Position * FinalShare);

                            row["Position2"] = TeamBposition2;

                            TeamBposition = TeamBposition + TeamBposition2;


                        }
                        k++;
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
                }


                double dValue1 = double.Parse(TeamAposition.ToString());
                double dValue2 = double.Parse(TeamBposition.ToString());
                lblPositionA.Text = dValue1.ToString();
                lblPositionB.Text = dValue2.ToString();
                if (TeamAposition > 0)
                {
                    lblPositionA.ForeColor = System.Drawing.Color.Blue;

                }
                else if (TeamAposition < 0)
                {
                    lblPositionA.ForeColor = System.Drawing.Color.Red;

                }
                if (TeamBposition > 0)
                {
                    lblPositionB.ForeColor = System.Drawing.Color.Blue;

                }
                else if (TeamBposition < 0)
                {
                    lblPositionB.ForeColor = System.Drawing.Color.Red;

                }
            }
        }

        public void ClientCalculation()
        {

            ClientTable1.Columns.Add(new DataColumn("runnerID"));
            ClientTable1.Columns.Add(new DataColumn("Amount"));
            ClientTable1.Columns.Add(new DataColumn("Rate"));
            ClientTable1.Columns.Add(new DataColumn("Mode"));
            ClientTable1.Columns.Add(new DataColumn("DateTime"));
            ClientTable1.Columns.Add(new DataColumn("Team"));
            ClientTable1.Columns.Add(new DataColumn("ClientID"));
            ClientTable1.Columns.Add(new DataColumn("Name"));
            ClientTable1.Columns.Add(new DataColumn("CreatedBy"));
            ClientTable1.Columns.Add(new DataColumn("Position1"));
            ClientTable1.Columns.Add(new DataColumn("Position2"));
            DataRow Clientrow1 = ClientTable1.NewRow();

            string MatchID = Request.QueryString["MatchID"];
            string CN = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            using (MySqlConnection cn = new MySqlConnection(CN))
            {
                cn.Open();
                Decimal TeamAposition = 0;
                Decimal TeamBposition = 0;


                string share = "Select myshare From SuperAgentMaster where SuperagentID = '" + Session["SuperAgentID"] + "'";
                MySqlCommand sharecmd = new MySqlCommand(share, cn);
                MySqlDataAdapter shareadp = new MySqlDataAdapter(sharecmd);
                DataTable sharedt = new DataTable();
                shareadp.Fill(sharedt);

                decimal myshare = Convert.ToDecimal(sharedt.Rows[0]["myshare"]);
                decimal myshare1 = myshare / 100;

                string s = "select runner.runnerID,runner.Amount,runner.rate,runner.Mode,runner.DateTime,runner.Team,runner.clientID,clientmaster.Name,clientmaster.createdby,runner.Position1,runner.Position2 from Runner inner join clientmaster on runner.ClientID = clientmaster.ClientID where Clientmaster.clientID = '" + Dropdownclient.SelectedValue + "' and runner.MatchID = '" + MatchID + "' order by DateTime DESC";
                MySqlCommand cmd1 = new MySqlCommand(s, cn);
                MySqlDataAdapter adp1 = new MySqlDataAdapter(cmd1);
                DataTable dt5 = new DataTable();
                adp1.Fill(dt5);
                int j = 0;

                Decimal TeamAposition1 = 0;
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

                    string selectteam = "select TeamA,TeamB From Matches where apiID = '" + MatchID + "'";
                    MySqlCommand selectteamcmd = new MySqlCommand(selectteam, cn);
                    MySqlDataAdapter selectteamadp = new MySqlDataAdapter(selectteamcmd);
                    DataTable selectteamdt = new DataTable();
                    selectteamadp.Fill(selectteamdt);
                    string TeamA = selectteamdt.Rows[0]["TeamA"].ToString();
                    string TeamB = selectteamdt.Rows[0]["TeamB"].ToString();
                    lblTeamA.Text = TeamA;
                    lblTeamB.Text = TeamB;

                    string selectteam1 = "select Team From runner where MatchID = '" + MatchID + "' and ClientID = '" + Dropdownclient.SelectedValue + "' order by DateTime DESC";
                    MySqlCommand selectteamcmd1 = new MySqlCommand(selectteam1, cn);
                    MySqlDataAdapter selectteamadp1 = new MySqlDataAdapter(selectteamcmd1);
                    DataTable selectteamdt1 = new DataTable();
                    selectteamadp1.Fill(selectteamdt1);
                    if (selectteamdt1.Rows.Count > 0)
                    {
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


                        string selectAgentshare = "select Agent_share,createdby From ClientMaster where ClientID = '" + clientID + "'";
                        MySqlCommand selectAgentsharecmd = new MySqlCommand(selectAgentshare, cn);
                        MySqlDataAdapter selectAgentshareadp = new MySqlDataAdapter(selectAgentsharecmd);
                        DataTable selectAgentsharedt = new DataTable();
                        selectAgentshareadp.Fill(selectAgentsharedt);
                        TeamAposition1 = 0;

                        Decimal Clientshare = Convert.ToDecimal(selectAgentsharedt.Rows[0]["Agent_Share"]);

                        decimal finalClientshare = Clientshare / 100;
                        Decimal FinalShare = myshare1 - finalClientshare;
                        TeamAposition1 = Convert.ToDecimal(Position * FinalShare);

                        row["Position1"] = TeamAposition1;

                        TeamAposition = TeamAposition + TeamAposition1;

                    }
                    j++;
                }

                int k = 0;

                Decimal TeamBposition2 = 0;

                foreach (DataRow row in dt5.Rows)
                {
                    string selectteam = "select TeamA,TeamB From Matches where apiID = '" + MatchID + "'";
                    MySqlCommand selectteamcmd = new MySqlCommand(selectteam, cn);
                    MySqlDataAdapter selectteamadp = new MySqlDataAdapter(selectteamcmd);
                    DataTable selectteamdt = new DataTable();
                    selectteamadp.Fill(selectteamdt);
                    string TeamA = selectteamdt.Rows[0]["TeamA"].ToString();
                    string TeamB = selectteamdt.Rows[0]["TeamB"].ToString();

                    string selectteam1 = "select Team From runner where MatchID = '" + MatchID + "' And ClientID = '" + Dropdownclient.SelectedValue + "' order by DateTime DESC";
                    MySqlCommand selectteamcmd1 = new MySqlCommand(selectteam1, cn);
                    MySqlDataAdapter selectteamadp1 = new MySqlDataAdapter(selectteamcmd1);
                    DataTable selectteamdt1 = new DataTable();
                    selectteamadp1.Fill(selectteamdt1);
                    if (selectteamdt1.Rows.Count > 0)
                    {
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


                        string selectAgentshare = "select Agent_share,createdby From ClientMaster where ClientID = '" + clientID + "'";
                        MySqlCommand selectAgentsharecmd = new MySqlCommand(selectAgentshare, cn);
                        MySqlDataAdapter selectAgentshareadp = new MySqlDataAdapter(selectAgentsharecmd);
                        DataTable selectAgentsharedt = new DataTable();
                        selectAgentshareadp.Fill(selectAgentsharedt);
                        TeamBposition2 = 0;

                        Decimal Clientshare = Convert.ToDecimal(selectAgentsharedt.Rows[0]["Agent_Share"]);



                        decimal finalClientshare = Clientshare / 100;
                        Decimal FinalShare = myshare1 - finalClientshare;
                        TeamBposition2 = Convert.ToDecimal(Position * FinalShare);

                        row["Position2"] = TeamBposition2;

                        TeamBposition = TeamBposition + TeamBposition2;


                    }
                    k++;
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
                lblPositionA.Text = dValue1.ToString();
                lblPositionB.Text = dValue2.ToString();
                if (TeamAposition > 0)
                {
                    lblPositionA.ForeColor = System.Drawing.Color.Blue;

                }
                else if (TeamAposition < 0)
                {
                    lblPositionA.ForeColor = System.Drawing.Color.Red;

                }
                if (TeamBposition > 0)
                {
                    lblPositionB.ForeColor = System.Drawing.Color.Blue;

                }
                else if (TeamBposition < 0)
                {
                    lblPositionB.ForeColor = System.Drawing.Color.Red;

                }
            }

        }
    }
}
