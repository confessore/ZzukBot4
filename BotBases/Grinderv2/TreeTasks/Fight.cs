using System.Collections.Generic;
using System.Linq;
using TreeTaskCore;
using ZzukBot.Core.Framework;
using ZzukBot.Core.Game.Objects;
using ZzukBot.Core.Game.Statics;

namespace Grinderv2.TreeTasks
{
    public class Fight : TTask
    {
        public override int Priority => 1000;

        public override bool Activate()
        {
            return PartyMembers.Where(x => NotNullOrDead(x) && x.IsInCombat).Any() && !ObjectManager.Instance.Player.IsSwimming;
        }

        public override void Execute()
        {
            CustomClasses.Instance.Current.Fight(ObjectManager.Instance.Units.Where(x => x.IsInCombat));
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
                ObjectManager.Instance.Player,
                ObjectManager.Instance.Pet
            };
    }
}
