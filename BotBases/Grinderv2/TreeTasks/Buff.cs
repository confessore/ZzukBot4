using TreeTaskCore;
using ZzukBot.Core.Framework;

namespace Grinderv2.TreeTasks
{
    public class Buff : TTask
    {
        public override int Priority => 102;

        public override bool Activate()
        {
            return CustomClasses.Instance.Current.IsBuffRequired();
        }

        public override void Execute()
        {
            CustomClasses.Instance.Current.Rebuff();
        }
    }
}
