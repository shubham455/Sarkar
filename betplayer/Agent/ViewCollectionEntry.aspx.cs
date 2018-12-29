using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace betplayer.Agent
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
                string s = "Select clientcollectionmaster.CollectionID,clientmaster.Name,clientcollectionmaster.CollectionType,clientcollectionmaster.Date,clientcollectionmaster.Amount,clientcollectionmaster.PaynmentType,clientcollectionmaster.Remark from clientcollectionmaster inner join clientmaster on clientcollectionmaster.clientID = clientmaster.clientID  where AgentID = '" + Session["AgentID"] + "'";
                MySqlCommand cmd = new MySqlCommand(s, cn);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adp.Fill(dt);
                
            }
        }
    }
}