using TreeTaskCore;
using ZzukBot.Core.Game.Statics;

namespace WarriorRotation.TTasks
{
    public class X : TTask
    {
        public override int Priority => 999;

        public override bool Activate()
        {
            return ObjectManager.Instance.Player.IsInCombat
                && ObjectManager.Instance.Target != null
                && ObjectManager.Instance.Player.Rage > 9
                && ObjectManager.Instance.Target.HealthPercent <= 20
                && Spell.Instance.CanCast("Execute");
        }

        public override void Execute()
        {
            Spell.Instance.Cast("Execute");
        }
    }
}
