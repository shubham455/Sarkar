using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace betplayer.Client
{
    public partial class BetDetails_Declare : System.Web.UI.Page
    {
        private DataTable dt;
        private DataTable dt1;
        private DataTable Sessiondt;
        public DataTable MatchesDataTable { get { return dt; } }
        public DataTable MatchesDataTable1 { get { return dt1; } }
        public DataTable SessionDataTable { get { return Sessiondt; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            int ClientID = Convert.ToInt32(Session["ClientID"]);
            int MatchID = Convert.ToInt32(Request.QueryString["id"]);

            string CN = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            using (MySqlConnection cn = new MySqlConnection(CN))
            {
                cn.Open();
                string s = "Select * From Runner where MatchID = @MatchID && ClientID =@ClientID order by DateTime DESC ";
                MySqlCommand cmd = new MySqlCommand(s, cn);
                cmd.Parameters.AddWithValue("@ClientID", ClientID);
                cmd.Parameters.AddWithValue("@MatchID", MatchID);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adp.Fill(dt);


                string s1 = "Select TeamA,TeamB,WinnerTeam From Matches where apiID = @MatchID";
                MySqlCommand cmd1 = new MySqlCommand(s1, cn);
                cmd1.Parameters.AddWithValue("@MatchID", MatchID);
                MySqlDataAdapter adp1 = new MySqlDataAdapter(cmd1);
                dt1 = new DataTable();
                adp1.Fill(dt1);
                string TeamA = dt1.Rows[0]["TeamA"].ToString();
                string TeamB = dt1.Rows[0]["TeamB"].ToString();
                lblteamA.Text = TeamA;
                lblteamB.Text = TeamB;
                string winnerteam = "";

                if (dt.Rows.Count > 0)
                {
                    lblteamA.Text = dt1.Rows[0]["TeamA"].ToString();
                    lblteamB.Text = dt1.Rows[0]["TeamB"].ToString();
                    winnerteam = dt1.Rows[0]["WinnerTeam"].ToString();
                }
                string winnerteamposition = "";
                if (winnerteam == TeamA)
                {
                    winnerteamposition = "position1";
                }
                else if (winnerteam == TeamB)
                {
                    winnerteamposition = "position2";
                }

                string checkbets = "Select * From Positions Where MatchID = '" + MatchID + "' && ClientID = '" + ClientID + "'";
                MySqlCommand checkbetscmd = new MySqlCommand(checkbets, cn);
                MySqlDataAdapter checkbetsadp = new MySqlDataAdapter(checkbetscmd);
                DataTable checkbetsdt = new DataTable();
                checkbetsadp.Fill(checkbetsdt);
                int FinalBetAmount = 0;

                if (winnerteamposition == "position1")
                {
                    FinalBetAmount = Convert.ToInt32(checkbetsdt.Rows[0]["Position1"]);
                }
                else if (winnerteamposition == "position2")
                {
                    FinalBetAmount = Convert.ToInt32(checkbetsdt.Rows[0]["Position2"]);
                }

                if (FinalBetAmount > 0)
                {
                    lblMatch.Text = "You Won(" + FinalBetAmount + ")Coins";
                }
                if (FinalBetAmount < 0)
                {
                    lblMatch.Text = "You Lose(" + FinalBetAmount + ")Coins";
                }

                int TotalAmount1 = 0;
                string Amount = "Select TotalAmount from SessionCalculation where MatchID = @MatchID && ClientID =@ClientID";
                MySqlCommand Amountcmd = new MySqlCommand(Amount, cn);
                Amountcmd.Parameters.AddWithValue("@ClientID", ClientID);
                Amountcmd.Parameters.AddWithValue("@MatchID", MatchID);
                MySqlDataAdapter Amountadp = new MySqlDataAdapter(Amountcmd);
                DataTable Amountdt = new DataTable();
                Amountadp.Fill(Amountdt);
                if (Amountdt.Rows.Count > 0)
                {
                    int TotalAmount = 0;


                    for (int i = 0; i < Amountdt.Rows.Count; i++)
                    {
                        TotalAmount = Convert.ToInt32(Amountdt.Rows[i]["TotalAmount"]);
                        TotalAmount1 = TotalAmount1 + TotalAmount;
                    }
                    TotalAmount = 0;

                    if (TotalAmount1 > 0)
                    {
                        lblSession.Text = "You Won(" + TotalAmount1 + ")Coins";
                    }
                    if (TotalAmount1 < 0)
                    {
                        lblSession.Text = "You Lose(" + TotalAmount1 + ")Coins";
                    }

                }
                int FinalPlusminus = (FinalBetAmount + TotalAmount1) - 100;
                lblfinalAmount.Text = FinalPlusminus.ToString();

                string Session = "Select * From Session where MatchID = '" + MatchID + "' && ClientID = '" + ClientID + "' order by DateTime DESC ";
                MySqlCommand Sessioncmd = new MySqlCommand(Session, cn);

                MySqlDataAdapter Sessionadp = new MySqlDataAdapter(Sessioncmd);
                Sessiondt = new DataTable();
                Sessionadp.Fill(Sessiondt);
            }
        }
    }
}