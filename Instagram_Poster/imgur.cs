using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Instagram_Poster
{
    internal class imgur
    {
        private static string imgurClientId = Environment.GetEnvironmentVariable("imgurClientID");

        public static string uploadToImgur(string localFilePath)
        {
            string uploadedImageUrl = postToImgur(localFilePath);

            if (!string.IsNullOrEmpty(uploadedImageUrl))
            {
                Console.WriteLine("Image uploaded successfully: " + uploadedImageUrl);
                return uploadedImageUrl;
            }
            else
            {
                Console.WriteLine("Failed to upload image.");
            }
            return null;
        }

        private static string postToImgur(string imagFilePath="")
        {
            byte[] imageData;

            FileStream fileStream = File.OpenRead(imagFilePath);
            imageData = new byte[fileStream.Length];
            fileStream.Read(imageData, 0, imageData.Length);
            fileStream.Close();

            string uploadRequestString = "image=" + Uri.EscapeDataString(System.Convert.ToBase64String(imageData)) + "&key=" + imgurClientId;

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("https://api.imgur.com/3/image");
            webRequest.Method = "POST";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.ServicePoint.Expect100Continue = false;

            StreamWriter streamWriter = new StreamWriter(webRequest.GetRequestStream());
            streamWriter.Write(uploadRequestString);
            streamWriter.Close();

            WebResponse response = webRequest.GetResponse();
            Stream responseStream = response.GetResponseStream();
            StreamReader responseReader = new StreamReader(responseStream);

            string responseString = responseReader.ReadToEnd();


            //convert to just link
            using JsonDocument doc = JsonDocument.Parse(responseString);
            JsonElement root = doc.RootElement;
            string link = root.GetProperty("data").GetProperty("link").GetString();

            //Console.WriteLine("Successfully Uploaded to Imgur");
            //Console.WriteLine(link);
            return link;
        }
    }
}
