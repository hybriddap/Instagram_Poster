using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
            //Define the argument to pass to the batch file
            string argument = imagFilePath;

            //Console.WriteLine(imagFilePath);

            string exeDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string batScriptPath = Path.Combine(exeDirectory, "imgur.bat");

            ProcessStartInfo ProcessInfo;
            Process process;

            ProcessInfo = new ProcessStartInfo(batScriptPath, argument);
            ProcessInfo.CreateNoWindow = true;
            ProcessInfo.UseShellExecute = false;
            ProcessInfo.RedirectStandardOutput = true;
            ProcessInfo.RedirectStandardError = true;

            process = Process.Start(ProcessInfo);

            //Read the output
            string output = process.StandardOutput.ReadToEnd();
            string errors = process.StandardError.ReadToEnd();

            int startIndex = output.IndexOf("{\"status\"");
            string result = output.Substring(startIndex);

            process.WaitForExit();
            process.Close();

            //Console.WriteLine("Output: " + output);
            //Console.WriteLine("Errors: " + errors);
            using JsonDocument doc = JsonDocument.Parse(result);
            JsonElement root = doc.RootElement;
            string link = root.GetProperty("data").GetProperty("link").GetString();
            //Console.WriteLine(link);

            return link;
        }
    }
}
