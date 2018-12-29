using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Web.SessionState;



namespace betplayer.PowerUser
{
    /// <summary>
    /// Summary description for Session
    /// </summary>
    public class Session : IHttpHandler, IReadOnlySessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/json";
            int UserID = Convert.ToInt16(context.Session["PoweruserID"]);
            if (IsClient(UserID))
            {
                string Result = declareSession(context);
                if (Result == "success")
                    context.Response.Write(new JavaScriptSerializer().Serialize(new
                    {
                        status = true,
                        Result = Result
                    }));
                else if (Result == "unsuccess")
                    context.Response.Write(new JavaScriptSerializer().Serialize(new
                    {
                        status = "unsuccess"

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
        private string declareSession(HttpContext context)
        {

            if (context.Session["PowerUserID"] != null)
            {

                string SessionKey = context.Request["SessionKey"].ToString();
                int declarevalue = Convert.ToInt16(context.Request["declareValue"]);
                string MatchID = context.Request["MatchID"].ToString();
                try
                {
                    Decimal TotalAmount = 0;
                    Decimal Amount1;

                    string CN = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                    using (MySqlConnection cn = new MySqlConnection(CN))
                    {
                        cn.Open();

                        string checkSession = "Select * From DeclaredSession where Session = '" + SessionKey + "' && MatchID = '" + MatchID + "'";
                        MySqlCommand checkSessioncmd = new MySqlCommand(checkSession, cn);
                        MySqlDataReader rdr = checkSessioncmd.ExecuteReader();
                        if (rdr.Read())
                        {
                            return "Match Declared";
                        }
                        else
                        {

                            rdr.Close();
                            string s1 = "select * from Session where Session = @SessionKey && matchID= @MatchID  ORDER BY ClientID DESC;";
                            MySqlCommand cmd1 = new MySqlCommand(s1, cn);
                            cmd1.Parameters.AddWithValue("@SessionKey", SessionKey);
                            cmd1.Parameters.AddWithValue("@MatchID", MatchID);
                            MySqlDataAdapter adp1 = new MySqlDataAdapter(cmd1);
                            DataTable dt1 = new DataTable();
                            adp1.Fill(dt1);
                            Decimal Potition = 0;
                            for (int i = 0; i < dt1.Rows.Count; i++)
                            {
                                int Amount = Convert.ToInt32(dt1.Rows[i]["Amount"]);
                                string Mode = dt1.Rows[i]["Mode"].ToString();
                                int Runs = Convert.ToInt16(dt1.Rows[i]["Runs"]);
                                Decimal Rate = Convert.ToDecimal(dt1.Rows[i]["Rate"]);
                                int clientID = Convert.ToInt32(dt1.Rows[i]["ClientID"]);
                                Potition = Convert.ToDecimal(dt1.Rows[i]["Position"]);

                                if (declarevalue == Runs)
                                {
                                    if (Mode == "Y")
                                    {
                                        Amount1 = Amount * Rate;
                                        TotalAmount = TotalAmount + Amount1;

                                    }
                                    else if (Mode == "N")
                                    {
                                        Amount1 = Amount * Rate;
                                        TotalAmount = TotalAmount - Amount1;
                                    }

                                }
                                else if (declarevalue < Runs)
                                {
                                    if (Mode == "Y")
                                    {
                                        Amount1 = Amount;
                                        TotalAmount = TotalAmount - Amount1;

                                    }
                                    else if (Mode == "N")
                                    {
                                        Amount1 = Amount;
                                        TotalAmount = TotalAmount + Amount1;
                                    }

                                }
                                else if (declarevalue > Runs)
                                {
                                    if (Mode == "Y")
                                    {
                                        Amount1 = Amount * Rate;
                                        TotalAmount = TotalAmount + Amount1;

                                    }
                                    else if (Mode == "N")
                                    {
                                        Amount1 = Amount * Rate;
                                        TotalAmount = TotalAmount - Amount1;
                                    }

                                }

                                string s2 = "select * from Session where Session = @SessionKey && matchID= @MatchID  ORDER BY ClientID DESC;";
                                MySqlCommand cmd2 = new MySqlCommand(s2, cn);
                                cmd2.Parameters.AddWithValue("@SessionKey", SessionKey);
                                cmd2.Parameters.AddWithValue("@MatchID", MatchID);
                                MySqlDataAdapter adp2 = new MySqlDataAdapter(cmd1);
                                DataTable dt2 = new DataTable();
                                adp1.Fill(dt2);
                                for (int j = i + 1; j <= dt2.Rows.Count;)
                                {
                                    if (dt2.Rows.Count > j)
                                    {
                                        int ClientID = Convert.ToInt32(dt2.Rows[j]["ClientID"]);
                                        Potition = Convert.ToDecimal(dt2.Rows[j - 1]["Position"]);
                                        if (clientID == ClientID)
                                        {

                                        }
                                        else
                                        {
                                            string StoreAmount = "Insert Into sessioncalculation (Session,ClientID,MatchID,TotalAmount) Values (@Session,@ClientID,@MatchID,@TotalAmount)";
                                            MySqlCommand cmd3 = new MySqlCommand(StoreAmount, cn);
                                            cmd3.Parameters.AddWithValue("@Session", SessionKey);
                                            cmd3.Parameters.AddWithValue("@ClientID", clientID);
                                            cmd3.Parameters.AddWithValue("@MatchID", MatchID);
                                            cmd3.Parameters.AddWithValue("@TotalAmount", TotalAmount);
                                            cmd3.ExecuteNonQuery();


                                            string insertintosessionposition = "Insert into SessionPosition(Session,ClientID, Position,MatchID) values (@Session,@ClientID,@Position,@MatchID)";
                                            MySqlCommand insertintosessionpositioncmd = new MySqlCommand(insertintosessionposition, cn);
                                            insertintosessionpositioncmd.Parameters.AddWithValue("@Session", SessionKey);
                                            insertintosessionpositioncmd.Parameters.AddWithValue("@ClientID", clientID);
                                            insertintosessionpositioncmd.Parameters.AddWithValue("@Position", Potition);
                                            insertintosessionpositioncmd.Parameters.AddWithValue("@MatchID", MatchID);
                                            insertintosessionpositioncmd.ExecuteNonQuery();

                                            TotalAmount = 0;
                                        }
                                        break;
                                    }
                                    else
                                    {

                                        string StoreAmount = "Insert Into sessioncalculation (Session,ClientID,MatchID,TotalAmount) Values (@Session,@ClientID,@MatchID,@TotalAmount)";
                                        MySqlCommand cmd3 = new MySqlCommand(StoreAmount, cn);
                                        cmd3.Parameters.AddWithValue("@Session", SessionKey);
                                        cmd3.Parameters.AddWithValue("@ClientID", clientID);
                                        cmd3.Parameters.AddWithValue("@MatchID", MatchID);
                                        cmd3.Parameters.AddWithValue("@TotalAmount", TotalAmount);
                                        cmd3.ExecuteNonQuery();

                                        string insertintosessionposition = "Insert into SessionPosition(Session,ClientID, Position,MatchID) values (@Session,@ClientID,@Position,@MatchID)";
                                        MySqlCommand insertintosessionpositioncmd = new MySqlCommand(insertintosessionposition, cn);
                                        insertintosessionpositioncmd.Parameters.AddWithValue("@Session", SessionKey);
                                        insertintosessionpositioncmd.Parameters.AddWithValue("@ClientID", clientID);
                                        insertintosessionpositioncmd.Parameters.AddWithValue("@Position", Potition);
                                        insertintosessionpositioncmd.Parameters.AddWithValue("@MatchID", MatchID);
                                        insertintosessionpositioncmd.ExecuteNonQuery();
                                        TotalAmount = 0;
                                    }
                                    break;
                                }
                            }


                            string s = "Insert Into DeclaredSession (Session, declareRun,MatchID) values (@Session,@declareRun,@MatchID)";
                            MySqlCommand cmd = new MySqlCommand(s, cn);

                            cmd.Parameters.AddWithValue("@Session", SessionKey);
                            cmd.Parameters.AddWithValue("@declareRun", declarevalue);
                            cmd.Parameters.AddWithValue("@MatchID", MatchID);
                            cmd.ExecuteNonQuery();


                            string ClearPosition = "Update Session Set Position = '0' where Session = '" + SessionKey + "'";
                            MySqlCommand ClearPositioncmd = new MySqlCommand(ClearPosition, cn);
                            ClearPositioncmd.ExecuteNonQuery();

                            return "success";
                        }
                    }
                }




                catch (Exception e)
                {
                    return e.Message;
                }

            }
            else
            {
                return "unsuccess";
            }
        }
    }
}