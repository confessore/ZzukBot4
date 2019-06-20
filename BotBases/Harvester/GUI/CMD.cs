using Harvester.Engine.Loaders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Harvester.GUI
{
    public partial class CMD : Form
    {
        private ProfileLoader ProfileLoader { get; }

        public CMD(ProfileLoader profileLoader)
        {
            ProfileLoader = profileLoader;
            InitializeComponent();
        }

        public List<string> herbCheckedBoxes = new List<string> { };
        public List<string> mineCheckedBoxes = new List<string> { };
        public bool mountDisabled;

        private void LoadProfileButton_Click(object sender, EventArgs e)
            => ProfileLoader.LoadProfile(LoadProfileOFD);

        private void LoadProfileOFD_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void HerbCheckListBox_MouseLeave(object sender, EventArgs e)
        {
            herbCheckedBoxes.Clear();

            foreach (var item in Herbs.CheckedItems)
                herbCheckedBoxes.Add(item.ToString());
        }

        private void MineCheckListBox_MouseLeave(object sender, EventArgs e)
        {
            mineCheckedBoxes.Clear();

            foreach (var item in Mines.CheckedItems)
                mineCheckedBoxes.Add(item.ToString());
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