using System.Reflection;
using ZzukBot.Server.Core.Utilities.Extensions;

namespace ZzukBot.Server.Settings
{
    internal static class Paths
    {
        static Assembly Assembly = Assembly.GetExecutingAssembly();

        internal static string Bot = Assembly.Location;
        internal static string Settings = Assembly.ExtJumpUp(1) + "/appsettings.json";
    }
}
