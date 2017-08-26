using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using JSONUtils;
using log4net;
using PavigymDeveloperTest.Model;
using System.IO;
using System.Net.Http;

namespace PavigymDeveloperTest.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MainViewModel));

        #region Login Service
        // URL
        private const string loginServiceURL = "http://pramacloud.com/api/user/logingym";

        // Constants
        private const int kGym = 1;
        public const string kIso = "ES";
        public const short kIsFavs = 0;
        #endregion

        // Login Service Data
        public LoginServiceData LoginData { get; set; }

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

            LoginData = new LoginServiceData()
            {
                Gym = kGym,
                Iso = kIso,
                IsFavs = kIsFavs
            };
        }

        public override void Cleanup()
        {
            CommandLogin = null;
            CommandClear = null;
            LoginData = null;

            base.Cleanup();
        }

        #region CommandLogin
        private bool CommandLogin_CanExecute()
        {
            return LoginData != null && !string.IsNullOrWhiteSpace(LoginData.Login) && !string.IsNullOrWhiteSpace(LoginData.Password);
        }

        private void CommandLogin_Execute()
        {
            // Removing white spaces
            LoginData.Login = LoginData.Login.Trim();
            LoginData.Password = LoginData.Password.Trim();

            // Calling the HTTP Service
            log.DebugFormat("Calling the login service for Username '{0}'", LoginData.Login);
            string response = JSONRequest.MakeRequest<LoginServiceData>(loginServiceURL, LoginData);

            System.Windows.MessageBox.Show(response);
        }
        #endregion

        #region CommandClear
        private bool CommandClear_CanExecute()
        {
            return LoginData != null &&  !string.IsNullOrWhiteSpace(LoginData.Login) || !string.IsNullOrWhiteSpace(LoginData.Password);
        }

        private void CommandClear_Execute()
        {
            LoginData.Login = null;
            LoginData.Password = null;
        }
        #endregion
    }
}