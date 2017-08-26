using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace JSONUtils
{
    class JSONConverter
    {
        /// <summary>
        /// Serializes an object as JSON
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="obj">Object instance</param>
        /// <returns>JSON string representing obj</returns>
        public static string ObjectToJSON<T>(T obj)
        {
            MemoryStream stream1 = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));

            ser.WriteObject(stream1, obj);

            stream1.Position = 0;
            StreamReader sr = new StreamReader(stream1);

            return sr.ReadToEnd();
        }
    }
}
