using System.Linq;
using ZzukBot.Core.Constants;
using ZzukBot.Core.Framework;
using ZzukBot.Core.Game.Objects;
using ZzukBot.Core.Game.Statics;

namespace Harvester.Engine.Modules
{
    public class CombatModule
    {
        private CustomClasses CustomClasses { get; }
        private ObjectManager ObjectManager { get; }
        private PathModule PathModule { get; }

        public CombatModule(CustomClasses customClasses, ObjectManager objectManager, PathModule pathModule)
        {
            CustomClasses = customClasses;
            ObjectManager = objectManager;
            PathModule = pathModule;
        }

        public WoWUnit EliteInCombatNPC()
        {
            return ObjectManager.Npcs.Where(x => x.IsInCombat && x.TargetGuid == ObjectManager.Player.Guid
                && ((x.CreatureRank & Enums.CreatureRankTypes.Elite) == Enums.CreatureRankTypes.Elite
                || (x.CreatureRank & Enums.CreatureRankTypes.RareElite) == Enums.CreatureRankTypes.RareElite))
                .OrderBy(x => ObjectManager.Player.Position.GetDistanceTo(x.Position))
                .FirstOrDefault();
        }

        public WoWUnit ClosestCombattableNPC()
        {
            return ObjectManager.Npcs.Where(x => !x.IsCritter && !x.IsDead && (x.IsMob || x.IsPlayer)
                && x.Reaction != Enums.UnitReaction.Friendly && x.NpcFlags != Enums.NpcFlags.UNIT_NPC_FLAG_VENDOR
                && x.NpcFlags != Enums.NpcFlags.UNIT_NPC_FLAG_FLIGHTMASTER && x.NpcFlags != Enums.NpcFlags.UNIT_NPC_FLAG_GOSSIP
                && x.NpcFlags != Enums.NpcFlags.UNIT_NPC_FLAG_QUESTGIVER && x.NpcFlags != Enums.NpcFlags.UNIT_NPC_FLAG_REPAIR
                && x.NpcFlags != Enums.NpcFlags.UNIT_NPC_FLAG_STABLEMASTER
                && x.CreatureRank != Enums.CreatureRankTypes.Elite && x.Guid != ObjectManager.Player.Guid)
                .OrderBy(x => ObjectManager.Player.Position.GetDistanceTo(x.Position))
                .FirstOrDefault();
        }

        public WoWUnit ClosestLootableNPC()
        {
            return ObjectManager.Npcs.Where(x => x.CanBeLooted)
                .OrderBy(x => ObjectManager.Player.Position.GetDistanceTo(x.Position))
                .FirstOrDefault();
        }

        public void Fight()
        {
            if (ObjectManager.Units.Count() > 0 && ObjectManager.Target != null)
                CustomClasses.Current.Fight(ObjectManager.Units);
        }

        public bool IsReadyToFight()
        {
            return CustomClasses.Current.IsReadyToFight(ObjectManager.Units);
        }

        public void Pull(WoWUnit target)
        {
            if (target != null)
                CustomClasses.Current.Pull(target);
        }
    }
}
