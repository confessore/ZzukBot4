using Gatherer.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using TreeTaskCore;
using ZzukBot.Core.Constants;
using ZzukBot.Core.Game.Objects;
using ZzukBot.Core.Game.Statics;

namespace Gatherer.TreeTasks
{
    public class Search : TTask
    {
        public override int Priority => 50;

        public void AddPosition()
        {
            CMD.positions.Add(Convert.ToInt32(ObjectManager.Instance.Player.Position.X).ToString()
                            + Convert.ToInt32(ObjectManager.Instance.Player.Position.Y).ToString()
                            + Convert.ToInt32(ObjectManager.Instance.Player.Position.Z).ToString());
        }

        public bool Stuck => CMD.positions.FindAll(x => x.Equals(CMD.positions.Last())).Count() > 20;

        public override bool Activate()
        {
            return Gatherer.db.Rows.Where(x => !CMD.goBlacklist.Keys.Contains(x)).Any();
        }

        public override void Execute()
        {
            AddPosition();
            foreach (var pair in CMD.goBlacklist.ToList())
                if (pair.Value.AddSeconds(pair.Key.SpawnTimeSecsMax) < DateTime.Now)
                    CMD.goBlacklist.Remove(pair.Key);
            var go = Gatherer.db.Rows.Where(x => !CMD.goBlacklist.Keys.Contains(x))
                .OrderBy(x => 
                    new Location(x.Position_X, x.Position_Y, x.Position_Z)
                    .DistanceToPlayer())
                .FirstOrDefault();
            var loc = new Location(go.Position_X, go.Position_Y, go.Position_Z);
            if (loc.DistanceToPlayer() > 45f)
            {
                if (Stuck || (ObjectManager.Instance.Player.IsSwimming && loc.DistanceToPlayer() < 150f))
                {
                    CMD.positions.Clear();
                    CMD.goBlacklist.Add(go, DateTime.Now.AddYears(25));
                }
                else
                    Navigation.Instance.Traverse(loc);
            }
            else
                CMD.goBlacklist.Add(go, DateTime.Now);
            //Common.Instance.DebugMessage($"{loc} Distance: {loc.DistanceToPlayer()}");
            //Common.Instance.DebugMessae($"herb level {HerbLevel()}");
        }

        public int HerbLevel()
        {
            List<Skills.Skill> skills = Skills.Instance.GetAllPlayerSkills();
            Skills.Skill herb = skills.Where(x => x.Id == Enums.Skills.HERBALISM).FirstOrDefault();

            return herb != null ? herb.CurrentLevel : 0;
        }
    }
}
