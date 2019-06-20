using System.Linq;
using TreeTaskCore;
using ZzukBot.Core.Game.Statics;

namespace Gatherer.TreeTasks
{
    public class Mount : TTask
    {
        public override int Priority => 51;

        public override bool Activate()
        {
            return ObjectManager.Instance.Player.Level >= 40 
                && Common.Instance.ExistingMounts().Count() > 0 
                && !ObjectManager.Instance.Player.IsMounted
                && !ObjectManager.Instance.Player.IsSwimming;
        }

        public override void Execute()
        {
            Common.Instance.TryUseMount();
        }
    }
}
