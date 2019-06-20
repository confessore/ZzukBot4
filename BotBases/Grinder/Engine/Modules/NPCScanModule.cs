using Grinder.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using ZzukBot.Core.Constants;
using ZzukBot.Core.Framework.Loaders;
using ZzukBot.Core.Game.Frames;
using ZzukBot.Core.Game.Objects;
using ZzukBot.Core.Game.Statics;

namespace Grinder.Engine.Modules
{
    public class NPCScanModule
    {
        ObjectManager ObjectManager { get; }
        PathModule PathModule { get; }
        ProfileLoader ProfileLoader { get; }
        Skills Skills { get; }

        public NPCScanModule(
            ObjectManager objectManager,
            PathModule pathModule,
            ProfileLoader profileLoader,
            Skills skills)
        {
            ObjectManager = objectManager;
            PathModule = pathModule;
            ProfileLoader = profileLoader;
            Skills = skills;
        }

        public WoWUnit ClosestCombattableNPC()
        {
            return ObjectManager.Npcs.Where(x =>
            !x.IsCritter && !x.IsDead && (!x.IsPlayerPet && x.IsMob || (x.IsPlayer && x.IsInCombat &&
            x.NearbyPlayers.Where(y => y.IsInCombat && y.Guid == x.TargetGuid &&
            !(y.Reaction.Equals(Enums.UnitReaction.Neutral) || y.Reaction.Equals(Enums.UnitReaction.Hostile) || y.Reaction.Equals(Enums.UnitReaction.Hostile2))).Any()))
            && !x.TappedByOther &&
            !Common.Instance.FactionBlacklist().Where(y => y == x.FactionId).Any() &&
            x.Reaction != Enums.UnitReaction.Friendly && x.NpcFlags != Enums.NpcFlags.UNIT_NPC_FLAG_VENDOR &&
            x.NpcFlags != Enums.NpcFlags.UNIT_NPC_FLAG_FLIGHTMASTER && x.NpcFlags != Enums.NpcFlags.UNIT_NPC_FLAG_GOSSIP &&
            x.NpcFlags != Enums.NpcFlags.UNIT_NPC_FLAG_QUESTGIVER && x.NpcFlags != Enums.NpcFlags.UNIT_NPC_FLAG_REPAIR &&
            x.NpcFlags != Enums.NpcFlags.UNIT_NPC_FLAG_STABLEMASTER && x.CreatureRank != Enums.CreatureRankTypes.Elite &&
            ObjectManager.Player.InLosWith(x) &&
            /*Enumerable.Range((int)ObjectManager.Player.Position.Z - 5, (int)ObjectManager.Player.Position.Z + 5).Contains((int)x.Position.Z)*/
            x.Guid != ObjectManager.Player.Guid)
                .OrderBy(x => ObjectManager.Player.Position.GetDistanceTo(x.Position))
                .FirstOrDefault();
        }

        public WoWUnit ClosestCombattableNPCV()
        {
            return ObjectManager.Npcs.Where(x =>
            !x.IsCritter && !x.IsDead && (!x.IsPlayerPet && x.IsMob || (x.IsPlayer && x.IsInCombat &&
            x.NearbyPlayers.Where(y => y.IsInCombat && y.Guid == x.TargetGuid &&
            !(y.Reaction.Equals(Enums.UnitReaction.Neutral) || y.Reaction.Equals(Enums.UnitReaction.Hostile) || y.Reaction.Equals(Enums.UnitReaction.Hostile2))).Any())) 
            && !x.TappedByOther &&
            x.Level > ObjectManager.Player.Level - 5 &&
            !Common.Instance.FactionBlacklist().Where(y => y == x.FactionId).Any() &&
            x.Reaction != Enums.UnitReaction.Friendly && x.Reaction != Enums.UnitReaction.Neutral &&
            x.NpcFlags != Enums.NpcFlags.UNIT_NPC_FLAG_VENDOR &&
            x.NpcFlags != Enums.NpcFlags.UNIT_NPC_FLAG_FLIGHTMASTER && x.NpcFlags != Enums.NpcFlags.UNIT_NPC_FLAG_GOSSIP &&
            x.NpcFlags != Enums.NpcFlags.UNIT_NPC_FLAG_QUESTGIVER && x.NpcFlags != Enums.NpcFlags.UNIT_NPC_FLAG_REPAIR &&
            x.NpcFlags != Enums.NpcFlags.UNIT_NPC_FLAG_STABLEMASTER && x.CreatureRank != Enums.CreatureRankTypes.Elite &&
            ObjectManager.Player.InLosWith(x) &&
            /*Enumerable.Range((int)ObjectManager.Player.Position.Z - 5, (int)ObjectManager.Player.Position.Z + 5).Contains((int)x.Position.Z)*/
            x.Guid != ObjectManager.Player.Guid)
                .OrderBy(x => ObjectManager.Player.Position.GetDistanceTo(x.Position))
                .FirstOrDefault();
        }

