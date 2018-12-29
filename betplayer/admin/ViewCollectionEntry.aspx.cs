using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace betplayer.Admin
{
    public partial class ViewCollectionEntry : System.Web.UI.Page
    {
        private DataTable dt;
        public DataTable MatchesDataTable { get { return dt; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            
            string CN = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            using (MySqlConnection cn = new MySqlConnection(CN))
            {
                cn.Open();
                string s = "Select superagentcollectionmaster.CollectionID,superagentmaster.Name,superagentcollectionmaster.CollectionType,superagentcollectionmaster.Date,superagentcollectionmaster.Amount,superagentcollectionmaster.PaynmentType,superagentcollectionmaster.Remark from superagentcollectionmaster inner join superagentmaster on superagentcollectionmaster.superagentID = superagentmaster.superagentID  where AdminID = '" + Session["AdminID"] + "'";
                MySqlCommand cmd = new MySqlCommand(s, cn);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adp.Fill(dt);
                
            }
        }
    }
}