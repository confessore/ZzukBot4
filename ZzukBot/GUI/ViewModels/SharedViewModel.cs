using System.Collections.ObjectModel;
using ZzukBot.GUI.ViewModels.Abstractions;

namespace ZzukBot.GUI.ViewModels
{
    internal class SharedViewModel : ViewModel
    {
        public static SharedViewModel Instance = new SharedViewModel();
        private ObservableCollection<string> _debugLog;

        private SharedViewModel()
        {
            DebugLog = new ObservableCollection<string>();
        }

        public ObservableCollection<string> DebugLog
        {
            get { return _debugLog; }
            set
            {
                _debugLog = value;
                OnPropertyChanged();
            }
        }
    }
}
