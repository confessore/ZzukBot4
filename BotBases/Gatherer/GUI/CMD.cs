using Gatherer.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ZzukBot.Core.Game.Statics;

namespace Gatherer.GUI
{
    public partial class CMD : Form
    {
        public CMD()
        {
            InitializeComponent();
        }

        public static List<string> herbCheckedBoxes = new List<string> { };
        public static List<string> oreCheckedBoxes = new List<string> { };
        public static List<ulong> guidBlacklist = new List<ulong> { };
        public static Dictionary<GameObject, DateTime> goBlacklist = new Dictionary<GameObject, DateTime> { };
        public static List<string> positions = new List<string> { };
        public bool mountDisabled;

        private void LoadProfileOFD_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void HerbCheckListBox_MouseLeave(object sender, EventArgs e)
        {
            herbCheckedBoxes.Clear();
            foreach (var item in Herbs.CheckedItems)
                herbCheckedBoxes.Add(item.ToString());
            Gatherer.db.Rows.Clear();
            var rows = JsonConvert.DeserializeObject<Database>(File.ReadAllText("gameobject.json")).Rows;
            Gatherer.db.Rows.AddRange(
                rows.Where(x => ObjectManager.Instance.Player.MapId == x.Map
                    && herbCheckedBoxes.Any(y => (int)Enum.Parse(typeof(Herbs), y) == x.Id)));
            Gatherer.db.Rows.AddRange(
                rows.Where(x => ObjectManager.Instance.Player.MapId == x.Map
                    && oreCheckedBoxes.Any(y => (int)Enum.Parse(typeof(Ores), y) == x.Id)));
        }

        private void MineCheckListBox_MouseLeave(object sender, EventArgs e)
        {
            oreCheckedBoxes.Clear();
            foreach (var item in Ores.CheckedItems)
                oreCheckedBoxes.Add(item.ToString());
            Gatherer.db.Rows.Clear();
            var rows = JsonConvert.DeserializeObject<Database>(File.ReadAllText("gameobject.json")).Rows;
            Gatherer.db.Rows.AddRange(
                rows.Where(x => ObjectManager.Instance.Player.MapId == x.Map 
                    && herbCheckedBoxes.Any(y => (int)Enum.Parse(typeof(Herbs), y) == x.Id)));
            Gatherer.db.Rows.AddRange(
                rows.Where(x => ObjectManager.Instance.Player.MapId == x.Map 
                    && oreCheckedBoxes.Any(y => (int)Enum.Parse(typeof(Ores), y) == x.Id)));
        }

        private void disableMount_MouseLeave(object sender, EventArgs e)
        {
            if (disableMountBox.Checked)
                mountDisabled = true;
            if (!disableMountBox.Checked)
                mountDisabled = false;
        }
    }
}