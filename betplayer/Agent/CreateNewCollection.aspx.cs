using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace betplayer.Agent
{
    public partial class CreateNewCollection : System.Web.UI.Page
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
                string Select = "Select * From ClientCollectionName where ContactNo = @Contactno ";
                MySqlCommand cmd2 = new MySqlCommand(Select, cn);
                cmd2.Parameters.AddWithValue("@Contactno", txtContactNo.Text);
                MySqlDataReader rdr = cmd2.ExecuteReader();
                if (rdr.Read())
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Client Already Exists.....');", true);
                }
                else
                {
                    rdr.Close();
                    string s = "insert into ClientCollectionName(Name,AccountNo,ContactNo,Address,GroupName,Status,CreatedBy,Date) values (@Name,@AccountNo,@ContactNo,@Address,@GroupName,@Status,@CreatedBy,@Date); SELECT LAST_INSERT_ID()";
                    MySqlCommand cmd = new MySqlCommand(s, cn);

                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                    cmd.Parameters.AddWithValue("@ContactNo", txtContactNo.Text);
                    cmd.Parameters.AddWithValue("@AccountNo", txtAccountNo.Text);
                    cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@GroupName", GroupNameDropDown.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Status", "Active");
                    cmd.Parameters.AddWithValue("@CreatedBy", Session["Agentcode"]);
                    cmd.Parameters.AddWithValue("@Date", DateTime.Today.ToString("yyyy/MM/dd"));
                    
                    int ID = Convert.ToInt16(cmd.ExecuteScalar());
                    string update = "Update ClientCollectionName Set Code = 'CA" + ID + "'where ClientCollectionNameID = '" + ID + "' ";
                    MySqlCommand cmd1 = new MySqlCommand(update, cn);
                    cmd1.ExecuteNonQuery();
                    Response.Redirect("~/Agent/Collectiondetails.aspx?msg=Add");
                }
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Agent/Collectiondetails.aspx");
        }
    }
}