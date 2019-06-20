using TreeTaskCore;
using ZzukBot.Core.Game.Statics;

namespace WarriorRotation.TTasks
{
    public class BloodRage : TTask
    {
        public override int Priority => 1001;

        public override bool Activate()
        {
            return ObjectManager.Instance.Player.IsInCombat
                && Spell.Instance.CanCast("Blood Rage")
                && ObjectManager.Instance.Player.GotAura("Blood Rage");
        }

        public override void Execute()
        {
            Spell.Instance.Cast("Blood Rage");
        }
    }
}
