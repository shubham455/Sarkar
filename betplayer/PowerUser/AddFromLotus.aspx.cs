using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using System.Configuration;
using MySql.Data.MySqlClient;
using System.Data;
using System.Net;
using System.Web.Script.Serialization;
using System.IO;

namespace betplayer.PowerUser
{
    public partial class AddFromLotus : System.Web.UI.Page
    {
        LotusResponse LotusResult;
        JavaScriptSerializer js = new JavaScriptSerializer();
        protected void Page_Load(object sender, EventArgs e)
        {
           
                string html = string.Empty;
                string url = @"https://www.lotusbook.com/api/exchange/odds/eventType/4";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.AutomaticDecompression = DecompressionMethods.GZip;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    html = reader.ReadToEnd();

                    LotusResult = js.Deserialize<LotusResponse>(html);

                    matchdropdown.DataSource = getMatches(LotusResult).ToList();
                    matchdropdown.DataTextField = "name";
                    matchdropdown.DataValueField = "id";
                    matchdropdown.DataBind();
                
            }
        }
        protected void submit_Click(object sender, EventArgs e)
        {
            string matchId = matchdropdown.SelectedItem.Value;
            string team1name = "", team2name = "", team3name = "", DateTimeGMT="", lotusMatchID="";
            string CN = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            foreach (Result match in LotusResult.result)
            {
                if (match.@event.id == matchId)
                {
                    team1name = match.runners[0].name;
                    team2name = match.runners[1].name;
                    if (match.runners.Count > 2)
                        team3name = match.runners[2].name;
                    DateTimeGMT = match.@event.openDate.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
                    lotusMatchID = match.groupById;

                }
            }
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
                        match_id = Convert.ToInt32(matchId),
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
                            channel = ""
                        },
                        team_1 = new
                        {
                            Name = team1name,
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
                            Name = team2name,
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
                            Name = team3name,
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



                string insert = " insert into Matches (TeamA,TeamB,TeamC,ApiID,DateTime,LotusmatchID,firebasekey,Apitype,AutoSession,Active,Status) values (@TeamA,@TeamB,@TeamC,@LotusmatchID,@DateTime,@LotusmatchID,@fk,@ApiType,@AutoSession,@Active,@Status)";
                MySqlCommand cmd = new MySqlCommand(insert, cn);
                cmd.Parameters.AddWithValue("@TeamA", team1name);
                cmd.Parameters.AddWithValue("@TeamB", team2name);
                cmd.Parameters.AddWithValue("@Teamc", team3name);
                cmd.Parameters.AddWithValue("@DateTime", DateTimeGMT);
                cmd.Parameters.AddWithValue("@LotusmatchID", lotusMatchID);
                cmd.Parameters.AddWithValue("@fk", fkey);
                cmd.Parameters.AddWithValue("@ApiType", "LOTUS");
                cmd.Parameters.AddWithValue("@AutoSession", 1);
                cmd.Parameters.AddWithValue("@Active", 1);
                cmd.Parameters.AddWithValue("@Status", 1);
                cmd.ExecuteNonQuery();

            }

            Response.Redirect("CreateMatch.aspx?msg=Add");

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
