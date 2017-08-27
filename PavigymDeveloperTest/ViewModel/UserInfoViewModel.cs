using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PavigymDeveloperTest.Model;

namespace PavigymDeveloperTest.ViewModel
{
    public class UserInfoViewModel : ViewModelBase
    {
        // User data
        private UserData user;
        public UserData User
        {
            get
            {
                return user;
            }
            set
            {
                if (user != value)
                {
                    user = value;
                    RaisePropertyChanged();
                }
            }
        }

        // Commands
        public RelayCommand CommandLogOut { get; set; }

        public UserInfoViewModel()
        {
            CommandLogOut = new RelayCommand(CommandLogOut_Execute);
        }

        public override void Cleanup()
        {
            CommandLogOut = null;

            User = null;

            base.Cleanup();
        }

        private void CommandLogOut_Execute()
        {
            MessengerInstance.Send<MessageType>(MessageType.LOGOUT);
        }

    }
}
