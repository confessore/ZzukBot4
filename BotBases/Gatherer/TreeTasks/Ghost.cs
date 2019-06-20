using TreeTaskCore;
using ZzukBot.Core.Game.Statics;

namespace Gatherer.TreeTasks
{
    public class Ghost : TTask
    {
        public override int Priority => 150;

        public override bool Activate()
        {
            return ObjectManager.Instance.Player.InGhostForm;
        }

        public override void Execute()
        {
            if (ObjectManager.Instance.Player.CorpsePosition.DistanceToPlayer() < 20)
                ObjectManager.Instance.Player.RetrieveCorpse();
            Navigation.Instance.Traverse(ObjectManager.Instance.Player.CorpsePosition);
            //Common.Instance.DebugMessage("CORPSE");
        }
    }
}
