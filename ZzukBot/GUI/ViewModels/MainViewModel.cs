using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Threading;
using ZzukBot.Core.Constants;
using ZzukBot.Core.Framework;
using ZzukBot.Core.Framework.Interfaces;
using ZzukBot.Core.Game;
using ZzukBot.Core.Game.Statics;
using ZzukBot.Core.Utilities.Extensions;
using ZzukBot.GUI.Utilities.Extensions;
using ZzukBot.GUI.ViewModels.Abstractions;
using ZzukBot.Properties;
using ZzukBot.Settings;

namespace ZzukBot.GUI.ViewModels
{
    internal class MainViewModel : ViewModel
    {
        [DllImport("cleverbot.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern string cleverbot(string message);

        internal MainViewModel()
        {
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingD‌​efault(false);

            PropertyChanged += OnPropertyChanged;

            AvailableBotBases = new ObservableCollection<IBotBase>(BotBases.Instance.Items);
            AvailablePlugins = new ObservableCollection<PluginInfo>(Plugins.Instance.Items.Select(x => new PluginInfo(x, ShouldAutoLoad(x))));
            ChatMessages = new ObservableCollection<WoWEventHandler.ChatMessageArgs>();
            Invites = new ObservableCollection<string>();
            LoadedPlugins = new ObservableCollection<IPlugin>(AvailablePlugins.Where(x => x.AutoLoad).Select(x => x.Plugin));
            LootedItems = new ObservableCollection<WoWEventHandler.OnLootArgs>();
            Stats = new Statistics();

            Commands.Register<IPlugin>("PluginToggledCommand", plugin => true, LoadPlugin);
            Commands.Register("ReloadBotBasesCommand", CanReloadBotBases, ReloadBotBases);
            Commands.Register("ReloadPluginsCommand", CanReloadPlugins, ReloadPlugins);
            Commands.Register("ResetChatLogCommand", ChatMessages.Clear);
            Commands.Register("ResetDebugLogCommand", SharedViewModel.Instance.DebugLog.Clear);
            Commands.Register("ResetInviteLogCommand", Invites.Clear);
            Commands.Register("ResetLootLogCommand", LootedItems.Clear);
            Commands.Register("ResetStatsCommand", ResetStats);
            Commands.Register("SaveSettingsCommand", SaveSettings);
            Commands.Register("ShowBotBaseGuiCommand", CanShowBotBaseGUI, ShowBotBaseGUI);
            Commands.Register("ShowPluginGuiCommand", CanShowPluginGUI, ShowPluginGUI);
            Commands.Register("StartBotBaseCommand", CanStartBotBase, StartBotBase);
            Commands.Register("StopBotBaseCommand", CanStopBotBase, StopBotBase);

            var dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += UpdaterTick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 60);
            dispatcherTimer.Start();

            Activatecleverbot = Default.Activatecleverbot;
            BeepName = Default.BeepName;
            BeepSay = Default.BeepSay;
            BeepWhisper = Default.BeepWhisper;
            BeepYell = Default.BeepYell;
            GameAccount = Default.GameAccount;
            GamePassword = Default.GamePassword;
            LogChat = Default.LogChat;
            LogInvites = Default.LogInvites;

            WoWEventHandler.Instance.OnChatMessage += OnWoWChatMessage;
            WoWEventHandler.Instance.OnDuelRequest += OnWoWDuelRequest;
            WoWEventHandler.Instance.OnGuildInvite += OnWoWGuildInvite;
            WoWEventHandler.Instance.OnLoot += OnWoWLoot;
            WoWEventHandler.Instance.OnPartyInvite += OnWoWPartyInvite;

            PluginLoaderEnabled = true;
        }

        bool activatecleverbot;
        public bool Activatecleverbot
        {
            get => activatecleverbot;
            set
            {
                activatecleverbot = value;
                Default.Activatecleverbot = value;
                OnPropertyChanged();
            }
        }

        ObservableCollection<IBotBase> availableBotBases;
        public ObservableCollection<IBotBase> AvailableBotBases
        {
            get => availableBotBases;
            set
            {
                availableBotBases = value;
                if (value.Count == 0) return;
                CurrentBotBase = value[0];
                OnPropertyChanged();
            }
        }

        ObservableCollection<PluginInfo> availablePlugins;
        public ObservableCollection<PluginInfo> AvailablePlugins
        {
            get => availablePlugins;
            set
            {
                availablePlugins = value;
                OnPropertyChanged();
            }
        }

        bool beepName;
        public bool BeepName
        {
            get => beepName;
            set
            {
                beepName = value;
                Default.BeepName = value;
                OnPropertyChanged();
            }
        }

        bool beepSay;
        public bool BeepSay
        {
            get => beepSay;
            set
            {
                beepSay = value;
                Default.BeepSay = value;
                OnPropertyChanged();
            }
        }

        bool beepWhisper;
        public bool BeepWhisper
        {
            get => beepWhisper;
            set
            {
                beepWhisper = value;
                Default.BeepWhisper = value;
                OnPropertyChanged();
            }
        }

        bool beepYell;
        public bool BeepYell
        {
            get => beepYell;
            set
            {
                beepYell = value;
                Default.BeepYell = value;
                OnPropertyChanged();
            }
        }

        bool botBaseRunning;
        public bool BotBaseRunning
        {
            get => botBaseRunning;
            set
            {
                botBaseRunning = value;
                OnPropertyChanged();
            }
        }

        ObservableCollection<WoWEventHandler.ChatMessageArgs> chatMessages;
        public ObservableCollection<WoWEventHandler.ChatMessageArgs> ChatMessages
        {
            get => chatMessages;
            set
            {
                chatMessages = value;
                OnPropertyChanged();
            }
        }

        IBotBase currentBotBase;
        public IBotBase CurrentBotBase
        {
            get => currentBotBase;
            set
            {
                currentBotBase = value;
                OnPropertyChanged();
            }
        }

        PluginInfo currentPlugin;
        public PluginInfo CurrentPlugin
        {
            get => currentPlugin;
            set
            {
                currentPlugin = value;
                OnPropertyChanged();
            }
        }

        string gameAccount;
        public string GameAccount
        {
            get => gameAccount;
            set
            {
                gameAccount = value;
                OnPropertyChanged();
            }
        }

        string gamePassword;
        public string GamePassword
        {
            get => gamePassword;
            set
            {
                gamePassword = value;
                OnPropertyChanged();
            }
        }

        ObservableCollection<string> invites;
        public ObservableCollection<string> Invites
        {
            get => invites;
            set
            {
                invites = value;
                OnPropertyChanged();
            }
        }

        ObservableCollection<IPlugin> loadedPlugins;
        public ObservableCollection<IPlugin> LoadedPlugins
        {
            get => loadedPlugins;
            set
            {
                loadedPlugins = value;
                OnPropertyChanged();
            }
        }

        bool logChat;
        public bool LogChat
        {
            get => logChat;
            set
            {
                logChat = value;
                Default.LogChat = value;
                OnPropertyChanged();
            }
        }

        bool logInvites;
        public bool LogInvites
        {
            get => logInvites;
            set
            {
                logInvites = value;
                Default.LogInvites = value;
                OnPropertyChanged();
            }
        }

        ObservableCollection<WoWEventHandler.OnLootArgs> lootedItems;
        public ObservableCollection<WoWEventHandler.OnLootArgs> LootedItems
        {
            get => lootedItems;
            set
            {
                lootedItems = value;
                OnPropertyChanged();
            }
        }

        bool pluginLoaderEnabled;
        public bool PluginLoaderEnabled
        {
            get => pluginLoaderEnabled;
            set
            {
                pluginLoaderEnabled = value;
                OnPropertyChanged();
            }
        }

        private void SaveSettings()
        {
            Default.GameAccount = GameAccount;
            Default.GamePassword = GamePassword;
            Default.BeepName = BeepName;
            Default.BeepSay = BeepSay;
            Default.BeepYell = BeepYell;
            Default.LogChat = LogChat;
            Default.LogInvites = LogInvites;
        }

        Statistics stats;
        public Statistics Stats
        {
            get => stats;
            set
            {
                stats = value;
                OnPropertyChanged();
            }
        }

        void BotBaseCallBack()
        {
            new Action(() => { BotBaseRunning = false; }).BeginDispatch(DispatcherPriority.Normal);
        }

        bool CanReloadBotBases()
        {
            return !BotBaseRunning;
        }

        bool CanReloadPlugins()
        {
            if (!PluginLoaderEnabled) return false;
            return LoadedPlugins.Count == 0;
        }

        bool CanShowBotBaseGUI()
        {
            return CurrentBotBase != null;
        }

        bool CanShowPluginGUI()
        {
            return CurrentPlugin != null && PluginLoaderEnabled;
        }

        bool CanStartBotBase()
        {
            if (CurrentBotBase == null) return false;
            return !BotBaseRunning;
        }

        bool CanStopBotBase()
        {
            if (CurrentBotBase == null) return false;
            return BotBaseRunning;
        }

        void LoadPlugin(IPlugin plugin)
        {
            var savePlugin = $"{plugin.Name}_{plugin.Author}";
            var temp = Default.LoadedPlugins;
            if (LoadedPlugins.Contains(plugin))
            {
                LoadedPlugins.Remove(plugin);

                temp.Remove(savePlugin);
                Default.LoadedPlugins = temp;

                plugin.Unload();
                return;
            }
            LoadedPlugins.Add(plugin);

            temp.Add(savePlugin);
            Default.LoadedPlugins = temp;

            plugin.Load();
        }

        void OnWoWChatMessage(object sender, WoWEventHandler.ChatMessageArgs args)
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (Action)(() =>
            {
                try
                {
                    ChatMessages.Add(args);
                    if (args.ChatChannel == "Say" && BeepSay)
                        PlayBeep();
                    if (args.ChatChannel == "Whisper")
                    {
                        if (BeepWhisper)
                            PlayBeep();
                        if (Activatecleverbot)
                        {
                            try
                            {
                                Lua.Instance.SendWhisper(cleverbot(args.Message), args.UnitName);
                            }
                            catch(Exception e)
                            {
                                Console.WriteLine(e);
                            }
                        }
                    }
                    if (args.ChatChannel == "Yell" && BeepYell)
                        PlayBeep();
                    if (args.Message.ToLower().Contains(Stats.ToonName) && BeepName)
                        PlayBeep();

                    if (!LogChat) return;
                    args.ToString().Log(Logs.Chat);
                }
                catch
                {
                }
            }));
        }

