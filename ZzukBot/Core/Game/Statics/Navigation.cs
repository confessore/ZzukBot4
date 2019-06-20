using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using ZzukBot.Core.Constants;
using ZzukBot.Core.Mem;
using ZzukBot.Core.Game.Objects;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ZzukBot.Core.Game.Statics
{
    /// <summary>
    /// Helps the bot generate paths through the world
    /// </summary>
    //[App.Singleton]
    public unsafe class Navigation
    {
        /// <summary>
        /// Access to the pathfinder
        /// </summary>
        public static Navigation Instance = new Navigation();

        private Navigation()
        {
            var calculatePathPtr = WinImports.GetProcAddress(Libs.Instance.PathfinderPtr,
                       "CalculatePath");
            _calculatePath = Memory.Reader.RegisterDelegate<CalculatePathDelegate>(calculatePathPtr);

            var freePathPtr = WinImports.GetProcAddress(Libs.Instance.PathfinderPtr,
                        "FreePathArr");
            _freePathArr = Memory.Reader.RegisterDelegate<FreePathArr>(freePathPtr);

            //Memory.ErasePeHeader(Libs.Instance.PathfinderPtr);
            //Memory.UnlinkFromPeb("038.mmap");
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate XYZ* CalculatePathDelegate(
            uint mapId, XYZ start, XYZ end, bool parSmooth,
            out int length);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void FreePathArr(
            XYZ* pathArr);

        private static CalculatePathDelegate _calculatePath;
        private static FreePathArr _freePathArr;

        /// <summary>
        /// The player is executing a backup movement
        /// </summary>
        public bool BackingUp { get; private set; }
        Location BackupLocation { get; set; }
        Stopwatch Stopwatch { get; set; }

        /// <summary>
        /// Moves a specified distance behind the player
        /// </summary>
        /// <param name="distance"></param>
        public void Backup(float distance)
        {
            if (!BackingUp)
            {
                if (ObjectManager.Instance.Player.InLosWith(PointBehindPlayer(distance)))
                {
                    BackupLocation = PointBehindPlayer(distance);
                    BackingUp = true;
                    Stopwatch = Stopwatch.StartNew();
                }
                else
                    return;
            }
            else
            {
                Traverse(BackupLocation);
                if (ObjectManager.Instance.Player.Position.GetDistanceTo2D(BackupLocation) < 1 || Stopwatch.Elapsed.Seconds > 3)
                {
                    BackingUp = false;
                    Stopwatch.Stop();
                }
            }
        }

        /// <summary>
        /// Generate a path from start to end
        /// </summary>
        /// <param name="mapId">The map ID the player is on</param>
        /// <param name="start">Start</param>
        /// <param name="end">End</param>
        /// <param name="parSmooth">Smooth path</param>
        /// <returns>An array of points</returns>
        public Location[] CalculatePath(
            uint mapId, Location start, Location end, bool parSmooth)
        {
            int length;
            var ret = _calculatePath(mapId, start.ToStruct, end.ToStruct, parSmooth, out length);
            var list = new Location[length];
            for (var i = 0; i < length; i++)
            {
                list[i] = new Location(ret[i]);
            }
            _freePathArr(ret);
            return list;
        }

        /// <summary>
        /// Finds the location that is a specificed distance behind the player
        /// </summary>
        /// <param name="distance"></param>
        /// <returns></returns>
        public Location PointBehindPlayer(float distance)
        {
            var newX = ObjectManager.Instance.Player.Position.X + distance * (float)-Math.Cos(ObjectManager.Instance.Player.Facing);
            var newY = ObjectManager.Instance.Player.Position.Y + distance * (float)-Math.Sin(ObjectManager.Instance.Player.Facing);
            return new Location(newX, newY, ObjectManager.Instance.Player.Position.Z);
        }

        Location Path(Location destination)
        {
            LocalPlayer player = ObjectManager.Instance.Player;
            Location playerPos = player.Position;

            Location[] pathArray = CalculatePath(player.MapId, playerPos, destination, true);
            List<Location> pathList = pathArray.ToList();
            Location closestWaypoint = pathList.OrderBy(x => playerPos.GetDistanceTo(x)).First();
            int index = pathList.FindIndex(x => x.Equals(closestWaypoint)) + 1;

            if (index > pathList.Count() - 1)
                index = 1;

            return pathList[index];
        }

        /// <summary>
        /// Generates a path to a location and then traverses the path to that location
        /// </summary>
        /// <param name="destination"></param>
        public void Traverse(Location destination)
        {
            LocalPlayer player = ObjectManager.Instance.Player;

            player.CtmTo(Path(destination));
        }
    }
}

//using System;
//using System.CodeDom.Compiler;
//using System.Collections;
//using System.Collections.Generic;
//using System.Runtime.InteropServices;
//using System.Threading;
//using ZzukBot.Constants;
//using ZzukBot.Mem;
//using ZzukBot.Objects;
//using ZzukBot.Settings;
//using File = System.IO.File;

//namespace ZzukBot.Game.Statics
//{
//    /// <summary>
//    /// Class that handles path generation
//    /// </summary>
//    internal static class Navigation
//    {
//        [DllImport(Libs.Pathfinder)]
//        private static extern unsafe _XYZ* CalculatePath(uint mapId, _XYZ start, _XYZ end, bool parSmooth,
//            out int length);

//        [DllImport(Libs.Pathfinder)]
//        private static extern unsafe void FreePathArr(_XYZ* pathArr);

//        //[DllImport(Libs.NavigationPath)]
//        //private static extern int GetPathArray([Out] _XYZ[] path, int length);

//        /// <summary>
//        /// CalculatePath
//        /// </summary>
//        /// <param name="parStart"></param>
//        /// <param name="parEnd"></param>
//        /// <param name="parSmooth"></param>
//        /// <returns></returns>
//        public static unsafe Location[] CalculatePath(Location parStart, Location parEnd, bool parSmooth)
//        {
//            Location[] ret;
//            try
//            {
//                int length;
//                var pathArr = CalculatePath((uint)ObjectManager.Instance.Player.MapId, parStart.ToStruct, parEnd.ToStruct,
//                    parSmooth, out length);
//                ret = new Location[length];
//                for (var i = 0; i < length; i++)
//                {
//                    ret[i] = new Location(pathArr[i]);
//                }
//                FreePathArr(pathArr);
//            }
//            catch
//            {
//                ret = new[] { parStart, parEnd };
//            }
//            return ret;
//        }

//        /// <summary>
//        /// CalculatePathAsync
//        /// </summary>
//        /// <param name="parStart"></param>
//        /// <param name="parEnd"></param>
//        /// <param name="parSmooth"></param>
//        /// <param name="parCallback"></param>
//        public static void CalculatePathAsync(Location parStart, Location parEnd, bool parSmooth,
//            CalculatePathAsyncCallBack parCallback)
//        {
//            var list = new ArrayList { parStart, parEnd, parSmooth, parCallback };

//            ParameterizedThreadStart pts = CalculatePathProxy;
//            var thr = new Thread(pts) { IsBackground = true };
//            thr.Start(list);
//        }

//        private static void CalculatePathProxy(object parDetails)
//        {
//            var list = (ArrayList)parDetails;
//            var res = CalculatePath((Location)list[0], (Location)list[1], (bool)list[2]);
//            ((CalculatePathAsyncCallBack)list[3])(res);
//        }

//        /// <summary>
//        /// GetRandomPoint
//        /// </summary>
//        /// <param name="parStart"></param>
//        /// <param name="parMaxDistance"></param>
//        /// <returns></returns>
//        public static Location GetRandomPoint(Location parStart, float parMaxDistance)
//        {
//            var random = new Random();
//            var end = new Location(parStart.X - parMaxDistance + random.Next((int)parMaxDistance * 2),
//                parStart.Y - parMaxDistance + random.Next((int)parMaxDistance * 2), parStart.Z);
//            return end;
//        }

//        /// <summary>
//        /// GetRandomPoint
//        /// </summary>
//        /// <param name="parStart"></param>
//        /// <param name="parDistanceToMove"></param>
//        /// <returns></returns>
//        public static Location GetPointBehindPlayer(Location parStart, float parDistanceToMove)
//        {
//            var newX = parStart.X + parDistanceToMove * (float)-Math.Cos(ObjectManager.Instance.Player.Facing);
//            var newY = parStart.Y + parDistanceToMove * (float)-Math.Sin(ObjectManager.Instance.Player.Facing);
//            var end = new Location(newX, newY, parStart.Z);
//            return end;
//        }

//        /// <summary>
//        /// CalculatePathAsyncCallBack
//        /// </summary>
//        /// <param name="parPath"></param>
//        public delegate void CalculatePathAsyncCallBack(Location[] parPath);
//    }
//}

