using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using ZzukBot.GUI.Utilities.Commands;
using ZzukBot.Settings;

namespace ZzukBot.GUI.ViewModels.Abstractions
{
    internal abstract class ViewModel : INotifyPropertyChanged
    {
        internal readonly CommandHandler Commands;

        internal ViewModel()
        {
            Commands = new CommandHandler(this);
        }

        private DialogResult? result;
        public DialogResult? Result
        {
            get => result;
            set
            {
                result = value;
                OnPropertyChanged();
            }
        }

        private bool isEnabled = true;
        public bool IsEnabled
        {
            get => isEnabled;
            set
            {
                isEnabled = value;
                OnPropertyChanged();
            }
        }

        public string WindowTitle => $"{Default.ProcessName} - {Assembly.GetExecutingAssembly().GetName().Version}";

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
