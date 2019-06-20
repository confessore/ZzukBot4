using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using ZzukBot.Core.Authentication;
using ZzukBot.Core.Game.Objects;

namespace ZzukBot.Core.Framework.Loaders
{
    /// <summary>
    ///     Loader for v4 profiles
    /// </summary>
    public sealed class ProfileLoader
    {
        static readonly Lazy<ProfileLoader> instance = new Lazy<ProfileLoader>(() => new ProfileLoader());

        /// <summary>
        ///     Access to the profile loader
        /// </summary>
        public static ProfileLoader Instance => instance.Value;

        /// <summary>
        ///     A List of locations that represent grinding hotspots
        /// </summary>
        public List<Location> Hotspots { get; set; }
        /// <summary>
        ///     A list of locations that represent vendor hotspots
        /// </summary>
        public List<Location> VendorHotspots { get; set; }
        /// <summary>
        ///     The name of the vendor NPC
        /// </summary>
        public string VendorName { get; set; }

        /// <summary>
        ///     Loads an existing JSON grinding profile
        /// </summary>
        /// <param name="ofd"></param>
        public void LoadProfile(OpenFileDialog ofd)
        {
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;
            ofd.Filter = "Profiles (*.json)|*.json";
            ofd.FilterIndex = 1;
            ofd.Title = "Please load a profile";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                using (var sw = new StreamReader(ofd.FileName))
                {
                    var username = sw.ReadLine();
                    new SslProfileClientAsync(username, sw.ReadToEnd()).StartClient(out string json);
                    var profile = JsonConvert.DeserializeObject<ProfileModel>(json);

                    Hotspots = new List<Location>();
                    foreach (var hotspot in profile.Hotspots)
                        Hotspots.Add(new Location(hotspot.X, hotspot.Y, hotspot.Z));
                    VendorHotspots = new List<Location>();
                    foreach (var vendorHotspot in profile.VendorHotspots)
                        VendorHotspots.Add(new Location(vendorHotspot.X, vendorHotspot.Y, vendorHotspot.Z));
                    VendorName = profile.VendorName;
                }
            }
        }

        class ProfileModel
        {
            public List<Location> Hotspots { get; set; }
            public List<Location> VendorHotspots { get; set; }
            public string VendorName { get; set; }
        }
    }
}
