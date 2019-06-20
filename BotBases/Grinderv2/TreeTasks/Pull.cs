using Grinderv2.GUI;
using System.Linq;
using TreeTaskCore;
using ZzukBot.Core.Framework;
using ZzukBot.Core.Game.Statics;

namespace Grinderv2.TreeTasks
{
    public class Pull : TTask
    {
        public override int Priority => 95;

        public override bool Activate()
        {
            return !ObjectManager.Instance.Player.IsInCombat 
                && CustomClasses.Instance.Current.IsReadyToFight(ObjectManager.Instance.Units) 
                && ObjectManager.Instance.Units.Where(x => CMD.CreatureWhitelist.Any(y => y.Id == x.Id) 
                    && !x.IsDead && !x.IsPet && !x.TappedByOther 
                    && ObjectManager.Instance.Player.InLosWith(x)).Any();
        }

        public override void Execute()
        {
            CustomClasses.Instance.Current.Pull(
                ObjectManager.Instance.Units.Where(x => CMD.CreatureWhitelist.Any(y => y.Id == x.Id)
                    && !x.IsDead && !x.IsPet && !x.TappedByOther 
                    && ObjectManager.Instance.Player.InLosWith(x))
                .OrderBy(x => x.DistanceToPlayer)
                .OrderBy(x => x.Reaction)
                .FirstOrDefault());
        }
    }
}
