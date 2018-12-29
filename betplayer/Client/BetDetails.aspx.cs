
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

using System.Web.UI.WebControls;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace betplayer.Client
{
    public partial class BetDetails : System.Web.UI.Page
    {

        private DataTable dt;
        private DataTable dt1;
        
        private DataTable dt3;
        private DataTable dt4;
        private DataTable dt5;
        private DataTable dt6 = new DataTable();
        public DataTable MatchesDataTable { get { return dt; } }
        public DataTable MatchesDataTable1 { get { return dt1; } }
        
        public DataTable MatchesDataTable3 { get { return dt3; } }
        public DataTable MatchesDataTable5 { get { return dt6; } }
        public Boolean emptyLedgerTable = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            string type = Request.QueryString["Type"];
            if (type == "Test" || type == "test")
            {
                emptyLedgerTable = true;
            }
            dt6.Columns.Add(new DataColumn("SessionID"));
            dt6.Columns.Add(new DataColumn("Session"));
            dt6.Columns.Add(new DataColumn("Rate"));
            dt6.Columns.Add(new DataColumn("Amount"));
            dt6.Columns.Add(new DataColumn("Runs"));
            dt6.Columns.Add(new DataColumn("Mode"));
            dt6.Columns.Add(new DataColumn("DateTime"));
            dt6.Columns.Add(new DataColumn("Dec"));

            DataRow Clientrow = dt6.NewRow();

            if (Session["ClientID"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                apiID.Value = Request.QueryString["id"];
                firebasekey.Value = Request.QueryString["fk"];
                int ClientID = Convert.ToInt16(Session["ClientID"]);
                string CN = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                using (MySqlConnection cn = new MySqlConnection(CN))
                {
                    cn.Open();
                    string SELECT = "Select * from Matches Where apiID = '" + apiID.Value + "'";
                    MySqlCommand cmd = new MySqlCommand(SELECT, cn);
                    MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                    dt = new DataTable();
                    adp.Fill(dt);

                    string SMatches = "Select * from Matches where Status = '1' && Active = '1'";
                    MySqlCommand cmd1 = new MySqlCommand(SMatches, cn);
                    MySqlDataAdapter adp1 = new MySqlDataAdapter(cmd1);
                    dt1 = new DataTable();
                    adp1.Fill(dt1);
                    

                    string selectSession = "select Session From Session  where ClientID  = @ClientID && MatchID = @MatchID group by Session ";
                    MySqlCommand selectSessioncmd = new MySqlCommand(selectSession, cn);
                    selectSessioncmd.Parameters.AddWithValue("@MatchID", apiID.Value);
                    selectSessioncmd.Parameters.AddWithValue("@ClientID", ClientID);
                    MySqlDataAdapter selectSessionadp = new MySqlDataAdapter(selectSessioncmd);
                    DataTable selectSessiondt = new DataTable();
                    selectSessionadp.Fill(selectSessiondt);


                    for (int i = 0; i < selectSessiondt.Rows.Count; i++)
                    {
                        string Sessionname = selectSessiondt.Rows[i]["Session"].ToString();

                        string selectDeclaredSession = "select * from declaredSession where Session = '" + Sessionname + "' && MatchID = '" + apiID.Value + "' ";
                        MySqlCommand selectDeclaredSessioncmd = new MySqlCommand(selectDeclaredSession, cn);
                        MySqlDataReader rdr1 = selectDeclaredSessioncmd.ExecuteReader();

                        if (rdr1.Read())
                        {
                            rdr1.Close();
                            string selectSession1 = "select Session.SessionID, Session.session,Session.rate,Session.Amount,Session.Runs,Session.Mode,Session.DateTime,DeclaredSession.DeclareRun from Session inner join declaredSession on Session.Session = DeclaredSession.Session && Session.MatchID  = DeclaredSession.MatchID where Session.Session = '" + Sessionname + "' && declaredsession.MatchID = '" + apiID.Value + "' && Session.ClientID = '" + Session["ClientID"] + "'";
                            MySqlCommand selectSessioncmd1 = new MySqlCommand(selectSession1, cn);
                            MySqlDataAdapter selectSessionadp1 = new MySqlDataAdapter(selectSessioncmd1);
                            dt5 = new DataTable();
                            selectSessionadp1.Fill(dt5);

                        }
                        else
                        {
                            rdr1.Close();
                            string Session = "Select Session.SessionID, Session.session,Session.rate, Session.Amount,Session.Runs,Session.Mode,Session.DateTime from Session where ClientID  = @ClientID && MatchID = @MatchID && Session.session = '" + Sessionname + "'   order by DateTime DESC";
                            MySqlCommand Sessioncmd = new MySqlCommand(Session, cn);
                            Sessioncmd.Parameters.AddWithValue("@MatchID", apiID.Value);
                            Sessioncmd.Parameters.AddWithValue("@ClientID", ClientID);
                            MySqlDataAdapter sessionadp = new MySqlDataAdapter(Sessioncmd);
                            dt5 = new DataTable();
                            sessionadp.Fill(dt5);
                        }
                        IEnumerable<DataRow> orderedRows = dt5.AsEnumerable();
                        DataTable dt7 = orderedRows.CopyToDataTable();
                        foreach (DataRow row in dt7.Rows)
                        {
                            dt6.Rows.Add(row.ItemArray);
                        }
                    }


                    string Amount = "Select TotalAmount from SessionCalculation where ClientID = '" + ClientID + "' && MatchID = '" + apiID.Value + "'";
                    MySqlCommand Amountcmd = new MySqlCommand(Amount, cn);
                    MySqlDataAdapter Amountadp = new MySqlDataAdapter(Amountcmd);
                    DataTable Amountdt = new DataTable();
                    Amountadp.Fill(Amountdt);
                    if (Amountdt.Rows.Count > 0)
                    {
                        int TotalAmount = 0;
                        int TotalAmount1 = 0;

                        for (int j = 0; j < Amountdt.Rows.Count; j++)
                        {
                            TotalAmount = Convert.ToInt32(Amountdt.Rows[j]["TotalAmount"]);
                            TotalAmount1 = TotalAmount1 + TotalAmount;
                        }
                        TotalAmount = 0;



                        lblAmount.Text = TotalAmount1.ToString();
                        if (Convert.ToInt32(lblAmount.Text) > 0)
                        {
                            lblAmount.ForeColor = System.Drawing.Color.Blue;
                        }
                        else if (Convert.ToInt32(lblAmount.Text) < 0)
                        {
                            lblAmount.ForeColor = System.Drawing.Color.Red;
                        }

                    }



                    string runner = "Select *  from runner  where ClientID = '" + ClientID + "' and MatchID = '" + apiID.Value + "' order by DateTime DESC";
                    MySqlCommand cmd3 = new MySqlCommand(runner, cn);
                    MySqlDataAdapter adp3 = new MySqlDataAdapter(cmd3);
                    dt3 = new DataTable();
                    adp3.Fill(dt3);
                    if (dt3.Rows.Count > 0)
                    {
                        PositionTeam1.Text = dt3.Rows[0]["Position1"].ToString();
                        PositionTeam2.Text = dt3.Rows[0]["Position2"].ToString();
                        PositionTeam3.Text = dt3.Rows[0]["Position3"].ToString();
                    }
                    else
                    {
                        PositionTeam1.Text = "0.00";
                        PositionTeam2.Text = "0.00";
                        PositionTeam3.Text = "0.00";
                    }

                    string result = "SELECT * FROM ViewMatch WHERE ClientID='" + ClientID + "' && MatchID = '" + apiID.Value + "'";
                    MySqlCommand resultcmd = new MySqlCommand(result, cn);
                    MySqlDataReader rdr = resultcmd.ExecuteReader();
                    if (rdr.Read())
                    {

                    }
                    else
                    {
                        rdr.Close();
                        string insert = "Insert Into ViewMatch (MatchID,ClientID) values (@MatchID,@ClientID)";
                        MySqlCommand insertcmd = new MySqlCommand(insert, cn);
                        insertcmd.Parameters.AddWithValue("@MatchID", apiID.Value);
                        insertcmd.Parameters.AddWithValue("@ClientID", ClientID);
                        insertcmd.ExecuteNonQuery();

                        string checkfordeduction = "Select *  from viewmatch  where ClientID = '" + ClientID + "' and MatchID = '" + apiID.Value + "'";
                        MySqlCommand cmd4 = new MySqlCommand(checkfordeduction, cn);
                        MySqlDataAdapter adp4 = new MySqlDataAdapter(cmd4);
                        dt4 = new DataTable();
                        adp4.Fill(dt4);
                        if (dt4.Rows.Count > 0)
                        {
                            int Isview = Convert.ToInt16(dt4.Rows[0]["Isview"]);
                            if (Isview == 0)
                            {
                                string coinDeduct = "Select Client_limit,CurrentLimit,MobileAppAmount From ClientMaster where ClientID = '" + ClientID + "'";
                                MySqlCommand cmd5 = new MySqlCommand(coinDeduct, cn);
                                MySqlDataAdapter adp5 = new MySqlDataAdapter(cmd5);
                                DataTable dt = new DataTable();
                                adp5.Fill(dt);
                                decimal clientlimit = Convert.ToDecimal(dt.Rows[0]["CurrentLimit"]);
                                int MobileAppAmount = Convert.ToInt32(dt.Rows[0]["MobileAppAmount"]);
                                decimal Deductdlimit = clientlimit - MobileAppAmount;

                                string update = "update clientmaster set Currentlimit =@Deductdlimit where clientID = @ClientID";
                                MySqlCommand cmd6 = new MySqlCommand(update, cn);
                                cmd6.Parameters.AddWithValue("@Deductdlimit", Deductdlimit);
                                cmd6.Parameters.AddWithValue("@ClientID", ClientID);
                                cmd6.ExecuteNonQuery();

                                string update1 = "update viewmatch set Isview = '1' where clientID = '" + ClientID + "' && MatchID = '" + apiID.Value + "'";
                                MySqlCommand cmd7 = new MySqlCommand(update1, cn);
                                cmd7.ExecuteNonQuery();
                            }

                        }
                        else
                        {

                        }
                    }
                }
            }
        }

    }
}
