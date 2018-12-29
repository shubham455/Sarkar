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
                string s = "Select * From Matches where Active = '1' order by DateTime DESC ";
                MySqlCommand cmd = new MySqlCommand(s, cn);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adp.Fill(dt);
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

        public string date()

        {
            string CN = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            using (MySqlConnection cn = new MySqlConnection(CN))

            {
                cn.Open();
                string s = "Select * From Matches where Active = '1'";
                MySqlCommand cmd = new MySqlCommand(s, cn);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adp.Fill(dt);
                string DateTimeFromDB = dt.Rows[0]["DateTime"].ToString();
                DateTime oDate = DateTime.Parse(DateTimeFromDB);

                string datetime = "Time: " + oDate.TimeOfDay.ToString();
                string date = oDate.Date.ToString("dd-MM-yyyy");
                string time = oDate.TimeOfDay.ToString();
                return date;
            }
        }
    }
}