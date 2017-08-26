using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace PavigymDeveloperTest.Model
{
    [DataContract]
    public class LoginServiceData : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        [DataMember(Name = "id_name")]
        public string Login
        {
            get
            {
                return login;
            }
            set
            {
                login = value;
                RaisePropertyChanged();
            }
        }
        private string login;

        [DataMember(Name = "pin")]
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                RaisePropertyChanged();
            }
        }
        private string password;

        [DataMember(Name = "gym")]
        public int Gym { get; set; }

        [DataMember(Name = "iso")]
        public string Iso { get; set; }

        [DataMember(Name = "isFavs")]
        public short IsFavs { get; set; }
    }
}
