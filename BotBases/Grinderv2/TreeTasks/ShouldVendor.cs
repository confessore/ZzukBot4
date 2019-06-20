using Grinderv2.GUI;
using TreeTaskCore;
using ZzukBot.Core.Game.Statics;

namespace Grinderv2.TreeTasks
{
    public class ShouldVendor : TTask
    {
        public override int Priority => 1000;

        public override bool Activate()
        {
            return Settings.Vendor && !CMD.ShouldVendor && Inventory.Instance.CountFreeSlots(false) <= Settings.FreeSlotsToVendor;
        }

        public override void Execute()
        {
            CMD.ShouldVendor = true;
        }
    }
}
