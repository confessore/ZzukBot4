using Gatherer.GUI;
using Gatherer.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TreeTaskCore;
using ZzukBot.Core.Framework.Interfaces;
using ZzukBot.Core.Framework.Loaders;
using ZzukBot.Core.Game.Statics;
using ZzukBot.Core.Mem;

namespace Gatherer
{
    [Export(typeof(IBotBase))]
    public class Gatherer : IBotBase
    {
        Action stopCallback;
        readonly MainThread.Updater MainThread;
        public static CMD cmd;
        public static Database db;
        public static Dictionary<GameObject, DateTime> bl;

        public Gatherer()
        {
            MainThread = new MainThread.Updater(Pulse, 100);
            cmd = new CMD();
            db = JsonConvert.DeserializeObject<Database>(File.ReadAllText("gameobject.json"));
            bl = JsonConvert.DeserializeObject<List<KeyValuePair<GameObject, DateTime>>>(File.ReadAllText("blacklist.json")).ToDictionary(x => x.Key, y => y.Value);
            foreach (var go in bl)
                if (go.Value > DateTime.Now.AddYears(1))
                    CMD.goBlacklist.Add(go.Key, go.Value);
        }

        public string Name => "Gatherer";
        public string Author => "krycess";
        public Version Version => new Version(0, 0, 0, 1);
        public void Dispose()
        {
            cmd = null;
            db = null;
            bl = null;
        }
        public void PauseBotbase(Action onPauseCallback)
        {

        }

        void Pulse()
        {
            //Common.Instance.DebugMessage(ObjectManager.Instance.Player.Position.ToString());
            //Common.Instance.DebugMessage(db.Rows.Count.ToString());
            File.WriteAllText("blacklist.json", JsonConvert.SerializeObject(CMD.goBlacklist.ToArray(), Formatting.Indented));
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
            if (!cmd.Visible)
                cmd.Show();
            else
                cmd.Hide();
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
