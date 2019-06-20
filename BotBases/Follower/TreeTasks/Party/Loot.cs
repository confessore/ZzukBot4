using System.Linq;
using TreeTaskCore;
using ZzukBot.Core.Game.Statics;

namespace Follower.TreeTasks.Idle
{
    public class Loot : TTask
    {
        public override int Priority => 80;

        public override bool Activate()
        {
            return ObjectManager.Instance.Units.Where(x => x.CanBeLooted && x.DistanceToPlayer < 20).Any();
        }

        public override void Execute()
        {
            var target = ObjectManager.Instance.Units
                .Where(x => x.CanBeLooted)
                .OrderBy(x => x.DistanceToPlayer)
                .FirstOrDefault();
            if (target.DistanceToPlayer > 5f)
                Navigation.Instance.Traverse(target.Position);
            else
                target.Interact(true);
        }
    }
}
