using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using System.Globalization;


namespace Panchayat_System.Admin
{
    public partial class BirthCertificate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;

            if (!IsPostBack)
            {
                string CN = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                using (MySqlConnection cn = new MySqlConnection(CN))
                {
                    cn.Open();
                    string s3 = "Select agentshare,AgentLimit,Currentlimit From AgentMaster where AgentID = '" + Session["AgentID"] + "'";
                    MySqlCommand cmd3 = new MySqlCommand(s3, cn);
                    MySqlDataAdapter adp3 = new MySqlDataAdapter(cmd3);
                    DataTable dt3 = new DataTable();
                    adp3.Fill(dt3);

                    decimal AgentLimit = Convert.ToDecimal(dt3.Rows[0]["Currentlimit"]);
                    decimal Total = 0;
                    string s4 = "Select * From ClientMaster where CreatedBy = '" + Session["Agentcode"] + "'";
                    MySqlCommand cmd4 = new MySqlCommand(s4, cn);
                    MySqlDataAdapter adp4 = new MySqlDataAdapter(cmd4);
                    DataTable dt4 = new DataTable();
                    adp4.Fill(dt4);

                    for (int i = 0; i < dt4.Rows.Count; i++)
                    {

                        decimal ClientLimit = Convert.ToDecimal(dt4.Rows[i]["CurrentLimit"]);
                        Total = Total + ClientLimit;

                    }

                    txtAgentlimit.Text = (AgentLimit - Total).ToString();

                    txtAgentShare2.Text = dt3.Rows[0]["agentshare"].ToString();
                }
            }
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            string strNewPassword1 = GeneratePassword().ToString();

            string CN = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            using (MySqlConnection cn = new MySqlConnection(CN))
            {
                cn.Open();

                string s = "insert into ClientMaster(Name,Contact_No,Password,Client_limit,Currentlimit,FixLimit,Agent_Share,mobileApp,Session_Commision_Type,Status,CreatedBy,Date,Mode,MobileAppAmount) values (@Name,@Contact_No,@Password,@Clientlimit,@Currentlimit,@FixLimit,@Agentshare,@MobApp,@SessionType,@Status,@CreatedBy,@Date,@Mode,@MobAppAmount); SELECT LAST_INSERT_ID()";
                MySqlCommand cmd = new MySqlCommand(s, cn);

                cmd.Parameters.AddWithValue("@Name", txtname.Text);
                cmd.Parameters.AddWithValue("@Contact_No", txtContactno.Text);
                cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
                cmd.Parameters.AddWithValue("@Clientlimit", txtClientlimit.Text);
                cmd.Parameters.AddWithValue("@CurrentLimit", txtClientlimit.Text);
                cmd.Parameters.AddWithValue("@Agentshare", txtAgentShare.Text);
                cmd.Parameters.AddWithValue("@SessionType", SessionDropDown.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Status", "Active");
                cmd.Parameters.AddWithValue("@CreatedBy", Session["Agentcode"]);
                cmd.Parameters.AddWithValue("@Date", DateTime.Today.ToString("yyyy/MM/dd"));
                cmd.Parameters.AddWithValue("@Mode", "Agent");
                cmd.Parameters.AddWithValue("@FixLimit", txtClientlimit.Text);
                cmd.Parameters.AddWithValue("@MobApp","Yes");
                cmd.Parameters.AddWithValue("@MobAppAmount",txtMobapp.Text);

                int ID = Convert.ToInt16(cmd.ExecuteScalar());
                string update = "Update ClientMaster Set Code = 'C" + ID + "'where ClientID = '" + ID + "' ";
                MySqlCommand cmd1 = new MySqlCommand(update, cn);
                cmd1.ExecuteNonQuery();
                Response.Redirect("~/Agent/ClientDetails.aspx?msg=Add");
            }
        }

        public string GeneratePassword()
        {
            string PasswordLength = "8";
            string NewPassword = "";

            string allowedChars = "";
            allowedChars = "1,2,3,4,5,6,7,8,9,0";
            allowedChars += "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,";
            allowedChars += "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,";


            char[] sep = { ',' };
            string[] arr = allowedChars.Split(sep);


            string IDString = "";
            string temp = "";

            Random rand = new Random();

            for (int i = 0; i < Convert.ToInt32(PasswordLength); i++)
            {
                temp = arr[rand.Next(0, arr.Length)];
                IDString += temp;
                NewPassword = IDString;

            }
            return NewPassword;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Agent/ClientDetails.aspx");
        }

        protected void txtClientlimit_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(txtAgentlimit.Text) < Convert.ToDecimal(txtClientlimit.Text))
            {
                txtClientlimit.Text = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Client Limit Do not Greater Than Agent Limit.....');", true);
            }
        }

        protected void txtAgentShare_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(txtAgentShare.Text) > Convert.ToDecimal(txtAgentShare2.Text))
            {
                txtAgentShare.Text = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Agent Share Do not Greater Than My Share.....');", true);
            }
        }
    }
}




