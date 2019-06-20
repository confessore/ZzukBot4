using System.Collections.Generic;
using System.Linq;
using ZzukBot.Core.Framework.Loaders;
using ZzukBot.Core.Game.Objects;
using ZzukBot.Core.Game.Statics;

namespace Grinder.Engine.Modules
{
    public class PathModule
    {
        Navigation Navigation { get; }
        ObjectManager ObjectManager { get; }
        ProfileLoader ProfileLoader { get; }

        public PathModule(Navigation navigation, ObjectManager objectManager, ProfileLoader profileLoader)
        {
            Navigation = navigation;
            ObjectManager = objectManager;
            ProfileLoader = profileLoader;
        }

        public int index = -1;
        public List<string> playerPositions = new List<string> { };

        Location Path(Location destination)
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

        public List<Location> Hotspots => ProfileLoader.Hotspots;
        public List<Location> Vendor => ProfileLoader.VendorHotspots;

        public Location GetNextHotspot()
        {
            LocalPlayer player = ObjectManager.Player;
            Location playerPos = player.Position;

            if (index == -1 || index >= ProfileLoader.Hotspots.Count())
            {
                Location closestHotspot = ProfileLoader.Hotspots.OrderBy(x => playerPos.GetDistanceTo(x)).FirstOrDefault();
                index = ProfileLoader.Hotspots.FindIndex(x => x.Equals(closestHotspot));
            }

            if (playerPos.GetDistanceTo(ProfileLoader.Hotspots[index]) < 5)
                index++;

            if (index >= ProfileLoader.Hotspots.Count())
                index = 0;

            return ProfileLoader.Hotspots[index];
        }

        public Location GetNextVendorHotspot()
        {
            LocalPlayer player = ObjectManager.Player;
            Location playerPos = player.Position;

            if (index == -1 || index >= ProfileLoader.VendorHotspots.Count())
            {
                Location closestHotspot = ProfileLoader.VendorHotspots.OrderBy(x => playerPos.GetDistanceTo(x)).First();
                index = ProfileLoader.VendorHotspots.FindIndex(x => x.Equals(closestHotspot));
            }

            if (playerPos.GetDistanceTo(ProfileLoader.VendorHotspots[index]) < 5)
                index++;

            if (index >= ProfileLoader.VendorHotspots.Count())
                index = 0;

            return ProfileLoader.VendorHotspots[index];
        }

        /*public List<List<Location>> PathsToHotspots(List<Location> hotspots)
        {
            List<List<Location>> pathsToHotspots = new List<List<Location>>();

            foreach (var hotspot in hotspots)
                pathsToHotspots.Add(Navigation.CalculatePath(ObjectManager.Player.MapId, ObjectManager.Player.Position, hotspot, true).ToList());

            return pathsToHotspots;
        }*/

        public bool Stuck()
        {
            if (playerPositions.FindAll(x => x.Equals(playerPositions.Last())).Count() >= 20)
                return true;

            return false;
        }
    }
}
