using Grinderv2.GUI;
using System;
using System.Linq;
using TreeTaskCore;
using ZzukBot.Core.Game.Objects;
using ZzukBot.Core.Game.Statics;

namespace Grinderv2.TreeTasks
{
    public class SearchCreature : TTask
    {
        public override int Priority => 50;

        public override bool Activate()
        {
            return !ObjectManager.Instance.Units.Where(x => CMD.CreatureWhitelist.Any(y => y.Id == x.Id) && !x.IsDead).Any();
        }

        public override void Execute()
        {
            foreach (var pair in CMD.CreatureBlacklist.ToList())
                if (pair.Value.AddSeconds(pair.Key.SpawnTimeSecsMax) < DateTime.Now)
                    CMD.CreatureBlacklist.Remove(pair.Key);
            var go = CMD.CreatureWhitelist.Where(x => !CMD.CreatureBlacklist.Keys.Contains(x))
                .OrderBy(x =>
                    new Location(x.Position_X, x.Position_Y, x.Position_Z)
                    .DistanceToPlayer())
                .FirstOrDefault();
            var loc = new Location(go.Position_X, go.Position_Y, go.Position_Z);
            if (loc.DistanceToPlayer() > 25f)
                Navigation.Instance.Traverse(loc);
            else
                CMD.CreatureBlacklist.Add(go, DateTime.Now);
            //Common.Instance.DebugMessage($"{loc} Distance: {loc.DistanceToPlayer()}");
        }
    }
}
