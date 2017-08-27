using System.Runtime.Serialization;

namespace PavigymDeveloperTest.Model
{
    /// <summary>
    /// Service response base model
    /// </summary>
    [DataContract]
    public class ServiceResponse
    {
        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "token")]
        public string Token { get; set; }

        [DataMember(Name = "msg")]
        public string Msg { get; set; }
    }
}
