using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace betplayer.agent
{
    public partial class MatchPlusMinusDisplay : System.Web.UI.Page
    {
        private DataTable dt;
        private DataTable runtable;
        private DataTable runtable1;

        public DataTable MatchesDataTable { get { return dt; } }
        public DataTable runTable { get { return runtable; } }
        public DataTable runTable1 { get { return runtable1; } }
        protected void Page_Load(object sender, EventArgs e)
        {

            runtable = new DataTable();
            runtable.Columns.Add(new DataColumn("Name"));
            runtable.Columns.Add(new DataColumn("MatchAmount"));
            runtable.Columns.Add(new DataColumn("SessionAmount"));
            runtable.Columns.Add(new DataColumn("TotalAmount"));
            runtable.Columns.Add(new DataColumn("SessionCommision"));
            runtable.Columns.Add(new DataColumn("MatchCommision"));
            runtable.Columns.Add(new DataColumn("TotalCommisionAmount"));
            runtable.Columns.Add(new DataColumn("TotalNetAmount"));
            runtable.Columns.Add(new DataColumn("TotalHalfAmount"));
            runtable.Columns.Add(new DataColumn("ClientShare"));
            runtable.Columns.Add(new DataColumn("MOBAppAmount"));
            runtable.Columns.Add(new DataColumn("FinalAmount"));


            runtable1 = new DataTable();
            runtable1.Columns.Add(new DataColumn("MatchAmount"));
            runtable1.Columns.Add(new DataColumn("SessionAmount"));
            runtable1.Columns.Add(new DataColumn("TotalPlusMinusAmount"));
            runtable1.Columns.Add(new DataColumn("Match1Amount"));
            runtable1.Columns.Add(new DataColumn("Session1Amount"));
            runtable1.Columns.Add(new DataColumn("TotalCommisionAmount"));
            runtable1.Columns.Add(new DataColumn("TotalNetAmount"));
            runtable1.Columns.Add(new DataColumn("TotalHalfAmount"));
            runtable1.Columns.Add(new DataColumn("TotalAppAmount"));
            runtable1.Columns.Add(new DataColumn("TotalFinalAmount"));
            DataRow row1 = runtable1.NewRow();




            int MatchID = Convert.ToInt32(Request.QueryString["MatchID"]);
            string CN = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            using (MySqlConnection cn = new MySqlConnection(CN))
            {
                cn.Open();



                string s = "select clientmaster.clientID,clientmaster.Name from clientmaster inner join viewmatch on clientmaster.ClientID = viewmatch.ClientID  where MatchID = '" + MatchID + "' && clientmaster.createdBy = '" + Session["Agentcode"] + "' group by Name";
                MySqlCommand cmd = new MySqlCommand(s, cn);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adp.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string Name = dt.Rows[i]["Name"].ToString();
                    string ID = dt.Rows[i]["ClientID"].ToString();
                    string CodeName = ID + Name;
                    DataRow row = runTable.NewRow();
                    row["Name"] = CodeName;
                    string s1 = "Select Amount From MatchCalculation where ClientID = '" + ID + "' and MatchID = '" + MatchID + "'";
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
                    double Matchtotalvalue = double.Parse(MatchTotalAmount.ToString());
                    row["MatchAmount"] = Matchtotalvalue;


                    row["SessionAmount"] = 0;

                    Decimal TotalMatch = MatchTotalAmount;
                    double TotalMatchvalue = double.Parse(TotalMatch.ToString());
                    row["TotalAmount"] = TotalMatchvalue;


                    Decimal TotalHalfAmount = 0, TotalNetAmouunt = 0;
                    string s55 = "Select * from sharetable where MatchID = '" + MatchID + "' && ClientID = '" + ID + "'";
                    MySqlCommand cmd55 = new MySqlCommand(s55, cn);
                    MySqlDataAdapter adp55 = new MySqlDataAdapter(cmd55);
                    DataTable dt55 = new DataTable();
                    adp55.Fill(dt55);

                    if (dt55.Rows.Count > 0)
                    {

                        string s5 = "Select Name  From AgentMaster where AgentID = '" + Session["AgentID"] + "' ";
                        MySqlCommand cmd5 = new MySqlCommand(s5, cn);
                        MySqlDataAdapter adp5 = new MySqlDataAdapter(cmd5);
                        DataTable dt5 = new DataTable();
                        adp5.Fill(dt5);
                        lblAgentName.Text = dt5.Rows[0]["Name"].ToString();

                        row["SessionCommision"] = 0;


                        decimal TotalMatchCommision1 = 0;
                        Decimal MatchCommision = Convert.ToDecimal(dt55.Rows[0]["AgentMatchComm"]);
                        Decimal MatchCommision1 = MatchCommision / 100;

                        string s6 = "Select Amount From MatchCalculation where MatchID = '" + MatchID + "' && ClientID = '" + ID + "'";
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
                                row["MatchCommision"] = TotalMatchCommisionvalue;
                            }
                            else if (MatchAmount < 0)
                            {
                                MatchAmount = MatchAmount * -1;
                                TotalMatchCommision1 = MatchCommision1 * MatchAmount;
                                double TotalMatchCommisionvalue1 = double.Parse(TotalMatchCommision1.ToString());
                                row["MatchCommision"] = TotalMatchCommisionvalue1;
                            }
                            else
                            {
                                row["MatchCommision"] = 0;
                            }


                            decimal totalcommision = TotalMatchCommision1;
                            double totalcommisionvalue = double.Parse(totalcommision.ToString());

                            row["TotalCommisionAmount"] = totalcommisionvalue;
                        }
                        else
                        {
                            row["MatchCommision"] = 0;
                            row["TotalCommisionAmount"] = 0;
                        }
                        Decimal Match = MatchTotalAmount;
                        Decimal Commision = TotalMatchCommision1;
                        Decimal To = 0;

                        if (Match > 0)
                        {

                            To = Match - Commision;

                        }
                        else if (Match < 0)
                        {

                            To = Match - Commision;

                        }
                        else
                        {
                            To = Commision * -1;
                        }

                        TotalNetAmouunt = To;

                        

                        decimal clientshare = Convert.ToDecimal(dt55.Rows[0]["ClientShare"]);
                        decimal clientshare1 = clientshare / 100;


                        TotalHalfAmount = TotalNetAmouunt * clientshare1;

                        double TotalNetAmouuntvalue = double.Parse(TotalNetAmouunt.ToString());
                        row["TotalNetAmount"] = TotalNetAmouuntvalue;
                        double TotalHalfAmountvalue = double.Parse(TotalHalfAmount.ToString());
                        row["TotalHalfAmount"] = TotalHalfAmountvalue;
                        row["ClientShare"] = clientshare + "%";

                    }
                    else
                    {
                        row["SessionCommision"] = 0;
                        row["MatchCommision"] = 0;
                        row["TotalCommisionAmount"] = 0;
                        row["TotalNetAmount"] = 0;
                        row["TotalHalfAmount"] = 0;
                    }



                    string s8 = "Select * From AgentMaster where  AgentID = '" + Session["AgentID"] + "'";
                    MySqlCommand cmd8 = new MySqlCommand(s8, cn);
                    MySqlDataAdapter adp8 = new MySqlDataAdapter(cmd8);
                    DataTable dt8 = new DataTable();
                    adp8.Fill(dt8);


                    Decimal MobileAppAmount = 0;
                    String MobileApp = dt8.Rows[0]["Mobapp"].ToString();
                    

                    MobileAppAmount = Convert.ToDecimal(MobileApp);
                    double MobileAppAmountvalue = double.Parse(MobileAppAmount.ToString());
                    row["MOBAppAmount"] = MobileAppAmountvalue;

                    decimal FinalAmount = (TotalNetAmouunt - TotalHalfAmount) + MobileAppAmount;
                    double finalamountvalue = double.Parse(FinalAmount.ToString());
                    row["FinalAmount"] = finalamountvalue;


                    runTable.Rows.Add(row.ItemArray);
                }





                decimal TotalMatchAmount1 = 0;
                for (int l = 0; l < runtable.Rows.Count; l++)
                {
                    decimal MatchAmount = Convert.ToDecimal(runtable.Rows[l]["MatchAmount"]);
                    TotalMatchAmount1 = TotalMatchAmount1 + MatchAmount;
                }

                row1["MatchAmount"] = TotalMatchAmount1;

                decimal TotalSessionAmount1 = 0;
                for (int l = 0; l < runtable.Rows.Count; l++)
                {
                    decimal MatchAmount = Convert.ToDecimal(runtable.Rows[l]["SessionAmount"]);
                    TotalSessionAmount1 = TotalSessionAmount1 + MatchAmount;
                }

                row1["SessionAmount"] = TotalSessionAmount1;

                decimal TotalPlusMinusAmount = 0;
                for (int l = 0; l < runtable.Rows.Count; l++)
                {
                    decimal TotalAmount = Convert.ToDecimal(runtable.Rows[l]["TotalAmount"]);
                    TotalPlusMinusAmount = TotalPlusMinusAmount + TotalAmount;
                }

                row1["TotalPlusMinusAmount"] = TotalPlusMinusAmount;


                decimal Match1Amount = 0;
                for (int l = 0; l < runtable.Rows.Count; l++)
                {
                    if (runtable.Rows.Count > 0)
                    {
                        decimal MatchCommision = Convert.ToDecimal(runtable.Rows[l]["MatchCommision"]);
                        Match1Amount = Match1Amount + MatchCommision;
                    }
                }

                row1["Match1Amount"] = Match1Amount;

                decimal Session1Amount = 0;
                for (int l = 0; l < runtable.Rows.Count; l++)
                {
                    decimal SessionCommision = Convert.ToDecimal(runtable.Rows[l]["SessionCommision"]);
                    Session1Amount = Session1Amount + SessionCommision;
                }

                row1["Session1Amount"] = Session1Amount;

                decimal TotalCommisionAmount = 0;
                for (int l = 0; l < runtable.Rows.Count; l++)
                {
                    decimal TotalCommisionAmount1 = Convert.ToDecimal(runtable.Rows[l]["TotalCommisionAmount"]);
                    TotalCommisionAmount = TotalCommisionAmount + TotalCommisionAmount1;
                }

                row1["TotalCommisionAmount"] = TotalCommisionAmount;

                decimal TotalNetAmount1 = 0;
                for (int l = 0; l < runtable.Rows.Count; l++)
                {
                    decimal TotalNetAmount = Convert.ToDecimal(runtable.Rows[l]["TotalNetAmount"]);
                    TotalNetAmount1 = TotalNetAmount1 + TotalNetAmount;
                }

                row1["TotalNetAmount"] = TotalNetAmount1;

                decimal TotalHalfAmount1 = 0;
                for (int l = 0; l < runtable.Rows.Count; l++)
                {
                    decimal TotalHalfAmount = Convert.ToDecimal(runtable.Rows[l]["TotalHalfAmount"]);
                    TotalHalfAmount1 = TotalHalfAmount1 + TotalHalfAmount;
                }

                row1["TotalHalfAmount"] = TotalHalfAmount1;

                decimal TotalAppAmount1 = 0;
                for (int l = 0; l < runtable.Rows.Count; l++)
                {
                    decimal MatchAmount = Convert.ToDecimal(runtable.Rows[l]["MOBAppAmount"]);
                    TotalAppAmount1 = TotalAppAmount1 + MatchAmount;
                }

                row1["TotalAppAmount"] = TotalAppAmount1;

                decimal TotalFinalAmount1 = 0;
                for (int l = 0; l < runtable.Rows.Count; l++)
                {
                    decimal MatchAmount = Convert.ToDecimal(runtable.Rows[l]["FinalAmount"]);
                    TotalFinalAmount1 = TotalFinalAmount1 + MatchAmount;
                }
                row1["TotalFinalAmount"] = TotalFinalAmount1;



                string matchdeclare = "Select * From matches where apiID = '" + MatchID + "'";
                MySqlCommand matchdeclarecmd = new MySqlCommand(matchdeclare, cn);
                MySqlDataAdapter matchdeclareadp = new MySqlDataAdapter(matchdeclarecmd);
                DataTable matchdeclaredt = new DataTable();
                matchdeclareadp.Fill(matchdeclaredt);

                int declear = Convert.ToInt16(matchdeclaredt.Rows[0]["declear"]);

                if (declear == 1)
                {

                    decimal Dabit = 0, Credit = 0;
                    string Remark = "";

                    if (TotalFinalAmount1 < 0)
                    {
                        Dabit = TotalFinalAmount1 * -1;
                        Remark = "Agent Plus";

                    }
                    else if (TotalFinalAmount1 > 0)
                    {
                        Credit = TotalFinalAmount1;
                        Remark = "Agent Minus";
                    }

                    string s9 = "Select * From  Agentledger where AgentID = '" + Session["AgentID"] + "' && MatchID  = '" + MatchID + "'";
                    MySqlCommand cmd9 = new MySqlCommand(s9, cn);
                    MySqlDataReader rdr = cmd9.ExecuteReader();
                    if (rdr.Read())
                    {
                        rdr.Close();
                        string s10 = "Select Amount from Agentledger where AgentID = '" + Session["AgentID"] + "' && MatchID  = '" + MatchID + "'";
                        MySqlCommand cmd10 = new MySqlCommand(s10, cn);
                        MySqlDataAdapter adp10 = new MySqlDataAdapter(cmd10);
                        DataTable dt10 = new DataTable();
                        adp10.Fill(dt10);

                        decimal Amount = Convert.ToDecimal(dt10.Rows[0]["Amount"]);
                        if (Amount != TotalFinalAmount1)
                        {

                            string s11 = "update Agentledger set Amount = @Amount,Dabit =@Dabit,Credit=@Credit,Remark=@Remark  where AgentID = '" + Session["AgentID"] + "' && matchID = '" + MatchID + "'";
                            MySqlCommand cmd11 = new MySqlCommand(s11, cn);
                            cmd11.Parameters.AddWithValue("@Amount", TotalFinalAmount1);
                            cmd11.Parameters.AddWithValue("@Dabit", Dabit);
                            cmd11.Parameters.AddWithValue("@Credit", Credit);
                            cmd11.Parameters.AddWithValue("@Remark", Remark);
                            cmd11.ExecuteNonQuery();
                        }

                    }
                    else
                    {

                        rdr.Close();
                        string s8 = "Insert Into Agentledger (AgentID,MatchID,Amount,Dabit,Credit,Remark) Values(@AgentID,@MatchID,@Amount,@Dabit,@Credit,@Remark)";
                        MySqlCommand cmd8 = new MySqlCommand(s8, cn);
                        cmd8.Parameters.AddWithValue("@AgentID", Session["AgentID"]);
                        cmd8.Parameters.AddWithValue("@MatchID", MatchID);
                        cmd8.Parameters.AddWithValue("@Amount", TotalFinalAmount1);
                        cmd8.Parameters.AddWithValue("@Dabit", Dabit);
                        cmd8.Parameters.AddWithValue("@Credit", Credit);
                        cmd8.Parameters.AddWithValue("@Remark", Remark);

                        cmd8.ExecuteNonQuery();

                        string s10 = "Select CurrentLimit from Agentmaster where AgentID = '" + Session["AgentID"] + "'";
                        MySqlCommand cmd10 = new MySqlCommand(s10, cn);
                        MySqlDataAdapter adp10 = new MySqlDataAdapter(cmd10);
                        DataTable dt10 = new DataTable();
                        adp10.Fill(dt10);

                        decimal Currentlimit = Convert.ToDecimal(dt10.Rows[0]["CurrentLimit"]);
                        decimal finalcurrentlimit = Currentlimit - TotalFinalAmount1;


                        string s11 = "update AgentMaster set CurrentLimit = '" + finalcurrentlimit + "' where AgentID = '" + Session["AgentID"] + "'";
                        MySqlCommand cmd11 = new MySqlCommand(s11, cn);
                        cmd11.ExecuteNonQuery();
                    }
                }

                runtable1.Rows.Add(row1.ItemArray);


            }
        }
    }
}
