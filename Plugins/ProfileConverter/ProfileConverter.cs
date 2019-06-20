using ProfileConverter.GUI;
using System;
using System.ComponentModel.Composition;
using System.Windows.Forms;
using ZzukBot.Core.Framework.Interfaces;
using ZzukBot.Core.Utilities.DependencyInjection;

namespace ProfileConverter
{
    [Export(typeof(IPlugin))]
    public class ProfileConverter : IPlugin
    {
        DependencyMap DM { get; set; }

        public string Name => "Profile Converter";

        public string Author => "krycess";

        public Version Version => new Version(4, 0, 0, 1901);

        public void Dispose()
        {
            DM = null;
        }

        public bool Load()
        {
            Dispose();
            if (DM == null)
            {
                DM = new DependencyMap();
                DM.Add(this);
                DM.Add(new CMD());
            }
            return true;
        }

        public void ShowGui()
        {
            if (DM != null)
            {
                CMD settings = DM.Get<CMD>();

                if (!settings.Visible)
                    settings.Show();
                else
                    settings.Hide();
            }
            else
            {
                Load();
                MessageBox.Show("plugin loaded");
            }
        }

        public void Unload()
        {
            DM = null;
        }
    }
}
