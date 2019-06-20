using System.Windows;
using System.Windows.Input;

namespace ZzukBot.GUI.Utilities.Extensions
{
    internal static class CommandExtensions
    {
        internal static RoutedCommand GetCommand(this string resourceKey)
        {
            return (RoutedCommand)Application.Current.FindResource(resourceKey);
        }
    }
}
