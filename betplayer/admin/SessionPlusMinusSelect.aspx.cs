using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;


namespace betplayer.admin
{
    public partial class SessionPlusMinus : System.Web.UI.Page
    {
        private DataTable dt;
        private DataTable dt1;
        private DataTable dt2;

        public DataTable SessionDatatable { get { return dt; } }
        public DataTable AgentDatatable { get { return dt1; } }
        public DataTable ClientDatatable { get { return dt2; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            dt2 = new DataTable();
            dt2.Columns.Add(new DataColumn("Name"));
            DataRow row = dt2.NewRow();



            int MatchID = Convert.ToInt32(Request.QueryString["MatchID"]);
            string CN = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            using (MySqlConnection cn = new MySqlConnection(CN))
            {
                cn.Open();
                string s = "select Session  From DeclaredSession where MatchID = '" + MatchID + "'";
                MySqlCommand cmd = new MySqlCommand(s, cn);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adp.Fill(dt);


                string Name = "Select Name From SuperAgentMAster where CreatedBy = '" + Session["AdminCode"] + "'";
                MySqlCommand Namecmd = new MySqlCommand(Name, cn);
                MySqlDataAdapter Nameadp = new MySqlDataAdapter(Namecmd);
                DataTable Namedt = new DataTable();
                Nameadp.Fill(Namedt);
                lblname.Text = Namedt.Rows[0]["Name"].ToString();

                string TeamName = "Select * from Matches Where apiID = '" + MatchID + "'";
                MySqlCommand TeamNamecmd = new MySqlCommand(TeamName, cn);
                MySqlDataAdapter TeamNameadp = new MySqlDataAdapter(TeamNamecmd);
                DataTable TeamNamedt = new DataTable();
                TeamNameadp.Fill(TeamNamedt);
                string TeamA = TeamNamedt.Rows[0]["TeamA"].ToString();
                string TeamB = TeamNamedt.Rows[0]["TeamB"].ToString();
                lblTeamA.Text = TeamA;
                lblTeamB.Text = TeamB;


                string s1 = "select Name,Code From AgentMaster where CreatedBy ='" + Session["SuperAgentCode"] + "' ";
                MySqlCommand cmd1 = new MySqlCommand(s1, cn);
                MySqlDataAdapter adp1 = new MySqlDataAdapter(cmd1);
                dt1 = new DataTable();
                adp1.Fill(dt1);

                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    string Agentcode = dt1.Rows[i]["Code"].ToString();

                    string s2 = "select Session.sessionID,Session.session,Session.Runs,Session.Amount,Session.rate,Session.Mode,Session.DateTime,Session.Team,Session.clientID,clientmaster.Name from Session inner join clientmaster on Session.ClientID = clientmaster.ClientID where clientmaster.mode = 'Agent' && clientmaster.CreatedBy = '" + Agentcode + "' && Session.MatchID = '" + MatchID + "' group by ClientMaster.Name ";
                    MySqlCommand cmd2 = new MySqlCommand(s2, cn);
                    MySqlDataAdapter adp2 = new MySqlDataAdapter(cmd2);
                    DataTable dt3 = new DataTable();
                    adp2.Fill(dt3);

                    for (int j = 0; j < dt3.Rows.Count; j++)
                    {
                        string ClientName = dt3.Rows[j]["Name"].ToString();
                        row["Name"] = ClientName;
                        dt2.Rows.Add(row.ItemArray);
                    }

                }
            }
        }

        protected void btnshow_Click(object sender, EventArgs e)
        {
            int MatchID = Convert.ToInt32(Request.QueryString["MatchID"]);

            Response.Redirect("SessionplusminusDisplay.aspx?MatchID=" + MatchID);
        }
    }
}