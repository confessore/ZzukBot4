using System.Linq;
using TreeTaskCore;
using ZzukBot.Core.Game.Statics;

namespace Quester.TreeTasks
{
    public class Loot : TTask
    {
        public override int Priority => 500;

        public override bool Activate()
        {
            return ObjectManager.Instance.Units.Where(x => x.CanBeLooted).Any();
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
