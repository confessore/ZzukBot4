using System.Collections.Generic;
using System.Linq;
using TreeTaskCore;
using ZzukBot.Core.Constants;
using ZzukBot.Core.Game.Objects;
using ZzukBot.Core.Game.Statics;

namespace Follower.TreeTasks.Party
{
    public class Heal : TTask
    {
        public override int Priority => 100;

        public override bool Activate()
        {
            return ObjectManager.Instance.Player.Class == Enums.ClassId.Priest &&
                ObjectManager.Instance.Player.ManaPercent > 20 &&
                PartyMembers.Where(x => NotNullOrDead(x) && x.HealthPercent < 65).Any();
        }

        public override void Execute()
        {
            Common.Instance.DebugMessage("HEAL");
            var partyMember = PartyMembers.Where(x => NotNullOrDead(x) && x.HealthPercent < 65).FirstOrDefault();
            if (partyMember != null)
            {
                ObjectManager.Instance.Player.SetTarget(partyMember.Guid);
                if (partyMember.DistanceToPlayer > 29f)
                    Navigation.Instance.Traverse(partyMember.Position);
                else
                {
                    //if (Spell.Instance.IsKnown("Greater Heal") && partyMember.HealthPercent < 50)
                    //    Spell.Instance.Cast("Greater Heal");
                    //else if (Spell.Instance.IsKnown("Heal"))
                        //Spell.Instance.Cast("Heal");
                    //else
                    Spell.Instance.Cast("Heal");
                }
                ObjectManager.Instance.Player.SetTarget(null);
            }
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
