using System;
using System.ComponentModel.Composition;
using System.Windows.Forms;
using ZzukBot.Core.Framework.Interfaces;

namespace Fisher
{
    [Export(typeof(IBotBase))]
    public class Fisher : IBotBase
    {
        public Action OnStopCallback;
        private FishingLogic fishingLogic;

        public string Name => "Fisherman";

        public string Author => "Zzuk";

        public Version Version => new Version(4, 0, 0, 3); 

        public void Dispose()
        {
            
        }

        public void PauseBotbase(Action onPauseCallback)
        {
            
        }

        public bool ResumeBotbase()
        {
            return true;
        }

        public void ShowGui()
        {
            MessageBox.Show("No GUI!");
        }

        public bool Start(Action onStopCallback)
        {
            // Checking if we are already running
            if (fishingLogic != null)
                // We are already running - return here and dont do anything further
                return false;
            // initialising new fishinglogic instace
            fishingLogic = new FishingLogic(this);
            // Saving the callback to a private variable
            OnStopCallback = onStopCallback;
            fishingLogic.Start();
            // return true (true means the botbase got started successfully)
            return true;
        }

        public void Stop()
        {
            // if _logic is null the botbase isnt running   
            if (fishingLogic == null) return;
            fishingLogic.Stop();
            fishingLogic = null;
        }
    }
}
