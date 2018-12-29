using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;


namespace betplayer.Client
{

    public partial class AllGamesList : System.Web.UI.Page
    {

        private DataTable dt;
        private DataTable matchesinfodt;

        public DataTable MatchesDataTable { get { return matchesinfodt; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            matchesinfodt = new DataTable();
            matchesinfodt.Columns.Add(new DataColumn("teamname"));
            matchesinfodt.Columns.Add(new DataColumn("apiID"));
            matchesinfodt.Columns.Add(new DataColumn("firebasekey"));
            matchesinfodt.Columns.Add(new DataColumn("type"));
            matchesinfodt.Columns.Add(new DataColumn("status"));
            matchesinfodt.Columns.Add(new DataColumn("date"));
            matchesinfodt.Columns.Add(new DataColumn("time"));
            matchesinfodt.Columns.Add(new DataColumn("MatchBetCount"));
            matchesinfodt.Columns.Add(new DataColumn("SessionBetcount"));
            matchesinfodt.Columns.Add(new DataColumn("Winnerteam"));
            DataRow row = matchesinfodt.NewRow();


            string CN = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            using (MySqlConnection cn = new MySqlConnection(CN))
            {
                cn.Open();
                string s = "Select * From Matches where Active = '1' order by DateTime DESC ";
                MySqlCommand cmd = new MySqlCommand(s, cn);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int a = 0; a < dt.Rows.Count; a++)
                    {
                        string TeamA = dt.Rows[a]["TeamA"].ToString();
                        string TeamB = dt.Rows[a]["TeamB"].ToString();
                        string name = TeamA + "VS" + TeamB;
                        row["teamname"] = name;
                        string timeFromDB = dt.Rows[a]["DateTime"].ToString();
                        //DateTime oDate = DateTime.ParseExact(timeFromDB, "yyyy-MM-ddTHH:mm tt", System.Globalization.CultureInfo.InvariantCulture);
                        DateTime oDate = DateTime.Parse(timeFromDB);
                        string datetime = "Date: " + oDate.Date.ToString("dd/MM/yyyy").Substring(0, 10) + " Time: " + oDate.ToString("hh:mm:ss tt");
                        row["Date"] = datetime;


                        int MatchID = Convert.ToInt32(dt.Rows[a]["apiID"]);
                        string fk = dt.Rows[a]["firebasekey"].ToString();
                        string type = dt.Rows[a]["type"].ToString();
                        string status = dt.Rows[a]["status"].ToString();
                        string winnerteam = dt.Rows[a]["Winnerteam"].ToString();
                        row["apiID"] = MatchID;
                        row["firebasekey"] = fk;
                        row["Type"] = type;
                        row["status"] = status;
                        row["Winnerteam"] = winnerteam;
                        string ClientID = Session["ClientID"].ToString();

                        string MatchBet = "select count(clientID) From runner where MatchID = '" + MatchID + "' && ClientID = '" + ClientID + "' ";
                        MySqlCommand MatchBetcmd = new MySqlCommand(MatchBet, cn);
                        string MatchBetcount = MatchBetcmd.ExecuteScalar().ToString();

                        row["MatchBetCount"] = MatchBetcount;

                        string SessionBet = "select count(clientID) From session where MatchID = '" + MatchID + "' && ClientID = '" + ClientID + "' ";
                        MySqlCommand SessionBetcmd = new MySqlCommand(SessionBet, cn);
                        string SessionBetcount = SessionBetcmd.ExecuteScalar().ToString();

                        row["SessionBetcount"] = SessionBetcount;
                        matchesinfodt.Rows.Add(row.ItemArray);
                    }
                }
            }
        }
    }
}