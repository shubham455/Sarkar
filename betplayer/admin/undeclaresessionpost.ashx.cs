using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using System.Data;

namespace betplayer.admin
{
    /// <summary>
    /// Summary description for undeclaresessionpost
    /// </summary>
    public class undeclaresessionpost : IHttpHandler, IReadOnlySessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/json";
            int UserID = Convert.ToInt16(context.Session["AdminID"]);
            if (IsClient(UserID))
            {
                string Result = undeclareSession(context);
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
                string SELECT = "Select AdminID from AdminMaster Where AdminID = '" + id + "'";
                MySqlCommand cmd = new MySqlCommand(SELECT, cn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read()) return true;
                else return false;

            }
        }
        private string undeclareSession(HttpContext context)
        {
            string SessionName = context.Request["SessionKey"].ToString();
            string apiID = context.Request["MatchID"].ToString();
            try
            {
                string CN = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                using (MySqlConnection cn = new MySqlConnection(CN))
                {
                    cn.Open();
                    string positions = "Select * From SessionPosition where MatchID = '" + apiID + "' and Session ='"+ SessionName+"' ";
                    MySqlCommand positionscmd = new MySqlCommand(positions, cn);
                    MySqlDataAdapter positionsadp = new MySqlDataAdapter(positionscmd);
                    DataTable positionsdt = new DataTable();
                    positionsadp.Fill(positionsdt);

                    for (int i = 0; i < positionsdt.Rows.Count; i++)
                    {
                        int ClientID = Convert.ToInt16(positionsdt.Rows[i]["ClientID"]);
                        decimal Position = Convert.ToDecimal(positionsdt.Rows[i]["Position"]);

                        string update = "Update Session Set Position = @Position where ClientID = @ClientID && MatchID= @matchID && Session = @Session order by Datetime DESC";
                        MySqlCommand updatecmd = new MySqlCommand(update, cn);
                        updatecmd.Parameters.AddWithValue("@Position", Position);
                        updatecmd.Parameters.AddWithValue("@ClientID", ClientID);
                        updatecmd.Parameters.AddWithValue("@matchID", apiID);
                        updatecmd.Parameters.AddWithValue("@Session", SessionName);
                        updatecmd.ExecuteNonQuery();
                    }



                    string Delete = "Delete From SessionCalculation where MatchID = '" + apiID + "' and Session = '"+SessionName+"'";
                    MySqlCommand Deletecmd = new MySqlCommand(Delete, cn);
                    Deletecmd.ExecuteNonQuery();


                    string Deletefromdeclared = "Delete From DeclaredSession where MatchID = '" + apiID + "' and Session = '" + SessionName + "'";
                    MySqlCommand Deletefromdeclaredcmd = new MySqlCommand(Deletefromdeclared, cn);
                    Deletefromdeclaredcmd.ExecuteNonQuery();
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