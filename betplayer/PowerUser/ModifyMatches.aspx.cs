using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;

namespace betplayer.poweruser
{
    public partial class ModifyMatches : System.Web.UI.Page
    {

        private DataTable dt;
        public DataTable MatchesDataTable { get { return dt; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                string CN = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                using (MySqlConnection cn = new MySqlConnection(CN))
                {
                    cn.Open();
                    string s = "Select * From Matches where Active = '1'  order by DateTime DESC";
                    MySqlCommand cmd = new MySqlCommand(s, cn);
                    MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                    dt = new DataTable();
                    adp.Fill(dt);
                }
            }
        }
        public string toTime(object DateTimefromDB)
        {
            DateTime oDate = DateTime.Parse(DateTimefromDB.ToString());
            return oDate.ToString("hh:mm:ss tt");
        }
        public string toDateString(object date)
        {
            DateTime rowDate = DateTime.Parse(date.ToString());
            return rowDate.Date.ToString("dd-MM-yyyy");
        }

        public int sechdule(int matchid)
        {
            
            return matchid;
        }

        protected void Unnamed_ServerClick(object sender, EventArgs e)
        {
            System.Web.UI.HtmlControls.HtmlAnchor lnk = sender as System.Web.UI.HtmlControls.HtmlAnchor;
            String Value1 = lnk.Attributes["matchid"].ToString();

        }
    }
}
