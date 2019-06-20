using Harvester.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using ZzukBot.Core.Constants;
using ZzukBot.Core.Game.Objects;
using ZzukBot.Core.Game.Statics;

namespace Harvester.Engine.Modules
{
    public class NodeScanModule
    {
        private CMD CMD { get; }
        private ObjectManager ObjectManager { get; }
        private Skills Skills { get; }

        public NodeScanModule(CMD cmd, ObjectManager objectManager, Skills skills)
        {
            CMD = cmd;
            ObjectManager = objectManager;
            Skills = skills;
        }

        public List<ulong> blacklist = new List<ulong> { };

        public int HerbLevel()
        {
            List<Skills.Skill> skills = Skills.GetAllPlayerSkills();
            Skills.Skill herb = skills.Where(x => x.Id == Enums.Skills.HERBALISM).FirstOrDefault();

            return herb.CurrentLevel;
        }

        public int MineLevel()
        {
            List<Skills.Skill> skills = Skills.GetAllPlayerSkills();
            Skills.Skill mine = skills.Where(x => x.Id == Enums.Skills.MINING).FirstOrDefault();

            return mine.CurrentLevel;
        }

        public WoWGameObject ClosestNode()
        {
            List<WoWGameObject> herbNodes = ObjectManager.GameObjects
                .Where(x => x.GatherInfo.Type == Enums.GatherType.Herbalism).ToList();
            List<WoWGameObject> mineNodes = ObjectManager.GameObjects
                .Where(x => x.GatherInfo.Type == Enums.GatherType.Mining).ToList();

            herbNodes = herbNodes.Where(x => x.GatherInfo.RequiredSkill <= HerbLevel() 
                    && CMD.herbCheckedBoxes.Any(y => y == x.Name)
                    && !blacklist.Any(z => z == x.Guid)).ToList();
            mineNodes = mineNodes.Where(x => x.GatherInfo.RequiredSkill <= MineLevel() 
                    && CMD.mineCheckedBoxes.Any(y => y == x.Name)
                    && !blacklist.Any(z => z == x.Guid)).ToList();

            return herbNodes.Concat(mineNodes).OrderBy(x => ObjectManager.Player.Position.GetDistanceTo(x.Position)).FirstOrDefault();
        }

        public WoWUnit NodeGuardian(WoWGameObject closestNode)
        {
            Location nodePosition = closestNode.Position;

            WoWUnit nodeGuardian = ObjectManager.Npcs.Where(x => !x.IsCritter && !x.IsDead
                    && (x.Reaction & Enums.UnitReaction.Friendly) != Enums.UnitReaction.Friendly)
                    .OrderBy(y => ObjectManager.Player.Position.GetDistanceTo(y.Position)).FirstOrDefault();

            if (nodeGuardian != null)
            {
                float d;

                float nodePositionX = nodePosition.X;
                float nodePositionY = nodePosition.Y;
                float nodeGuardianPositionX = nodeGuardian.Position.X;
                float nodeGuardianPositionY = nodeGuardian.Position.Y;

                d = Convert.ToSingle(Math.Sqrt(Math.Pow(nodeGuardianPositionX - nodePositionX, 2)
                    + Math.Pow(nodeGuardianPositionY - nodePositionY, 2)));

                if (d < 15)
                    return nodeGuardian;
            }

            return null;
        }
    }
}
