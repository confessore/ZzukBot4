using System.Linq;
using TreeTaskCore;
using ZzukBot.Core.Game.Statics;

namespace WarriorRotation.TTasks
{
    public class SunderArmor : TTask
    {
        public override int Priority => 101;

        public override bool Activate()
        {
            return ObjectManager.Instance.Player.IsInCombat
                && ObjectManager.Instance.Target != null
                && ObjectManager.Instance.Player.Rage > 59
                && Spell.Instance.CanCast("Sunder Armor")
                && !Spell.Instance.CanCast("Bloodthirst")
                && !Spell.Instance.CanCast("Whirlwind");
                //&& ObjectManager.Instance.Target.Debuffs.Where(x => x == Spell.Instance.GetId("Sunder Armor")).Count() < 1;
        }

        public override void Execute()
        {
            Spell.Instance.Cast("Sunder Armor");
        }
    }
}
