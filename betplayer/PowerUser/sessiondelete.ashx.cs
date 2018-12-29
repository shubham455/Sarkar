using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Web.Script.Serialization;
using System.Data;
using System.Web.SessionState;


namespace betplayer.PowerUser
{
    /// <summary>
    /// Summary description for sessiondelete
    /// </summary>
    public class sessiondelete : IHttpHandler,IReadOnlySessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/json";
            int UserID = Convert.ToInt16(context.Session["PoweruserID"]);
            if (IsClient(UserID))
            {
                string Result = deleteSession(context);
                if (Result == "success")
                    context.Response.Write(new JavaScriptSerializer().Serialize(new
                    {
                        status = true,
                        Result = Result
                    }));
                else context.Response.Write(new JavaScriptSerializer().Serialize(new
                {
                    status = false,
                    error = Result
                }));
            }
        }



        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        private bool IsClient(int id)
        {

            string CN = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            using (MySqlConnection cn = new MySqlConnection(CN))
            {
                cn.Open();
                string SELECT = "Select PoweruserID from PoweruserMaster Where PoweruserID = '" + id + "'";
                MySqlCommand cmd = new MySqlCommand(SELECT, cn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read()) return true;
                else return false;

            }
        }
        private string deleteSession(HttpContext context)
        {
            string Session = context.Request["SessionKey"].ToString();
            string MatchID = context.Request["MatchID"].ToString();

            try
            {
                string CN = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                using (MySqlConnection cn = new MySqlConnection(CN))
                {
                    cn.Open();
                    string deletebet = "Delete  From Session where Session = '" + Session + "'";
                    MySqlCommand deletebetcmd = new MySqlCommand(deletebet, cn);
                    deletebetcmd.ExecuteNonQuery();

                    string deletesessioncal = "Delete  From SessionCalculation Where Session = '" + Session + "'";
                    MySqlCommand deletesessioncalcmd = new MySqlCommand(deletesessioncal, cn);
                    deletesessioncalcmd.ExecuteNonQuery();

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
