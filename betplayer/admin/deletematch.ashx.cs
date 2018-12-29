using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Web.Script.Serialization;
using System.Data;

namespace betplayer.admin
{
    /// <summary>
    /// Summary description for deletematch
    /// </summary>
    public class deletematch : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/json";
            string result = DeleteMatch(Convert.ToInt32(context.Request["apiID"]));
            if (result == "success")
                context.Response.Write(new JavaScriptSerializer().Serialize(new
                {
                    status = true,

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
        public string DeleteMatch(int apiID)
        {
            try
            {
                {

                    string CN = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                    using (MySqlConnection cn = new MySqlConnection(CN))
                    {
                        cn.Open();
                        string DeleteMatch = "Delete From Matches where apiID = '" + apiID + "'";
                        MySqlCommand DeleteMatchcmd = new MySqlCommand(DeleteMatch, cn);
                        DeleteMatchcmd.ExecuteNonQuery();
                    }
                    return "success";
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

    }
}