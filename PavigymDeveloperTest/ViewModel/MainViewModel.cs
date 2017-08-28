using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using log4net;
using PavigymDeveloperTest.Model;
using System;
using System.Configuration;

namespace PavigymDeveloperTest.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MainViewModel));

        // Login service URL
        private const string loginServiceURL = "http://pramacloud.com/api/user/logingym";

        // GetUserData service URL
        private const string getUserDataURL = "http://pramacloud.com/api/user/getData";

        // Service constants
        private readonly int kGym = Convert.ToInt32(ConfigurationManager.AppSettings["Gym"]);
        public readonly string kIso = ConfigurationManager.AppSettings["Iso"];
        public readonly short kIsFavs = Convert.ToInt16(ConfigurationManager.AppSettings["IsFavs"]);

        #region Error handling
        private string errorMsg;
        public string ErrorMsg
        {
            get
            {
                return errorMsg;
            }
            set
            {
                if (errorMsg != value)
                {
                    if (string.IsNullOrWhiteSpace(value))
                        errorMsg = null;
                    else
                        errorMsg = value;

                    RaisePropertyChanged();
                }
            }
        }

        // Service response status
        private const string STATUS_OK = "OK";
        private const string STATUS_ERROR = "ERROR";

        // Error messages
        private const string CONNECTION_ERROR = "Connection error. Please try again later.";
        private const string UNKNOWN_ERROR = "Unknown error. Please try again later.";

        #endregion

        // Login Service Data
        public LoginServiceData LoginData { get; set; }

        // Commands
        public RelayCommand CommandLogin { get; set; }
        public RelayCommand CommandClear { get; set; }
        public RelayCommand CommandClose { get; set; }
        public RelayCommand<string> CommandShowKeyboard { get; set; }
        
        // Keyboard visibility
        private VisibleKeyboard visibleKeyboard;
        public VisibleKeyboard VisibleKeyboard
        {
            get
            {
                return visibleKeyboard;
            }
            set
            {
                if (visibleKeyboard != value)
                {
                    visibleKeyboard = value;
                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            CommandLogin = new RelayCommand(CommandLogin_Execute, CommandLogin_CanExecute);
            CommandClear = new RelayCommand(CommandClear_Execute, CommandClear_CanExecute);
            CommandClose = new RelayCommand(CommandClose_Execute);
            CommandShowKeyboard = new RelayCommand<string>(CommandShowKeyboard_Execute);

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
            CommandClose = null;
            CommandShowKeyboard = null;

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
            ServiceResponse response = null;

            HideErrorMsg();

            // Removing white spaces from login
            LoginData.Login = LoginData.Login.Trim();
            LoginData.Password = LoginData.Password.Trim();

            // Calling Login Service
            log.DebugFormat("Calling the login service with login '{0}'", LoginData.Login);

            try
            {
                response = HTTPRequest.MakeRequest<ServiceResponse>(loginServiceURL, LoginData);
            }
            catch(Exception ex)
            {
                ErrorMsg = CONNECTION_ERROR;
                log.Fatal("Exception calling login service", ex);
                return;
            }

            log.DebugFormat("Login service response. Status: {0}, Token: {1}", response.Status, response.Token);

            if(ProcessServiceResponse(response))
            {
                // Showing user info
                ShowUserInfo(response.Token);
            }
        }
        #endregion

        #region CommandClear
        private bool CommandClear_CanExecute()
        {
            return LoginData != null &&  (!string.IsNullOrWhiteSpace(LoginData.Login) || !string.IsNullOrWhiteSpace(LoginData.Password));
        }

        private void CommandClear_Execute()
        {
            HideErrorMsg();

            LoginData.Login = null;
            LoginData.Password = null;
        }
        #endregion

        #region CommandClose
        private void CommandClose_Execute()
        {
            MessengerInstance.Send<MessageType>(MessageType.CLOSE);
        }
        #endregion

        #region CommandShowKeyboard
        private void CommandShowKeyboard_Execute(string keyboard)
        {
            var selectedKeyboard = (VisibleKeyboard)Enum.Parse(typeof(VisibleKeyboard), keyboard);

            VisibleKeyboard = VisibleKeyboard == selectedKeyboard ? VisibleKeyboard.NONE : selectedKeyboard;
        }
        #endregion

        /// <summary>
        /// Hides the error message panel
        /// </summary>
        private void HideErrorMsg()
        {
            ErrorMsg = null;
        }

        /// <summary>
        /// Show the user info
        /// </summary>
        /// <param name="loginServiceToken">Login service token</param>
        private void ShowUserInfo(string loginServiceToken)
        {
            GetUserInfoServiceResponse response = null;

            // Checking token
            if (string.IsNullOrWhiteSpace(loginServiceToken))
            {
                ErrorMsg = UNKNOWN_ERROR;
                log.Fatal("Unknown error. Token was not present in service response.");
                return;
            }

            // Calling GetUserInfo Service
            log.DebugFormat("Calling the getUserInfo service with token '{0}'", loginServiceToken);

            try
            {
                response = HTTPRequest.MakeRequest<GetUserInfoServiceResponse>(getUserDataURL, token: loginServiceToken);
            }
            catch (Exception ex)
            {
                ErrorMsg = CONNECTION_ERROR;
                log.Fatal("Exception calling getUserInfo service", ex);
                return;
            }

            log.DebugFormat("GetUserInfo service response. Status: {0}", response.Status);

            if (ProcessServiceResponse(response))
            {
                // Checking user data
                if (response.ServiceData == null || response.ServiceData.UserData == null)
                {
                    ErrorMsg = UNKNOWN_ERROR;
                    log.Fatal("Unknown error. User data was not present in service response.");
                    return;
                }

                UserInfoWindow win = new UserInfoWindow(response.ServiceData.UserData);
                win.ShowDialog();
            }
        }

        /// <summary>
        /// Process the service response
        /// </summary>
        /// <param name="serviceResponse">Service response</param>
        /// <returns>True if service returned status OK</returns>
        public bool ProcessServiceResponse(ServiceResponse serviceResponse)
        {
            switch (serviceResponse.Status)
            {
                case STATUS_OK:
                    log.Info("Service returned status OK");
                    return true;

                default:
                    if(serviceResponse.Status != STATUS_ERROR)
                        log.FatalFormat("Unknown error. Status: {0} received form login service.", serviceResponse.Status ?? "<null>");

                    ErrorMsg = string.IsNullOrWhiteSpace(serviceResponse.Msg) ? UNKNOWN_ERROR : serviceResponse.Msg;
                    break;
            }

            return false;
        }
    }
}