using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.UI.WebControls;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Data;
using System.Net;
using System.Web.Script.Serialization;
using System.IO;

namespace betplayer.poweruser
{
    public partial class AddMatch : System.Web.UI.Page
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        protected void Page_Load(object sender, EventArgs e)
        {

            int Id = Convert.ToInt32(Request.QueryString["ID"]);
            string CN = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            using (MySqlConnection cn = new MySqlConnection(CN))
            {
                string s = "Select * From Matches where MatchesID = '" + Id + "'";
                MySqlCommand cmd = new MySqlCommand(s, cn);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                txtcode.Text = dt.Rows[0]["MatchesID"].ToString();
                txtTeamA.Text = dt.Rows[0]["TeamA"].ToString();
                txtTeamB.Text = dt.Rows[0]["TeamB"].ToString();
                txtTime.Text = dt.Rows[0]["DateTime"].ToString();
                txtMatchType.Text = dt.Rows[0]["Type"].ToString();

                

                string html = string.Empty;
                string url = @"https://www.lotusbook.com/api/exchange/odds/eventType/4";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    html = reader.ReadToEnd();

                    LotusResponse LotusResult = js.Deserialize<LotusResponse>(html);
                    
                    matchdropdown.DataSource = getMatches(LotusResult).ToList();
                    matchdropdown.DataTextField = "name";
                    matchdropdown.DataValueField = "id";
                    matchdropdown.DataBind();
                }


            }
        }

        protected List<lotusmatch> getMatches(LotusResponse response)
        {
            List<lotusmatch> matches = new List<lotusmatch>();
            matches.Add(new lotusmatch
            {
                name = "Select Match.",
                id = ""
            });
            for (int i = 0; i < response.result.Count; i++)
            {
                matches.Add(new lotusmatch
                {
                    name = response.result[i].@event.name,
                    id = response.result[i].@event.id
                });
            }
            return matches;
        }

        protected void submit_Click(object sender, EventArgs e)
        {
            string id = Request.QueryString["ID"];
            string CN = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            using (MySqlConnection cn = new MySqlConnection(CN))
            {
                cn.Open();
                string matchId = matchdropdown.SelectedItem.Value;

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
                        livetv = new {
                            enabled = false,
                            channel = ""
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
                                Khai = "0.00",
                                Lagai = "0.00"
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
                                Khai = "0.00",
                                Lagai = "0.00"
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






                if (matchId == "")
                {
                    string s = "Update Matches set Firebasekey  = @Firebasekey, Active = @Active,Status = @Status where matchesID = @MatchID";

                    MySqlCommand cmd = new MySqlCommand(s, cn);
                    cmd.Parameters.AddWithValue("@Active", '1');
                    cmd.Parameters.AddWithValue("@Status", '1');
                    cmd.Parameters.AddWithValue("@MatchID", id);
                    cmd.Parameters.AddWithValue("@Firebasekey", fkey);
                    cmd.ExecuteNonQuery();


                }
                else
                {
                    string s = "Update Matches set Firebasekey  = @Firebasekey, lotusmatchid = @lotusmatchId,AutoSession = @Autosession, Active = @Active,Status = @Status,Apitype = @Apitype where matchesID = @MatchID";

                    MySqlCommand cmd = new MySqlCommand(s, cn);
                    cmd.Parameters.AddWithValue("@Active", '1');
                    cmd.Parameters.AddWithValue("@Status", '1');
                    cmd.Parameters.AddWithValue("@MatchID", id);
                    cmd.Parameters.AddWithValue("@lotusmatchId", matchId);
                    cmd.Parameters.AddWithValue("@Firebasekey", fkey);
                    cmd.Parameters.AddWithValue("@Autosession", '1');
                    cmd.Parameters.AddWithValue("@ApiType", "ESPN-LOTUS");

                    cmd.ExecuteNonQuery();




                    Response.Redirect("CreateMatch.aspx?msg=Add");

                }
            }
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateMatch.aspx");
        }

        public class FirebaseResponse
        {
            public string name { get; set; }

        }
        public class Status
        {
            public string statusCode { get; set; }
            public string statusDesc { get; set; }
        }

        public class Event
        {
            public string id { get; set; }
            public string name { get; set; }
            public DateTime openDate { get; set; }
        }

        public class Competition
        {
            public string id { get; set; }
            public string name { get; set; }
        }

        public class Back
        {
            public double price { get; set; }
            public double size { get; set; }
        }

        public class Lay
        {
            public double price { get; set; }
            public double size { get; set; }
        }

        public class Runner
        {
            public int id { get; set; }
            public string name { get; set; }
            public double hdp { get; set; }
            public int sort { get; set; }
            public List<Back> back { get; set; }
            public List<Lay> lay { get; set; }
        }

        public class Result
        {
            public string id { get; set; }
            public string groupById { get; set; }
            public string name { get; set; }
            public bool aussieExchange { get; set; }
            public string exchangeId { get; set; }
            public object start { get; set; }
            public string btype { get; set; }
            public string mtype { get; set; }
            public Event @event { get; set; }
            public string eventTypeId { get; set; }
            public bool inPlay { get; set; }
            public Competition competition { get; set; }
            public double matched { get; set; }
            public int numWinners { get; set; }
            public int numRunners { get; set; }
            public int numActiveRunners { get; set; }
            public string status { get; set; }
            public object statusLabel { get; set; }
            public object lastUpdateTime { get; set; }
            public object oddsType { get; set; }
            public string provider { get; set; }
            public List<Runner> runners { get; set; }
            public object maxLiabilityPerBet { get; set; }
            public object maxLiabilityPerMarket { get; set; }
            public int betDelay { get; set; }
            public bool isBettable { get; set; }
            public object bettableTime { get; set; }
        }

        public class LotusResponse
        {

            public Status status { get; set; }
            public List<Result> result { get; set; }
            public bool success { get; set; }
        }
        public class lotusmatch
        {
            public string name { get; set; }
            public string id { get; set; }

        }
    }
}