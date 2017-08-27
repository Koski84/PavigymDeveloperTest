using Newtonsoft.Json;
using PavigymDeveloperTest.Model;
using System.IO;
using System.Net;

namespace PavigymDeveloperTest
{
    internal class HTTPRequest
    {

        /// <summary>
        /// Makes a HTTP web request to a service
        /// </summary>
        /// <typeparam name="T">Type of service response</typeparam>
        /// <param name="url">Service url</param>
        /// <param name="obj">obj sent as JSON</param>
        /// <param name="token">token from previous service calls if needed</param>
        /// <returns>JSON service response</returns>
        public static T MakeRequest<T>(string url, object obj = null, string token = null)
        {
            string jsonData = null;

            // Serializing object to JSON
            if(obj != null)
                jsonData = JsonConvert.SerializeObject(obj); 

            // Request
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            httpWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.101 Safari/537.36";

            // If token is present it is included in the headers
            if (!string.IsNullOrWhiteSpace(token))
                httpWebRequest.Headers.Add("Authorization", string.Format("bearer {0}", token));

            // if we have json data we send it to the service
            if (!string.IsNullOrWhiteSpace(jsonData))
            {
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(jsonData);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
            }

            // Getting serveice response
            string response;
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                response = streamReader.ReadToEnd();
            }

            // Response to object
            return JsonConvert.DeserializeObject<T>(response);
        }
    }
}
