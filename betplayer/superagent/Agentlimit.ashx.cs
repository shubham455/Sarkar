using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Web.Script.Serialization;
using System.Data;
using System.Web.SessionState;



namespace betplayer.superagent
{
    /// <summary>
    /// Summary description for Agentlimit
    /// </summary>
    public class Agentlimit : IHttpHandler, IReadOnlySessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/json";
            int Length = Convert.ToInt16(context.Request["Length"]);
            List<Object> AgentLimits = new List<Object>();
            for (int i = 0; i < Length; i++)
            {
               AgentLimits.Add(new
                {
                    AgentID = context.Request["AgentID" + i.ToString()],
                    AgentLimit = context.Request["AgentLimit" + i.ToString()],
                    FixLimit = context.Request["FixLimit" + i.ToString()],
                });
            }
            //string result = ChangeclientStatus(Convert.ToInt16(context.Request["userId"]));
            int SuperAgentID = Convert.ToInt16(context.Session["SuperAgentID"]);
            string result = UpdateagentLimit(AgentLimits, SuperAgentID);
            if (result == "success")
                context.Response.Write(new JavaScriptSerializer().Serialize(new
                {
                    status = "success"

                }));
            else if (result == "unsuccess")
                context.Response.Write(new JavaScriptSerializer().Serialize(new
                {
                    status = "unsuccess"

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
        public string UpdateagentLimit(List<Object> agentValues,int SuperAgentID)
        {
            try
            {

                string CN = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                using (MySqlConnection cn = new MySqlConnection(CN))
                {
                    cn.Open();
                    string checkAgentlimit = "Select CurrentLimit From SuperAgentMaster where SuperAgentID = '" + SuperAgentID + "'";
                    MySqlCommand cmd1 = new MySqlCommand(checkAgentlimit, cn);
                    MySqlDataAdapter adp = new MySqlDataAdapter(cmd1);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);

                    decimal AgentLimit = Convert.ToDecimal(dt.Rows[0]["CurrentLimit"]);


                    Decimal Total = 0;
                    foreach (Object client in agentValues)
                    {
                        Decimal AgentCurrentLimit = Convert.ToDecimal(client.GetType().GetProperty("AgentLimit").GetValue(client, null));

                        Total = Total + AgentCurrentLimit;
                    }

                    if (AgentLimit >= Total)
                    {
                        foreach (Object agent in agentValues)
                        {
                            string agentID = agent.GetType().GetProperty("AgentID").GetValue(agent, null).ToString();
                            string agentLimit = agent.GetType().GetProperty("AgentLimit").GetValue(agent, null).ToString();
                            string fixLimit = agent.GetType().GetProperty("FixLimit").GetValue(agent, null).ToString();

                            string updatelimit = "Update agentMaster set  CurrentLimit= '" + agentLimit + "',FixLimit= '"+fixLimit+"'  Where agentID = '" + agentID + "'";
                            MySqlCommand cmd = new MySqlCommand(updatelimit, cn);
                            cmd.ExecuteNonQuery();

                        }
                       
                    }
                    else { return "unsuccess"; }
                }
                return "success";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

    }
}