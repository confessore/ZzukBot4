using System.IO;
using System.Reflection;
using ZzukBot.Core.Utilities.Extensions;

namespace ZzukBot.Settings
{
    /// <summary>
    ///     The paths to files and directories
    /// </summary>
    public static class Paths
    {
        static Assembly Assembly = Assembly.GetExecutingAssembly();

        internal static string Bot = Assembly.Location;
        internal static string BotBases = Assembly.ExtJumpUp(1) + "\\..\\BotBases";
        internal static string CustomClasses = Assembly.ExtJumpUp(1) + "\\..\\CustomClasses";
        /// <summary>
        /// The path to the data directory
        /// </summary>
        public static string Data = Assembly.ExtJumpUp(1) + "\\..\\Data";
        internal static string Internal = Assembly.ExtJumpUp(1);
        internal static string Launcher = Assembly.ExtJumpUp(2) + "\\Launcher.exe";
        /// <summary>
        /// The path to the logs directory
        /// </summary>
        public static string Logs = Assembly.ExtJumpUp(1) + "\\..\\Logs";
        internal static string PathToWoW = Directory.GetCurrentDirectory();
        internal static string Plugins = Assembly.ExtJumpUp(1) + "\\..\\Plugins";
        internal static string Release = Assembly.ExtJumpUp(2);
        /// <summary>
        /// The path to the settings file
        /// </summary>
        public static string Settings = Assembly.ExtJumpUp(1) + "\\appsettings.json";
    }
}
