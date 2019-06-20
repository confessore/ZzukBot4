using Newtonsoft.Json;
using ProfileCreator.Utilities;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ZzukBot.Core.Authentication.Objects;
using ZzukBot.Core.Game.Objects;
using ZzukBot.Core.Game.Statics;
using ZzukBot.Core.Utilities.Helpers;

namespace ProfileCreator.GUI
{
    public partial class CMD : Form
    {
        KeyboardHook KeyboardHook { get; }

        public CMD(
            KeyboardHook keyboardHook)
        {
            KeyboardHook = keyboardHook;

            InitializeComponent();

            FormClosing += new FormClosingEventHandler(OnFormClosing);
            KeyboardHook.KeyPressed += new System.EventHandler<KeyPressedEventArgs>(OnKeyPressed);

            Hotspots = new List<Location>();
            VendorHotspots = new List<Location>();
            VendorName = string.Empty;
        }

        List<Location> Hotspots { get; set; }
        List<Location> VendorHotspots { get; set; }
        string VendorName { get; set; }

        void DeleteHotspot_Click(object sender, System.EventArgs e)
        {
            DeleteSelectedHotspot();
        }

        void DeleteVendorHotspot_Click(object sender, System.EventArgs e)
        {
            DeleteSelectedVendorHotspot();
        }

        void DeleteSelectedHotspot()
        {
            var index = HotspotsListBox.SelectedIndex;

            if (index > -1)
            {
                Hotspots.RemoveAt(HotspotsListBox.SelectedIndex);
                HotspotsListBox.Items.RemoveAt(HotspotsListBox.SelectedIndex);

                if (index != 0)
                    HotspotsListBox.SelectedIndex = index - 1;
            }
        }

        void DeleteSelectedVendorHotspot()
        {
            var index = VendorHotspotsListBox.SelectedIndex;

            if (index > -1)
            {
                VendorHotspots.RemoveAt(VendorHotspotsListBox.SelectedIndex);
                VendorHotspotsListBox.Items.RemoveAt(VendorHotspotsListBox.SelectedIndex);

                if (index != 0)
                    VendorHotspotsListBox.SelectedIndex = index - 1;
            }
        }

        void EnableGlobalHotkeys_CheckedChanged(object sender, System.EventArgs e)
        {
            if (EnableGlobalHotkeys.Checked)
            {
                KeyboardHook.RegisterHotKey(Utilities.ModifierKeys.Control, Keys.Insert);
                KeyboardHook.RegisterHotKey(Utilities.ModifierKeys.Control, Keys.Delete);
                KeyboardHook.RegisterHotKey(Utilities.ModifierKeys.Shift, Keys.Insert);
                KeyboardHook.RegisterHotKey(Utilities.ModifierKeys.Shift, Keys.Delete);
            }
            else
                KeyboardHook.UnregisterAllHotkeys();
        }

        void InsertHotspot_Click(object sender, System.EventArgs e)
        {
            InsertNewHotspot();
        }

        void InsertVendorHotspot_Click(object sender, System.EventArgs e)
        {
            InsertNewVendorHotspot();
        }

        void InsertNewHotspot()
        {
            Hotspots.Add(ObjectManager.Instance.Player.Position);
            HotspotsListBox.Items.Add(Hotspots.LastOrDefault());
            HotspotsListBox.SelectedIndex = HotspotsListBox.Items.Count - 1;
        }

        void InsertNewVendorHotspot()
        {
            VendorHotspots.Add(ObjectManager.Instance.Player.Position);
            VendorHotspotsListBox.Items.Add(VendorHotspots.LastOrDefault());
            VendorHotspotsListBox.SelectedIndex = VendorHotspotsListBox.Items.Count - 1;
        }

        void LoadProfile_Click(object sender, System.EventArgs e)
        {
            var ofd = new OpenFileDialog
            {
                Filter = "JSON files (*.json)|*.json"
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                using (var sw = new StreamReader(ofd.FileName))
                {
                    var username = sw.ReadLine();
                    var json = Cryptography.DecryptStringAES(sw.ReadToEnd(), UserProfile.Id);
                    var profile = JsonConvert.DeserializeObject<ProfileModel>(json);

                    Hotspots.Clear();
                    HotspotsListBox.Items.Clear();
                    foreach (var hotspot in profile.Hotspots)
                    {
                        var location = new Location(hotspot.X, hotspot.Y, hotspot.Z);
                        Hotspots.Add(location);
                        HotspotsListBox.Items.Add(location);
                        HotspotsListBox.SelectedIndex = HotspotsListBox.Items.Count - 1;
                    }
                    VendorHotspots.Clear();
                    VendorHotspotsListBox.Items.Clear();
                    foreach (var vendorHotspot in profile.VendorHotspots)
                    {
                        var location = new Location(vendorHotspot.X, vendorHotspot.Y, vendorHotspot.Z);
                        VendorHotspots.Add(location);
                        VendorHotspotsListBox.Items.Add(location);
                        VendorHotspotsListBox.SelectedIndex = VendorHotspotsListBox.Items.Count - 1;
                    }
                    VendorName = profile.VendorName;
                    VendorNameInput.Text = profile.VendorName;
                }
            }
        }

        void NewProfile_Click(object sender, System.EventArgs e)
        {
            Hotspots.Clear();
            HotspotsListBox.Items.Clear();
            VendorHotspots.Clear();
            VendorHotspotsListBox.Items.Clear();
            VendorName = string.Empty;
            VendorNameInput.Text = string.Empty;
        }

        void SaveProfile_Click(object sender, System.EventArgs e)
        {
            var sfd = new SaveFileDialog
            {
                Filter = "JSON files (*.json)|*.json"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                using (var sw = new StreamWriter(sfd.FileName))
                {
                    sw.WriteLine(UserProfile.UserName);
                    sw.Write(
                        Cryptography.EncryptStringAES(
                            JsonConvert.SerializeObject(
                                new ProfileModel() { Hotspots = Hotspots, VendorHotspots = VendorHotspots, VendorName = VendorName }, Formatting.Indented),
                            UserProfile.Id));
                }
            }
        }

        void VendorNameInput_TextChanged(object sender, System.EventArgs e)
        {
            VendorName = VendorNameInput.Text;
        }

        void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        void OnKeyPressed(object sender, KeyPressedEventArgs e)
        {
            if (e.Modifier == Utilities.ModifierKeys.Control && e.Key == Keys.Insert)
                InsertNewHotspot();
            if (e.Modifier == Utilities.ModifierKeys.Control && e.Key == Keys.Delete)
                DeleteSelectedHotspot();
            if (e.Modifier == Utilities.ModifierKeys.Shift && e.Key == Keys.Insert)
                InsertNewVendorHotspot();
            if (e.Modifier == Utilities.ModifierKeys.Shift && e.Key == Keys.Delete)
                DeleteSelectedVendorHotspot();
        }

        class ProfileModel
        {
            public List<Location> Hotspots { get; set; }
            public List<Location> VendorHotspots { get; set; }
            public string VendorName { get; set; }
        }
    }
}
