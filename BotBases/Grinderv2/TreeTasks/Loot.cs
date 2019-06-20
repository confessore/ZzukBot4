using System.Linq;
using TreeTaskCore;
using ZzukBot.Core.Game.Statics;

namespace Grinderv2.TreeTasks
{
    public class Loot : TTask
    {
        public override int Priority => 120;

        public override bool Activate()
        {
            return Settings.CorpseLoot && Inventory.Instance.CountFreeSlots(false) > 0 && ObjectManager.Instance.Units.Where(x => x.CanBeLooted).Any();
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
