using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace betplayer.admin
{
    public partial class SportsDetails : System.Web.UI.Page
    {
        private DataTable dt;
        public DataTable MatchesDataTable { get { return dt; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            string CN = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            using (MySqlConnection cn = new MySqlConnection(CN))

            {
                cn.Open();
                string s = "Select * From Matches where Active = '1' order by DateTime DESC";
                MySqlCommand cmd = new MySqlCommand(s, cn);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adp.Fill(dt);
                int i = 0;
                foreach (DataRow row in dt.Rows)
                {
                    string declare="";
                    int Declear =Convert.ToInt16( dt.Rows[i]["declear"]);
                    if(Declear == 0)
                    {
                        declare = "False";
                    }
                    if (Declear == 1)
                    {
                        declare = "True";
                    }
                    row["declear"] = Convert.ToBoolean(declare);
                    i++;
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
    }
}