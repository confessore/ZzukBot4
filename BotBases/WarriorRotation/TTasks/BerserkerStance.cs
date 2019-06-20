using TreeTaskCore;
using ZzukBot.Core.Game.Statics;

namespace WarriorRotation.TTasks
{
    public class BerserkerStance : TTask
    {
        public override int Priority => 5000;

        public override bool Activate()
        {
            return ObjectManager.Instance.Player.IsInCombat
                && Spell.Instance.CanCast("Berserker Stance")
                && !ObjectManager.Instance.Player.GotAura("Berserker Stance");
        }

        public override void Execute()
        {
            Spell.Instance.Cast("Berserker Stance");
        }
    }
}
