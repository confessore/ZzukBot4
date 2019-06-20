using System.Linq;
using TreeTaskCore;
using ZzukBot.Core.Framework;
using ZzukBot.Core.Game.Statics;

namespace Quester.TreeTasks
{
    public class Fight : TTask
    {
        public override int Priority => 999;

        public override bool Activate()
        {
            return ObjectManager.Instance.Player.IsInCombat;
        }

        public override void Execute()
        {
            CustomClasses.Instance.Current.Fight(ObjectManager.Instance.Units.Where(x => x.IsInCombat));
        }
    }
}
