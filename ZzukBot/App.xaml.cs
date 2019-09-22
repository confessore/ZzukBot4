using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ZzukBot.Core.Constants;
using ZzukBot.Core.Framework;
using ZzukBot.Core.Mem;
using ZzukBot.Core.Utilities.Extensions;
using ZzukBot.Core.Utilities.GreyMagic;
using ZzukBot.GUI.Utilities.Commands;
using ZzukBot.GUI.ViewModels;
using ZzukBot.GUI.Views;
using ZzukBot.Settings;
using DialogResult = System.Windows.Forms.DialogResult;
using Form = System.Windows.Forms.Form;
using MessageBox = System.Windows.Forms.MessageBox;
using OpenFileDialog = System.Windows.Forms.OpenFileDialog;

namespace ZzukBot
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    internal partial class App : Application
    {
        internal App()
        {
            FrameworkElement.DataContextProperty.OverrideMetadata(typeof(UserControl),
                new FrameworkPropertyMetadata(OnElementDataContextChanged));

            FrameworkElement.DataContextProperty.OverrideMetadata(typeof(TreeViewItem),
                new FrameworkPropertyMetadata(OnElementDataContextChanged));

            FrameworkElement.DataContextProperty.OverrideMetadata(typeof(TabItem), 
                new FrameworkPropertyMetadata(OnElementDataContextChanged));

            FrameworkElement.DataContextProperty.OverrideMetadata(typeof(Window),
                new FrameworkPropertyMetadata(OnElementDataContextChanged));
        }

        protected static void OnElementDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var element = (FrameworkElement)sender;
            if (e.OldValue != null)
                CommandHandler.UnregisterUiElement(element, e.OldValue);
            if (e.NewValue != null)
                CommandHandler.RegisterUiElement(element, e.NewValue);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            if (!File.Exists(Paths.Settings))
                "appsettings.json".FileCreate(ZzukBot.Properties.Resources.appsettings);

            LaunchBot().GetAwaiter();

            base.OnStartup(e);
        }

        /// <summary>
        /// Checks files and settings and then injects the bot into the game client
        /// </summary>
        async Task CheckAndInject()
        {
            Logs.PreInject.ClearLog();
            Logs.Quit.ClearLog();
            Logs.Injected.ClearLog();

            "We are not injected yet".Log(Logs.PreInject);

            await CheckFiles();
            await CheckSettings();
            await Inject();
        }

        /// <summary>
        /// Ensures that the necessary folders and files exist
        /// </summary>
        async Task CheckFiles()
        {
            await Task.Run(() =>
            {
                "Checking folder structures of the bot".Log(Logs.PreInject);

                Paths.BotBases.CreateFolderStructure();
                Paths.Plugins.CreateFolderStructure();
                Paths.CustomClasses.CreateFolderStructure();
                Paths.Logs.CreateFolderStructure();
                Paths.Data.CreateFolderStructure();

                "Checking AntiQuery.dll".Log(Logs.PreInject);

                if (!"AntiQuery.dll".FileEqualTo(ZzukBot.Properties.Resources.AntiQuery))
                    "AntiQuery.dll".FileCreate(ZzukBot.Properties.Resources.AntiQuery);

                "Checking Fasm.NET.dll".Log(Logs.PreInject);

                if (!"Fasm.NET.dll".FileEqualTo(ZzukBot.Properties.Resources.Fasm_NET))
                    "Fasm.NET.dll".FileCreate(ZzukBot.Properties.Resources.Fasm_NET);

                "Checking FastCall.dll".Log(Logs.PreInject);

                if (!"FastCall.dll".FileEqualTo(ZzukBot.Properties.Resources.FastCall))
                    "FastCall.dll".FileCreate(ZzukBot.Properties.Resources.FastCall);

                "Checking Loader.dll".Log(Logs.PreInject);

                if (!"Loader.dll".FileEqualTo(ZzukBot.Properties.Resources.Loader))
                    "Loader.dll".FileCreate(ZzukBot.Properties.Resources.Loader);

                "Checking cleverbot.dll".Log(Logs.PreInject);

                if (!"cleverbot.dll".FileEqualTo(ZzukBot.Properties.Resources.cleverbot))
                    "cleverbot.dll".FileCreate(ZzukBot.Properties.Resources.cleverbot);

                "Checking mmaps".Log(Logs.PreInject);

                if (!Directory.Exists("mmaps"))
                    QuitWithMessage("Download the mmaps first please");

                if (Directory.GetFileSystemEntries("mmaps").Length < 1000)
                    QuitWithMessage("Download the mmaps first please");
            });
        }

        /// <summary>
        /// Ensures that a path to the game client exists
        /// </summary>
        async Task CheckSettings()
        {

            "Checking settings".Log(Logs.PreInject);

            if (Default.PathToWoW != "") return;

            "Asking user for path to the WoW.exe".Log(Logs.PreInject);

            var ofd = new OpenFileDialog
            {
                CheckFileExists = true,
                CheckPathExists = true,
                Filter = "executable (*.exe)|*.exe",
                FilterIndex = 1,
                Title = "Please locate your WoW.exe"
            };

            if (ofd.ShowDialog() != DialogResult.OK) return;

            "File Dialog Open".Log(Logs.PreInject);

            $"User selected {ofd.FileName} as 1.12.1 WoW executable".Log(Logs.PreInject);

            await Task.Run(() =>
            {
                if (ofd.FileName == Assembly.GetEntryAssembly().Location || !ofd.FileName.Contains("WoW"))
                    QuitWithMessage("Please select the WoW executable!");
                else
                    Default.PathToWoW = ofd.FileName;
            });

            "Settings saved!".Log(Logs.PreInject);
        }

        /// <summary>
        /// Injects the bot into the game client
        /// </summary>
        async Task Inject()
        {
            await Task.Run(() =>
            {
                try
                {
                    $"Loading path of WoW.exe from the settings".Log(Logs.PreInject);

                    $"Starting up the WoW process".Log(Logs.PreInject);

                    var si = new WinImports.STARTUPINFO();

                    WinImports.CreateProcess(Default.PathToWoW, null,
                            IntPtr.Zero, IntPtr.Zero, false,
                            WinImports.ProcessCreationFlags.CREATE_DEFAULT_ERROR_MODE,
                            IntPtr.Zero, null, ref si, out WinImports.PROCESS_INFORMATION pi);

                    var proc = Process.GetProcessById((int)pi.dwProcessId);

                    if (proc.Id == 0)
                    {
                        MessageBox.Show(
                            "Couldnt get the WoW process. Is the path in Settings.xml right? If no delete it and rerun ZzukBot");

                        return;
                    }

                    $"Waiting for WoW process to initialise".Log(Logs.PreInject);

                    while (!proc.WaitForInputIdle(1000))
                    {
                        $"WaitForInputIdle returned false. Trying again".Log(Logs.PreInject);
                        proc.Refresh();
                    }

                    while (string.IsNullOrWhiteSpace(proc.MainWindowTitle))
                    {
                        Thread.Sleep(200);
                        proc.Refresh();
                    }

                    Thread.Sleep(2000);

                    $"Initialising new ProcessReader".Log(Logs.PreInject);

                    using (var reader = new ExternalProcessReader(proc))
                    {
                        $"Retrieving function addresses for injection".Log(Logs.PreInject);

                        var loadDllPtr = WinImports.GetProcAddress(WinImports.GetModuleHandle("kernel32.dll"), "LoadLibraryW");

                        if (loadDllPtr == IntPtr.Zero)
                        {
                            MessageBox.Show("Couldnt get address of LoadLibraryW");

                            return;
                        }

                        $"Allocating memory for injection".Log(Logs.PreInject);

                        var LoaderStrPtr = reader.AllocateMemory(1000);

                        if (LoaderStrPtr == IntPtr.Zero)
                        {
                            MessageBox.Show("Couldnt allocate memory 2");

                            return;
                        }

                        var LoaderStr = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Loader.dll";

                        $"Preparing Loader.dll for injection".Log(Logs.PreInject);

                        var res = reader.WriteString(LoaderStrPtr, LoaderStr, Encoding.Unicode);

                        if (!res)
                        {
                            MessageBox.Show("Couldnt write dll path to WoW's memory");

                            return;
                        }

                        Thread.Sleep(1000);

                        $"Starting injection".Log(Logs.PreInject);

                        if (WinImports.CreateRemoteThread(proc.Handle, (IntPtr)null, (IntPtr)0, loadDllPtr, LoaderStrPtr, 0, (IntPtr)null) == (IntPtr)0)
                            MessageBox.Show("Couldnt inject the dll");

                        Thread.Sleep(1);

                        "Freeing allocated memory for injection".Log(Logs.PreInject);

                        reader.FreeMemory(LoaderStrPtr);
                    }
                }
                catch (Exception e)
                {
                    $"Exception occured while injecting: {e.Message}".Log(Logs.PreInject);
                    MessageBox.Show(e.Message);
                }
            });
        }

        /// <summary>
        /// Checks to see if the process has been injected
        /// </summary>
        bool Injected => !Process.GetCurrentProcess().ProcessName.StartsWith(Default.ProcessName);

        /// <summary>
        /// The main entry point for the application
        /// </summary>
        public async Task LaunchBot()
        {
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

            if (!Injected)
            {
                await CheckAndInject();

                Environment.Exit(0);
            }
            else
            {
                "Injected!!".Log(Logs.Injected);

                AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
                {
                    MessageBox.Show("A exception occured! The details were logged");
                    args.ExceptionObject.ToString().Log(Logs.Exceptions, false);
                };
                AppDomain.CurrentDomain.AssemblyResolve += DependencyLoader.CurrentDomain_AssemblyResolve;
#if DEBUG
                Debugger.Launch();
                WinImports.AllocConsole();
                DebugAssist.Init();
#endif
                await SetRealmlist();

                try
                {
                    $"Enabling the login block until the user authenticates".Log(Logs.Injected, true);
                    LoginBlock.Enable();

                    Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;
                    //$"Bringing up the login window".Log(Logs.Injected, true);
                    //var authenticationView = new AuthenticationView();
                    //authenticationView.ShowDialog();
                    //var authenticationModel = (AuthenticationViewModel)authenticationView.DataContext;
                    //if (authenticationModel.Result == null || authenticationModel.Result.Value != DialogResult.OK)
                        //Environment.Exit(0);
                    
                    $"Initialising the bot".Log(Logs.Injected, true);
                    Memory.Init();

                    $"Disabling the login block".Log(Logs.Injected, true);
                    LoginBlock.Disable();

                    $"Showing the bots mainwindow".Log(Logs.Injected, true);
                    var mainView = new MainView();
                    Current.MainWindow = mainView;
                    mainView.Closed += (sender, args) => { Environment.Exit(0); };
                    mainView.Show();
                }
                catch (Exception e)
                {
                    e.ToString().Log(Logs.Exceptions);
                }
            }
        }

        async Task SetRealmlist()
        {
            await Task.Run(() =>
            {
                $"Reading realmlist.wtf to determine which project we are connecting to".Log(Logs.Injected, true);

                var realmlist = Paths.PathToWoW + "\\realmlist.wtf";
                var project = File.ReadAllLines(realmlist);
                var name = "";

                foreach (var x in project)
                    if (x.ToLower().StartsWith("set realmlist "))
                        name = x.ToLower();

                Default.Realmlist = name;

                $"We are on project {name.Replace("set realmlist ", "")}".Log(Logs.Injected, true);
            });
        }

        /// <summary>
        /// Displays a message and then exits
        /// </summary>
        /// <param name="parMessage"></param>
        void QuitWithMessage(string parMessage)
        {
            MessageBox.Show(new Form() { TopMost = true }, parMessage);

            if (!string.IsNullOrWhiteSpace(Paths.Logs))
                parMessage.Log(Logs.Quit);

            Environment.Exit(0);
        }
    }
}
