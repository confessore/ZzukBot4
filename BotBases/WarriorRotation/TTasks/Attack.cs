using TreeTaskCore;
using ZzukBot.Core.Game.Statics;

namespace WarriorRotation.TTasks
{
    public class Attack : TTask
    {
        public override int Priority => 1;

        public override bool Activate()
        {
            return ObjectManager.Instance.Player.IsInCombat
                && ObjectManager.Instance.Target != null;
        }

        public override void Execute()
        {
            Spell.Instance.Attack();
        }
    }
}
