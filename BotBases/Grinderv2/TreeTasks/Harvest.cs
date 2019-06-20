using Grinderv2.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using TreeTaskCore;
using ZzukBot.Core.Constants;
using ZzukBot.Core.Game.Objects;
using ZzukBot.Core.Game.Statics;

namespace Grinderv2.TreeTasks
{
    public class Harvest : TTask
    {
        public override int Priority => 101;

        public void AddPosition()
        {
            CMD.Positions.Add(Convert.ToInt32(ObjectManager.Instance.Player.Position.X).ToString()
                            + Convert.ToInt32(ObjectManager.Instance.Player.Position.Y).ToString()
                            + Convert.ToInt32(ObjectManager.Instance.Player.Position.Z).ToString());
        }

        public bool Stuck => CMD.Positions.FindAll(x => x.Equals(CMD.Positions.Last())).Count() > 20;

        public int HerbLevel()
        {
            List<Skills.Skill> skills = Skills.Instance.GetAllPlayerSkills();
            Skills.Skill herb = skills.Where(x => x.Id == Enums.Skills.HERBALISM).FirstOrDefault();

            return herb != null ? ObjectManager.Instance.Player.FactionId == (int)Enums.FactionPlayerHorde.Tauren ? herb.CurrentLevel + 15 : herb.CurrentLevel : -1;
        }

        public int MineLevel()
        {
            List<Skills.Skill> skills = Skills.Instance.GetAllPlayerSkills();
            Skills.Skill mine = skills.Where(x => x.Id == Enums.Skills.MINING).FirstOrDefault();

            return mine != null ? mine.CurrentLevel : -1;
        }

        public WoWGameObject ClosestNode()
        {
            List<WoWGameObject> herbNodes = ObjectManager.Instance.GameObjects
                .Where(x => x.GatherInfo.Type == Enums.GatherType.Herbalism).ToList();
            List<WoWGameObject> mineNodes = ObjectManager.Instance.GameObjects
                .Where(x => x.GatherInfo.Type == Enums.GatherType.Mining).ToList();

            herbNodes = herbNodes.Where(x => x.GatherInfo.RequiredSkill <= HerbLevel()
                    && !CMD.GuidBlacklist.Any(z => z == x.Guid)).ToList();
            mineNodes = mineNodes.Where(x => x.GatherInfo.RequiredSkill <= MineLevel()
                    && !CMD.GuidBlacklist.Any(z => z == x.Guid)).ToList();

            return herbNodes.Concat(mineNodes).OrderBy(x => x.Position.DistanceToPlayer()).FirstOrDefault();
        }

        public override bool Activate()
        {
            return Settings.Harvest && Inventory.Instance.CountFreeSlots(false) > 0 && ClosestNode() != null;
        }

        public override void Execute()
        {
            AddPosition();
            var closestNode = ClosestNode();
            if (closestNode.Position.DistanceToPlayer() < 3f)
            {
                /*if (ObjectManager.Instance.Player.IsMounted)
                    Inventory.GetItem(ConsumablesModule.Mount().Name).Use();*/
                ObjectManager.Instance.Player.CtmStopMovement();
                if (ObjectManager.Instance.Player.CastingAsName != "Herb Gathering"
                    && ObjectManager.Instance.Player.CastingAsName != "Mining")
                {
                    Lua.Instance.Execute("DoEmote('stand')");
                    closestNode.Interact(true);
                }
                return;
            }
            else
            {
                if (Stuck)
                {
                    CMD.Positions.Clear();
                    CMD.GuidBlacklist.Add(closestNode.Guid);
                }
                else
                    Navigation.Instance.Traverse(closestNode.Position);
            }
        }
    }
}
