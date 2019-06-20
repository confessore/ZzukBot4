using Grinderv2.GUI;
using System;
using System.Linq;
using TreeTaskCore;
using ZzukBot.Core.Game.Objects;
using ZzukBot.Core.Game.Statics;

namespace Grinderv2.TreeTasks
{
    public class SearchVendor : TTask
    {
        public override int Priority => 500;

        public override bool Activate()
        {
            return Settings.Vendor && CMD.ShouldVendor 
                && !ObjectManager.Instance.Units
                    .Where(x => CMD.VendorWhitelist.Any(y => y.Id == x.Id && !CMD.VendorBlacklist.Keys.Contains(y)) && !x.IsDead && ObjectManager.Instance.Player.InLosWith(x)).Any();
        }

        public override void Execute()
        {
            foreach (var pair in CMD.VendorBlacklist.ToList())
                if (pair.Value.AddSeconds(pair.Key.SpawnTimeSecsMax) < DateTime.Now)
                    CMD.VendorBlacklist.Remove(pair.Key);
            var tmp = CMD.VendorWhitelist.Where(x => !CMD.VendorBlacklist.Keys.Contains(x)).OrderBy(x => new Location(x.Position_X, x.Position_Y, x.Position_Z).DistanceToPlayer()).FirstOrDefault();
            var loc = new Location(tmp.Position_X, tmp.Position_Y, tmp.Position_Z);
            if (ObjectManager.Instance.Player.Position.GetDistanceTo2D(loc) > 5f)
                Navigation.Instance.Traverse(loc);
            else
                CMD.VendorBlacklist.Add(tmp, DateTime.Now);
            //Common.Instance.DebugMessage(CMD.VendorWhitelist.Count.ToString());
        }
    }
}
