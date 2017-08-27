using System.Runtime.Serialization;

namespace PavigymDeveloperTest.Model
{
    /// <summary>
    /// Service response model for GetUserInfo service
    /// </summary>
    [DataContract]
    public class GetUserInfoServiceResponse : ServiceResponse
    {
        [DataMember(Name = "data")]
        public ServiceData ServiceData { get; set; }
    }
}
