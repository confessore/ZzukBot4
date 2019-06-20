using TreeTaskCore;
using ZzukBot.Core.Framework;
using ZzukBot.Core.Game.Statics;

namespace Gatherer.TreeTasks
{
    public class Buff : TTask
    {
        public override int Priority => 95;

        public override bool Activate()
        {
            return CustomClasses.Instance.Current.IsBuffRequired();
        }

        public override void Execute()
        {
            CustomClasses.Instance.Current.Rebuff();
            //Common.Instance.DebugMessage("BUFF");
        }
    }
}
