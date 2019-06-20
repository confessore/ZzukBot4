using TreeTaskCore;
using ZzukBot.Core.Game.Statics;

namespace Quester.TreeTasks
{
    public class Rest : TTask
    {
        public override int Priority => 120;

        public override bool Activate()
        {
            return !ObjectManager.Instance.Player.IsInCombat
                && ObjectManager.Instance.Player.HealthPercent < 51
                || ObjectManager.Instance.Player.GotAura("Food")
                || ObjectManager.Instance.Player.GotAura("Drink");
        }

        public override void Execute()
        {
            Common.Instance.TryUseFood();
        }
    }
}
