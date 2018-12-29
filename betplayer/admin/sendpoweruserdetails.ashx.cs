using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;
using System.Web.Script.Serialization;
namespace betplayer.admin
{
    /// <summary>
    /// Summary description for sendpoweruserdetails
    /// </summary>
    public class sendpoweruserdetails : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/json";
            string result = sendmessage(Convert.ToInt16(context.Request["userId"]));
            if (result == "success")
                context.Response.Write(new JavaScriptSerializer().Serialize(new
                {
                    status = true,
                    userDeletedId = context.Request["userId"]
                }));
            else context.Response.Write(new JavaScriptSerializer().Serialize(new
            {
                status = false,
                error = result
            }));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        public string sendmessage(int id)
        {
            try
            {
                string CN = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                using (MySqlConnection cn = new MySqlConnection(CN))
                {
                    cn.Open();
                    string checkstatus = "Select * From PoweruserMaster Where PoweruserID = '" + id + "'";
                    MySqlCommand cmd1 = new MySqlCommand(checkstatus, cn);
                    MySqlDataAdapter adp = new MySqlDataAdapter(cmd1);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);
                    string ContactNo = (dt.Rows[0]["ContactNo"]).ToString();
                    string Username = dt.Rows[0]["Code"].ToString();
                    string Password = dt.Rows[0]["Password"].ToString();

                    string Message = "This Is Your Username " + Username + "  And " + Password + " \nFrom Sarkar50. net";

                    sendSMS smsSender = new sendSMS();
                    return smsSender.SendSMS(ContactNo, Message);

                }

            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

    }
}