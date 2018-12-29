using System;
using System.Collections.Generic;
using System.Net;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Web.Script.Serialization;

namespace betplayer.superagent
{
    class sendSMS
    {
        public string SendSMS(string numbers, string message)
        {
            String result;
            string apiKey = "iH3pD1uKFSU-UoYzcm4VzISDHDQ91uy9LslJb2m5ox";
            string username = "hemantagrawal16061991@gmail.com";
            string password = "livecricADMIN@12345";
            string hash = "a4dd7a4a8dec1dbdd583a49600464f6b6727e8d43e593b9d24be015131f14814";
            string sender = "SHREEG";
            string Templates = "TEST";

            String url = "https://api.textlocal.in/send/?";
            //refer to parameters to complete correct url string

            StreamWriter myWriter = null;
            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(url);

            var postData = "apikey = " + apiKey;
            postData += " & numbers=" + numbers;
            postData += "&message=" + message;
            postData += "&sender=" + sender;
            postData += "&Templates=" + Templates;
            postData += "&hash=" + hash;
            postData += "&username=" + username;
            postData += "&password=" + password;
            var data = Encoding.ASCII.GetBytes(postData);

            objRequest.Method = "POST";
            objRequest.ContentLength = data.Length;
            objRequest.ContentType = "application/x-www-form-urlencoded";

            /*try
            {
                myWriter = new StreamWriter(objRequest.GetRequestStream());
                myWriter.Write(data, 0, data.Length);
            }
            catch (Exception e)
            {
                return e.Message;
            }
            finally
            {
                myWriter.Close();
            }*/
            using (var stream = objRequest.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
            using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
            {
                result = sr.ReadToEnd();
                // Close and clean up the StreamReader
                sr.Close();
            }
            var serializer = new JavaScriptSerializer();
            var response = serializer.Deserialize<TextLocalResponse>(result);
            return response.status;
        }
    }
    public class Message
    {
        public int num_parts { get; set; }
        public string sender { get; set; }
        public string content { get; set; }
    }

    public class Message2
    {
        public string id { get; set; }
        public long recipient { get; set; }
    }

    public class TextLocalResponse
    {
        public int balance { get; set; }
        public int batch_id { get; set; }
        public int cost { get; set; }
        public int num_messages { get; set; }
        public Message message { get; set; }
        public string receipt_url { get; set; }
        public string custom { get; set; }
        public List<Message2> messages { get; set; }
        public string status { get; set; }
    }
}