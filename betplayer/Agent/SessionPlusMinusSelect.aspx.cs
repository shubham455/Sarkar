using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;


namespace betplayer.agent
{
    public partial class SessionPlusMinus : System.Web.UI.Page
    {
        private DataTable dt1;
        private DataTable dt3;
        private DataTable Runnerclientdt;
        public DataTable MatchesDataTable { get { return dt1; } }
        public DataTable MatchesDataTable3 { get { return dt3; } }
        public DataTable RunnerclientDataTable { get { return Runnerclientdt; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            apiID.Value = (Request.QueryString["MatchID"]).ToString();
            int MatchID = Convert.ToInt32(Request.QueryString["MatchID"]);
            string CN = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            using (MySqlConnection cn = new MySqlConnection(CN))
            {
                cn.Open();
                string s = "select Name From AgentMaster where AgentID = '" + Session["AgentID"] + "'";
                MySqlCommand cmd = new MySqlCommand(s, cn);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);

               
                lblAgentNAme1.Text = dt.Rows[0]["Name"].ToString();

                string Runnerclient = "select Runner.RunnerID,Runner.Amount,Runner.rate,Runner.Mode,Runner.DateTime,Runner.Team,Runner.clientID,clientmaster.Name from Runner inner join clientmaster on Runner.ClientID = clientmaster.ClientID where clientmaster.mode = 'Agent' && clientmaster.CreatedBy = '" + Session["Agentcode"] + "' && Runner.MatchID = '" + MatchID + "' group by ClientMaster.Name";
                MySqlCommand Runnerclientcmd = new MySqlCommand(Runnerclient, cn);
                MySqlDataAdapter Runnerclientadp = new MySqlDataAdapter(Runnerclientcmd);
                Runnerclientdt = new DataTable();
                Runnerclientadp.Fill(Runnerclientdt);


                string s1 = "select Session.sessionID,Session.session,Session.Runs,Session.Amount,Session.rate,Session.Mode,Session.DateTime,Session.Team,Session.clientID,clientmaster.Name from Session inner join clientmaster on Session.ClientID = clientmaster.ClientID where clientmaster.mode = 'Agent' && clientmaster.CreatedBy = '" + Session["Agentcode"] + "' && Session.MatchID = '" + MatchID + "' group by ClientMaster.Name ";
                MySqlCommand cmd1 = new MySqlCommand(s1, cn);
                MySqlDataAdapter adp1 = new MySqlDataAdapter(cmd1);
                dt1 = new DataTable();
                adp1.Fill(dt1);


                string s2 = "select TeamA,TeamB From Matches where apiID = '" + MatchID + "'";
                MySqlCommand cmd2 = new MySqlCommand(s2, cn);
                MySqlDataAdapter adp2 = new MySqlDataAdapter(cmd2);
                DataTable dt2 = new DataTable();
                adp2.Fill(dt2);

                lblTeamA.Text = dt2.Rows[0]["TeamA"].ToString();
                lblTeamB.Text = dt2.Rows[0]["TeamB"].ToString();

                string s3 = "select Session  From DeclaredSession where MatchID = '" + MatchID + "'";
                MySqlCommand cmd3 = new MySqlCommand(s3, cn);
                MySqlDataAdapter adp3 = new MySqlDataAdapter(cmd3);
                dt3 = new DataTable();
                adp3.Fill(dt3);



            }
        }
    }
}