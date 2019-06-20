using System;
using TreeTaskCore;
using ZzukBot.Core.Framework;
using ZzukBot.Core.Game.Statics;

namespace Follower.TreeTasks.Party
{
    public class Buff : TTask
    {
        public override int Priority => 99;

        public override bool Activate()
        {
            return !ObjectManager.Instance.Player.IsDead && ObjectManager.Instance.Player.ManaPercent > 50 &&
                (CustomClasses.Instance.Current.IsBuffRequired() ||
                CustomClasses.Instance.Current.CanBuffAnotherPlayer());
        }

        public override void Execute()
        {
            Common.Instance.DebugMessage("BUFF");
            ObjectManager.Instance.Player.SetTarget(ObjectManager.Instance.Player);
            CustomClasses.Instance.Current.Rebuff();
            CustomClasses.Instance.Current.TryToBuffAnotherPlayer(ObjectManager.Instance.Player);
        }
    }
}
