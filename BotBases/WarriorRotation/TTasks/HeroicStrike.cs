using TreeTaskCore;
using ZzukBot.Core.Game.Statics;

namespace WarriorRotation.TTasks
{
    public class HeroicStrike : TTask
    {
        public override int Priority => 97;

        public override bool Activate()
        {
            return ObjectManager.Instance.Player.IsInCombat
                && ObjectManager.Instance.Target != null
                && ObjectManager.Instance.Player.Rage > 49
                && ObjectManager.Instance.Target.HealthPercent > 20
                && Spell.Instance.CanCast("Heroic Strike")
                && !Spell.Instance.CanCast("Bloodthirst")
                && !Spell.Instance.CanCast("Whirlwind");
            /* return ObjectManager.Instance.Player.IsInCombat
                && ObjectManager.Instance.Target != null
                && Spell.Instance.CanCast("Heroic Strike")
                && ((ObjectManager.Instance.Player.Rage > 49
                && ObjectManager.Instance.Target.HealthPercent > 20
                && !Spell.Instance.CanCast("Bloodthirst")
                && !Spell.Instance.CanCast("Whirlwind"))
                || (ObjectManager.Instance.Player.Rage > 14)
                && (!Spell.Instance.IsKnown("Bloodthirst")
                || !Spell.Instance.IsKnown("Whirlwind"))); */
        }

        public override void Execute()
        {
            Spell.Instance.Cast("Heroic Strike");
        }
    }
}
