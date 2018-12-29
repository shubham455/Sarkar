using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;
using System.Text;

namespace betplayer.Client
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
                FillCapctha();
                if (Session["clientID"] != null)
                {
                    Session.Remove("ClientID");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Logout Successfully.....');", true);
                }



            }
        }
        protected void Submitbtn_Click(object sender, EventArgs e)
        {
            string captcha = (Session["captcha"].ToString());
            if (txtusername.Text == "" && txtpassword.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please Give Username & Password.....');", true);
            }

            else if (txtusername.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please Give Username.....');", true);
            }
            else if (txtpassword.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please Give Password.....');", true);
            }

            else if (txtCaptcha.Text != captcha.ToString())
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('captcha invalid....');", true);
            }

            else
            {

                string CN = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                using (MySqlConnection cn = new MySqlConnection(CN))
                {
                    cn.Open();

                    string SELECT = "Select * from ClientMaster Where Code = '" + txtusername.Text + "' and Password='" + txtpassword.Text + "'";
                    MySqlCommand cmd = new MySqlCommand(SELECT, cn);
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        rdr.Close();

                        MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adp.Fill(dt);
                        int ClientID = Convert.ToInt16(dt.Rows[0]["ClientID"]);
                        string  status = (dt.Rows[0]["status"]).ToString();
                        Decimal ClientLimit = Convert.ToDecimal(dt.Rows[0]["Client_Limit"]);
                        if(ClientLimit >= 0 && status == "Active")
                        {
                            string ht = (string)HttpContext.Current.Application["SESSION_LIST"];
                            string sID = HttpContext.Current.Session.SessionID;
                            if (ht == sID)
                            {
                                HttpContext.Current.Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
                                Session.Remove("ClientID");
                            }
                            Session["ClientID"] = ClientID;
                            Session["clientUsername"] = txtusername.Text;
                            Response.Redirect("~/Client/Terms_Condition.aspx");
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('you Dont Enough Coins... ');", true);
                        }

                       
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please Check Username & Password.....');", true);
                    }
                }
            }
        }
        void FillCapctha()

        {
            try
            {
                Random random = new Random();

                string combination = "0123456789";

                StringBuilder captcha = new StringBuilder();

                for (int i = 0; i < 4; i++)
                {

                    captcha.Append(combination[random.Next(combination.Length)]);

                    Session["captcha"] = captcha.ToString();

                    imgCaptcha.ImageUrl = "GenerateCaptcha.aspx?" + DateTime.Now.Ticks.ToString();

                }
            }

            catch

            {

                throw;

            }
        }
        protected void btnRefresh_Click(object sender, EventArgs e)

        {

            FillCapctha();

        }

        protected void btnHOME_Click(object sender, EventArgs e)
        {
            Response.Redirect("Homepage.aspx");
        }
    }
}

