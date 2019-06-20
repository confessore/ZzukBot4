using TreeTaskCore;
using ZzukBot.Core.Game.Statics;

namespace WarriorRotation.TTasks
{
    public class Bloodthirst : TTask
    {
        public override int Priority => 100;

        public override bool Activate()
        {
            return ObjectManager.Instance.Player.IsInCombat
                && ObjectManager.Instance.Target != null
                && ObjectManager.Instance.Player.Rage > 29
                && ObjectManager.Instance.Target.HealthPercent > 20
                && Spell.Instance.CanCast("Bloodthirst");
        }

        public override void Execute()
        {
            Spell.Instance.Cast("Bloodthirst");
        }
    }
}
