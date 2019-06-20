using TreeTaskCore;
using ZzukBot.Core.Framework;
using ZzukBot.Core.Game.Statics;

namespace Grinderv2.TreeTasks
{
    public class Rest : TTask
    {
        public override int Priority => 110;

        public override bool Activate()
        {
            return !CustomClasses.Instance.Current.IsReadyToFight(ObjectManager.Instance.Units) 
                && !ObjectManager.Instance.Player.IsSwimming;
        }

        public override void Execute()
        {
            CustomClasses.Instance.Current.PrepareForFight();
        }
    }
}
