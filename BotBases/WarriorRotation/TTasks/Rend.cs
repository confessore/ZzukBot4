using System.Linq;
using TreeTaskCore;
using ZzukBot.Core.Game.Statics;
using static ZzukBot.Core.Constants.Enums;

namespace WarriorRotation.TTasks
{
    public class Rend : TTask
    {
        public override int Priority => 5001;

        public override bool Activate()
        {
            return ObjectManager.Instance.Player.IsInCombat
                && ObjectManager.Instance.Target != null
                && ObjectManager.Instance.Player.Rage > 9
                && ObjectManager.Instance.Player.Rage < 30
                && ObjectManager.Instance.Target.HealthPercent > 20
                && Spell.Instance.CanCast("Rend")
                && !ObjectManager.Instance.Target.GotDebuff("Rend")
                && ObjectManager.Instance.Target.Level < 60
                && ObjectManager.Instance.Target.CreatureType != CreatureType.Elemental
                && ObjectManager.Instance.Target.CreatureType != CreatureType.Mechanical
                && ObjectManager.Instance.Target.CreatureType != CreatureType.Undead;
            //&& ObjectManager.Instance.Target.Debuffs.Where(x => x == Spell.Instance.GetId("Sunder Armor")).Count() < 1;
        }

        public override void Execute()
        {
            if (ObjectManager.Instance.Player.GotAura("Battle Stance"))
                Spell.Instance.Cast("Rend");
            else
                Spell.Instance.Cast("Battle Stance");
        }
    }
}
