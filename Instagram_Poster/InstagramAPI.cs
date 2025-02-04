using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Instagram_Poster
{
    internal class InstagramAPI
    {
        private static string accessToken = Environment.GetEnvironmentVariable("accessToken");
        private static string instagramUserId = Environment.GetEnvironmentVariable("instagramUserId");

        public static string uploadPhotoToInstagram(string imageUrl, string caption)
        {
            string uploadUrl = $"https://graph.facebook.com/v22.0/{instagramUserId}/media";
            string postData = $"image_url={imageUrl}&caption={caption}&access_token={accessToken}";

            try
            {
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(uploadUrl);
                webRequest.Method = "POST";
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.ServicePoint.Expect100Continue = false;

                StreamWriter streamWriter = new StreamWriter(webRequest.GetRequestStream());
                streamWriter.Write(postData);
                streamWriter.Close();

                WebResponse response = webRequest.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader responseReader = new StreamReader(responseStream);
                string responseString = responseReader.ReadToEnd();


                //convert to just id
                using JsonDocument doc = JsonDocument.Parse(responseString);
                JsonElement root = doc.RootElement;
                string id = root.GetProperty("id").GetString();

                //Console.WriteLine(id);
                Console.WriteLine("Successfully uploaded to Instagram");
                return id;
            }
            catch (WebException ex)
            {
                using (var stream = ex.Response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    string errorResponse = reader.ReadToEnd();
                    Console.WriteLine("Upload Error: " + errorResponse);
                }
                return null;
            }
        }

        public static void publishPhoto(string id)
        {
            string uploadUrl = $"https://graph.facebook.com/v22.0/{instagramUserId}/media_publish";
            string postData = $"creation_id={id}&access_token={accessToken}";

            try
            {
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(uploadUrl);
                webRequest.Method = "POST";
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.ServicePoint.Expect100Continue = false;

                StreamWriter streamWriter = new StreamWriter(webRequest.GetRequestStream());
                streamWriter.Write(postData);
                streamWriter.Close();

                WebResponse response = webRequest.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader responseReader = new StreamReader(responseStream);
                string responseString = responseReader.ReadToEnd();

                Console.WriteLine("Successfully Published!");
                return;
            }
            catch (WebException ex)
            {
                using (var stream = ex.Response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    string errorResponse = reader.ReadToEnd();
                    Console.WriteLine("Upload Error: " + errorResponse);
                }
                return;
            }
        }
    }
}
