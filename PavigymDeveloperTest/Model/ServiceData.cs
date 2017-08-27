using System.Runtime.Serialization;

namespace PavigymDeveloperTest.Model
{
    /// <summary>
    /// Model for service data
    /// </summary>
    [DataContract]
    public class ServiceData
    {
        [DataMember(Name = "data")]
        public UserData UserData { get; set; }
    }
}
