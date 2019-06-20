using ProfileCreator.GUI;
using ProfileCreator.Utilities;
using System;
using System.ComponentModel.Composition;
using System.Windows.Forms;
using ZzukBot.Core.Framework.Interfaces;
using ZzukBot.Core.Utilities.DependencyInjection;

namespace ProfileCreator
{
    [Export(typeof(IPlugin))]
    public class ProfileCreator : IPlugin
    {
        DependencyMap DM { get; set; }

        public string Name => "Profile Creator";

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
                DM.Add(new KeyboardHook());
                DM.Add(new CMD(
                    DM.Get<KeyboardHook>()));
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
