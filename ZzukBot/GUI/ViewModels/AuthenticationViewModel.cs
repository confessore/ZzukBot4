using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using ZzukBot.Core.Authentication;
using ZzukBot.GUI.ViewModels.Abstractions;
using ZzukBot.Settings;
using DialogResult = System.Windows.Forms.DialogResult;

namespace ZzukBot.GUI.ViewModels
{
    internal class AuthenticationViewModel : ViewModel
    {
        internal AuthenticationViewModel()
        {
            PropertyChanged += OnPropertyChanged;
            Commands.Register("AuthenticateCommand", CanAuthenticate, Authenticate);
            Email = Default.BotAccount;
            RememberPassword = Default.RememberPassword;

            if (Default.RememberPassword && Default.BotPassword != string.Empty)
                Password = Default.BotPassword;
        }

        string email;
        public string Email
        {
            get => email;
            set
            {
                email = value;
                Default.BotAccount = Email;
                OnPropertyChanged();
            }
        }

        string password;
        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }

        bool rememberPassword;
        public bool RememberPassword
        {
            get => rememberPassword;
            set
            {
                rememberPassword = value;
                Default.RememberPassword = RememberPassword;
                OnPropertyChanged();
            }
        }

        void Authenticate()
        {
            if (Default.RememberPassword)
                Default.BotPassword = Password;
            else
                Default.BotPassword = string.Empty;

            if (new SslAuthClientAsync(Email, Password).StartClient(out string reason))
            {
                if (reason == string.Empty)
                    Application.Current.Dispatcher.Invoke(() => { Result = DialogResult.OK; });
                else
                {
                    Application.Current.Dispatcher.Invoke(() => { Result = DialogResult.OK; });
                    MessageBox.Show($"{reason}. The application will close after 30 minutes");
                    new Thread(async () =>
                    {
                        await Task.Delay(1800000);
                        Environment.Exit(0);
                    }).Start();
                }
            }
            else
            {
                if (reason.Contains("Update"))
                {
                    MessageBox.Show(reason);
                    var start = new ProcessStartInfo
                    {
                        FileName = "Launcher.exe",
                        WorkingDirectory = Paths.Release,
                        Arguments = "UPDATE"
                    };
                    Process.Start(start);
                    Environment.Exit(0);
                }
                else
                    MessageBox.Show(reason);
            }
        }

        bool CanAuthenticate()
        {
            if (!Email.Contains('@')) return false;
            if (Password == null || Password.Length < 3) return false;
            return true;
        }

        void OnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
        }
    }
}
