using Grinderv2.GUI;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows.Forms;
using TreeTaskCore;
using ZzukBot.Core.Framework.Interfaces;
using ZzukBot.Core.Framework.Loaders;
using ZzukBot.Core.Game.Statics;
using ZzukBot.Core.Mem;

namespace Grinderv2
{
    [Export(typeof(IBotBase))]
    public class Grinderv2 : IBotBase
    {
        Action stopCallback;
        MainThread.Updater MainThread;
        public static CMD cmd;

        public string Name => "Grinderv2 pre-Release";
        public string Author => "krycess";
        public Version Version => new Version(0, 0, 0, 7);
        public void Dispose()
        {
            cmd = null;
        }
        public void PauseBotbase(Action onPauseCallback)
        {

        }

        void Pulse()
        {
            ObjectManager.Instance.Player.AntiAfk();
            TreeTask tt = new TreeTask(100, new List<List<TreeTask>>() { TTS.collect, });
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
            if (ObjectManager.Instance.Player != null)
            {
                if (cmd == null) cmd = new CMD();
                if (!cmd.Visible) cmd.Show();
                else cmd.Hide();
            }
            else
                MessageBox.Show("Please log in first");
        }

        bool running;
        public void Stop() => running = false;

        public bool Start(Action stopCallback)
        {
            if (running) return false;
            if (!ObjectManager.Instance.IsIngame) return false;
            if (ObjectManager.Instance.Player == null) return false;
            if (!CCLoader.Instance.LoadCustomClass(ObjectManager.Instance.Player.Class)) return false;
            if (cmd == null) cmd = new CMD();
            MainThread = new MainThread.Updater(Pulse, Settings.PulseRate);
            running = true;
            this.stopCallback = stopCallback;
            MainThread.Start();
            return running;
        }
    }
}
