using Harvester.Engine.Modules;
using Harvester.GUI;
using System;
using ZzukBot.Core.Constants;
using ZzukBot.Core.Game.Objects;
using ZzukBot.Core.Game.Statics;

namespace Harvester.Engine
{
    public class Flow
    {
        private CMD CMD { get; }
        private CombatModule CombatModule { get; }
        private ConsumablesModule ConsumablesModule { get; }
        private Inventory Inventory { get; }
        private Lua Lua { get; }
        private NodeScanModule NodeScanModule { get; }
        private ObjectManager ObjectManager { get; }
        private PathModule PathModule { get; }
        private Spell Spell { get; }

        public Flow(CMD cmd, CombatModule combatModule, ConsumablesModule consumablesModule, 
            Inventory inventory, Lua lua, NodeScanModule nodeScanModule, 
            ObjectManager objectManager, PathModule pathModule, Spell spell)
        {
            CMD = cmd;
            CombatModule = combatModule;
            ConsumablesModule = consumablesModule;
            Inventory = inventory;
            Lua = lua;
            NodeScanModule = nodeScanModule;
            ObjectManager = objectManager;
            PathModule = pathModule;
            Spell = spell;
        }

        public void ExecuteFlow()
        {

        }
    }
}
