using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Web.Script.Serialization;
using System.Data;


namespace betplayer.Client
{
    /// <summary>
    /// Summary description for AddDataToSession
    /// </summary>
    public class AddDataToSession : IHttpHandler, IReadOnlySessionState
    {
        private DataTable runTable;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/json";
            int UserID = Convert.ToInt16(context.Session["ClientID"]);
            if (IsClient(UserID))
            {
                string Result = AddEntryToSession(context, UserID);
                if (Result == "success")
                    context.Response.Write(new JavaScriptSerializer().Serialize(new
                    {
                        status = "success",
                        Result = Result
                    }));
                else if (Result == "unsuccess")
                    context.Response.Write(new JavaScriptSerializer().Serialize(new
                    {
                        status = "unsuccess",
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
                string SELECT = "Select ClientID from ClientMaster Where ClientID = '" + id + "'";
                MySqlCommand cmd = new MySqlCommand(SELECT, cn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read()) return true;
                else return false;

            }
        }
        private string AddEntryToSession(HttpContext context, int ClientID)
        {
            decimal Position = 0;
            string Session = context.Request["Session"].ToString();
            Decimal Amount = Convert.ToDecimal(context.Request["Amount"]);
            Decimal Rate = Convert.ToDecimal(context.Request["Rate"]);
            Decimal AccurateRate = Rate;
            int Run = Convert.ToInt16(context.Request["Run"]);
            string Team = context.Request["Team"].ToString();
            string Mode = context.Request["Mode"].ToString();
            string MatchID = context.Request["MatchID"].ToString();


            try
            {
                string CN = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                using (MySqlConnection cn = new MySqlConnection(CN))
                {
                    cn.Open();
                    string Clientlimit = "Select Client_Limit From ClientMaster where ClientID = '" + ClientID + "'";
                    MySqlCommand Clientlimitcmd = new MySqlCommand(Clientlimit, cn);
                    MySqlDataAdapter adp = new MySqlDataAdapter(Clientlimitcmd);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);
                    Decimal ClientLimit1 = Convert.ToDecimal(dt.Rows[0]["Client_Limit"]);
                    decimal finalposition = Amount * AccurateRate;
                    decimal checkClientLimit = ClientLimit1 - finalposition;





                    string s = "Insert Into Session (ClientID,Session, Amount, Runs,Rate, Mode, DateTime, MatchID,Team,Position) values (@ClientID,@Session,@Amount,@Run,@Rate,@Mode,@DateTime,@MatchID,@Team,@Position); SELECT LAST_INSERT_ID()";
                    MySqlCommand cmd = new MySqlCommand(s, cn);
                    cmd.Parameters.AddWithValue("@ClientID", ClientID);
                    cmd.Parameters.AddWithValue("@Session", Session);
                    cmd.Parameters.AddWithValue("@Amount", Amount);
                    cmd.Parameters.AddWithValue("@Run", Run);
                    cmd.Parameters.AddWithValue("@Rate", AccurateRate);
                    cmd.Parameters.AddWithValue("@Mode", Mode);
                    cmd.Parameters.AddWithValue("@DateTime", DateTime.Now);
                    cmd.Parameters.AddWithValue("@MatchID", MatchID);
                    cmd.Parameters.AddWithValue("@Team", Team);
                    cmd.Parameters.AddWithValue("@Position", 0);

                    int ID = Convert.ToInt16(cmd.ExecuteScalar());



                    string check = "Select * from Sharetable where  MatchID = '" + MatchID + "' && ClientID = '" + ClientID + "'";
                    MySqlCommand checkcmd = new MySqlCommand(check, cn);
                    MySqlDataReader rdr = checkcmd.ExecuteReader();
                    if (rdr.Read())
                    {

                    }
                    else
                    {
                        rdr.Close();
                        string s3 = "Select Agent_Share,Createdby from ClientMaster where ClientID = '" + ClientID + "'";
                        MySqlCommand cmd3 = new MySqlCommand(s3, cn);
                        MySqlDataAdapter adp3 = new MySqlDataAdapter(cmd3);
                        DataTable dt3 = new DataTable();
                        adp3.Fill(dt3);
                        string ClientShare = dt3.Rows[0]["Agent_Share"].ToString();
                        string Agentcode = dt3.Rows[0]["CreatedBy"].ToString();


                        string s4 = "Select AgentShare,Createdby,MatchCommision,SessionCommision from AgentMaster where code = '" + Agentcode + "'";
                        MySqlCommand cmd4 = new MySqlCommand(s4, cn);
                        MySqlDataAdapter adp4 = new MySqlDataAdapter(cmd4);
                        DataTable dt4 = new DataTable();
                        adp4.Fill(dt4);
                        string AgentShare = dt4.Rows[0]["AgentShare"].ToString();
                        string AgentMatchCommision = dt4.Rows[0]["MatchCommision"].ToString();
                        string AgentSessionCommision = dt4.Rows[0]["SessionCommision"].ToString();
                        string SuperAgentCode = dt4.Rows[0]["CreatedBy"].ToString();


                        string s5 = "Select myshare,Createdby,MatchCommision,SessionCommision from SuperAgentMaster where code = '" + SuperAgentCode + "'";
                        MySqlCommand cmd5 = new MySqlCommand(s5, cn);
                        MySqlDataAdapter adp5 = new MySqlDataAdapter(cmd5);
                        DataTable dt5 = new DataTable();
                        adp5.Fill(dt5);
                        string SAgentShare = dt5.Rows[0]["myshare"].ToString();
                        string SAgentMatchCommision = dt5.Rows[0]["MatchCommision"].ToString();
                        string SAgentSessionCommision = dt5.Rows[0]["SessionCommision"].ToString();


                        string s6 = "Insert into sharetable (ClientID,MatchID,AgentShare,SAgentshare,ClientShare,AgentMatchComm,AgentSessionComm,SAgentMatchComm,SAgentSessionComm) values(@ClientID,@MatchID,@AgentShare,@SAgentShare,@ClientShare,@AgentMatchComm,@AgentSessionComm,@SAgentMatchComm,@SAgentSessionComm)";
                        MySqlCommand cmd6 = new MySqlCommand(s6, cn);

                        cmd6.Parameters.AddWithValue("@ClientID", ClientID);
                        cmd6.Parameters.AddWithValue("@MatchID", MatchID);
                        cmd6.Parameters.AddWithValue("@AgentShare", AgentShare);
                        cmd6.Parameters.AddWithValue("@SAgentShare", SAgentShare);
                        cmd6.Parameters.AddWithValue("@ClientShare", ClientShare);
                        cmd6.Parameters.AddWithValue("@AgentMatchComm", AgentMatchCommision);
                        cmd6.Parameters.AddWithValue("@AgentSessionComm", AgentSessionCommision);
                        cmd6.Parameters.AddWithValue("@SAgentMatchComm", SAgentMatchCommision);
                        cmd6.Parameters.AddWithValue("@SAgentSessionComm", SAgentSessionCommision);
                        cmd6.ExecuteNonQuery();

                    }


                    //SessionCalculation
                    rdr.Close();
                    runTable = new DataTable();
                    runTable.Columns.Add(new DataColumn("RUNS"));
                    runTable.Columns.Add(new DataColumn("AMOUNT"));

                    string session = "select Session.sessionID,Session.session,Session.Position,Session.Runs,Session.Amount,Session.rate,Session.Mode,Session.DateTime,Session.Team,Session.clientID,clientmaster.Name from Session inner join clientmaster on Session.ClientID = clientmaster.ClientID where clientmaster.ClientID = '" + ClientID + "' && Session.MatchID = '" + MatchID + "' && Session.Session = '" + Session + "'";
                    MySqlCommand sessioncmd = new MySqlCommand(session, cn);
                    MySqlDataAdapter sessionadp1 = new MySqlDataAdapter(sessioncmd);
                    DataTable sessiondt = new DataTable();
                    sessionadp1.Fill(sessiondt);

                    for (int j = 0; j < sessiondt.Rows.Count; j++)
                    {
                        if (j == 0)
                        {

                            int runs = Convert.ToInt16(sessiondt.Rows[j]["Runs"]);
                            int SessionAmount = Convert.ToInt32(sessiondt.Rows[j]["Amount"]);
                            Decimal SessionRate = Convert.ToDecimal(sessiondt.Rows[j]["Rate"]);
                            string SessionMode = sessiondt.Rows[j]["Mode"].ToString();


                            for (int i = runs + 5; i >= runs - 5; i--)
                            {

                                DataRow row = runTable.NewRow();
                                row["RUNS"] = i.ToString();
                                row["Amount"] = CalculateAmount(SessionMode,
                                    i, 0,
                                    SessionRate,
                                    runs, SessionAmount).ToString();
                                runTable.Rows.Add(row.ItemArray);
                            }

                        }
                        else
                        {

                            int runs = Convert.ToInt32(sessiondt.Rows[j]["Runs"]);
                            int SessionAmount1 = Convert.ToInt32(sessiondt.Rows[j]["Amount"]);
                            Decimal SessionRate1 = Convert.ToDecimal(sessiondt.Rows[j]["Rate"]);
                            string SessionMode1 = sessiondt.Rows[j]["Mode"].ToString();


                            int highVal = Convert.ToInt32(runTable.Rows[0]["Runs"]);
                            int lowVal = Convert.ToInt32(runTable.Rows[runTable.Rows.Count - 1]["Runs"]);
                            if (runs > highVal)
                            {
                                for (int i = runs + 5; i > highVal; i--)
                                {
                                    DataRow row = runTable.NewRow();
                                    row["RUNS"] = i.ToString();
                                    row["Amount"] = runTable.Rows[0]["Amount"];

                                    runTable.Rows.InsertAt(row, (runs + 5 - i));
                                }
                            }
                            else if ((runs - 5) < lowVal)
                            {
                                for (int i = lowVal - 1; i >= runs - 5; i--)
                                {
                                    DataRow row = runTable.NewRow();
                                    row["RUNS"] = i.ToString();
                                    row["Amount"] = runTable.Rows[runTable.Rows.Count - 1]["Amount"];

                                    runTable.Rows.InsertAt(row, ((highVal - lowVal) + (lowVal - i)));
                                }
                            }

                            for (int i = 0; i < runTable.Rows.Count; i++)
                            {
                                DataRow row = runTable.Rows[i];
                                if (Convert.ToInt16(runTable.Rows[i]["Runs"]) >= Convert.ToInt16(sessiondt.Rows[0]["Runs"]) &&
                                    Convert.ToInt16(runTable.Rows[i]["Runs"]) < Convert.ToInt16(sessiondt.Rows[j]["Runs"]))
                                {


                                    row["AMOUNT"] = CalculateAmount(SessionMode1,
                                        Convert.ToInt16(runTable.Rows[i]["Runs"]),
                                        Convert.ToDecimal(runTable.Rows[i]["Amount"]),
                                        SessionRate1,
                                        runs,
                                        SessionAmount1
                                        ).ToString();
                                }
                                else
                                {
                                    row["AMOUNT"] = CalculateAmount(SessionMode1,
                                        Convert.ToInt16(runTable.Rows[i]["Runs"]),
                                        Convert.ToDecimal(runTable.Rows[i]["Amount"]),
                                        SessionRate1,
                                        runs,
                                        SessionAmount1
                                        ).ToString();
                                }
                            }
                        }
                    }
                    decimal Amount12 = 0;
                    for (int a = 0; a < runTable.Rows.Count; a++)
                    {

                        decimal runamount = Convert.ToDecimal(runTable.Rows[a]["Amount"]);
                        if (runamount > Amount12)
                        {
                            Amount12 = runamount;

                        }
                    }
                    string update = "update Session set position = '" + Amount12 + "' where SessionID = '" + ID + "'  ";
                    MySqlCommand updatecmd = new MySqlCommand(update, cn);
                    updatecmd.ExecuteNonQuery();

                    string CheckSession = "Select * From Session where matchID = '" + MatchID + "' and clientID = '" + ClientID + "' and  Session = '" + Session + "' order by DateTime DESC";
                    MySqlCommand Sessioncmd = new MySqlCommand(CheckSession, cn);
                    MySqlDataAdapter Sessionadp = new MySqlDataAdapter(Sessioncmd);
                    DataTable Sessiondt = new DataTable();
                    Sessionadp.Fill(Sessiondt);

                    if (Sessiondt.Rows.Count > 1)
                    {
                        int lastAmount = Convert.ToInt32(Sessiondt.Rows[1]["Amount"]);
                        Position = Convert.ToDecimal(Sessiondt.Rows[1]["Position"]);
                        string mode = Sessiondt.Rows[1]["Mode"].ToString();
                        int run = Convert.ToInt16(Sessiondt.Rows[1]["Runs"]);

                        decimal finalamount = Position + ClientLimit1;
                        if (Amount12 > finalamount)
                        {
                            string delete = "Delete From Session where SessionID = '" + ID + "'";
                            MySqlCommand deletecmd = new MySqlCommand(delete, cn);
                            deletecmd.ExecuteNonQuery();
                            return "unsuccess";
                        }
                    }
                    else
                    {
                        if (Amount12 > ClientLimit1)
                        {
                            string delete = "Delete From Session where SessionID = '" + ID + "'";
                            MySqlCommand deletecmd = new MySqlCommand(delete, cn);
                            deletecmd.ExecuteNonQuery();
                            return "unsuccess";
                        }
                    }
                }
            }


            catch (Exception e)
            {
                return e.Message;
            }
            return "success";
        }
        public Decimal CalculateAmount(string Mode, int Initruns, Decimal InitAmount, Decimal AccurateRate, int runs, int Amount)
        {


            Decimal Difference = 0;



            if (Initruns < runs)
            {


                if (Mode == "Y")
                {
                    Difference = (Amount) + InitAmount;
                }
                else if (Mode == "N")
                {
                    Difference = (Amount * -1) + InitAmount;
                }


            }
            if (Initruns >= runs)
            {
                if (Mode == "Y")
                {
                    Difference = Amount * AccurateRate * -1 + InitAmount;
                }
                else if (Mode == "N")
                {
                    Difference = (Amount * AccurateRate) + InitAmount;
                }
            }

            return Difference;
        }
    }
}