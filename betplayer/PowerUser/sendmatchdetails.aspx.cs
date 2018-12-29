using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;

namespace betplayer.PowerUser
{
    public partial class sendmatchdetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnsend_Click(object sender, EventArgs e)
        {
            string ContactNowithCommaSeperate = "";
            if (CheckBox1.Checked == true)
            {
                string SuperagentContactNowithCommaSeperate = "";
                string CN = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                using (MySqlConnection cn = new MySqlConnection(CN))
                {
                    cn.Open();
                    string selectsuperagent = "Select ContactNo  From SuperagentMaster where Status = 'Active'";
                    MySqlCommand selectsuperagentcmd = new MySqlCommand(selectsuperagent, cn);
                    MySqlDataAdapter selectsuperagentadp = new MySqlDataAdapter(selectsuperagentcmd);
                    DataTable selectsuperagentdt = new DataTable();
                    selectsuperagentadp.Fill(selectsuperagentdt);

                    for (int i = 0; i < selectsuperagentdt.Rows.Count; i++)
                    {
                        string Contactno = selectsuperagentdt.Rows[i]["ContactNo"].ToString();
                        SuperagentContactNowithCommaSeperate = SuperagentContactNowithCommaSeperate + Contactno + ",";
                    }

                }
                ContactNowithCommaSeperate = ContactNowithCommaSeperate + SuperagentContactNowithCommaSeperate;
            }

            if (CheckBox2.Checked == true)
            {
                string SuperagentContactNowithCommaSeperate = "";
                string CN = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                using (MySqlConnection cn = new MySqlConnection(CN))
                {
                    cn.Open();
                    string selectsuperagent = "Select ContactNo  From agentMaster where Status = 'Active'";
                    MySqlCommand selectsuperagentcmd = new MySqlCommand(selectsuperagent, cn);
                    MySqlDataAdapter selectsuperagentadp = new MySqlDataAdapter(selectsuperagentcmd);
                    DataTable selectsuperagentdt = new DataTable();
                    selectsuperagentadp.Fill(selectsuperagentdt);

                    for (int i = 0; i < selectsuperagentdt.Rows.Count; i++)
                    {
                        string Contactno = selectsuperagentdt.Rows[i]["ContactNo"].ToString();
                        SuperagentContactNowithCommaSeperate = SuperagentContactNowithCommaSeperate + Contactno + ",";
                    }

                }
                ContactNowithCommaSeperate = ContactNowithCommaSeperate + SuperagentContactNowithCommaSeperate;
            }

            if (CheckBox3.Checked == true)
            {
                string SuperagentContactNowithCommaSeperate = "";
                string CN = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                using (MySqlConnection cn = new MySqlConnection(CN))
                {
                    cn.Open();
                    string selectsuperagent = "Select Contact_No  From ClientMaster where Status = 'Active'";
                    MySqlCommand selectsuperagentcmd = new MySqlCommand(selectsuperagent, cn);
                    MySqlDataAdapter selectsuperagentadp = new MySqlDataAdapter(selectsuperagentcmd);
                    DataTable selectsuperagentdt = new DataTable();
                    selectsuperagentadp.Fill(selectsuperagentdt);

                    for (int i = 0; i < selectsuperagentdt.Rows.Count; i++)
                    {
                        string Contactno = selectsuperagentdt.Rows[i]["Contact_No"].ToString();
                        SuperagentContactNowithCommaSeperate = SuperagentContactNowithCommaSeperate + Contactno + ",";
                    }

                }
                ContactNowithCommaSeperate = ContactNowithCommaSeperate + SuperagentContactNowithCommaSeperate;
            }
            string message = "Match Sechduled \n "+txtdescription.Text+" \n\nFrom Sarkar50.com";
            sendSMS smsSender = new sendSMS();
            smsSender.SendSMS(ContactNowithCommaSeperate, message);
            txtdescription.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Sent Message SuccessFully.....');", true);
        }


        protected void btncancel_Click(object sender, EventArgs e)
        {

        }
    }
}