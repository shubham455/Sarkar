﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using System.Net;
using System.IO;



namespace betplayer.PowerUser
{
    /// <summary>
    /// Summary description for Sechdule
    /// </summary>
    public class Sechdule : IHttpHandler, IReadOnlySessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/json";
            int UserID = Convert.ToInt16(context.Session["PoweruserID"]);
            if (IsClient(UserID))
            {
                string CN = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                using (MySqlConnection cn = new MySqlConnection(CN))
                {
                    cn.Open();
                    string html = string.Empty;
                    string s = "Select ApiType from Matches where MatchesID = '" + context.Request["matchesID"].ToString() + "' ";
                    MySqlCommand cmd = new MySqlCommand(s, cn);
                    MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);
                    string apitype = dt.Rows[0]["ApiType"].ToString();
                    string url = "";
                    if (apitype == "ESPN-LOTUS")
                    {
                        url = @"http://localhost:3000/schedule/Espn?apiID=" + context.Request["apiID"].ToString() + "&matchesID=" + context.Request["matchesID"].ToString() + "&sessionapi=Lotus";
                    }
                    else if (apitype == "LOTUS")
                    {
                        url = @"http://localhost:3000/schedule/Lotus?apiID=" + context.Request["apiID"].ToString() + "&matchesID=" + context.Request["matchesID"].ToString();

                    }
                    else if (apitype == "ESPN-DIAMOND")
                    {
                        url = @"http://localhost:3000/schedule/Diamond?apiID=" + context.Request["apiID"].ToString() + "&matchesID=" + context.Request["matchesID"].ToString() + "&sessionapi=Diamond";

                    }
                    else if (apitype == "DIAMOND")
                    {
                        url = @"http://localhost:3000/schedule/Diamond?apiID=" + context.Request["apiID"].ToString() + "&matchesID=" + context.Request["matchesID"].ToString();

                    }
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    request.AutomaticDecompression = DecompressionMethods.GZip;

                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    using (Stream stream = response.GetResponseStream())
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        html = reader.ReadToEnd();

                        string Result = html;
                        if (Result == "success")
                            context.Response.Write(new JavaScriptSerializer().Serialize(new
                            {
                                status = true,
                                Result = Result
                            }));
                        else context.Response.Write(new JavaScriptSerializer().Serialize(new
                        {
                            status = false,
                            error = Result
                        }));
                    }
                }
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
                string SELECT = "Select PowerUserID from PoweruserMaster  Where PowerUserID = '" + id + "'";
                MySqlCommand cmd = new MySqlCommand(SELECT, cn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read()) return true;
                else return false;

            }
        }

    }
}
