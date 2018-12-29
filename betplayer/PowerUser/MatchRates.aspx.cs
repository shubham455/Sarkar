using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;


namespace betplayer.poweruser
{
    public partial class MatchRates : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            firebasekey.Value = Request.QueryString["fk"];
        }

        protected void btnDeclare_ServerClick(object sender, EventArgs e)
        {
            int apiID = Convert.ToInt32(Request.QueryString["Matchid"]);

            decimal LedgerAmount = 0;
            string team1, team2;
            string CN = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            using (MySqlConnection cn = new MySqlConnection(CN))
            {
                cn.Open();
                string Match = "Select * From Matches where apiID = '" + apiID + "'";
                MySqlCommand Matchcmd = new MySqlCommand(Match, cn);
                MySqlDataAdapter Matchadp = new MySqlDataAdapter(Matchcmd);
                DataTable Matchdt = new DataTable();
                Matchadp.Fill(Matchdt);
                string Declear = Matchdt.Rows[0]["Declear"].ToString();
                team1 = Matchdt.Rows[0]["TeamA"].ToString();
                team2 = Matchdt.Rows[0]["TeamB"].ToString();
                string session1 = "";
                if (Declear == "False")
                {
                    string session = "Select Session,ClientID From Session where MatchID ='" + apiID + "'  group by Session";
                    MySqlCommand sessioncmd = new MySqlCommand(session, cn);
                    MySqlDataAdapter sessionadp = new MySqlDataAdapter(sessioncmd);
                    DataTable sessiondt = new DataTable();
                    sessionadp.Fill(sessiondt);
                    Boolean check = false;
                    for (int s = 0; s < sessiondt.Rows.Count; s++)
                    {
                        session1 = sessiondt.Rows[s]["Session"].ToString();

                        string declaredsession = "Select Session From DeclaredSession where '" + apiID + "' and Session = '" + session1 + "'";
                        MySqlCommand declaredsessioncmd = new MySqlCommand(declaredsession, cn);
                        MySqlDataReader rdr = declaredsessioncmd.ExecuteReader();
                        if (!rdr.Read())
                        {
                            DataTable dt = new DataTable();
                            dt.Load(rdr);

                            check = true;
                            break;
                        }
                        rdr.Close();
                    }

                    if (check == false)
                    {
                        string checkclient = "Select ClientID  From ViewMatch  where MatchID = '" + apiID + "'";
                        MySqlCommand checkclientcmd = new MySqlCommand(checkclient, cn);
                        MySqlDataAdapter checkclientadp = new MySqlDataAdapter(checkclientcmd);
                        DataTable checkclientdt = new DataTable();
                        checkclientadp.Fill(checkclientdt);

                        for (int i = 0; i < checkclientdt.Rows.Count; i++)
                        {
                            int ClientID = Convert.ToInt16(checkclientdt.Rows[i]["ClientID"]);


                            int Position1 = 0, Position2 = 0;
                            string Position3 = "";

                            string Runner = "Select Position1,Position2,Position3 from Runner where ClientID = '" + ClientID + "' && MatchID = '" + apiID + "' order by DateTime DESC";
                            MySqlCommand Runnercmd = new MySqlCommand(Runner, cn);
                            MySqlDataAdapter Runneradp = new MySqlDataAdapter(Runnercmd);
                            DataTable Runnerdt = new DataTable();
                            Runneradp.Fill(Runnerdt);
                            decimal MatchtotalAmount = 0;
                            if (Runnerdt.Rows.Count > 0)
                            {
                                Position1 = Convert.ToInt32(Runnerdt.Rows[0]["Position1"]);
                                Position2 = Convert.ToInt32(Runnerdt.Rows[0]["Position2"]);
                                Position3 = (Runnerdt.Rows[0]["Position3"]).ToString();

                                if (team_selector1.Value == "team_1")
                                {
                                    MatchtotalAmount = Position1;
                                    LedgerAmount = LedgerAmount + Position1;
                                }
                                else if (team_selector1.Value == "team_2")
                                {
                                    MatchtotalAmount = Position2;
                                    LedgerAmount = LedgerAmount + Position2;
                                }

                                string AddMatchCalculation = "Insert into MatchCalculation (MatchID,ClientID,Amount) Values( @MatchID,@ClientID,@Amount)";
                                MySqlCommand AddMatchCalculationcmd = new MySqlCommand(AddMatchCalculation, cn);
                                AddMatchCalculationcmd.Parameters.AddWithValue("@ClientID", ClientID);
                                AddMatchCalculationcmd.Parameters.AddWithValue("@MatchID", apiID);
                                AddMatchCalculationcmd.Parameters.AddWithValue("@Amount", MatchtotalAmount);
                                AddMatchCalculationcmd.ExecuteNonQuery();
                            }





                            string Selectcurrentlimit = "Select * from ClientMaster where ClientID = '" + ClientID + "'";
                            MySqlCommand Selectcurrentlimitcmd = new MySqlCommand(Selectcurrentlimit, cn);
                            MySqlDataAdapter Selectcurrentlimitadp = new MySqlDataAdapter(Selectcurrentlimitcmd);
                            DataTable Selectcurrentlimitdt = new DataTable();
                            Selectcurrentlimitadp.Fill(Selectcurrentlimitdt);

                            decimal MobileAppAmount = Convert.ToDecimal(Selectcurrentlimitdt.Rows[0]["MobileAppAmount"]);

                            string sessionamt = "Select * From sessioncalculation where clientID = '" + ClientID + "' and MatchID = '" + apiID + "'";
                            MySqlCommand sessionamtcmd = new MySqlCommand(sessionamt, cn);
                            MySqlDataAdapter sessionamtadp = new MySqlDataAdapter(sessionamtcmd);
                            DataTable sessionamtdt = new DataTable();
                            sessionamtadp.Fill(sessionamtdt);
                            decimal totalsessionamt = 0;
                            if (sessionamtdt.Rows.Count > 0)
                            {
                                for (int j = 0; j < sessionamtdt.Rows.Count; j++)
                                {
                                    decimal amt = Convert.ToDecimal(sessionamtdt.Rows[j]["TotalAmount"]);
                                    totalsessionamt = totalsessionamt + amt;
                                }
                            }
                            if (LedgerAmount < 0)
                            {
                                LedgerAmount = (LedgerAmount + totalsessionamt) - MobileAppAmount;
                            }
                            else
                            {
                                LedgerAmount = (LedgerAmount + totalsessionamt) - MobileAppAmount;
                            }

                            decimal Dabit = 0, Credit = 0;
                            if (LedgerAmount > 0)
                            {
                                Dabit = LedgerAmount;
                            }
                            else if (LedgerAmount < 0)
                            {
                                Credit = LedgerAmount * -1;
                            }

                            string insertpositions = "Insert into positions (MatchID,ClientID,Position1,position2,Position3) values (@MatchID,@ClientID,@Position1,@position2,@Position3)";
                            MySqlCommand insertpositionscmd = new MySqlCommand(insertpositions, cn);
                            insertpositionscmd.Parameters.AddWithValue("@MatchID", apiID);
                            insertpositionscmd.Parameters.AddWithValue("@ClientID", ClientID);
                            insertpositionscmd.Parameters.AddWithValue("@Position1", Position1);
                            insertpositionscmd.Parameters.AddWithValue("@Position2", Position2);
                            insertpositionscmd.Parameters.AddWithValue("@Position3", Position3);
                            insertpositionscmd.ExecuteNonQuery();



                            string clearusedlimit = "update runner set Position1 = '0',Position2 = '0' where ClientID = '" + ClientID + "' && MatchID = '" + apiID + "' order by DateTime DESC ";
                            MySqlCommand clearusedlimitcmd = new MySqlCommand(clearusedlimit, cn);
                            clearusedlimitcmd.ExecuteNonQuery();


                            if (LedgerAmount < 0)
                            {

                                decimal currentlimit = Convert.ToDecimal(Selectcurrentlimitdt.Rows[0]["CurrentLimit"]);

                                decimal finaldeductedamount = currentlimit + LedgerAmount;

                                string updateamount = "update clientmaster set Currentlimit = '" + finaldeductedamount + "',client_limit= '" + finaldeductedamount + "' where ClientID = '" + ClientID + "' ";
                                MySqlCommand updateamountcmd = new MySqlCommand(updateamount, cn);
                                updateamountcmd.ExecuteNonQuery();
                            }


                            string MatchBet = "select count(clientID) From runner where MatchID = '" + apiID + "' && ClientID = '" + ClientID + "' ";
                            MySqlCommand MatchBetcmd = new MySqlCommand(MatchBet, cn);
                            string MatchBetcount = MatchBetcmd.ExecuteScalar().ToString();

                            string SessionBet = "select count(clientID) From session where MatchID = '" + apiID + "' && ClientID = '" + ClientID + "' ";
                            MySqlCommand SessionBetcmd = new MySqlCommand(SessionBet, cn);
                            string SessionBetcount = SessionBetcmd.ExecuteScalar().ToString();



                            string InsertClientLedger = "Insert Into ClientLedger (clientID,MatchID,Dabit,Credit,Amount,MatchBets,SessionBets) values (@ClientID,@MatchID,@Dabit,@Credit,@Amount,@MatchBets,@SessionBets)";
                            MySqlCommand Insertclientcmd = new MySqlCommand(InsertClientLedger, cn);
                            Insertclientcmd.Parameters.AddWithValue("@ClientID", ClientID);
                            Insertclientcmd.Parameters.AddWithValue("@MatchID", apiID);
                            Insertclientcmd.Parameters.AddWithValue("@Dabit", Dabit);
                            Insertclientcmd.Parameters.AddWithValue("@Credit", Credit);
                            Insertclientcmd.Parameters.AddWithValue("@Amount", LedgerAmount);
                            Insertclientcmd.Parameters.AddWithValue("@MatchBets", MatchBetcount);
                            Insertclientcmd.Parameters.AddWithValue("@SessionBets", SessionBetcount);
                            Insertclientcmd.ExecuteNonQuery();
                            LedgerAmount = 0;
                        }


                        //Calculation  For Agent Ledger
                        int AgentID = 0;
                        decimal TotalFinalmatchcalculation = 0;
                        decimal TotalFinalagentcommision = 0;
                        decimal TotalFinalNetAmount = 0;
                        decimal TotalFinalHalfAmount = 0;
                        decimal TotalFinalMobileAppAmount = 0;
                        decimal TotalFinalAmount = 0;

                        string checkAgentcode = "select clientmaster.clientID,clientmaster.Name,clientmaster.createdBy from clientmaster inner join viewmatch on clientmaster.ClientID = viewmatch.ClientID  where MatchID = '" + apiID + "'  group by CreatedBy";
                        MySqlCommand checkAgentcodecmd = new MySqlCommand(checkAgentcode, cn);
                        MySqlDataAdapter checkAgentcodeadp = new MySqlDataAdapter(checkAgentcodecmd);
                        DataTable checkAgentcodedt = new DataTable();
                        checkAgentcodeadp.Fill(checkAgentcodedt);

                        for (int a1 = 0; a1 < checkAgentcodedt.Rows.Count; a1++)
                        {
                            string AgentCode = checkAgentcodedt.Rows[a1]["createdBy"].ToString();

                            string matchviewsclient = "select clientmaster.clientID,clientmaster.Name from clientmaster inner join viewmatch on clientmaster.ClientID = viewmatch.ClientID  where MatchID = '" + apiID + "' && clientmaster.createdBy = '" + AgentCode + "' group by Name";
                            MySqlCommand matchviewsclientcmd = new MySqlCommand(matchviewsclient, cn);
                            MySqlDataAdapter matchviewsclientadp = new MySqlDataAdapter(matchviewsclientcmd);
                            DataTable matchviewsclientdt = new DataTable();
                            matchviewsclientadp.Fill(matchviewsclientdt);

                            for (int i = 0; i < matchviewsclientdt.Rows.Count; i++)
                            {
                                string Name = matchviewsclientdt.Rows[i]["Name"].ToString();
                                int ID = Convert.ToInt16(matchviewsclientdt.Rows[i]["ClientID"]);
                                string CodeName = ID + Name;


                                decimal Totalmatchcalculation = Totalclientmatchcalculation(ID, apiID);
                                TotalFinalmatchcalculation = TotalFinalmatchcalculation + Totalmatchcalculation;

                                decimal Totalagentcommision1 = Totalagentcommision(ID, apiID);
                                TotalFinalagentcommision = TotalFinalagentcommision + Totalagentcommision1;

                                decimal totalcommision = Totalagentcommision1;
                                double totalcommisionvalue = double.Parse(totalcommision.ToString());


                                Decimal Match1 = Totalmatchcalculation;
                                Decimal Commision = totalcommision;
                                Decimal To = 0;

                                if (Match1 > 0)
                                {
                                    To = Match1 - Commision;
                                }
                                else if (Match1 < 0)
                                {
                                    To = Match1 - Commision;
                                }
                                else
                                {
                                    To = Commision * -1;
                                }

                                Decimal TotalNetAmount = To;
                                TotalFinalNetAmount = TotalFinalNetAmount + TotalNetAmount;


                                decimal clientshare1 = 0;
                                string s55 = "Select * from sharetable where MatchID = '" + apiID + "' And ClientID = '" + ID + "'";
                                MySqlCommand cmd55 = new MySqlCommand(s55, cn);
                                MySqlDataAdapter adp55 = new MySqlDataAdapter(cmd55);
                                DataTable dt55 = new DataTable();
                                adp55.Fill(dt55);
                                if (dt55.Rows.Count > 0)
                                {
                                    decimal clientshare = Convert.ToDecimal(dt55.Rows[0]["ClientShare"]);
                                    clientshare1 = clientshare / 100;

                                }
                                Decimal TotalHalfAmount = TotalNetAmount * clientshare1;
                                TotalFinalHalfAmount = TotalFinalHalfAmount + TotalHalfAmount;


                                string Agentdetails = "Select * From AgentMaster where  code = '" + AgentCode + "'";
                                MySqlCommand Agentdetailscmd = new MySqlCommand(Agentdetails, cn);
                                MySqlDataAdapter Agentdetailsadp = new MySqlDataAdapter(Agentdetailscmd);
                                DataTable Agentdetailsdt = new DataTable();
                                Agentdetailsadp.Fill(Agentdetailsdt);
                                Decimal MobileAppAmount = 0;
                                String MobileApp = Agentdetailsdt.Rows[0]["Mobapp"].ToString();
                                AgentID = Convert.ToInt16(Agentdetailsdt.Rows[0]["AgentID"]);

                                MobileAppAmount = Convert.ToDecimal(MobileApp);
                                TotalFinalMobileAppAmount = TotalFinalMobileAppAmount + MobileAppAmount;
                                decimal FinalAmount = (TotalNetAmount - TotalHalfAmount) + MobileAppAmount;
                                TotalFinalAmount = TotalFinalAmount + FinalAmount;
                            }


                            decimal Dabit = 0, Credit = 0;
                            string Remark = "";

                            int Total = (int)Math.Round(TotalFinalAmount, 0);
                            if (Total < 0)
                            {
                                Dabit = Total * -1;
                                Remark = "Agent Plus";

                            }
                            else if (Total > 0)
                            {
                                Credit = Total;
                                Remark = "Agent Minus";
                            }


                            string s8 = "Insert Into Agentledger (AgentID,MatchID,Amount,Dabit,Credit,Remark) Values(@AgentID,@MatchID,@Amount,@Dabit,@Credit,@Remark)";
                            MySqlCommand cmd8 = new MySqlCommand(s8, cn);
                            cmd8.Parameters.AddWithValue("@AgentID", AgentID);
                            cmd8.Parameters.AddWithValue("@MatchID", apiID);
                            cmd8.Parameters.AddWithValue("@Amount", Total);
                            cmd8.Parameters.AddWithValue("@Dabit", Dabit);
                            cmd8.Parameters.AddWithValue("@Credit", Credit);
                            cmd8.Parameters.AddWithValue("@Remark", Remark);
                            cmd8.ExecuteNonQuery();



                            TotalFinalmatchcalculation = 0;
                            TotalFinalagentcommision = 0;
                            TotalFinalNetAmount = 0;
                            TotalFinalHalfAmount = 0;
                            TotalFinalMobileAppAmount = 0;
                            TotalFinalAmount = 0;



                            string s10 = "Select CurrentLimit from Agentmaster where AgentID = '" + AgentID + "'";
                            MySqlCommand cmd10 = new MySqlCommand(s10, cn);
                            MySqlDataAdapter adp10 = new MySqlDataAdapter(cmd10);
                            DataTable dt10 = new DataTable();
                            adp10.Fill(dt10);

                            decimal Currentlimit = Convert.ToDecimal(dt10.Rows[0]["CurrentLimit"]);
                            decimal finalcurrentlimit = Currentlimit - Convert.ToDecimal(Total);


                            string s11 = "update AgentMaster set CurrentLimit = '" + finalcurrentlimit + "' where AgentID = '" + AgentID + "'";
                            MySqlCommand cmd11 = new MySqlCommand(s11, cn);
                            cmd11.ExecuteNonQuery();
                        }

                        //Calculation For SuperAgentLedger

                        int SuperAgentID = 0;

                        string checkSuperAgentcode = "select clientmaster.clientID,clientmaster.Name,clientmaster.createdBy,AgentMaster.CreatedBy from clientmaster inner join viewmatch on clientmaster.ClientID = viewmatch.ClientID inner join AgentMaster on Clientmaster.CreatedBy = AgentMaster.code where MatchID = '" + apiID + "'  group by agentmaster.CreatedBY";
                        MySqlCommand checkSuperAgentcodecmd = new MySqlCommand(checkSuperAgentcode, cn);
                        MySqlDataAdapter checkSuperAgentcodeadp = new MySqlDataAdapter(checkSuperAgentcodecmd);
                        DataTable checkSuperAgentcodedt = new DataTable();
                        checkSuperAgentcodeadp.Fill(checkSuperAgentcodedt);

                        for (int a0 = 0; a0 < checkSuperAgentcodedt.Rows.Count; a0++)
                        {
                            string Superagentcode = checkSuperAgentcodedt.Rows[a0]["createdBy1"].ToString();


                            string checkAgentcode1 = "select clientmaster.clientID,clientmaster.Name,clientmaster.createdBy,AgentMaster.CreatedBy from clientmaster inner join viewmatch on clientmaster.ClientID = viewmatch.ClientID inner join AgentMaster on Clientmaster.CreatedBy = AgentMaster.code where MatchID = '" + apiID + "' && AgentMaster.CreatedBy = '" + Superagentcode + "'  group by clientmaster.CreatedBY";
                            MySqlCommand checkAgentcodecmd1 = new MySqlCommand(checkAgentcode1, cn);
                            MySqlDataAdapter checkAgentcodeadp1 = new MySqlDataAdapter(checkAgentcodecmd1);
                            DataTable checkAgentcodedt1 = new DataTable();
                            checkAgentcodeadp1.Fill(checkAgentcodedt1);

                            for (int a1 = 0; a1 < checkAgentcodedt1.Rows.Count; a1++)
                            {
                                string AgentCode = checkAgentcodedt1.Rows[a1]["createdBy"].ToString();

                                string matchviewsclient = "select clientmaster.clientID,clientmaster.Name from clientmaster inner join viewmatch on clientmaster.ClientID = viewmatch.ClientID  where MatchID = '" + apiID + "' && clientmaster.createdBy = '" + AgentCode + "' group by Name";
                                MySqlCommand matchviewsclientcmd = new MySqlCommand(matchviewsclient, cn);
                                MySqlDataAdapter matchviewsclientadp = new MySqlDataAdapter(matchviewsclientcmd);
                                DataTable matchviewsclientdt = new DataTable();
                                matchviewsclientadp.Fill(matchviewsclientdt);

                                for (int i = 0; i < matchviewsclientdt.Rows.Count; i++)
                                {
                                    string Name = matchviewsclientdt.Rows[i]["Name"].ToString();
                                    int ID = Convert.ToInt16(matchviewsclientdt.Rows[i]["ClientID"]);
                                    string CodeName = ID + Name;


                                    decimal Totalmatchcalculation = Totalclientmatchcalculation(ID, apiID);
                                    TotalFinalmatchcalculation = TotalFinalmatchcalculation + Totalmatchcalculation;

                                    decimal Totalsuperagentcommision1 = Totalagentcommision(ID, apiID);
                                    TotalFinalagentcommision = TotalFinalagentcommision + Totalsuperagentcommision1;

                                    decimal totalcommision = Totalsuperagentcommision1;
                                    double totalcommisionvalue = double.Parse(totalcommision.ToString());


                                    Decimal Match1 = Totalmatchcalculation;
                                    Decimal Commision = totalcommision;
                                    Decimal To = 0;

                                    if (Match1 > 0)
                                    {

                                        To = Match1 - Commision;

                                    }
                                    else if (Match1 < 0)
                                    {

                                        To = Match1 - Commision;

                                    }
                                    else
                                    {
                                        To = Commision * -1;
                                    }

                                    Decimal TotalNetAmount = To;
                                    TotalFinalNetAmount = TotalFinalNetAmount + TotalNetAmount;


                                    decimal SuperAgentShare2 = 0;
                                    string s55 = "Select * from sharetable where MatchID = '" + apiID + "' And ClientID = '" + ID + "'";
                                    MySqlCommand cmd55 = new MySqlCommand(s55, cn);
                                    MySqlDataAdapter adp55 = new MySqlDataAdapter(cmd55);
                                    DataTable dt55 = new DataTable();
                                    adp55.Fill(dt55);
                                    if (dt55.Rows.Count > 0)
                                    {
                                        decimal SuperAgentShare1 = Convert.ToDecimal(dt55.Rows[0]["SAgentshare"]);
                                        SuperAgentShare2 = SuperAgentShare1 / 100;
                                    }
                                    Decimal TotalSAShareAmount = TotalNetAmount * SuperAgentShare2;
                                    Decimal TotalSAShareAmount1 = TotalNetAmount - TotalSAShareAmount;
                                    TotalFinalHalfAmount = TotalFinalHalfAmount + TotalSAShareAmount1;




                                    string Agentdetails = "Select * From AgentMaster where  code = '" + AgentCode + "'";
                                    MySqlCommand Agentdetailscmd = new MySqlCommand(Agentdetails, cn);
                                    MySqlDataAdapter Agentdetailsadp = new MySqlDataAdapter(Agentdetailscmd);
                                    DataTable Agentdetailsdt = new DataTable();
                                    Agentdetailsadp.Fill(Agentdetailsdt);
                                    String SuperAgentCode = Agentdetailsdt.Rows[0]["CreatedBy"].ToString();
                                    AgentID = Convert.ToInt16(Agentdetailsdt.Rows[0]["AgentID"]);

                                    string SuperAgentdetails = "Select * From SuperagentMaster where  code = '" + Superagentcode + "'";
                                    MySqlCommand Superagentdetailscmd = new MySqlCommand(SuperAgentdetails, cn);
                                    MySqlDataAdapter Superagentdetailsadp = new MySqlDataAdapter(Superagentdetailscmd);
                                    DataTable Superagentdetailsdt = new DataTable();
                                    Superagentdetailsadp.Fill(Superagentdetailsdt);
                                    Decimal MobileAppAmount = 0;
                                    String MobileApp = Superagentdetailsdt.Rows[0]["MymobAmount"].ToString();
                                    SuperAgentID = Convert.ToInt16(Superagentdetailsdt.Rows[0]["SuperAgentID"]);

                                    MobileAppAmount = Convert.ToDecimal(MobileApp);
                                    TotalFinalMobileAppAmount = TotalFinalMobileAppAmount + MobileAppAmount;
                                    decimal FinalAmount = TotalSAShareAmount1 + MobileAppAmount;
                                    TotalFinalAmount = TotalFinalAmount + FinalAmount;
                                }
                            }

                            decimal Dabit = 0, Credit = 0;
                            string Remark = "";

                            int Total = (int)Math.Round(TotalFinalAmount, 0);
                            if (Total < 0)
                            {
                                Dabit = Total * -1;
                                Remark = "SuperAgent Plus";

                            }
                            else if (Total > 0)
                            {
                                Credit = Total;
                                Remark = "SuperAgent Minus";
                            }


                            string s8 = "Insert Into SuperAgentledger (SuperAgentID,MatchID,Amount,Dabit,Credit,Remark) Values(@SuperAgentID,@MatchID,@Amount,@Dabit,@Credit,@Remark)";
                            MySqlCommand cmd8 = new MySqlCommand(s8, cn);
                            cmd8.Parameters.AddWithValue("@SuperAgentID", SuperAgentID);
                            cmd8.Parameters.AddWithValue("@MatchID", apiID);
                            cmd8.Parameters.AddWithValue("@Amount", Total);
                            cmd8.Parameters.AddWithValue("@Dabit", Dabit);
                            cmd8.Parameters.AddWithValue("@Credit", Credit);
                            cmd8.Parameters.AddWithValue("@Remark", Remark);

                            cmd8.ExecuteNonQuery();



                            TotalFinalmatchcalculation = 0;
                            TotalFinalagentcommision = 0;
                            TotalFinalNetAmount = 0;
                            TotalFinalHalfAmount = 0;
                            TotalFinalMobileAppAmount = 0;
                            TotalFinalAmount = 0;



                            string s10 = "Select CurrentLimit from SuperAgentmaster where SuperAgentID = '" + SuperAgentID + "'";
                            MySqlCommand cmd10 = new MySqlCommand(s10, cn);
                            MySqlDataAdapter adp10 = new MySqlDataAdapter(cmd10);
                            DataTable dt10 = new DataTable();
                            adp10.Fill(dt10);

                            decimal Currentlimit = Convert.ToDecimal(dt10.Rows[0]["CurrentLimit"]);
                            decimal finalcurrentlimit = Currentlimit - Convert.ToDecimal(Total);


                            string s11 = "update SuperAgentMaster set CurrentLimit = '" + finalcurrentlimit + "' where SuperAgentID = '" + SuperAgentID + "'";
                            MySqlCommand cmd11 = new MySqlCommand(s11, cn);
                            cmd11.ExecuteNonQuery();

                        }

                        string declareteam = "";
                        if (team_selector1.Value == "team_1")
                        {
                            declareteam = team1;
                        }
                        else if (team_selector1.Value == "team_2")
                        {
                            declareteam = team2;
                        }

                        string SELECT = "Update Matches Set Status = '11', Declear = '1', winnerTeam ='" + declareteam + "'  Where apiID = '" + apiID + "'";
                        MySqlCommand cmd = new MySqlCommand(SELECT, cn);
                        cmd.ExecuteNonQuery();

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Match Declared Succesfully.....');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('First Declare " + session1 + " session of this match');", true);
                    }
                }

                else if (Declear == "True")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Match Already Declared.....');", true);

                }
            }
        }
        protected void btntie_Click(object sender, EventArgs e)
        {
            int MatchID = Convert.ToInt32(Request.QueryString["Matchid"]);
            tieabandonedmatch(MatchID, "Tie");
        }

        protected void btnabandoned_Click(object sender, EventArgs e)
        {
            int MatchID = Convert.ToInt32(Request.QueryString["Matchid"]);
            tieabandonedmatch(MatchID, "Abandoned");
        }

        public void tieabandonedmatch(int matchid, string type)
        {
            int apiID = Convert.ToInt32(Request.QueryString["Matchid"]);

            decimal LedgerAmount = 0;
            string team1, team2;
            string CN = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            using (MySqlConnection cn = new MySqlConnection(CN))
            {
                cn.Open();
                string Match = "Select * From Matches where apiID = '" + apiID + "'";
                MySqlCommand Matchcmd = new MySqlCommand(Match, cn);
                MySqlDataAdapter Matchadp = new MySqlDataAdapter(Matchcmd);
                DataTable Matchdt = new DataTable();
                Matchadp.Fill(Matchdt);
                string Declear = Matchdt.Rows[0]["Declear"].ToString();
                team1 = Matchdt.Rows[0]["TeamA"].ToString();
                team2 = Matchdt.Rows[0]["TeamB"].ToString();
                string session1 = "";
                if (Declear == "False")
                {
                    string session = "Select Session,ClientID From Session where MatchID ='" + apiID + "'  group by Session";
                    MySqlCommand sessioncmd = new MySqlCommand(session, cn);
                    MySqlDataAdapter sessionadp = new MySqlDataAdapter(sessioncmd);
                    DataTable sessiondt = new DataTable();
                    sessionadp.Fill(sessiondt);
                    Boolean check = false;
                    for (int s = 0; s < sessiondt.Rows.Count; s++)
                    {
                        session1 = sessiondt.Rows[s]["Session"].ToString();

                        string declaredsession = "Select Session From DeclaredSession where '" + apiID + "' and Session = '" + session1 + "'";
                        MySqlCommand declaredsessioncmd = new MySqlCommand(declaredsession, cn);
                        MySqlDataReader rdr = declaredsessioncmd.ExecuteReader();
                        if (!rdr.Read())
                        {
                            DataTable dt = new DataTable();
                            dt.Load(rdr);

                            check = true;
                            break;
                        }
                        rdr.Close();
                    }

                    if (check == false)
                    {
                        string checkclient = "Select ClientID  From ViewMatch  where MatchID = '" + apiID + "'";
                        MySqlCommand checkclientcmd = new MySqlCommand(checkclient, cn);
                        MySqlDataAdapter checkclientadp = new MySqlDataAdapter(checkclientcmd);
                        DataTable checkclientdt = new DataTable();
                        checkclientadp.Fill(checkclientdt);

                        for (int i = 0; i < checkclientdt.Rows.Count; i++)
                        {
                            int ClientID = Convert.ToInt16(checkclientdt.Rows[i]["ClientID"]);


                            int Position1 = 0, Position2 = 0;
                            string Position3 = "";

                            string Runner = "Select Position1,Position2,Position3 from Runner where ClientID = '" + ClientID + "' && MatchID = '" + apiID + "' order by DateTime DESC";
                            MySqlCommand Runnercmd = new MySqlCommand(Runner, cn);
                            MySqlDataAdapter Runneradp = new MySqlDataAdapter(Runnercmd);
                            DataTable Runnerdt = new DataTable();
                            Runneradp.Fill(Runnerdt);
                            
                            if (Runnerdt.Rows.Count > 0)
                            {
                                Position1 = Convert.ToInt32(Runnerdt.Rows[0]["Position1"]);
                                Position2 = Convert.ToInt32(Runnerdt.Rows[0]["Position2"]);
                                Position3 = (Runnerdt.Rows[0]["Position3"]).ToString();
                            }





                            string Selectcurrentlimit = "Select * from ClientMaster where ClientID = '" + ClientID + "'";
                            MySqlCommand Selectcurrentlimitcmd = new MySqlCommand(Selectcurrentlimit, cn);
                            MySqlDataAdapter Selectcurrentlimitadp = new MySqlDataAdapter(Selectcurrentlimitcmd);
                            DataTable Selectcurrentlimitdt = new DataTable();
                            Selectcurrentlimitadp.Fill(Selectcurrentlimitdt);

                            decimal MobileAppAmount = Convert.ToDecimal(Selectcurrentlimitdt.Rows[0]["MobileAppAmount"]);

                            string sessionamt = "Select * From sessioncalculation where clientID = '" + ClientID + "' and MatchID = '" + apiID + "'";
                            MySqlCommand sessionamtcmd = new MySqlCommand(sessionamt, cn);
                            MySqlDataAdapter sessionamtadp = new MySqlDataAdapter(sessionamtcmd);
                            DataTable sessionamtdt = new DataTable();
                            sessionamtadp.Fill(sessionamtdt);
                            decimal totalsessionamt = 0;
                            if (sessionamtdt.Rows.Count > 0)
                            {
                                for (int j = 0; j < sessionamtdt.Rows.Count; j++)
                                {
                                    decimal amt = Convert.ToDecimal(sessionamtdt.Rows[j]["TotalAmount"]);
                                    totalsessionamt = totalsessionamt + amt;
                                }
                            }
                            if (LedgerAmount < 0)
                            {
                                LedgerAmount =  (totalsessionamt) - MobileAppAmount;
                            }
                            else
                            {
                                LedgerAmount = (totalsessionamt) - MobileAppAmount;
                            }

                            decimal Dabit = 0, Credit = 0;
                            if (LedgerAmount > 0)
                            {
                                Dabit = LedgerAmount;
                            }
                            else if (LedgerAmount < 0)
                            {
                                Credit = LedgerAmount * -1;
                            }

                            string insertpositions = "Insert into positions (MatchID,ClientID,Position1,position2,Position3) values (@MatchID,@ClientID,@Position1,@position2,@Position3)";
                            MySqlCommand insertpositionscmd = new MySqlCommand(insertpositions, cn);
                            insertpositionscmd.Parameters.AddWithValue("@MatchID", apiID);
                            insertpositionscmd.Parameters.AddWithValue("@ClientID", ClientID);
                            insertpositionscmd.Parameters.AddWithValue("@Position1", Position1);
                            insertpositionscmd.Parameters.AddWithValue("@Position2", Position2);
                            insertpositionscmd.Parameters.AddWithValue("@Position3", Position3);
                            insertpositionscmd.ExecuteNonQuery();



                            string clearusedlimit = "update runner set Position1 = '0',Position2 = '0' where ClientID = '" + ClientID + "' && MatchID = '" + apiID + "' order by DateTime DESC ";
                            MySqlCommand clearusedlimitcmd = new MySqlCommand(clearusedlimit, cn);
                            clearusedlimitcmd.ExecuteNonQuery();


                            if (LedgerAmount < 0)
                            {

                                decimal currentlimit = Convert.ToDecimal(Selectcurrentlimitdt.Rows[0]["CurrentLimit"]);

                                decimal finaldeductedamount = currentlimit + LedgerAmount;

                                string updateamount = "update clientmaster set Currentlimit = '" + finaldeductedamount + "',client_limit= '" + finaldeductedamount + "' where ClientID = '" + ClientID + "' ";
                                MySqlCommand updateamountcmd = new MySqlCommand(updateamount, cn);
                                updateamountcmd.ExecuteNonQuery();
                            }


                            string MatchBet = "select count(clientID) From runner where MatchID = '" + apiID + "' && ClientID = '" + ClientID + "' ";
                            MySqlCommand MatchBetcmd = new MySqlCommand(MatchBet, cn);
                            string MatchBetcount = MatchBetcmd.ExecuteScalar().ToString();

                            string SessionBet = "select count(clientID) From session where MatchID = '" + apiID + "' && ClientID = '" + ClientID + "' ";
                            MySqlCommand SessionBetcmd = new MySqlCommand(SessionBet, cn);
                            string SessionBetcount = SessionBetcmd.ExecuteScalar().ToString();



                            string InsertClientLedger = "Insert Into ClientLedger (clientID,MatchID,Dabit,Credit,Amount,MatchBets,SessionBets) values (@ClientID,@MatchID,@Dabit,@Credit,@Amount,@MatchBets,@SessionBets)";
                            MySqlCommand Insertclientcmd = new MySqlCommand(InsertClientLedger, cn);
                            Insertclientcmd.Parameters.AddWithValue("@ClientID", ClientID);
                            Insertclientcmd.Parameters.AddWithValue("@MatchID", apiID);
                            Insertclientcmd.Parameters.AddWithValue("@Dabit", Dabit);
                            Insertclientcmd.Parameters.AddWithValue("@Credit", Credit);
                            Insertclientcmd.Parameters.AddWithValue("@Amount", LedgerAmount);
                            Insertclientcmd.Parameters.AddWithValue("@MatchBets", MatchBetcount);
                            Insertclientcmd.Parameters.AddWithValue("@SessionBets", SessionBetcount);
                            Insertclientcmd.ExecuteNonQuery();
                            LedgerAmount = 0;
                        }


                        //Calculation  For Agent Ledger
                        int AgentID = 0;
                        decimal TotalFinalmatchcalculation = 0;
                        decimal TotalFinalagentcommision = 0;
                        decimal TotalFinalNetAmount = 0;
                        decimal TotalFinalHalfAmount = 0;
                        decimal TotalFinalMobileAppAmount = 0;
                        decimal TotalFinalAmount = 0;

                        string checkAgentcode = "select clientmaster.clientID,clientmaster.Name,clientmaster.createdBy from clientmaster inner join viewmatch on clientmaster.ClientID = viewmatch.ClientID  where MatchID = '" + apiID + "'  group by CreatedBy";
                        MySqlCommand checkAgentcodecmd = new MySqlCommand(checkAgentcode, cn);
                        MySqlDataAdapter checkAgentcodeadp = new MySqlDataAdapter(checkAgentcodecmd);
                        DataTable checkAgentcodedt = new DataTable();
                        checkAgentcodeadp.Fill(checkAgentcodedt);

                        for (int a1 = 0; a1 < checkAgentcodedt.Rows.Count; a1++)
                        {
                            string AgentCode = checkAgentcodedt.Rows[a1]["createdBy"].ToString();

                            string matchviewsclient = "select clientmaster.clientID,clientmaster.Name from clientmaster inner join viewmatch on clientmaster.ClientID = viewmatch.ClientID  where MatchID = '" + apiID + "' && clientmaster.createdBy = '" + AgentCode + "' group by Name";
                            MySqlCommand matchviewsclientcmd = new MySqlCommand(matchviewsclient, cn);
                            MySqlDataAdapter matchviewsclientadp = new MySqlDataAdapter(matchviewsclientcmd);
                            DataTable matchviewsclientdt = new DataTable();
                            matchviewsclientadp.Fill(matchviewsclientdt);

                            for (int i = 0; i < matchviewsclientdt.Rows.Count; i++)
                            {
                                string Name = matchviewsclientdt.Rows[i]["Name"].ToString();
                                int ID = Convert.ToInt16(matchviewsclientdt.Rows[i]["ClientID"]);
                                string CodeName = ID + Name;


                                decimal Totalmatchcalculation = Totalclientmatchcalculation(ID, apiID);
                                TotalFinalmatchcalculation = TotalFinalmatchcalculation + Totalmatchcalculation;

                                decimal Totalagentcommision1 = Totalagentcommision(ID, apiID);
                                TotalFinalagentcommision = TotalFinalagentcommision + Totalagentcommision1;

                                decimal totalcommision = Totalagentcommision1;
                                double totalcommisionvalue = double.Parse(totalcommision.ToString());


                                Decimal Match1 = Totalmatchcalculation;
                                Decimal Commision = totalcommision;
                                Decimal To = 0;

                                if (Match1 > 0)
                                {
                                    To = Match1 - Commision;
                                }
                                else if (Match1 < 0)
                                {
                                    To = Match1 - Commision;
                                }
                                else
                                {
                                    To = Commision * -1;
                                }

                                Decimal TotalNetAmount = To;
                                TotalFinalNetAmount = TotalFinalNetAmount + TotalNetAmount;


                                decimal clientshare1 = 0;
                                string s55 = "Select * from sharetable where MatchID = '" + apiID + "' And ClientID = '" + ID + "'";
                                MySqlCommand cmd55 = new MySqlCommand(s55, cn);
                                MySqlDataAdapter adp55 = new MySqlDataAdapter(cmd55);
                                DataTable dt55 = new DataTable();
                                adp55.Fill(dt55);
                                if (dt55.Rows.Count > 0)
                                {
                                    decimal clientshare = Convert.ToDecimal(dt55.Rows[0]["ClientShare"]);
                                    clientshare1 = clientshare / 100;

                                }
                                Decimal TotalHalfAmount = TotalNetAmount * clientshare1;
                                TotalFinalHalfAmount = TotalFinalHalfAmount + TotalHalfAmount;


                                string Agentdetails = "Select * From AgentMaster where  code = '" + AgentCode + "'";
                                MySqlCommand Agentdetailscmd = new MySqlCommand(Agentdetails, cn);
                                MySqlDataAdapter Agentdetailsadp = new MySqlDataAdapter(Agentdetailscmd);
                                DataTable Agentdetailsdt = new DataTable();
                                Agentdetailsadp.Fill(Agentdetailsdt);
                                Decimal MobileAppAmount = 0;
                                String MobileApp = Agentdetailsdt.Rows[0]["Mobapp"].ToString();
                                AgentID = Convert.ToInt16(Agentdetailsdt.Rows[0]["AgentID"]);

                                MobileAppAmount = Convert.ToDecimal(MobileApp);
                                TotalFinalMobileAppAmount = TotalFinalMobileAppAmount + MobileAppAmount;
                                decimal FinalAmount = (TotalNetAmount - TotalHalfAmount) + MobileAppAmount;
                                TotalFinalAmount = TotalFinalAmount + FinalAmount;
                            }


                            decimal Dabit = 0, Credit = 0;
                            string Remark = "";

                            int Total = (int)Math.Round(TotalFinalAmount, 0);
                            if (Total < 0)
                            {
                                Dabit = Total * -1;
                                Remark = "Agent Plus";

                            }
                            else if (Total > 0)
                            {
                                Credit = Total;
                                Remark = "Agent Minus";
                            }


                            string s8 = "Insert Into Agentledger (AgentID,MatchID,Amount,Dabit,Credit,Remark) Values(@AgentID,@MatchID,@Amount,@Dabit,@Credit,@Remark)";
                            MySqlCommand cmd8 = new MySqlCommand(s8, cn);
                            cmd8.Parameters.AddWithValue("@AgentID", AgentID);
                            cmd8.Parameters.AddWithValue("@MatchID", apiID);
                            cmd8.Parameters.AddWithValue("@Amount", Total);
                            cmd8.Parameters.AddWithValue("@Dabit", Dabit);
                            cmd8.Parameters.AddWithValue("@Credit", Credit);
                            cmd8.Parameters.AddWithValue("@Remark", Remark);
                            cmd8.ExecuteNonQuery();



                            TotalFinalmatchcalculation = 0;
                            TotalFinalagentcommision = 0;
                            TotalFinalNetAmount = 0;
                            TotalFinalHalfAmount = 0;
                            TotalFinalMobileAppAmount = 0;
                            TotalFinalAmount = 0;



                            string s10 = "Select CurrentLimit from Agentmaster where AgentID = '" + AgentID + "'";
                            MySqlCommand cmd10 = new MySqlCommand(s10, cn);
                            MySqlDataAdapter adp10 = new MySqlDataAdapter(cmd10);
                            DataTable dt10 = new DataTable();
                            adp10.Fill(dt10);

                            decimal Currentlimit = Convert.ToDecimal(dt10.Rows[0]["CurrentLimit"]);
                            decimal finalcurrentlimit = Currentlimit - Convert.ToDecimal(Total);


                            string s11 = "update AgentMaster set CurrentLimit = '" + finalcurrentlimit + "' where AgentID = '" + AgentID + "'";
                            MySqlCommand cmd11 = new MySqlCommand(s11, cn);
                            cmd11.ExecuteNonQuery();
                        }

                        //Calculation For SuperAgentLedger

                        int SuperAgentID = 0;

                        string checkSuperAgentcode = "select clientmaster.clientID,clientmaster.Name,clientmaster.createdBy,AgentMaster.CreatedBy from clientmaster inner join viewmatch on clientmaster.ClientID = viewmatch.ClientID inner join AgentMaster on Clientmaster.CreatedBy = AgentMaster.code where MatchID = '" + apiID + "'  group by agentmaster.CreatedBY";
                        MySqlCommand checkSuperAgentcodecmd = new MySqlCommand(checkSuperAgentcode, cn);
                        MySqlDataAdapter checkSuperAgentcodeadp = new MySqlDataAdapter(checkSuperAgentcodecmd);
                        DataTable checkSuperAgentcodedt = new DataTable();
                        checkSuperAgentcodeadp.Fill(checkSuperAgentcodedt);

                        for (int a0 = 0; a0 < checkSuperAgentcodedt.Rows.Count; a0++)
                        {
                            string Superagentcode = checkSuperAgentcodedt.Rows[a0]["createdBy1"].ToString();


                            string checkAgentcode1 = "select clientmaster.clientID,clientmaster.Name,clientmaster.createdBy,AgentMaster.CreatedBy from clientmaster inner join viewmatch on clientmaster.ClientID = viewmatch.ClientID inner join AgentMaster on Clientmaster.CreatedBy = AgentMaster.code where MatchID = '" + apiID + "' && AgentMaster.CreatedBy = '" + Superagentcode + "'  group by clientmaster.CreatedBY";
                            MySqlCommand checkAgentcodecmd1 = new MySqlCommand(checkAgentcode1, cn);
                            MySqlDataAdapter checkAgentcodeadp1 = new MySqlDataAdapter(checkAgentcodecmd1);
                            DataTable checkAgentcodedt1 = new DataTable();
                            checkAgentcodeadp1.Fill(checkAgentcodedt1);

                            for (int a1 = 0; a1 < checkAgentcodedt1.Rows.Count; a1++)
                            {
                                string AgentCode = checkAgentcodedt1.Rows[a1]["createdBy"].ToString();

                                string matchviewsclient = "select clientmaster.clientID,clientmaster.Name from clientmaster inner join viewmatch on clientmaster.ClientID = viewmatch.ClientID  where MatchID = '" + apiID + "' && clientmaster.createdBy = '" + AgentCode + "' group by Name";
                                MySqlCommand matchviewsclientcmd = new MySqlCommand(matchviewsclient, cn);
                                MySqlDataAdapter matchviewsclientadp = new MySqlDataAdapter(matchviewsclientcmd);
                                DataTable matchviewsclientdt = new DataTable();
                                matchviewsclientadp.Fill(matchviewsclientdt);

                                for (int i = 0; i < matchviewsclientdt.Rows.Count; i++)
                                {
                                    string Name = matchviewsclientdt.Rows[i]["Name"].ToString();
                                    int ID = Convert.ToInt16(matchviewsclientdt.Rows[i]["ClientID"]);
                                    string CodeName = ID + Name;


                                    decimal Totalmatchcalculation = Totalclientmatchcalculation(ID, apiID);
                                    TotalFinalmatchcalculation = TotalFinalmatchcalculation + Totalmatchcalculation;

                                    decimal Totalsuperagentcommision1 = Totalagentcommision(ID, apiID);
                                    TotalFinalagentcommision = TotalFinalagentcommision + Totalsuperagentcommision1;

                                    decimal totalcommision = Totalsuperagentcommision1;
                                    double totalcommisionvalue = double.Parse(totalcommision.ToString());


                                    Decimal Match1 = Totalmatchcalculation;
                                    Decimal Commision = totalcommision;
                                    Decimal To = 0;

                                    if (Match1 > 0)
                                    {

                                        To = Match1 - Commision;

                                    }
                                    else if (Match1 < 0)
                                    {

                                        To = Match1 - Commision;

                                    }
                                    else
                                    {
                                        To = Commision * -1;
                                    }

                                    Decimal TotalNetAmount = To;
                                    TotalFinalNetAmount = TotalFinalNetAmount + TotalNetAmount;


                                    decimal SuperAgentShare2 = 0;
                                    string s55 = "Select * from sharetable where MatchID = '" + apiID + "' And ClientID = '" + ID + "'";
                                    MySqlCommand cmd55 = new MySqlCommand(s55, cn);
                                    MySqlDataAdapter adp55 = new MySqlDataAdapter(cmd55);
                                    DataTable dt55 = new DataTable();
                                    adp55.Fill(dt55);
                                    if (dt55.Rows.Count > 0)
                                    {
                                        decimal SuperAgentShare1 = Convert.ToDecimal(dt55.Rows[0]["SAgentshare"]);
                                        SuperAgentShare2 = SuperAgentShare1 / 100;
                                    }
                                    Decimal TotalSAShareAmount = TotalNetAmount * SuperAgentShare2;
                                    Decimal TotalSAShareAmount1 = TotalNetAmount - TotalSAShareAmount;
                                    TotalFinalHalfAmount = TotalFinalHalfAmount + TotalSAShareAmount1;




                                    string Agentdetails = "Select * From AgentMaster where  code = '" + AgentCode + "'";
                                    MySqlCommand Agentdetailscmd = new MySqlCommand(Agentdetails, cn);
                                    MySqlDataAdapter Agentdetailsadp = new MySqlDataAdapter(Agentdetailscmd);
                                    DataTable Agentdetailsdt = new DataTable();
                                    Agentdetailsadp.Fill(Agentdetailsdt);
                                    String SuperAgentCode = Agentdetailsdt.Rows[0]["CreatedBy"].ToString();
                                    AgentID = Convert.ToInt16(Agentdetailsdt.Rows[0]["AgentID"]);

                                    string SuperAgentdetails = "Select * From SuperagentMaster where  code = '" + Superagentcode + "'";
                                    MySqlCommand Superagentdetailscmd = new MySqlCommand(SuperAgentdetails, cn);
                                    MySqlDataAdapter Superagentdetailsadp = new MySqlDataAdapter(Superagentdetailscmd);
                                    DataTable Superagentdetailsdt = new DataTable();
                                    Superagentdetailsadp.Fill(Superagentdetailsdt);
                                    Decimal MobileAppAmount = 0;
                                    String MobileApp = Superagentdetailsdt.Rows[0]["MymobAmount"].ToString();
                                    SuperAgentID = Convert.ToInt16(Superagentdetailsdt.Rows[0]["SuperAgentID"]);

                                    MobileAppAmount = Convert.ToDecimal(MobileApp);
                                    TotalFinalMobileAppAmount = TotalFinalMobileAppAmount + MobileAppAmount;
                                    decimal FinalAmount = TotalSAShareAmount1 + MobileAppAmount;
                                    TotalFinalAmount = TotalFinalAmount + FinalAmount;
                                }
                            }

                            decimal Dabit = 0, Credit = 0;
                            string Remark = "";

                            int Total = (int)Math.Round(TotalFinalAmount, 0);
                            if (Total < 0)
                            {
                                Dabit = Total * -1;
                                Remark = "SuperAgent Plus";

                            }
                            else if (Total > 0)
                            {
                                Credit = Total;
                                Remark = "SuperAgent Minus";
                            }


                            string s8 = "Insert Into SuperAgentledger (SuperAgentID,MatchID,Amount,Dabit,Credit,Remark) Values(@SuperAgentID,@MatchID,@Amount,@Dabit,@Credit,@Remark)";
                            MySqlCommand cmd8 = new MySqlCommand(s8, cn);
                            cmd8.Parameters.AddWithValue("@SuperAgentID", SuperAgentID);
                            cmd8.Parameters.AddWithValue("@MatchID", apiID);
                            cmd8.Parameters.AddWithValue("@Amount", Total);
                            cmd8.Parameters.AddWithValue("@Dabit", Dabit);
                            cmd8.Parameters.AddWithValue("@Credit", Credit);
                            cmd8.Parameters.AddWithValue("@Remark", Remark);

                            cmd8.ExecuteNonQuery();



                            TotalFinalmatchcalculation = 0;
                            TotalFinalagentcommision = 0;
                            TotalFinalNetAmount = 0;
                            TotalFinalHalfAmount = 0;
                            TotalFinalMobileAppAmount = 0;
                            TotalFinalAmount = 0;



                            string s10 = "Select CurrentLimit from SuperAgentmaster where SuperAgentID = '" + SuperAgentID + "'";
                            MySqlCommand cmd10 = new MySqlCommand(s10, cn);
                            MySqlDataAdapter adp10 = new MySqlDataAdapter(cmd10);
                            DataTable dt10 = new DataTable();
                            adp10.Fill(dt10);

                            decimal Currentlimit = Convert.ToDecimal(dt10.Rows[0]["CurrentLimit"]);
                            decimal finalcurrentlimit = Currentlimit - Convert.ToDecimal(Total);


                            string s11 = "update SuperAgentMaster set CurrentLimit = '" + finalcurrentlimit + "' where SuperAgentID = '" + SuperAgentID + "'";
                            MySqlCommand cmd11 = new MySqlCommand(s11, cn);
                            cmd11.ExecuteNonQuery();

                        }

                        string declareteam = "";
                        if (team_selector1.Value == "team_1")
                        {
                            declareteam = team1;
                        }
                        else if (team_selector1.Value == "team_2")
                        {
                            declareteam = team2;
                        }

                        string SELECT = "Update Matches Set Status = '11', Declear = '1', winnerTeam ='" + declareteam + "'  Where apiID = '" + apiID + "'";
                        MySqlCommand cmd = new MySqlCommand(SELECT, cn);
                        cmd.ExecuteNonQuery();

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Match Declared Succesfully.....');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('First Declare " + session1 + " session of this match');", true);
                    }
                }

                else if (Declear == "True")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Match Already Declared.....');", true);

                }
            }

        }




        public decimal Totalclientmatchcalculation(int clientID, int MatchID)
        {
            string CN = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            using (MySqlConnection cn = new MySqlConnection(CN))
            {
                cn.Open();

                string s1 = "Select Amount From MatchCalculation where ClientID = '" + clientID + "' and MatchID = '" + MatchID + "'";
                MySqlCommand cmd1 = new MySqlCommand(s1, cn);
                MySqlDataAdapter adp1 = new MySqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                adp1.Fill(dt1);
                int MatchTotalAmount = 0;
                for (int j = 0; j < dt1.Rows.Count; j++)
                {

                    int Amount = Convert.ToInt32(dt1.Rows[j]["Amount"]);

                    MatchTotalAmount = MatchTotalAmount + Amount;

                }
                if (MatchTotalAmount > 0)
                {
                    MatchTotalAmount = MatchTotalAmount * -1;
                }
                else if (MatchTotalAmount < 0)
                {
                    MatchTotalAmount = MatchTotalAmount * -1;
                }



                string s2 = "Select TotalAmount From SessionCalculation where ClientID = '" + clientID + "' and MatchID = '" + MatchID + "'";
                MySqlCommand cmd2 = new MySqlCommand(s2, cn);
                MySqlDataAdapter adp2 = new MySqlDataAdapter(cmd2);
                DataTable dt2 = new DataTable();
                adp2.Fill(dt2);
                int SessionTotalAmount = 0;
                for (int k = 0; k < dt2.Rows.Count; k++)
                {

                    int Amount = Convert.ToInt32(dt2.Rows[k]["TotalAmount"]);

                    SessionTotalAmount = SessionTotalAmount + Amount;

                }
                double SessionTotalAmountvalue = double.Parse(SessionTotalAmount.ToString());

                if (SessionTotalAmount > 0)
                {
                    SessionTotalAmount = SessionTotalAmount * -1;
                }
                else if (SessionTotalAmount < 0)
                {
                    SessionTotalAmount = SessionTotalAmount * -1;
                }

                Decimal TotalMatch = MatchTotalAmount + SessionTotalAmount;
                return TotalMatch;
            }
        }
        public decimal Totalagentcommision(int clientID, int MatchID)
        {
            decimal TotalAgentCommision = 0;
            string CN = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            using (MySqlConnection cn = new MySqlConnection(CN))
            {
                cn.Open();

                string s3 = "Select Session From DeclaredSession where MatchID = '" + MatchID + "'";
                MySqlCommand cmd3 = new MySqlCommand(s3, cn);
                MySqlDataAdapter adp3 = new MySqlDataAdapter(cmd3);
                DataTable dt3 = new DataTable();
                adp3.Fill(dt3);
                int TotalAmount = 0;
                for (int m = 0; m < dt3.Rows.Count; m++)
                {
                    string Session = dt3.Rows[m]["Session"].ToString();

                    string s4 = "Select Amount From Session where MatchID = '" + MatchID + "' && ClientID = '" + clientID + "' && Session = '" + Session + "'";
                    MySqlCommand cmd4 = new MySqlCommand(s4, cn);
                    MySqlDataAdapter adp4 = new MySqlDataAdapter(cmd4);
                    DataTable dt4 = new DataTable();
                    adp4.Fill(dt4);

                    for (int n = 0; n < dt4.Rows.Count; n++)
                    {
                        int Amount = Convert.ToInt32(dt4.Rows[n]["Amount"]);
                        TotalAmount = TotalAmount + Amount;
                    }
                }

                string s55 = "Select * from sharetable where MatchID = '" + MatchID + "' And ClientID = '" + clientID + "'";
                MySqlCommand cmd55 = new MySqlCommand(s55, cn);
                MySqlDataAdapter adp55 = new MySqlDataAdapter(cmd55);
                DataTable dt55 = new DataTable();
                adp55.Fill(dt55);
                if (dt55.Rows.Count > 0)
                {


                    Decimal SessionCommision1 = Convert.ToDecimal(dt55.Rows[0]["AgentSessionComm"]);
                    Decimal SessionCommision2 = SessionCommision1 / 100;
                    Decimal SessionCommision = TotalAmount * SessionCommision2;

                    double Sessioncommisionvalue = double.Parse(SessionCommision.ToString());



                    decimal TotalMatchCommision1 = 0;
                    Decimal MatchCommision = Convert.ToDecimal(dt55.Rows[0]["AgentMatchComm"]);
                    Decimal MatchCommision1 = MatchCommision / 100;

                    string s6 = "Select Amount From MatchCalculation where MatchID = '" + MatchID + "' && ClientID = '" + clientID + "'";
                    MySqlCommand cmd6 = new MySqlCommand(s6, cn);
                    MySqlDataAdapter adp6 = new MySqlDataAdapter(cmd6);
                    DataTable dt6 = new DataTable();
                    adp6.Fill(dt6);

                    if (dt6.Rows.Count > 0)
                    {

                        Decimal MatchAmount = Convert.ToDecimal(dt6.Rows[0]["Amount"]);
                        if (MatchAmount > 0)
                        {
                            MatchAmount = MatchAmount * -1;
                            double TotalMatchCommisionvalue = double.Parse(TotalMatchCommision1.ToString());

                        }
                        else if (MatchAmount < 0)
                        {
                            MatchAmount = MatchAmount * -1;
                            TotalMatchCommision1 = MatchCommision1 * MatchAmount;
                            double TotalMatchCommisionvalue1 = double.Parse(TotalMatchCommision1.ToString());

                        }

                    }
                    TotalAgentCommision = TotalMatchCommision1 + SessionCommision;

                }
                return TotalAgentCommision;
            }

        }
        public decimal Totalsuperagentcommision(int clientID, int MatchID)
        {
            decimal TotalAgentCommision = 0;
            string CN = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            using (MySqlConnection cn = new MySqlConnection(CN))
            {
                cn.Open();

                string s3 = "Select Session From DeclaredSession where MatchID = '" + MatchID + "'";
                MySqlCommand cmd3 = new MySqlCommand(s3, cn);
                MySqlDataAdapter adp3 = new MySqlDataAdapter(cmd3);
                DataTable dt3 = new DataTable();
                adp3.Fill(dt3);
                int TotalAmount = 0;
                for (int m = 0; m < dt3.Rows.Count; m++)
                {
                    string Session = dt3.Rows[m]["Session"].ToString();

                    string s4 = "Select Amount From Session where MatchID = '" + MatchID + "' && ClientID = '" + clientID + "' && Session = '" + Session + "'";
                    MySqlCommand cmd4 = new MySqlCommand(s4, cn);
                    MySqlDataAdapter adp4 = new MySqlDataAdapter(cmd4);
                    DataTable dt4 = new DataTable();
                    adp4.Fill(dt4);

                    for (int n = 0; n < dt4.Rows.Count; n++)
                    {
                        int Amount = Convert.ToInt32(dt4.Rows[n]["Amount"]);
                        TotalAmount = TotalAmount + Amount;
                    }
                }

                string s55 = "Select * from sharetable where MatchID = '" + MatchID + "' And ClientID = '" + clientID + "'";
                MySqlCommand cmd55 = new MySqlCommand(s55, cn);
                MySqlDataAdapter adp55 = new MySqlDataAdapter(cmd55);
                DataTable dt55 = new DataTable();
                adp55.Fill(dt55);
                if (dt55.Rows.Count > 0)
                {


                    Decimal SessionCommision1 = Convert.ToDecimal(dt55.Rows[0]["SAgentSessionComm"]);
                    Decimal SessionCommision2 = SessionCommision1 / 100;
                    Decimal SessionCommision = TotalAmount * SessionCommision2;

                    double Sessioncommisionvalue = double.Parse(SessionCommision.ToString());



                    decimal TotalMatchCommision1 = 0;
                    Decimal MatchCommision = Convert.ToDecimal(dt55.Rows[0]["SAgentMatchComm"]);
                    Decimal MatchCommision1 = MatchCommision / 100;

                    string s6 = "Select Amount From MatchCalculation where MatchID = '" + MatchID + "' && ClientID = '" + clientID + "'";
                    MySqlCommand cmd6 = new MySqlCommand(s6, cn);
                    MySqlDataAdapter adp6 = new MySqlDataAdapter(cmd6);
                    DataTable dt6 = new DataTable();
                    adp6.Fill(dt6);

                    if (dt6.Rows.Count > 0)
                    {

                        Decimal MatchAmount = Convert.ToDecimal(dt6.Rows[0]["Amount"]);
                        if (MatchAmount > 0)
                        {
                            MatchAmount = MatchAmount * -1;
                            double TotalMatchCommisionvalue = double.Parse(TotalMatchCommision1.ToString());

                        }
                        else if (MatchAmount < 0)
                        {
                            MatchAmount = MatchAmount * -1;
                            TotalMatchCommision1 = MatchCommision1 * MatchAmount;
                            double TotalMatchCommisionvalue1 = double.Parse(TotalMatchCommision1.ToString());

                        }

                    }
                    TotalAgentCommision = TotalMatchCommision1 + SessionCommision;

                }
                return TotalAgentCommision;
            }

        }
    }
}

