using TreeTaskCore;
using ZzukBot.Core.Game.Statics;

namespace WarriorRotation.TTasks
{
    public class Whirlwind : TTask
    {
        public override int Priority => 99;

        public override bool Activate()
        {
            return ObjectManager.Instance.Player.IsInCombat
                && ObjectManager.Instance.Target != null
                && ObjectManager.Instance.Player.Rage > 24
                && ObjectManager.Instance.Target.HealthPercent > 20
                && Spell.Instance.CanCast("Whirlwind")
                && !Spell.Instance.CanCast("Bloodthirst");
        }

        public override void Execute()
        {
            Spell.Instance.Cast("Whirlwind");
        }
    }
}
