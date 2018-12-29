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
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            if (!IsPostBack)
            {

            }
        }

        protected void btnChangepass_Click(object sender, EventArgs e)
        {
            if (txtNewPassword.Text != txtConfirmPassword.Text)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('New Password Does not Match');", true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please Give Username.....');", true);
            }
            else
            {
                string CN = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                using (MySqlConnection cn = new MySqlConnection(CN))
                {
                    cn.Open();
                    string SELECT = "Select * from AdminMaster Where Password= '" + txtOldPassword.Text + "' And AdminID = '" + Session["AdminID"] + "'";
                    MySqlCommand cmd = new MySqlCommand(SELECT, cn);
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        rdr.Close();
                        string Update = "Update AdminMaster Set Password = '" + txtConfirmPassword.Text + "' where AdminID = '" + Session["AdminID"] + "'";
                        MySqlCommand cmd1 = new MySqlCommand(Update, cn);
                        cmd1.ExecuteNonQuery();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Password Update Successfully...');", true);
                        txtOldPassword.Text = "";
                        txtNewPassword.Text = "";
                        txtConfirmPassword.Text = "";
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Old Password Does not Match');", true);
                    }

                }
            }
        }




        protected void txtNewPassword_TextChanged(object sender, EventArgs e)
        {
            if (txtNewPassword.Text != txtConfirmPassword.Text)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('New Password Does not Match');", true);

            }
        }
        protected void txtConfirmPassword_TextChanged(object sender, EventArgs e)


        {
            if (txtNewPassword.Text != txtConfirmPassword.Text)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('New Password Does not Match');", true);

            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Settings.aspx");
        }
    }
}