using System;
using System.Windows;
using System.Windows.Threading;

namespace ZzukBot.GUI.Utilities.Extensions
{
    internal static class DispatcherExtensions
    {
        internal static void Dispatch(this Action value)
        {
            Application.Current.Dispatcher.Invoke(value);
        }

        internal static void BeginDispatch(this Action value, DispatcherPriority priority)
        {
            Application.Current.Dispatcher.BeginInvoke(value, priority);
        }
    }
}
