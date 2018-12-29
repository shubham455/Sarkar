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
    public partial class MatchplusminusSelect : System.Web.UI.Page
    {
        private DataTable dt1;
        private DataTable dt2;
        public DataTable MatchesDataTable { get { return dt1; } }
        public DataTable MatchesDataTable2 { get { return dt2; } }
        protected void Page_Load(object sender, EventArgs e)
        {

            dt2 = new DataTable();
            dt2.Columns.Add(new DataColumn("Name"));
            DataRow row = dt2.NewRow();


            
            string MatchID = Request.QueryString["MatchID"];
            string CN = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            using (MySqlConnection cn = new MySqlConnection(CN))
            {
                cn.Open();

                string Name = "Select Name From SuperAgentMAster where CreatedBy = '" + Session["AdminCode"] + "'";
                MySqlCommand Namecmd = new MySqlCommand(Name, cn);
                MySqlDataAdapter Nameadp = new MySqlDataAdapter(Namecmd);
                DataTable Namedt = new DataTable();
                Nameadp.Fill(Namedt);
                lblname.Text = Namedt.Rows[0]["Name"].ToString();



                string SELECT = "Select * from Matches Where apiID = '" + MatchID + "'";
                MySqlCommand cmd = new MySqlCommand(SELECT, cn);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                string TeamA = dt.Rows[0]["TeamA"].ToString();
                string TeamB = dt.Rows[0]["TeamB"].ToString();
                lblTeamA.Text = TeamA;
                lblTeamB.Text = TeamB;


                string s1 = "select Name,Code From AgentMaster where CreatedBy ='"+Session["SuperAgentCode"]+"' ";
                MySqlCommand cmd1 = new MySqlCommand(s1, cn);
                MySqlDataAdapter adp1 = new MySqlDataAdapter(cmd1);
                dt1 = new DataTable();
                adp1.Fill(dt1);

                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    string Agentcode = dt1.Rows[i]["Code"].ToString();

                    string s2 = "select Runner.RunnerID,Runner.Amount,Runner.rate,Runner.Mode,Runner.DateTime,Runner.Team,Runner.clientID,clientmaster.Name from Runner inner join clientmaster on Runner.ClientID = clientmaster.ClientID where clientmaster.mode = 'Agent' && clientmaster.CreatedBy = '" + Agentcode + "' && Runner.MatchID = '"+MatchID+"' group by Name";
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
            string MatchID = Request.QueryString["MatchID"];

            Response.Redirect("MatchPlusminusDisplay.aspx?MatchID="+MatchID);
        }
    }
}