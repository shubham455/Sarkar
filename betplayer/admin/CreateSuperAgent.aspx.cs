using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace betplayer.admin
{
    public partial class CreateSuperAgent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
        }
        protected void Submit_Click(object sender, EventArgs e)
        {
            string CN = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            using (MySqlConnection cn = new MySqlConnection(CN))
            {
                cn.Open();
                string Select = "Select * From SuperAgentMaster where ContactNo = @Contactno ";
                MySqlCommand cmd2 = new MySqlCommand(Select, cn);
                cmd2.Parameters.AddWithValue("@Contactno", txtContactno.Text);
                MySqlDataReader rdr = cmd2.ExecuteReader();
                if (rdr.Read())
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Agent Already Exists.....');", true);
                }
                else
                {
                    rdr.Close();
                    string s = "insert into SuperAgentMaster(Name,ContactNo,Password,Agentlimit,Currentlimit,FixLimit,myshare,mymobAmount,SessionCommisionType,Status,CreatedBy,Date,Mode,MatchCommision,SessionCommision) values (@Name,@Contact_No,@Password,@Agentlimit,@Currentlimit,@FixLimit,@Agentshare,@MobileAppAmount,@SessionType,@Status,@CreatedBy,@Date,@Mode,@MatchCommision,@SessionCommision); SELECT LAST_INSERT_ID()";
                    MySqlCommand cmd = new MySqlCommand(s, cn);

                    cmd.Parameters.AddWithValue("@Name", txtname.Text);
                    cmd.Parameters.AddWithValue("@Contact_No", txtContactno.Text);
                    cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
                    cmd.Parameters.AddWithValue("@Agentlimit", txtAgentlimit.Text);
                    cmd.Parameters.AddWithValue("@Currentlimit", txtAgentlimit.Text);
                    cmd.Parameters.AddWithValue("@Fixlimit", txtAgentlimit.Text);
                    cmd.Parameters.AddWithValue("@Agentshare", txtAgentShare.Text);
                    cmd.Parameters.AddWithValue("@MobileAppAmount", txtMobileApp.Text);
                    cmd.Parameters.AddWithValue("@SessionType", SessionDropDown.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Status", "Active");
                    cmd.Parameters.AddWithValue("@CreatedBy", Session["Admincode"]);
                    cmd.Parameters.AddWithValue("@Date", DateTime.Today.ToString("yyyy/MM/dd"));
                    cmd.Parameters.AddWithValue("@Mode", "Admin");
                    cmd.Parameters.AddWithValue("@MatchCommision", MatchCommissionAgent.Value);
                    cmd.Parameters.AddWithValue("@SessionCommision", SessionCommissionAgent.Value);

                    int ID = Convert.ToInt16(cmd.ExecuteScalar());
                    string update = "Update SuperAgentMaster Set Code = 'SA" + ID + "'where SuperAgentID = '" + ID + "' ";
                    MySqlCommand cmd1 = new MySqlCommand(update, cn);
                    cmd1.ExecuteNonQuery();


                    Response.Redirect("SuperAgentDetails.aspx?msg=Add");
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

            Response.Redirect("SuperAgentDetails.aspx");

        }


    }
}




