using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;
using System.Net;
using System.Web.Script.Serialization;
using System.IO;

namespace betplayer.poweruser
{
    public partial class AddMatchManually : System.Web.UI.Page
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        protected void Page_Load(object sender, EventArgs e)
        {
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;

            if (!IsPostBack)
            {
                {
                    string CN = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                    using (MySqlConnection cn = new MySqlConnection(CN))
                    {
                        cn.Open();
                        string s2 = "Select * From Matches where MatchesID = '" + txtcode.Text + "'";
                        MySqlCommand cmd2 = new MySqlCommand(s2, cn);
                        MySqlDataReader rdr = cmd2.ExecuteReader();
                        if (rdr.Read())
                        {
                        }
                        else
                        {
                            rdr.Close();
                            string s = "insert into Matches() values (); SELECT LAST_INSERT_ID()";
                            MySqlCommand cmd = new MySqlCommand(s, cn);
                            cmd.Parameters.AddWithValue("@TeamA", txtTeamA.Text);
                            cmd.Parameters.AddWithValue("@TeamB", "");
                            cmd.Parameters.AddWithValue("@TeamC", "");
                            cmd.Parameters.AddWithValue("@Date", "");
                            cmd.Parameters.AddWithValue("@MatchType", "");

                            int ID = Convert.ToInt32(cmd.ExecuteScalar());
                            string s1 = "Select MatchesID From Matches where MatchesID = '" + ID + "' ";
                            MySqlCommand cmd1 = new MySqlCommand(s1, cn);
                            MySqlDataAdapter adp = new MySqlDataAdapter(cmd1);
                            DataTable dt = new DataTable();
                            adp.Fill(dt);
                            txtcode.Text = dt.Rows[0]["MatchesID"].ToString();
                        }
                    }
                }
            }
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            string CN = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            using (MySqlConnection cn = new MySqlConnection(CN))
            {
                cn.Open();



                var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://sarkar-73a30.firebaseio.com/currentMatches.json");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = new JavaScriptSerializer().Serialize(new
                    {
                        match_id = Convert.ToInt32(txtcode.Text),
                        description = "",
                        message = "",
                        status = "",
                        minBet = "2000",
                        maxBet = "200000",
                        sessionMinBet = "1000",
                        sessionMaxBet = "200000",
                        fancyminbet = "500",
                        fancymaxbet = "20000",
                        lastBall = new
                        {
                            @event = "Bet Open"
                        },
                        livetv = new
                        {
                            enabled = false,
                            channel = "",
                            ip = "",
                            port = ""
                        },
                        team_1 = new
                        {
                            Name = txtTeamA.Text,
                            Overs = "0.0",
                            Runs = "0",
                            Status = "",
                            Wickets = "0",
                            sName = "",
                            Runner = new
                            {
                                Khai = "0",
                                Lagai = "0"
                            },
                            Session = new { }
                        },
                        team_2 = new
                        {
                            Name = txtTeamB.Text,
                            Overs = "0.0",
                            Runs = "0",
                            Status = "",
                            Wickets = "0",
                            sName = "",
                            Runner = new
                            {
                                Khai = "0",
                                Lagai = "0"
                            },
                            Session = new { }
                        },
                        team_c = new
                        {
                            Name = "DRAW",
                            Runner = new
                            {
                                Khai = "0.0",
                                Lagai = "0.0"
                            },
                            Session = new { }
                        }
                    });

                    streamWriter.Write(json);
                }
                var firebaseKey = "";
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    FirebaseResponse firebaseResult = js.Deserialize<FirebaseResponse>(result);
                    firebaseKey = firebaseResult.name;
                }
                string fkey = firebaseKey;

                string s = "Update Matches set TeamA =@TeamA , TeamB =@TeamB, TeamC =@TeamC, apiID=@MatchesID, DateTime =@DateTime ,Type = @MatchType,Status = @Status,Active =@Active,Firebasekey = @fk where MatchesId = @MatchesID";
                MySqlCommand cmd = new MySqlCommand(s, cn);


                cmd.Parameters.AddWithValue("@MatchesID", txtcode.Text);
                cmd.Parameters.AddWithValue("@TeamA", txtTeamA.Text);
                cmd.Parameters.AddWithValue("@TeamB", txtTeamB.Text);
                cmd.Parameters.AddWithValue("@TeamC", txtTeamC.Text);
                cmd.Parameters.AddWithValue("@DateTime", txtdate1.Text + "T" + txtTime.Text + ":00.000Z");
                cmd.Parameters.AddWithValue("@MatchType", DropdownMatchesType.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Status", '1');
                cmd.Parameters.AddWithValue("@Active", '1');
                cmd.Parameters.AddWithValue("@fk", fkey);
                cmd.ExecuteNonQuery();

                Response.Redirect("CreateMatch.aspx");

            }
        }
        protected void btncancel_Click(object sender, EventArgs e)
        {
            string CN = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            using (MySqlConnection cn = new MySqlConnection(CN))
            {
                cn.Open();
                string s = "Delete From  Matches where MatchesID = '" + txtcode.Text + "'";
                MySqlCommand cmd = new MySqlCommand(s, cn);
                cmd.ExecuteNonQuery();


                Response.Redirect("CreateMatch.aspx");
            }
        }
        public class FirebaseResponse
        {
            public string name { get; set; }

        }
    }
}