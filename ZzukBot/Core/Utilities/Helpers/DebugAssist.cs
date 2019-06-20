using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using ZzukBot.Core.Constants;
using ZzukBot.Core.Game.Frames;
using ZzukBot.Core.Game.Objects;
using ZzukBot.Core.Game.Statics;
using ZzukBot.Core.Game.Transport;
using ZzukBot.Core.Mem;
using ZzukBot.Core.Utilities.Extensions;

namespace ZzukBot.Core.Utilities.Extensions
{
#if DEBUG
    internal static class DebugAssist
    {
        private static bool Applied;

        internal static void Init()
        {
            if (Applied) return;
            Task.Run(() => ConsoleReader());
            Applied = true;
        }

        private static readonly Transport Transport;

        private static Location loc1;

        private static void ConsoleReader()
        {
            while (true)
                try
                {
                    var input = Console.ReadLine();
                    if (input != null)
                    {
                        if (input == "test")
                        {
                            Console.WriteLine(ObjectManager.Instance.Player.HasPet);
                        }
                    }
                    Task.Delay(10).Wait();
                }
                catch (Exception)
                {
                    // ignored
                }
            // ReSharper disable once FunctionNeverReturns
        }

        private static int DoSomething()
        {
            return 10;
        }
    }
#endif
}