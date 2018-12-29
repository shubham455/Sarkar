using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace betplayer.Agent
{
    public partial class Match_SessionPosition : System.Web.UI.Page
    {
        private DataTable dt;
        private DataTable dt1;
        public DataTable MatchesDataTable { get { return dt; } }
        public DataTable MatchesDataTable1 { get { return dt1; } }
        public Boolean emptyLedgerTable = false;

        protected void Page_Load(object sender, EventArgs e)
        {

            firebasekey.Value = (Request.QueryString["fk"]);
            apiID.Value = (Request.QueryString["MatchID"]);
            string type = Request.QueryString["Type"];
            if (type == "Test" || type == "test")
            {
                emptyLedgerTable = true;
            }
            string CN = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            using (MySqlConnection cn = new MySqlConnection(CN))
            {
                cn.Open();

                string SELECT = "Select * from Matches Where apiID= '" + apiID.Value + "'";
                MySqlCommand cmd = new MySqlCommand(SELECT, cn);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adp.Fill(dt);
                string TeamA1 = dt.Rows[0]["TeamA"].ToString();
                string TeamB1 = dt.Rows[0]["TeamB"].ToString();





                string s = "select runner.runnerID,runner.Amount,runner.rate,runner.Mode,runner.DateTime,runner.Team,runner.clientID,clientmaster.Name,runner.Position1,runner.Position2 from Runner inner join clientmaster on runner.ClientID = clientmaster.ClientID where clientmaster.mode = 'Agent' && clientmaster.CreatedBy = '" + Session["Agentcode"] + "' && runner.MatchID = '" + apiID.Value + "' order by DateTime DESC";
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

                    string selectteam = "select TeamA,TeamB From Matches where apiID = '" + apiID.Value + "'";
                    MySqlCommand selectteamcmd = new MySqlCommand(selectteam, cn);
                    MySqlDataAdapter selectteamadp = new MySqlDataAdapter(selectteamcmd);
                    DataTable selectteamdt = new DataTable();
                    selectteamadp.Fill(selectteamdt);
                    string TeamA = selectteamdt.Rows[0]["TeamA"].ToString();
                    string TeamB = selectteamdt.Rows[0]["TeamB"].ToString();
                    lblTeamA.Text = TeamA;
                    lblTeamB.Text = TeamB;
                    string selectteam1 = "select Team From runner where MatchID = '" + apiID.Value + "' order by DateTime DESC";
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
                double dValue1 = double.Parse(Teampos.ToString());
                Team1Amt.Text = dValue1.ToString();
                if (Teampos > 0)
                {
                    Team1Amt.ForeColor = System.Drawing.Color.Blue;
                }
                else if (Teampos < 0)
                {
                    Team1Amt.ForeColor = System.Drawing.Color.Red;
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

                    string selectteam1 = "select Team From runner where MatchID = '" + apiID.Value + "' order by DateTime DESC";
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
                double dValue2 = double.Parse(TeamposB.ToString());
                Team2Amt.Text = dValue2.ToString();
                if (TeamposB > 0)
                {
                    Team2Amt.ForeColor = System.Drawing.Color.Blue;
                }
                else if (TeamposB < 0)
                {
                    Team2Amt.ForeColor = System.Drawing.Color.Red;
                }
            }
        }


    }
}