using TreeTaskCore;
using ZzukBot.Core.Framework;
using ZzukBot.Core.Game.Statics;

namespace Gatherer.TreeTasks
{
    public class Rest : TTask
    {
        public override int Priority => 101;

        public override bool Activate()
        {
            return !ObjectManager.Instance.Player.IsSwimming && !CustomClasses.Instance.Current.IsReadyToFight(ObjectManager.Instance.Units);
        }

        public override void Execute()
        {
            CustomClasses.Instance.Current.PrepareForFight();
            //Common.Instance.DebugMessage("REST");
        }
    }
}
