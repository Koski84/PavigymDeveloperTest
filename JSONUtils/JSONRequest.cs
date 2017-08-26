using log4net;
using System.IO;
using System.Net;

namespace JSONUtils
{
    public class JSONRequest
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(JSONRequest));

        /// <summary>
        /// Makes a JSON request to a HTTP Service
        /// </summary>
        /// <typeparam name="T">Type of the object</typeparam>
        /// <param name="url">Service URL</param>
        /// <param name="obj">Object instance to be sent</param>
        /// <returns>Service response</returns>
        public static string MakeRequest<T>(string url, T obj)
        {
            string json = JSONConverter.ObjectToJSON<T>(obj);

            log.DebugFormat("JSON (source type: {0}): {1}", typeof(T), json);

            return MakeRequest(url, json);
        }

        /// <summary>
        /// Makes a JSON request to a HTTP Service
        /// </summary>
        /// <param name="url">Service URL</param>
        /// <param name="json">JSON data</param>
        /// <returns>Service response</returns>
        public static string MakeRequest(string url, string json)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                return streamReader.ReadToEnd();
            }
        }
    }
}
