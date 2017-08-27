using GalaSoft.MvvmLight.Messaging;
using PavigymDeveloperTest.Model;
using PavigymDeveloperTest.ViewModel;
using System.Windows;

namespace PavigymDeveloperTest
{
    public partial class UserInfoWindow : Window
    {
        public UserInfoWindow(UserData user)
        {
            InitializeComponent();

            // Set user info
            ViewModelLocator.UserInfoViewModel.User = user;

            // Listen to messages from viewmodel
            Messenger.Default.Register<MessageType>(this, OnMessageReceived);
        }

        private void OnMessageReceived(MessageType msg)
        {
            switch (msg)
            {
                // Close window
                case MessageType.LOGOUT:
                    Close();
                    break;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Unregistering messaging
            Messenger.Default.Unregister<MessageType>(this, OnMessageReceived);
        }
    }
}