        void OnWoWDuelRequest(object sender, WoWEventHandler.OnRequestArgs args)
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (Action)(() =>
            {
                var logItem = "[" + DateTime.Now.ToShortTimeString() + "] [" + args.Player + "] Duel request";
                Invites.Add(logItem);
                if (!LogInvites) return;
                logItem.Log(Logs.Invite);
            }));
        }

        void OnWoWGuildInvite(object sender, WoWEventHandler.GuildInviteArgs args)
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (Action)(() =>
            {
                var logItem = "[" + DateTime.Now.ToShortTimeString() + "] [" + args.Player + "] Guild invite to " +
                              args.Guild;
                Invites.Add(logItem);
                if (!LogInvites) return;
                logItem.Log(Logs.Invite);
            }));
        }

        void OnWoWLoot(object sender, WoWEventHandler.OnLootArgs args)
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                (Action)(() => { LootedItems.Add(args); }));
        }

        void OnWoWPartyInvite(object sender, WoWEventHandler.OnRequestArgs args)
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (Action)(() =>
            {
                var logItem = "[" + DateTime.Now.ToShortTimeString() + "] [" + args.Player + "] Party invite";
                Invites.Add(logItem);
                if (!LogInvites) return;
                logItem.Log(Logs.Invite);
            }));
        }

        private void PlayBeep()
        {
            WinImports.PlaySound(Resources.beep, IntPtr.Zero,
                WinImports.SoundFlags.SND_ASYNC | WinImports.SoundFlags.SND_MEMORY);
        }

        void ReloadBotBases()
        {
            AvailableBotBases.Clear();
            BotBases.Instance.Refresh();
            foreach (var item in BotBases.Instance.Items)
                AvailableBotBases.Add(item);
            CurrentBotBase = AvailableBotBases.FirstOrDefault();
        }

        void ReloadPlugins()
        {
            AvailablePlugins.Clear();
            Plugins.Instance.Refresh();
            foreach (var item in Plugins.Instance.Items)
                AvailablePlugins.Add(new PluginInfo(item, ShouldAutoLoad(item)));
            CurrentPlugin = AvailablePlugins.FirstOrDefault();
        }

        void ResetStats()
        {
            Stats.Dispose();
            Stats = new Statistics();
        }

        bool ShouldAutoLoad(IPlugin plugin)
        {
            var savePlugin = $"{plugin.Name}_{plugin.Author}";
            return Default.LoadedPlugins.Any(x => x == savePlugin);
        }

        void ShowBotBaseGUI()
        {
            CurrentBotBase.ShowGui();
        }

        void ShowPluginGUI()
        {
            CurrentPlugin?.Plugin.ShowGui();
        }

        void StartBotBase()
        {
            BotBaseRunning = CurrentBotBase.Start(BotBaseCallBack);
        }

        void StopBotBase()
        {
            CurrentBotBase.Stop();
        }

        void UpdaterTick(object sender, EventArgs args)
        {
            var player = ObjectManager.Instance.Player;
            if (player == null) return;
        }

        public class PluginInfo
        {
            public IPlugin Plugin { get; set; }
            public bool AutoLoad { get; set; }

            public PluginInfo(IPlugin plugin, bool autoLoad)
            {
                Plugin = plugin;
                AutoLoad = autoLoad;
            }
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
        }
    }
}
