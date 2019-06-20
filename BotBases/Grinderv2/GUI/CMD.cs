using Grinderv2.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ZzukBot.Core.Constants;
using ZzukBot.Core.Game.Statics;
using ZzukBot.Core.Utilities.Extensions;

namespace Grinderv2.GUI
{
    public partial class CMD : Form
    {
        [DllImport("kernel32")]
        static extern bool AllocConsole();
        [DllImport("kernel32")]
        static extern bool FreeConsole();

        static Creatures Creatures { get; set; }
        static CreatureTemplates CreatureTemplates { get; set; }
        public static List<Creature> CreatureWhitelist { get; set; }
        public static Dictionary<Creature, DateTime> CreatureBlacklist = new Dictionary<Creature, DateTime> { };
        public static List<Creature> VendorWhitelist { get; set; }
        public static Dictionary<Creature, DateTime> VendorBlacklist = new Dictionary<Creature, DateTime> { };
        public static List<ulong> GuidBlacklist = new List<ulong> { };
        public static List<string> Positions = new List<string> { };
        public static bool ShouldVendor;

        public CMD()
        {
            AllocConsole();
            InitializeComponent();

            FormClosing += new FormClosingEventHandler(OnFormClosing);

            if (!File.Exists(Paths.Settings))
                Paths.Settings.FileCreate(Properties.Resources.grinder);

            if (!File.Exists(Paths.Creature))
            {
                MessageBox.Show("creature.json 31Mb will be downloaded");
                Console.WriteLine("downloading creature.json");
                try
                {
                    // Downloading the archive
                    using (var webClient = new WebClient())
                    {
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        webClient.DownloadFile("https://www.zzukbot.org/downloads/creature.json", Paths.Creature);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Console.Write("Downloading from URL caused a crash. Aborting ");
                    Console.ReadLine();
                }
            }
            if (!File.Exists(Paths.CreatureTemplate))
            {
                MessageBox.Show("creature_template.json 16Mb will be downloaded");
                Console.WriteLine("downloading creature_template.json");
                try
                {
                    // Downloading the archive
                    using (var webClient = new WebClient())
                    {
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        webClient.DownloadFile("https://www.zzukbot.org/downloads/creature_template.json", Paths.CreatureTemplate);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Console.Write("Downloading from URL caused a crash. Aborting ");
                    Console.ReadLine();
                }
            }
            Creatures = JsonConvert.DeserializeObject<Creatures>(File.ReadAllText("creature.json"));
            CreatureTemplates = JsonConvert.DeserializeObject<CreatureTemplates>(File.ReadAllText("creature_template.json"));
            FreeConsole();
            InitializePulseRate();
            InitialiseRestingValues();
            VendorAtInput.Value = Settings.FreeSlotsToVendor;
            CorpseLoot.Checked = Settings.CorpseLoot;
            CorpseSkin.Checked = Settings.CorpseSkin;
            NinjaSkin.Checked = Settings.NinjaSkin;
            Harvest.Checked = Settings.Harvest;
            KeepGreens.Checked = Settings.KeepGreens;
            KeepBlues.Checked = Settings.KeepBlues;
            KeepPurples.Checked = Settings.KeepPurples;
            Vendor.Checked = Settings.Vendor;
            ProtectedItems.Text = GetProtectedItems();
            CreatureInput.Text = GetCreatures();
            RefreshCreatures();
            RefreshVendors();
        }

        void InitializePulseRate()
        {
            if (Settings.PulseRate > 0)
                PulseRateInput.Value = Settings.PulseRate;
            else
            {
                Settings.PulseRate = 100;
                PulseRateInput.Value = Settings.PulseRate;
            }
        }

        void RefreshCreatures()
        {
            CreatureWhitelist = new List<Creature>();
            var tmp = CreatureTemplates.Rows.Where(x => Settings.Creatures.Contains(x.Name));
            foreach (var i in tmp)
                if (Creatures.Rows.Where(x => x.Id == i.Entry && x.Map == ObjectManager.Instance.Player.MapId).Any())
                    CreatureWhitelist.AddRange(Creatures.Rows.Where(x => x.Id == i.Entry && x.Map == ObjectManager.Instance.Player.MapId));
        }

        void RefreshVendors()
        {
            VendorWhitelist = new List<Creature>();
            var tmp = new List<CreatureTemplate>();
            var vals = new List<int>();
            foreach (int item in Enum.GetValues(typeof(Enums.FactionNeutral)))
                vals.Add(item);
            if (ObjectManager.Instance.Player.IsAlliance)
            {
                foreach (int item in Enum.GetValues(typeof(Enums.FactionAlliance)))
                    vals.Add(item);
                tmp.AddRange(CreatureTemplates.Rows.Where(x => vals.Contains(x.Faction_A) 
                    && (x.NpcFlag & (int)Enums.NpcFlags.UNIT_NPC_FLAG_VENDOR) != 0));

            }
            else
            {
                foreach (int item in Enum.GetValues(typeof(Enums.FactionHorde)))
                    vals.Add(item);
                tmp.AddRange(CreatureTemplates.Rows.Where(x => vals.Contains(x.Faction_H)
                    && (x.NpcFlag & (int)Enums.NpcFlags.UNIT_NPC_FLAG_VENDOR) != 0));
            }
            foreach (var i in tmp)
                if (Creatures.Rows.Where(x => x.Id == i.Entry && x.Map == ObjectManager.Instance.Player.MapId).Any())
                    VendorWhitelist.AddRange(Creatures.Rows.Where(x => x.Id == i.Entry && x.Map == ObjectManager.Instance.Player.MapId));
        }

        string GetProtectedItems()
        {
            string items = string.Empty;
            foreach (var item in Settings.ProtectedItems)
            {
                if (!item.Equals(string.Empty))
                    items += item + Environment.NewLine;
            }
            return items;
        }

        string GetCreatures()
        {
            string creatures = string.Empty;
            foreach (var creature in Settings.Creatures)
            {
                if (!creature.Equals(string.Empty))
                    creatures += creature + Environment.NewLine;
            }
            return creatures;
        }

        void InitialiseRestingValues()
        {
            EatAtInput.Value = Settings.EatAt;
            Common.Instance.EatAt = (int)EatAtInput.Value;
            DrinkAtInput.Value = Settings.DrinkAt;
            Common.Instance.DrinkAt = (int)DrinkAtInput.Value;
        }

        void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        void CorpseLoot_CheckedChanged(object sender, EventArgs e)
        {
            Settings.CorpseLoot = CorpseLoot.Checked;
        }

        void CorpseSkin_CheckedChanged(object sender, EventArgs e)
        {
            Settings.CorpseSkin = CorpseSkin.Checked;
        }

        void DrinkAtInput_ValueChanged(object sender, EventArgs e)
        {
            Common.Instance.DrinkAt = (int)DrinkAtInput.Value;
            Settings.DrinkAt = (int)DrinkAtInput.Value;
        }

        void EatAtInput_ValueChanged(object sender, EventArgs e)
        {
            Common.Instance.EatAt = (int)EatAtInput.Value;
            Settings.EatAt = (int)EatAtInput.Value;
        }

        void VendorAtInput_ValueChanged(object sender, EventArgs e)
        {
            Settings.FreeSlotsToVendor = (int)VendorAtInput.Value;
        }

        void KeepGreens_CheckedChanged(object sender, EventArgs e)
        {
            Settings.KeepGreens = KeepGreens.Checked;
        }

        void KeepBlues_CheckedChanged(object sender, EventArgs e)
        {
            Settings.KeepBlues = KeepBlues.Checked;
        }

        void KeepPurples_CheckedChanged(object sender, EventArgs e)
        {
            Settings.KeepPurples = KeepPurples.Checked;
        }

        void NinjaSkin_CheckedChanged(object sender, EventArgs e)
        {
            Settings.NinjaSkin = NinjaSkin.Checked;
        }

        void Harvest_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Harvest = Harvest.Checked;
        }

        void ProtectedItems_TextChanged(object sender, EventArgs e)
        {
            List<string> items = new List<string>();

            foreach (var item in ProtectedItems.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.None))
            {
                if (!item.Equals(string.Empty))
                    items.Add(item);
            }

            Settings.ProtectedItems = items;
        }

        void Creatures_MouseLeave(object sender, EventArgs e)
        {
            List<string> creatures = new List<string>();

            foreach (var item in CreatureInput.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.None))
            {
                if (!item.Equals(string.Empty))
                    creatures.Add(item);
            }

            Settings.Creatures = creatures;
            RefreshCreatures();
        }

        void Vendor_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Vendor = Vendor.Checked;
        }

        void PulseRateInput_ValueChanged(object sender, EventArgs e)
        {
            Settings.PulseRate = (int)PulseRateInput.Value;
        }
    }
}
