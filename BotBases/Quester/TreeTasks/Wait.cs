using TreeTaskCore;
using ZzukBot.Core.Game.Statics;

namespace Quester.TreeTasks
{
    public class Wait : TTask
    {
        public override int Priority => -1;

        public override bool Activate()
        {
            return true;
        }

        public override void Execute()
        {
            Common.Instance.DebugMessage("WAIT");
        }
    }
}
