using System;
using TreeTaskCore;
using ZzukBot.Core.Game.Statics;

namespace WarriorRotation.TTasks
{
    public class Pummel : TTask
    {
        public override int Priority => 10001;

        public override bool Activate()
        {
            return ObjectManager.Instance.Player.IsInCombat
                && ObjectManager.Instance.Target != null
                && ObjectManager.Instance.Player.Rage > 9
                && Spell.Instance.CanCast("Pummel")
                && (ObjectManager.Instance.Target.IsCasting
                || ObjectManager.Instance.Target.IsChanneling);
        }

        public override void Execute()
        {
            if (ObjectManager.Instance.Player.GotAura("Berserker Stance"))
                Spell.Instance.Cast("Pummel");
            else
                Spell.Instance.Cast("Berserker Stance");
        }
    }
}
