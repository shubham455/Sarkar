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
    public partial class ViewMatchReport : System.Web.UI.Page
    {
        private DataTable dt1;
        public DataTable MatchesDataTable { get { return dt1; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int MatchID = Convert.ToInt32(Request.QueryString["MatchID"]);
                string CN = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                using (MySqlConnection cn = new MySqlConnection(CN))
                {
                    cn.Open();
                    string s = "Select * From ClientMaster where CreatedBy = '" + Session["Agentcode"] + "'";
                    MySqlCommand cmd = new MySqlCommand(s, cn);
                    MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);
                    Dropdownclient.DataSource = dt;
                    Dropdownclient.DataTextField = "Name";
                    Dropdownclient.DataValueField = "ClientID";
                    Dropdownclient.DataBind();

                    Bets();

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
                    string s = "select Runner.RunnerID,Runner.Amount,Runner.rate,Runner.Mode,Runner.DateTime,Runner.Team,Runner.Position1,Runner.Position2,Runner.clientID,clientmaster.Name,clientmaster.CreatedBy from Runner inner join clientmaster on Runner.ClientID = clientmaster.ClientID where clientmaster.mode = 'Agent' && Runner.MatchID = '" + MatchID + "' && runner.ClientID = '" + Dropdownclient.SelectedValue + "'  order by runner.DateTime DESC";
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
                        l++;
                    }
                    int j = 0;
                    Decimal TeamAposition = 0;
                    Decimal TeamAposition1 = 0;
                    Decimal Teampos = 0;
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
                        string selectteam1 = "select Team From runner where MatchID = '" + MatchID + "' && ClientID = '"+ Dropdownclient.SelectedValue+"' order by DateTime DESC";
                        MySqlCommand selectteamcmd1 = new MySqlCommand(selectteam1, cn);
                        MySqlDataAdapter selectteamadp1 = new MySqlDataAdapter(selectteamcmd1);
                        DataTable selectteamdt1 = new DataTable();
                        selectteamadp1.Fill(selectteamdt1);
                        string team = "";
                        if (selectteamdt1.Rows.Count > j)
                        {
                            team = dt1.Rows[j]["Team"].ToString();

                        }

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

                    lblPositionA.Text = Teampos.ToString();
                    if (Teampos > 0)
                    {
                        lblPositionA.ForeColor = System.Drawing.Color.Blue;
                    }
                    else if (Teampos < 0)
                    {
                        lblPositionA.ForeColor = System.Drawing.Color.Red;
                    }


                    int k = 0;
                    Decimal TeamBposition = 0;
                    Decimal TeamBposition2 = 0;
                    Decimal TeamposB = 0;
                    foreach (DataRow row in dt1.Rows)
                    {
                        string selectteam = "select TeamA,TeamB From Matches where apiID = '" + MatchID + "'";
                        MySqlCommand selectteamcmd = new MySqlCommand(selectteam, cn);
                        MySqlDataAdapter selectteamadp = new MySqlDataAdapter(selectteamcmd);
                        DataTable selectteamdt = new DataTable();
                        selectteamadp.Fill(selectteamdt);
                        string TeamA = selectteamdt.Rows[0]["TeamA"].ToString();
                        string TeamB = selectteamdt.Rows[0]["TeamB"].ToString();

                        string selectteam1 = "select Team From runner where MatchID = '" + MatchID + "' && ClientID = '"+ Dropdownclient.SelectedValue+"' order by DateTime DESC";
                        MySqlCommand selectteamcmd1 = new MySqlCommand(selectteam1, cn);
                        MySqlDataAdapter selectteamadp1 = new MySqlDataAdapter(selectteamcmd1);
                        DataTable selectteamdt1 = new DataTable();
                        selectteamadp1.Fill(selectteamdt1);
                        string team = "";
                        if (selectteamdt1.Rows.Count > k)
                        {
                            team = dt1.Rows[k]["Team"].ToString();

                        }
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
                    lblPositionB.Text = TeamposB.ToString();
                    if (TeamposB > 0)
                    {
                        lblPositionB.ForeColor = System.Drawing.Color.Blue;
                    }
                    else if (TeamposB < 0)
                    {
                        lblPositionB.ForeColor = System.Drawing.Color.Red;
                    }

                    decimal totalPosition1 = 0, totalPosition2 = 0;
                    for (int a = 0; a < dt1.Rows.Count; a++)
                    {
                        decimal Position1 = Convert.ToDecimal(dt1.Rows[a]["Position1"]);
                        decimal Position2 = Convert.ToDecimal(dt1.Rows[a]["Position2"]);
                        totalPosition1 = totalPosition1 + Position1;
                        totalPosition2 = totalPosition2 + Position2;
                    }
                    lblTotalPosition1.Text = totalPosition1.ToString();
                    lblTotalPosition2.Text = totalPosition2.ToString();
                }
            }
        }

        public void colorforpositionA(decimal Amount)
        {
            if (Amount > 0)
            {
                lblPositionA.ForeColor = System.Drawing.Color.Blue;
            }
            else if (Amount < 0)
            {
                lblPositionA.ForeColor = System.Drawing.Color.Red;
            }
        }
        public void colorforpositionB(decimal Amount)
        {
            if (Amount > 0)
            {
                lblPositionB.ForeColor = System.Drawing.Color.Blue;
            }
            else if (Amount < 0)
            {
                lblPositionB.ForeColor = System.Drawing.Color.Red;
            }
        }
        public void Bets()
        {

            int MatchID = Convert.ToInt32(Request.QueryString["MatchID"]);
            string CN = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            using (MySqlConnection cn = new MySqlConnection(CN))
            {
                cn.Open();
                string s = "select runner.runnerID,runner.Amount,runner.rate,runner.Mode,runner.DateTime,runner.Team,runner.clientID,clientmaster.Name,runner.Position1,runner.Position2 from Runner inner join clientmaster on runner.ClientID = clientmaster.ClientID where clientmaster.mode = 'Agent' && clientmaster.CreatedBy = '" + Session["Agentcode"] + "' && runner.MatchID = '" + MatchID + "' order by DateTime DESC";
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
                    l++;
                }
                int j = 0;
                Decimal TeamAposition = 0;
                Decimal TeamAposition1 = 0;
                Decimal Teampos = 0;
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
                    string selectteam1 = "select Team From runner where MatchID = '" + MatchID + "' order by DateTime DESC";
                    MySqlCommand selectteamcmd1 = new MySqlCommand(selectteam1, cn);
                    MySqlDataAdapter selectteamadp1 = new MySqlDataAdapter(selectteamcmd1);
                    DataTable selectteamdt1 = new DataTable();
                    selectteamadp1.Fill(selectteamdt1);
                    string team = "";
                    if (selectteamdt1.Rows.Count > j)
                    {
                        team = dt1.Rows[j]["Team"].ToString();

                    }

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

                lblPositionA.Text = Teampos.ToString();
                if (Teampos > 0)
                {
                    lblPositionA.ForeColor = System.Drawing.Color.Blue;
                }
                else if (Teampos < 0)
                {
                    lblPositionA.ForeColor = System.Drawing.Color.Red;
                }


                int k = 0;
                Decimal TeamBposition = 0;
                Decimal TeamBposition2 = 0;
                Decimal TeamposB = 0;
                foreach (DataRow row in dt1.Rows)
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
                    string team = "";
                    if (selectteamdt1.Rows.Count > k)
                    {
                        team = dt1.Rows[k]["Team"].ToString();

                    }
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
                lblPositionB.Text = TeamposB.ToString();
                if (TeamposB > 0)
                {
                    lblPositionB.ForeColor = System.Drawing.Color.Blue;
                }
                else if (TeamposB < 0)
                {
                    lblPositionB.ForeColor = System.Drawing.Color.Red;
                }
                decimal totalPosition1 = 0, totalPosition2 = 0;
                for (int a = 0; a < dt1.Rows.Count; a++)
                {
                    decimal Position1 = Convert.ToDecimal(dt1.Rows[a]["Position1"]);
                    decimal Position2 = Convert.ToDecimal(dt1.Rows[a]["Position2"]);
                    totalPosition1 = totalPosition1 + Position1;
                    totalPosition2 = totalPosition2 + Position2;
                }
                lblTotalPosition1.Text = totalPosition1.ToString();
                lblTotalPosition2.Text = totalPosition2.ToString();

            }
        }
    }
}