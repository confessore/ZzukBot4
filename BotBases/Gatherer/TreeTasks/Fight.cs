using Gatherer.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using TreeTaskCore;
using ZzukBot.Core.Constants;
using ZzukBot.Core.Framework;
using ZzukBot.Core.Game.Objects;
using ZzukBot.Core.Game.Statics;

namespace Gatherer.TreeTasks
{
    public class Fight : TTask
    {
        public override int Priority => 102;

        public int HerbLevel()
        {
            List<Skills.Skill> skills = Skills.Instance.GetAllPlayerSkills();
            Skills.Skill herb = skills.Where(x => x.Id == Enums.Skills.HERBALISM).FirstOrDefault();

            return herb != null ? herb.CurrentLevel : 0;
        }

        public int MineLevel()
        {
            List<Skills.Skill> skills = Skills.Instance.GetAllPlayerSkills();
            Skills.Skill mine = skills.Where(x => x.Id == Enums.Skills.MINING).FirstOrDefault();

            return mine != null ? mine.CurrentLevel : 0;
        }

        public WoWGameObject ClosestNode()
        {
            List<WoWGameObject> herbNodes = ObjectManager.Instance.GameObjects
                .Where(x => x.GatherInfo.Type == Enums.GatherType.Herbalism).ToList();
            List<WoWGameObject> mineNodes = ObjectManager.Instance.GameObjects
                .Where(x => x.GatherInfo.Type == Enums.GatherType.Mining).ToList();

            herbNodes = herbNodes.Where(x => x.GatherInfo.RequiredSkill <= HerbLevel()
                    && CMD.herbCheckedBoxes.Any(y => (int)Enum.Parse(typeof(Models.Herbs), y) == x.Id)
                    && !CMD.guidBlacklist.Any(z => z == x.Guid)).ToList();
            mineNodes = mineNodes.Where(x => x.GatherInfo.RequiredSkill <= MineLevel()
                    && CMD.oreCheckedBoxes.Any(y => (int)Enum.Parse(typeof(Models.Ores), y) == x.Id)
                    && !CMD.guidBlacklist.Any(z => z == x.Guid)).ToList();

            return herbNodes.Concat(mineNodes).OrderBy(x => x.Position.DistanceToPlayer()).FirstOrDefault();
        }

        public override bool Activate()
        {
            return !ObjectManager.Instance.Player.IsSwimming 
                && (!ObjectManager.Instance.Player.IsMounted || (ClosestNode() != null && ClosestNode().Position.DistanceToPlayer() < 10f))
                && ObjectManager.Instance.Player.IsInCombat 
                && ObjectManager.Instance.Units.Where(x => x.IsInCombat && !x.IsSwimming).Any();
        }

        public override void Execute()
        {
            CustomClasses.Instance.Current.Fight(ObjectManager.Instance.Units.Where(x => x.IsInCombat && !x.IsSwimming));
            //Common.Instance.DebugMessage("FIGHT");
        }
    }
}
