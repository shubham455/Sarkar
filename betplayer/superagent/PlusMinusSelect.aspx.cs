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
    public partial class PlusMinusSelect : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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
            }
        }

        protected void btnshow_Click(object sender, EventArgs e)
        {
            apiId.Value = Request.QueryString["MatchID"];
            string select = Dropdownselect.SelectedItem.Text;

            if(select == "Match")
            {
                Response.Redirect("MatchPlusminusSelect.aspx?MatchID="+apiId.Value);
            }
            else if (select == "Session")
            {
                Response.Redirect("SessionPlusminusSelect.aspx?MatchID=" + apiId.Value);
            }
            else if (select == "Both")
            {
                Response.Redirect("MatchSessionPlusminusSelect.aspx?MatchID=" + apiId.Value);
            }
            else if (select == "For Single Session")
            {
                Response.Redirect("SingleSessionPlusminusSelect.aspx?MatchID=" + apiId.Value);
            }
        }
    }
}