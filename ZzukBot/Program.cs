using System;
using System.Threading;

namespace ZzukBot
{
    internal static class Program
    {
        private static Thread Thread { get; set; }

        [STAThread]
        private static int Entry(string args)
        {
            Thread = new Thread(App.Main);
            Thread.SetApartmentState(ApartmentState.STA);
            Thread.Start();

            return 1;
        }
    }
}
