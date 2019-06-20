using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Grinder.Settings;
using ZzukBot.Core.Framework.Loaders;
using ZzukBot.Core.Game.Statics;

namespace Grinder.GUI
{
    public partial class CMD : Form
    {
        Common Common { get; }
        ProfileLoader ProfileLoader { get; }

        public CMD(
            Common common,
            ProfileLoader profileLoader)
        {
            Common = common;
            ProfileLoader = profileLoader;

            InitializeComponent();

            FormClosing += new FormClosingEventHandler(OnFormClosing);

            InitialiseRestingValues();
            CorpseLoot.Checked = GrinderDefault.CorpseLoot;
            CorpseSkin.Checked = GrinderDefault.CorpseSkin;
            KeepGreens.Checked = GrinderDefault.KeepGreens;
            KeepBlues.Checked = GrinderDefault.KeepBlues;
            KeepPurples.Checked = GrinderDefault.KeepPurples;
            NinjaSkin.Checked = GrinderDefault.NinjaSkin;
            ProtectedItems.Text = GetProtectedItems();
            SearchRadiusInput.Value = GrinderDefault.SearchRadius;
        }

        string GetProtectedItems()
        {
            string items = string.Empty;
            foreach (var item in GrinderDefault.ProtectedItems)
            {
                if (!item.Equals(string.Empty))
                    items += item + Environment.NewLine;
            }
            return items;
        }

        void InitialiseRestingValues()
        {
            EatAtInput.Value = GrinderDefault.EatAt;
            Common.EatAt = (int)EatAtInput.Value;
            DrinkAtInput.Value = GrinderDefault.DrinkAt;
            Common.DrinkAt = (int)DrinkAtInput.Value;
        }

        void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        void CorpseLoot_CheckedChanged(object sender, EventArgs e)
        {
            GrinderDefault.CorpseLoot = CorpseLoot.Checked;
        }

        void CorpseSkin_CheckedChanged(object sender, EventArgs e)
        {
            GrinderDefault.CorpseSkin = CorpseSkin.Checked;
        }

        void DrinkAtInput_ValueChanged(object sender, EventArgs e)
        {
            Common.DrinkAt = (int)DrinkAtInput.Value;
            GrinderDefault.DrinkAt = (int)DrinkAtInput.Value;
        }

        void EatAtInput_ValueChanged(object sender, EventArgs e)
        {
            Common.EatAt = (int)EatAtInput.Value;
            GrinderDefault.EatAt = (int)EatAtInput.Value;
        }

        void KeepGreens_CheckedChanged(object sender, EventArgs e)
        {
            GrinderDefault.KeepGreens = KeepGreens.Checked;
        }

        void KeepBlues_CheckedChanged(object sender, EventArgs e)
        {
            GrinderDefault.KeepBlues = KeepBlues.Checked;
        }

        void KeepPurples_CheckedChanged(object sender, EventArgs e)
        {
            GrinderDefault.KeepPurples = KeepPurples.Checked;
        }

        void LoadProfile_Click(object sender, EventArgs e) => ProfileLoader.LoadProfile(new OpenFileDialog());

        private void NinjaSkin_CheckedChanged(object sender, EventArgs e)
        {
            GrinderDefault.NinjaSkin = NinjaSkin.Checked;
        }

        void ProtectedItems_TextChanged(object sender, EventArgs e)
        {
            List<string> items = new List<string>();

            foreach (var item in ProtectedItems.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.None))
            {
                if (!item.Equals(string.Empty))
                    items.Add(item);
            }

            GrinderDefault.ProtectedItems = items;
        }

        private void SearchRadiusInput_ValueChanged(object sender, EventArgs e)
        {
            GrinderDefault.SearchRadius = (int)SearchRadiusInput.Value;
        }
    }
}
