using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace PavigymDeveloperTest.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private const string loginService = "http://pramacloud.com/api/user/logingym";

        // User Login
        private string login;
        public string Login
        {
            get
            {
                return login;
            }
            set
            {
                login = value;
                RaisePropertyChanged("Login");
            }
        }

        // User Password
        private string password;
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                RaisePropertyChanged("Password");
            }
        }

        // Commands
        public RelayCommand CommandLogin { get; set; }
        public RelayCommand CommandClear { get; set; }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            CommandLogin = new RelayCommand(CommandLogin_Execute, CommandLogin_CanExecute);
            CommandClear = new RelayCommand(CommandClear_Execute, CommandClear_CanExecute);
        }

        public override void Cleanup()
        {
            CommandLogin = null;
            CommandClear = null;

            base.Cleanup();
        }

        private bool CommandLogin_CanExecute()
        {
            return !string.IsNullOrWhiteSpace(Login) && !string.IsNullOrWhiteSpace(Password);
        }

        private void CommandLogin_Execute()
        {
            // TODO Login
        }

        private bool CommandClear_CanExecute()
        {
            return !string.IsNullOrWhiteSpace(Login) || !string.IsNullOrWhiteSpace(Password);
        }

        private void CommandClear_Execute()
        {
            Login = null;
            Password = null;
        }
    }
}