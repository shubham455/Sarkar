using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace betplayer.Super_Agent
{
    public partial class ModifyAgent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
       {
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            if (!IsPostBack)
            {

                int Id = Convert.ToInt16(Request.QueryString["AgentID"]);
                string CN = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                using (MySqlConnection cn = new MySqlConnection(CN))
                {
                    string s = "Select * From AgentMaster where AgentId = '" + Id + "' ";
                    MySqlCommand cmd = new MySqlCommand(s, cn);
                    MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);
                    txtCode.Text = dt.Rows[0]["Code"].ToString();
                    txtname.Text = dt.Rows[0]["Name"].ToString();
                    txtContactno.Text = dt.Rows[0]["ContactNo"].ToString();
                    txtPassword.Text = dt.Rows[0]["Password"].ToString();
                    txtAgentlimit.Text = dt.Rows[0]["FixLimit"].ToString();
                    txtAgentShare.Text = dt.Rows[0]["AgentShare"].ToString();
                    SessionDropDown.SelectedItem.Text = dt.Rows[0]["SessionCommisionType"].ToString();
                    DropDownstatus.SelectedItem.Text = dt.Rows[0]["status"].ToString();
                    MatchCommissionAgent.Value  = dt.Rows[0]["MatchCommision"].ToString();
                    SessionCommissionAgent.Value = dt.Rows[0]["SessionCommision"].ToString();

                    string s1 = "Select * From SuperAgentMaster where SuperAgentId = '" + Session["SuperAgentID"] + "' ";
                    MySqlCommand cmd1 = new MySqlCommand(s1, cn);
                    MySqlDataAdapter adp1 = new MySqlDataAdapter(cmd1);
                    DataTable dt1 = new DataTable();
                    adp1.Fill(dt1);
                    txtSuperAgentShare.Text = dt1.Rows[0]["myshare"].ToString();
                    Text1.Value = dt1.Rows[0]["MatchCommision"].ToString();
                    Text2.Value = dt1.Rows[0]["SessionCommision"].ToString();

                }
            }


        }


        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int Id = Convert.ToInt16(Request.QueryString["AgentId"]);
            string CN = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            using (MySqlConnection cn = new MySqlConnection(CN))
            {
                cn.Open();
                string s = "Update AgentMaster set Name = @Name,ContactNo= @ContactNo,Password=@Password,FixLimit=@FixLimit,AgentShare=@Agentshare,MobApp = @MobApp,SessionCommisionType=@SessionType,MatchCommision=@MatchCommision, SessionCommision=@SessionCommision,Status=@Status  where AgentId = '" + Id + "' ";
                MySqlCommand cmd = new MySqlCommand(s, cn);


                cmd.Parameters.AddWithValue("@Name", txtname.Text);
                cmd.Parameters.AddWithValue("@ContactNo", txtContactno.Text);
                cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
                cmd.Parameters.AddWithValue("@FixLimit", txtAgentlimit.Text);
                cmd.Parameters.AddWithValue("@Agentshare", txtAgentShare.Text);
                cmd.Parameters.AddWithValue("@MobApp", txtMobileApp.Text);
                cmd.Parameters.AddWithValue("@SessionType", SessionDropDown.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@MatchCommision", MatchCommissionAgent.Value);
                cmd.Parameters.AddWithValue("@SessionCommision", SessionCommissionAgent.Value);
                cmd.Parameters.AddWithValue("@status", DropDownstatus.SelectedItem.Text);
                
                cmd.ExecuteNonQuery();
                Response.Redirect("AgentDetails.aspx?msg=Update");

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("ClientDetails.aspx");
        }

        protected void txtAgentShare_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(txtSuperAgentShare.Text) < Convert.ToDecimal(txtAgentShare.Text))
            {
                txtAgentShare.Text = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Agent Share Do not Greater Than My Share.....');", true);
            }
        }
    }
}