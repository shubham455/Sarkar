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
    /// Summary description for UnDeclarematch
    /// </summary>
    public class UnDeclarematch : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/json";
            string result = UnDeclare(Convert.ToInt32(context.Request["apiID"]));
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
        public string UnDeclare(int apiID)
        {
            try
            {
                {

                    string CN = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                    using (MySqlConnection cn = new MySqlConnection(CN))
                    {
                        cn.Open();


                        string positions = "Select * From Positions where MatchID = '" + apiID + "'";
                        MySqlCommand positionscmd = new MySqlCommand(positions, cn);
                        MySqlDataAdapter positionsadp = new MySqlDataAdapter(positionscmd);
                        DataTable positionsdt = new DataTable();
                        positionsadp.Fill(positionsdt);

                        for (int i = 0; i < positionsdt.Rows.Count; i++)
                        {
                            int ClientID = Convert.ToInt16(positionsdt.Rows[i]["ClientID"]);
                            int Position1 = Convert.ToInt32(positionsdt.Rows[i]["Position1"]);
                            int Position2 = Convert.ToInt32(positionsdt.Rows[i]["Position2"]);
                            string Position3 = (positionsdt.Rows[i]["Position3"]).ToString();

                            string update = "Update Runner Set Position1 = @Position1,position2 = @Position2, Position3 = @Position3 where ClientID = @ClientID && MatchID= @matchID order by Datetime DESC";
                            MySqlCommand updatecmd = new MySqlCommand(update, cn);
                            updatecmd.Parameters.AddWithValue("@Position1", Position1);
                            updatecmd.Parameters.AddWithValue("@Position2", Position2);
                            updatecmd.Parameters.AddWithValue("@Position3", Position3);
                            updatecmd.Parameters.AddWithValue("@ClientID", ClientID);
                            updatecmd.Parameters.AddWithValue("@matchID", apiID);
                            updatecmd.ExecuteNonQuery();

                        }

                        string SELECT = "Update Matches Set Status = '1',winnerteam = '', Declear = '0' Where apiID = '" + apiID + "'";
                        MySqlCommand cmd = new MySqlCommand(SELECT, cn);
                        cmd.ExecuteNonQuery();

                        string Delete = "Delete From clientledger where MatchID = '" + apiID + "'";
                        MySqlCommand Deletecmd = new MySqlCommand(Delete, cn);
                        Deletecmd.ExecuteNonQuery();

                        string Deleteagentledger = "Delete From agentledger where MatchID = '" + apiID + "'";
                        MySqlCommand Deleteagentledgercmd = new MySqlCommand(Deleteagentledger, cn);
                        Deleteagentledgercmd.ExecuteNonQuery();

                        string Deletesuperagentledger = "Delete From superagentledger where MatchID = '" + apiID + "'";
                        MySqlCommand Deletesuperagentledgercmd = new MySqlCommand(Deletesuperagentledger, cn);
                        Deletesuperagentledgercmd.ExecuteNonQuery();



                        string DeleteAdminLedger = "Delete From adminledger where MatchID = '" + apiID + "'";
                        MySqlCommand DeleteAdminLedgercmd = new MySqlCommand(DeleteAdminLedger, cn);
                        DeleteAdminLedgercmd.ExecuteNonQuery();


                        string DeleteMatchCalcultion = "Delete From MatchCalculation where MatchID = '" + apiID + "'";
                        MySqlCommand DeleteMatchCalcultioncmd = new MySqlCommand(DeleteMatchCalcultion, cn);
                        DeleteMatchCalcultioncmd.ExecuteNonQuery();

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