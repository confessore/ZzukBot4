using System.Collections.Generic;
using System.Linq;
using TreeTaskCore;
using ZzukBot.Core.Game.Objects;
using ZzukBot.Core.Game.Statics;

namespace Follower.TreeTasks.Party
{
    public class Follow : TTask
    {
        public override int Priority => 50;

        public override bool Activate()
        {
            if (PartyMembers.Where(x => x != null && !x.IsDead).FirstOrDefault() != null)
                return PartyMembers.Where(x => x != null && !x.IsDead)
                    .FirstOrDefault().DistanceToPlayer > 5f;
            return false;
        }

        public override void Execute()
        {
            Common.Instance.DebugMessage("FOLLOW");
            Navigation.Instance.Traverse(ObjectManager.Instance.PartyLeader.Position);
        }

        bool NotNullOrDead(WoWUnit partyMember) => partyMember != null && !partyMember.IsDead;

        List<WoWUnit> PartyMembers =>
            new List<WoWUnit>()
            {
                ObjectManager.Instance.PartyLeader,
                ObjectManager.Instance.Party1,
                ObjectManager.Instance.Party2,
                ObjectManager.Instance.Party3,
                ObjectManager.Instance.Party4,
                ObjectManager.Instance.Player
            };
    }
}
