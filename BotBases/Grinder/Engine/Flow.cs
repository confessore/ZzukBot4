using Grinder.Engine.Modules;
using Grinder.GUI;
using Grinder.Settings;
using ZzukBot.Core.Game.Statics;

namespace Grinder.Engine
{
    public class Flow
    {
        CMD CMD { get; }
        CombatModule CombatModule { get; }
        ConsumablesModule ConsumablesModule { get; }
        Inventory Inventory { get; }
        Lua Lua { get; }
        MerchantModule MerchantModule { get; }
        NPCScanModule NPCScanModule { get; }
        ObjectManager ObjectManager { get; }
        PathModule PathModule { get; }
        Spell Spell { get; }

        public Flow(CMD cmd,
            CombatModule combatModule,
            ConsumablesModule consumablesModule,
            Inventory inventory,
            Lua lua,
            MerchantModule merchantModule,
            NPCScanModule npcScanModule,
            ObjectManager objectManager,
            PathModule pathModule,
            Spell spell)
        {
            CMD = cmd;
            CombatModule = combatModule;
            ConsumablesModule = consumablesModule;
            Inventory = inventory;
            Lua = lua;
            MerchantModule = merchantModule;
            NPCScanModule = npcScanModule;
            ObjectManager = objectManager;
            PathModule = pathModule;
            Spell = spell;
        }

        public void ExecuteFlow()
        {
            //if (ObjectManager.Target != null)
                //Common.Instance.DebugMessage(ObjectManager.Target.FactionId.ToString());
            if (MerchantModule.NeedToVendor())
                MerchantModule.Vendoring = true;
            if (ObjectManager.Player.IsInCombat)
                CombatModule.Fight();
            else
            {
                var closestSkinnableNpc = NPCScanModule.ClosestSkinnableNPC();
                var closestLootableNpc = NPCScanModule.ClosestLootableNPC();
                if (!MerchantModule.Vendoring)
                {
                    if (closestLootableNpc != null && GrinderDefault.CorpseLoot)
                        NPCScanModule.LootCorpse(closestLootableNpc);
                    else if (closestSkinnableNpc != null && GrinderDefault.CorpseLoot && GrinderDefault.CorpseSkin)
                        NPCScanModule.LootCorpse(closestSkinnableNpc);
                    else
                    {
                        if (CombatModule.IsReadyToFight())
                        {
                            if (!CombatModule.IsBuffRequired())
                            {
                                var closestCombattableNpc = NPCScanModule.ClosestCombattableNPC();
                                if (closestCombattableNpc != null &&
                                    NPCScanModule.UnitInHotspots(closestCombattableNpc, PathModule.Hotspots, GrinderDefault.SearchRadius))
                                    CombatModule.Pull(closestCombattableNpc);
                                else
                                    PathModule.Traverse(PathModule.GetNextHotspot());
                            }
                            else
                                CombatModule.Rebuff();
                        }
                        else
                            CombatModule.PrepareForFight();
                    }
                }
                else
                {
                    if (CombatModule.IsReadyToFight())
                    {
                        if (!CombatModule.IsBuffRequired())
                        {
                            var closestRepairNPC = NPCScanModule.RepairNPCFromProfile();
                            var closestCombattableNpcV = NPCScanModule.ClosestCombattableNPCV();
                            if (closestRepairNPC != null)
                            {
                                NPCScanModule.InteractWithVendor(closestRepairNPC);
                                MerchantModule.RepairAndVendor();
                            }
                            else
                            {
                                if (closestCombattableNpcV != null &&
                                    NPCScanModule.UnitInHotspots(closestCombattableNpcV, PathModule.Vendor, GrinderDefault.SearchRadius))
                                    CombatModule.Pull(closestCombattableNpcV);
                                else
                                    PathModule.Traverse(PathModule.GetNextVendorHotspot());
                            }
                        }
                        else
                            CombatModule.Rebuff();
                    }
                    else
                        CombatModule.PrepareForFight();
                }
            }
        }
    }
}
