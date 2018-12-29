using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace betplayer.poweruser
{
    public partial class CreateMatch : System.Web.UI.Page
    {
        private DataTable dt;

        public DataTable MatchesDataTable { get { return dt; } }
        public Boolean emptyLedgerTable = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string CN = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                using (MySqlConnection cn = new MySqlConnection(CN))
                {
                    cn.Open();

                    string datet = DateTime.Now.ToString("dd-MM-yyyy");
                    BillDate.Text = datet;
                    string s = "Select * From Matches where Active  = '0' order by DateTime DESC";
                    MySqlCommand cmd = new MySqlCommand(s, cn);
                    MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                    dt = new DataTable();
                    adp.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        dt.AcceptChanges();
                        foreach (DataRow row in dt.Rows)
                        {
                            string rowDate = DateTime.Parse(row["DateTime"].ToString()).Date.ToString("dd-MM-yyyy");
                            if (datet != rowDate)
                            {
                                row.Delete();
                            }
                        }
                        dt.AcceptChanges();
                    }

                    else
                    {
                        emptyLedgerTable = true;
                    }
                }
            }
        }
        public string toTime(object DateTimefromDB)
        {
            DateTime oDate = DateTime.Parse(DateTimefromDB.ToString());
            return oDate.TimeOfDay.ToString();
        }
        public string toDateString(object date)
        {
            DateTime rowDate = DateTime.Parse(date.ToString());
            return rowDate.Date.ToString("dd-MM-yyyy");
        }

        protected void BillDate_TextChanged(object sender, EventArgs e)
        {
            string datet = DateTime.Parse(BillDate.Text.ToString()).Date.ToString("dd-MM-yyyy");
            
            string CN = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            using (MySqlConnection cn = new MySqlConnection(CN))
            {
                cn.Open();

                string s = "Select * From Matches where Active  = '0' order by DateTime DESC";
                MySqlCommand cmd = new MySqlCommand(s, cn);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adp.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    dt.AcceptChanges();
                    foreach (DataRow row in dt.Rows)
                    {
                        string rowDate = DateTime.Parse(row["DateTime"].ToString()).Date.ToString("dd-MM-yyyy");
                        if (datet != rowDate)
                        {
                            row.Delete();
                        }
                    }
                    dt.AcceptChanges();
                }

                else
                {
                    emptyLedgerTable = true;
                }
            }
        }
    }
}
