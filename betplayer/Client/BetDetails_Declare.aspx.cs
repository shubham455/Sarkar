﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace betplayer.Client
{
    public partial class BetDetails_Declare : System.Web.UI.Page
    {
        private DataTable dt;
        private DataTable dt1;
        public DataTable MatchesDataTable { get { return dt; } }
        public DataTable MatchesDataTable1 { get { return dt1; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            int ClientID = Convert.ToInt32(Session["ClientID"]);
            int MatchID = 1136620; /*Convert.ToInt32(Request.QueryString["Matchid"]);*/

            string CN = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            using (MySqlConnection cn = new MySqlConnection(CN))
            {
                cn.Open();
                string s = "Select * From Runner where MatchID = @MatchID && ClientID =@ClientID order by DateTime DESC ";
                MySqlCommand cmd = new MySqlCommand(s, cn);
                cmd.Parameters.AddWithValue("@ClientID", ClientID);
                cmd.Parameters.AddWithValue("@MatchID", MatchID);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adp.Fill(dt);

                string s1 = "Select TeamA,TeamB From Matches where apiID = @MatchID";
                MySqlCommand cmd1 = new MySqlCommand(s1, cn);
                cmd1.Parameters.AddWithValue("@MatchID", MatchID);
                MySqlDataAdapter adp1 = new MySqlDataAdapter(cmd1);
                dt1 = new DataTable();
                adp1.Fill(dt1);

                if(dt.Rows.Count >0)
                {
                    lblteamA.Text = dt1.Rows[0]["TeamA"].ToString();
                    lblteamB.Text = dt1.Rows[0]["TeamB"].ToString();
                
                }

                string Amount = "Select TotalAmount from SessionCalculation where MatchID = @MatchID && ClientID =@ClientID";
                MySqlCommand Amountcmd = new MySqlCommand(Amount, cn);
                Amountcmd.Parameters.AddWithValue("@ClientID", ClientID);
                Amountcmd.Parameters.AddWithValue("@MatchID", MatchID);
                MySqlDataAdapter Amountadp = new MySqlDataAdapter(Amountcmd);
                DataTable Amountdt = new DataTable();
                Amountadp.Fill(Amountdt);
                if (Amountdt.Rows.Count > 0)
                {
                    int TotalAmount = 0;
                    int TotalAmount1 = 0;

                    for (int i = 0; i < Amountdt.Rows.Count; i++)
                    {
                        TotalAmount = Convert.ToInt32(Amountdt.Rows[i]["TotalAmount"]);
                        TotalAmount1 = TotalAmount1 + TotalAmount;
                    }
                    TotalAmount = 0;

                    if(TotalAmount1 > 0)
                    {
                        lblSession.Text = "You Won(" + TotalAmount1 + ")Coins";
                    }
                    if (TotalAmount1 < 0)
                    {
                        lblSession.Text = "You Lose(" + TotalAmount1 + ")Coins";
                    }




                }
            }
        }
    }
}