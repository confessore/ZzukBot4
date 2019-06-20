using ZzukBot.Core.Game.Statics;
using TreeTaskCore;

namespace Grinderv2.TreeTasks
{
    public class Dead : TTask
    {
        public override int Priority => 150;

        public override bool Activate()
        {
            return ObjectManager.Instance.Player.IsDead;
        }

        public override void Execute()
        {
            ObjectManager.Instance.Player.RepopMe();
        }
    }
}
