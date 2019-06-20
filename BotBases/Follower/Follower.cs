using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using TreeTaskCore;
using ZzukBot.Core.Framework.Interfaces;
using ZzukBot.Core.Framework.Loaders;
using ZzukBot.Core.Game.Statics;
using ZzukBot.Core.Mem;

namespace Follower
{
    [Export(typeof(IBotBase))]
    public class Follower : IBotBase
    {
        Action stopCallback;
        readonly MainThread.Updater MainThread;

        public Follower()
        {
            MainThread = new MainThread.Updater(Pulse, 250);
        }

        public string Name => "Follower";
        public string Author => "krycess";
        public Version Version => new Version(0, 0, 0, 1);
        public void Dispose()
        {
            
        }
        public void PauseBotbase(Action onPauseCallback)
        {
            
        }

        void Pulse()
        {
            ObjectManager.Instance.Player.AntiAfk();
            TreeTask tt = new TreeTask(100, new List<List<TreeTask>>() { TTS.alive, });
            if (tt.Activate())
                tt.Execute();
            if (running) return;
            stopCallback();
            MainThread.Stop();
        }

        public bool ResumeBotbase()
        {
            return true;
        }

        public void ShowGui()
        {
            
        }

        bool running;
        public void Stop() => running = false;

        public bool Start(Action stopCallback)
        {
            if (running) return false;
            if (!ObjectManager.Instance.IsIngame) return false;
            if (ObjectManager.Instance.Player == null) return false;
            if (!CCLoader.Instance.LoadCustomClass(ObjectManager.Instance.Player.Class)) return false;
            running = true;
            this.stopCallback = stopCallback;
            MainThread.Start();
            return running;
        }
    }
}
