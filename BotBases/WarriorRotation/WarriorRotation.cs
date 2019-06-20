using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows.Forms;
using TreeTaskCore;
using ZzukBot.Core.Constants;
using ZzukBot.Core.Framework.Interfaces;
using ZzukBot.Core.Game.Statics;
using ZzukBot.Core.Mem;

namespace WarriorRotation
{
    [Export(typeof(IBotBase))]
    public class WarriorRotation : IBotBase
    {
        Action stopCallback;
        readonly MainThread.Updater MainThread;
        TreeTask tt;

        public WarriorRotation()
        {
            MainThread = new MainThread.Updater(Pulse, 250);
        }

        public string Name => "Warrior Rotation";
        public string Author => "krycess";
        public Version Version => new Version(0, 0, 0, 1);
        public void Dispose()
        {
            tt = null;
        }
        public void PauseBotbase(Action onPauseCallback)
        {

        }

        void Pulse()
        {
            ObjectManager.Instance.Player.AntiAfk();
            tt = new TreeTask(100, new List<List<TreeTask>>() { TTS.collection, });
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
            MessageBox.Show("No GUI!");
        }

        bool running;
        public void Stop() => running = false;

        public bool Start(Action stopCallback)
        {
            if (running) return false;
            if (!ObjectManager.Instance.IsIngame) return false;
            if (ObjectManager.Instance.Player == null) return false;
            //if (!CCLoader.Instance.LoadCustomClass(ObjectManager.Instance.Player.Class)) return false;
            if (ObjectManager.Instance.Player.Class != Enums.ClassId.Warrior) return false;
            running = true;
            this.stopCallback = stopCallback;
            MainThread.Start();
            return running;
        }
    }
}
