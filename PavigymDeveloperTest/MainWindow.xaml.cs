﻿using GalaSoft.MvvmLight.Messaging;
using System.Windows;

namespace PavigymDeveloperTest
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Listen to messages from viewmodel
            Messenger.Default.Register<MessageType>(this, OnMessageReceived);
        }

        private void OnMessageReceived(MessageType msg)
        {
            switch(msg)
            {
                // Close window
                case MessageType.CLOSE:
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
