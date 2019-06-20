using Harvester.Engine.Loaders;
using System.Collections.Generic;
using System.Linq;
using ZzukBot.Core.Game.Objects;
using ZzukBot.Core.Game.Statics;

namespace Harvester.Engine.Modules
{
    public class PathModule
    {
        private Navigation Navigation { get; }
        private ObjectManager ObjectManager { get; }
        private ProfileLoader ProfileLoader { get; }

        public PathModule(Navigation navigation, ObjectManager objectManager, ProfileLoader profileLoader)
        {
            Navigation = navigation;
            ObjectManager = objectManager;
            ProfileLoader = profileLoader;
        }

        public int index = -1;
        public List<string> playerPositions = new List<string> { };

        private Location Path(Location destination)
        {
            LocalPlayer player = ObjectManager.Player;
            Location playerPos = player.Position;

            Location[] pathArray = Navigation.CalculatePath(player.MapId, playerPos, destination, true);
            List<Location> pathList = pathArray.ToList();
            Location closestWaypoint = pathList.OrderBy(x => playerPos.GetDistanceTo(x)).FirstOrDefault();
            int index = pathList.FindIndex(x => x.Equals(closestWaypoint)) + 1;

            if (index > pathList.Count() - 1)
                index = 1;

            return pathList[index];
        }

        public void Traverse(Location destination)
        {
            LocalPlayer player = ObjectManager.Player;

            player.CtmTo(Path(destination));
        }

        public Location GetNextHotspot()
        {
            LocalPlayer player = ObjectManager.Player;
            Location playerPos = player.Position;

            if (index == -1)
            {
                Location closestHotspot = ProfileLoader.Hotspots.OrderBy(x => playerPos.GetDistanceTo(x)).FirstOrDefault();
                index = ProfileLoader.Hotspots.FindIndex(x => x.Equals(closestHotspot));
            }

            if (playerPos.GetDistanceTo(ProfileLoader.Hotspots[index]) < 2)
                index++;

            if (index >= ProfileLoader.Hotspots.Count())
                index = 0;

            return ProfileLoader.Hotspots[index];
        }

        public Location GetNextVendorHotspot()
        {
            LocalPlayer player = ObjectManager.Player;
            Location playerPos = player.Position;

            if (index == -1)
            {
                Location closestHotspot = ProfileLoader.Vendor.OrderBy(x => playerPos.GetDistanceTo(x)).First();
                index = ProfileLoader.Vendor.FindIndex(x => x.Equals(closestHotspot));
            }

            if (playerPos.GetDistanceTo(ProfileLoader.Vendor[index]) < 2)
                index++;

            if (index >= ProfileLoader.Vendor.Count())
                index = 0;

            return ProfileLoader.Vendor[index];
        }

        public bool Stuck()
        {
            if (playerPositions.FindAll(x => x.Equals(playerPositions.Last())).Count() >= 20)
                return true;

            return false;
        }
    }
}
