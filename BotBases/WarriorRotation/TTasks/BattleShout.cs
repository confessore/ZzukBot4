using TreeTaskCore;
using ZzukBot.Core.Game.Statics;

namespace WarriorRotation.TTasks
{
    public class BattleShout : TTask
    {
        public override int Priority => 1000;

        public override bool Activate()
        {
            return ObjectManager.Instance.Player.IsInCombat
                && ObjectManager.Instance.Player.Rage > 9
                && Spell.Instance.CanCast("Battle Shout")
                && !ObjectManager.Instance.Player.GotAura("Battle Shout")
                && !ObjectManager.Instance.Player.IsSilenced;
        }

        public override void Execute()
        {
            Spell.Instance.Cast("Battle Shout");
        }
    }
}
