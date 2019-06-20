using System.Runtime.InteropServices;
using ZzukBot.Core.Game.Objects;
using ZzukBot.Core.Utilities.Extensions;

namespace ZzukBot.Core.Constants
{
    /// <summary>
    ///     Intersection struct
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal struct Intersection
    {
        internal float X;
        internal float Y;
        internal float Z;
        internal float R;

        public override string ToString()
        {
            return $"Intersection -> X: {X} Y: {Y} Z: {Z} R: {R}";
        }
    }

    /// <summary>
    /// Information about profession and skill level an object requires to be collected
    /// </summary>
    public struct GatherInfo
    {
        /// <summary>
        /// The type of gatherable object
        /// </summary>
        public Enums.GatherType Type { get; set; }
        /// <summary>
        /// The skill level required to gather the object
        /// </summary>
        public int RequiredSkill { get; set; }
    }

    /// <summary>
    ///     two coordinates (Location 1 and Location 2)
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal struct XYZXYZ
    {
        internal float X1;
        internal float Y1;
        internal float Z1;
        internal float X2;
        internal float Y2;
        internal float Z2;

        internal XYZXYZ(float x1, float y1, float z1,
            float x2, float y2, float z2)
            : this()
        {
            X1 = x1;
            Y1 = y1;
            Z1 = z1;
            X2 = x2;
            Y2 = y2;
            Z2 = z2;
        }

        public override string ToString()
        {
            return $"Start -> X: {X1} Y: {Y1} Z: {Z1}\n" + $"End -> X: {X2} Y: {Y2} Z: {Z2}";
        }
    }

    /// <summary>
    ///     Coordinate struct
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal struct XYZ
    {
        internal float X;
        internal float Y;
        internal float Z;

        internal XYZ(float x, float y, float z)
            : this()
        {
            X = x;
            Y = y;
            Z = z;
        }
    }

    /// <summary>
    ///     Struct with an item to restock at the restock npc
    /// </summary>
    internal struct RestockItem
    {
        internal string Item;
        internal int RestockUpTo;

        internal RestockItem(string parItem, int parRestockUpTo)
        {
            Item = parItem;
            RestockUpTo = parRestockUpTo;
        }
    }

    internal class NPC
    {
        internal NPC(string parName, Location parPos, string parMapPos)
        {
            Name = parName;
            Coordinates = parPos;
            MapPosition = parMapPos;
        }

        internal string Name { get; private set; }
        internal Location Coordinates { get; private set; }
        internal string MapPosition { get; private set; }
    }

    /// <summary>
    ///     ItemCacheEntry fetched from the game
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct ItemCacheEntry
    {
        /// <summary>
        ///     The id of the item
        /// </summary>
        [FieldOffset(0x0)] public int Id;

        /// <summary>
        ///     The class of the item
        /// </summary>
        [FieldOffset(0x2)] public int Class;
        /// <summary>
        ///     The subclass of the item
        /// </summary>
        [FieldOffset(0x4)] public int Subclass;
        [FieldOffset(0x8)] private readonly int NamePtr;

        /// <summary>
        ///     The quality of the item
        /// </summary>
        [FieldOffset(0x1C)] public int Quality;
        /// <summary>
        ///     The buy price of the item
        /// </summary>
        [FieldOffset(0x24)] public int BuyPrice;
        /// <summary>
        ///     The sell price of the item
        /// </summary>
        [FieldOffset(0x28)] public int SellPrice;
        /// <summary>
        ///     The inventory type of the item
        /// </summary>
        [FieldOffset(0x2c)] public int InventoryType;
        /// <summary>
        ///     The item level of the item
        /// </summary>
        [FieldOffset(0x38)] public int ItemLevel;
        /// <summary>
        ///     The required level of the item
        /// </summary>
        [FieldOffset(0x3C)] public int RequiredLevel;
        /// <summary>
        ///     The max count of the item
        /// </summary>
        [FieldOffset(0x5C)] public int MaxCount;
        /// <summary>
        ///     The max stack count of the item
        /// </summary>
        [FieldOffset(0x60)] public int MaxStackCount;
        /// <summary>
        ///     The container slots of the item
        /// </summary>
        [FieldOffset(0x64)] public int ContainerSlots;

        /// <summary>
        ///     The stats of the item
        /// </summary>
        [FieldOffset(0x68)] [MarshalAs(UnmanagedType.Struct)] public _ItemStats ItemStats;
        /// <summary>
        ///     The damage of the item
        /// </summary>
        [FieldOffset(0xB8)] [MarshalAs(UnmanagedType.Struct)] public _Damage Damage;
        /// <summary>
        ///     The armor of the item
        /// </summary>
        [FieldOffset(0xF4)] public int Armor;
        /// <summary>
        ///     The ammo type of the item
        /// </summary>
        [FieldOffset(0x114)] public int AmmoType;
        /// <summary>
        ///     The max durability of the item
        /// </summary>
        [FieldOffset(0x1C4)] public int MaxDurability;
        /// <summary>
        ///     The bag familt of the item
        /// </summary>
        [FieldOffset(0x1D0)] public int BagFamily;

        /// <summary>
        ///     The stats of an item
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct _ItemStats
        {
            /// <summary>
            ///     The stat type (int, agi, str, etc)
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)] public uint[] ItemStatType;
            /// <summary>
            ///     The value of the stat
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)] public int[] ItemStatValue;
        }
        /// <summary>
        ///     The damage of an item
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct _Damage
        {
            /// <summary>
            ///     The minimum damage
            /// </summary>
            public float DmgMin => BaseDmg + ExtraDmg;
            /// <summary>
            ///     The maximum damage
            /// </summary>
            public float DmgMax => BaseDmgMax + ExtraDmgMax;

            private readonly float BaseDmg;
            private readonly float ExtraDmg;

            private readonly int unk0;
            private readonly int unk1;
            private readonly int unk2;

            private readonly float BaseDmgMax;
            private readonly float ExtraDmgMax;
        }

        /// <summary>
        ///      The name of the item
        /// </summary>
        public string Name => NamePtr.ReadString();
    }
}
