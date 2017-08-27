using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PavigymDeveloperTest.Model
{
    /// <summary>
    /// Model for user data
    /// </summary>
    [DataContract]
    public class UserData
    {
        [DataMember(Name = "id")]
        public string UserId { get; set; }

        [DataMember(Name = "id_name")]
        public string Login { get; set; }

        [DataMember(Name = "fullname")]
        public string FullName { get; set; }

        [DataMember(Name = "avatar")]
        public string Avatar { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "sexo")]
        public string Sex { get; set; }

        [DataMember(Name = "birthday")]
        public string BirthDate { get; set; }

        [DataMember(Name = "height")]
        public int Height { get; set; }

        [DataMember(Name = "weight")]
        public int Weight { get; set; }

        [DataMember(Name = "bmp_max")]
        public int BmpMax { get; set; }

        [DataMember(Name = "heartrates")]
        public List<object> HeartRates { get; set; }

        [DataMember(Name = "total_calories")]
        public string TotalCalories { get; set; }

        [DataMember(Name = "total_points")]
        public string TotalPoints { get; set; }

        [DataMember(Name = "level")]
        public string Level { get; set; }

        [DataMember(Name = "iat")]
        public int Iat { get; set; }

        [DataMember(Name = "exp")]
        public int Exp { get; set; }

        [DataMember(Name = "isPersonalTrainer")]
        public int IsPersonalTrainer { get; set; }

        public bool IsPersonalTrainerBit
        {
            get
            {
                return IsPersonalTrainer > 0;
            }
        }

        [DataMember(Name = "isRecordVisual")]
        public bool IsRecordVisual { get; set; }
    }
}
