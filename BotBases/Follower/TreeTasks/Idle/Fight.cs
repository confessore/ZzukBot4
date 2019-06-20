using System.Collections.Generic;
using System.Linq;
using TreeTaskCore;
using ZzukBot.Core.Framework;
using ZzukBot.Core.Game.Objects;
using ZzukBot.Core.Game.Statics;

namespace Follower.TreeTasks.Party
{
    public class Fight : TTask
    {
        public override int Priority => 98;

        public override bool Activate()
        {
            return
                PartyMembers.Where(x => NotNullOrDead(x) && x.IsInCombat).Any();
        }

        public override void Execute()
        {
            Common.Instance.DebugMessage("COMBAT");
            if (ObjectManager.Instance.Units.Count() > 0)
                CustomClasses.Instance.Current.Fight(ObjectManager.Instance.Units.Where(x => x.IsInCombat || x.GotDebuff("Polymorph")));
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