        public WoWUnit ClosestLootableNPC()
        {
            return ObjectManager.Npcs.Where(x => x.CanBeLooted)
                .OrderBy(x => ObjectManager.Player.Position.GetDistanceTo(x.Position))
                .FirstOrDefault();
        }

        public WoWUnit ClosestRepairVendor()
        {
            return ObjectManager.Npcs.Where(x => !x.IsDead &&
                (x.Reaction == Enums.UnitReaction.Friendly || x.Reaction == Enums.UnitReaction.Neutral) &&
                ((x.NpcFlags == Enums.NpcFlags.UNIT_NPC_FLAG_REPAIR && x.NpcFlags == Enums.NpcFlags.UNIT_NPC_FLAG_VENDOR) ||
                x.NpcFlags == Enums.NpcFlags.UNIT_NPC_FLAG_REPAIR))
                .OrderBy(x => ObjectManager.Player.Position.GetDistanceTo(x.Position))
                .FirstOrDefault();
        }

        public WoWUnit ClosestSkinnableNPC()
        {
            return ObjectManager.Npcs.Where(x => x.IsDead && x.IsSkinnable &&
            (Skills.GetAllPlayerSkills().Where(y => y.Id == Enums.Skills.SKINNING).FirstOrDefault() != null ?
            Skills.GetAllPlayerSkills().Where(y => y.Id == Enums.Skills.SKINNING).FirstOrDefault().CurrentLevel >= x.RequiredSkinningLevel : false) &&
            (GrinderDefault.NinjaSkin ? (x.TappedByMe || x.TappedByOther) : x.TappedByMe)).FirstOrDefault();
        }

        public WoWUnit ClosestVendor()
        {
            return ObjectManager.Npcs.Where(x => !x.IsDead &&
                (x.Reaction == Enums.UnitReaction.Friendly || x.Reaction == Enums.UnitReaction.Neutral) &&
                x.NpcFlags == Enums.NpcFlags.UNIT_NPC_FLAG_VENDOR)
                .OrderBy(x => ObjectManager.Player.Position.GetDistanceTo(x.Position))
                .FirstOrDefault();
        }

        public void LootCorpse(WoWUnit corpse)
        {
            if (corpse.DistanceToPlayer < 2)
                corpse.Interact(true);
            else
                PathModule.Traverse(corpse.Position);
        }

        public void InteractWithVendor(WoWUnit vendor)
        {
            if (!MerchantFrame.IsOpen)
            {
                if (vendor.DistanceToPlayer < 2)
                    vendor.Interact(false);
                else
                    PathModule.Traverse(vendor.Position);
            }
        }

        public WoWUnit RepairNPCFromProfile()
        {
            return ObjectManager.Npcs.Where(x => x.Name == ProfileLoader.VendorName).FirstOrDefault();
        }

        public bool UnitInHotspots(WoWUnit unit, List<Location> hotspots, int searchRadius)
        {
            List<bool> unitIsInHotspot = new List<bool>();

            foreach (var hotspot in hotspots)
            {
                var d = Convert.ToSingle(Math.Sqrt(Math.Pow(unit.Position.X - hotspot.X, 2)
                    + Math.Pow(unit.Position.Y - hotspot.Y, 2)));

                if (d < searchRadius)
                    unitIsInHotspot.Add(true);
                else
                    unitIsInHotspot.Add(false);
            }

            return unitIsInHotspot.Any(x => x == true);
        }

        public bool HasAdds(WoWUnit unit)
        {
            List<bool> hasAdds = new List<bool>();

            foreach (var npc in ObjectManager.Npcs)
            {
                var d = Convert.ToSingle(Math.Sqrt(Math.Pow(npc.Position.X - unit.Position.X, 2)
                    + Math.Pow(npc.Position.Y - unit.Position.Y, 2)));

                if (d < 10)
                    hasAdds.Add(true);
                else
                    hasAdds.Add(false);
            }

            return hasAdds.Any(x => x == true);
        }

        /*public List<bool> UnitIsInHotspotTest(WoWUnit unit, List<List<Location>> pathsList)
        {
            List<bool> unitIsInHotspot = new List<bool>();

            foreach (var path in pathsList)
            {
                foreach (var hotspot in path)
                {
                    var d = Convert.ToSingle(Math.Sqrt(Math.Pow(unit.Position.X - hotspot.X, 2)
                    + Math.Pow(unit.Position.Y - hotspot.Y, 2)));

                    if (d < 30)
                        unitIsInHotspot.Add(true);
                    else
                        unitIsInHotspot.Add(false);
                }
            }

            return unitIsInHotspot;
        }*/
    }
}
