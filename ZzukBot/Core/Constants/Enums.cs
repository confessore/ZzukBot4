using System;

namespace ZzukBot.Core.Constants
{
    /// <summary>
    ///     Enums for all kind of things
    /// </summary>
    public static class Enums
    {
        /// <summary>
        ///     Type of units that can send chat messages
        /// </summary>
        public enum ChatSenderType
        {
            /// <summary>
            /// Chat message is from a player
            /// </summary>
            Player = 1,
            /// <summary>
            /// Chat message is from an NPC
            /// </summary>
            Npc = 2
        }

        /// <summary>
        /// CombatPosition used in CustomClasses
        /// </summary>
        public enum CombatPosition
        {
            /// <summary>
            /// The object is in front of the target object
            /// </summary>
            Before = 1,
            /// <summary>
            /// The object is behind the target object
            /// </summary>
            Behind = 2,
            /// <summary>
            /// The object is kiting the target object
            /// </summary>
            Kite = 3,
            /// <summary>
            /// The object is running away from the target object
            /// </summary>
            RunAway = 4
        }

        /// <summary>
        /// Faction ids for Alliance main cities
        /// </summary>
        public enum FactionAlliance
        {
            /// <summary>
            /// The Stormwind faction id
            /// </summary>
            Stormwind = 11,
            /// <summary>
            /// The Ironforce faction id
            /// </summary>
            Ironforge = 57,
            /// <summary>
            /// The Gomeragon Exiles faction id
            /// </summary>
            GnomeragonExiles = 64,
            /// <summary>
            /// The Darnassus faction id
            /// </summary>
            Darnassus = 79
        }

        /// <summary>
        /// Faction ids for Horde main cities
        /// </summary>
        public enum FactionHorde
        {
            /// <summary>
            /// The Orgrimmar faction id
            /// </summary>
            Orgrimmar = 85,
            /// <summary>
            /// The Darkspear Trolls faction id
            /// </summary>
            DarkspearTrolls = 126,
            /// <summary>
            /// The Thunder Bluff faction id
            /// </summary>
            ThunderBluff = 105,
            /// <summary>
            /// The Undercity faction id
            /// </summary>
            Undercity = 71
        }

        /// <summary>
        /// Faction ids for various neutral factions
        /// </summary>
        public enum FactionNeutral
        {
            /// <summary>
            /// The Argent Dawn faction id
            /// </summary>
            ArgentDawn = 1625,
            /// <summary>
            /// The Gadgetzan faction id
            /// </summary>
            Gadgetzan = 475,
            /// <summary>
            /// The Booty Bay faction id
            /// </summary>
            BootyBay = 120,
            /// <summary>
            /// The Steamwheedle Cartel faction id
            /// </summary>
            SteamwheedleCartel = 35,
            /// <summary>
            /// The Everlook faction id
            /// </summary>
            Everlook = 854,
            /// <summary>
            /// The Cenarion Circle faction id
            /// </summary>
            CenarionCircle = 1254,
            /// <summary>
            /// The Earthern Ring faction id
            /// </summary>
            EarthernRing = 1194,
            /// <summary>
            /// The Thorium Brotherhood faction id
            /// </summary>
            ThoriumBrotherhood = 1475
        }

        /// <summary>
        /// Faction ids for Alliance races
        /// </summary>
        public enum FactionPlayerAlliance
        {
            /// <summary>
            /// The Human faction id
            /// </summary>
            Human = 1,
            /// <summary>
            /// The Dwarf faction id
            /// </summary>
            Dwarf = 3,
            /// <summary>
            /// The Gnome faction id
            /// </summary>
            Gnome = 8,
            /// <summary>
            /// The Night Elf faction id
            /// </summary>
            Nelf = 4
        }

        /// <summary>
        /// Faction Ids for Horde races
        /// </summary>
        public enum FactionPlayerHorde
        {
            /// <summary>
            /// The Orc faction id
            /// </summary>
            Orc = 2,
            /// <summary>
            /// The Taroll faction id
            /// </summary>
            Troll = 9,
            /// <summary>
            /// The Tauren faction id
            /// </summary>
            Tauren = 6,
            /// <summary>
            /// The Undead faction id
            /// </summary>
            Undead = 5
        }

        /// <summary>
        /// Quest states of a NPC
        /// </summary>
        public enum NpcQuestOfferState
        {
            /// <summary>
            /// None
            /// </summary>
            None = 0,
            /// <summary>
            /// NPC offers a quest but its not yet acceptable (a silver !)
            /// </summary>
            OffersQuestNotAcceptable = 1,
            /// <summary>
            /// NPC offers a quest but the quest is out of the characters level range (no yellow !)
            /// </summary>
            OffersQuestLowLevel = 2,
            /// <summary>
            /// We can turn in a quest at that NPC but we havnt completed it yet (silver ?)
            /// </summary>
            CanTurnInNotCompleteable = 3,
            /// <summary>
            /// The NPC offers a quest (yellow !)
            /// </summary>
            OffersQuest = 5,
            /// <summary>
            /// We can turn in a quest at that NPC (yellow ?)
            /// </summary>
            CanTurnIn = 7,
        }


        /// <summary>
        /// Gather types of WoW
        /// </summary>
        public enum GatherType
        {
            /// <summary>
            /// The object is not a gatherable type
            /// </summary>
            None = -1,
            /// <summary>
            /// The object is an herbalism node
            /// </summary>
            Herbalism = 2,
            /// <summary>
            /// The object is a mining node
            /// </summary>
            Mining = 3
        }

        /// <summary>
        /// Skills in WoW
        /// </summary>
        public enum Skills : short
        {
            /// <summary>
            /// The Frost class skill
            /// </summary>
            FROST = 6,
            /// <summary>
            /// The Fire class skill
            /// </summary>
            FIRE = 8,
            /// <summary>
            /// The Arms class skill
            /// </summary>
            ARMS = 26,
            /// <summary>
            /// The Combat class skill
            /// </summary>
            COMBAT = 38,
            /// <summary>
            /// The Subtlety class skill
            /// </summary>
            SUBTLETY = 39,
            /// <summary>
            /// The Poisons class skill
            /// </summary>
            POISONS = 40,
            /// <summary>
            /// The One-Handed Swords weapon skill
            /// </summary>
            SWORDS = 43,
            /// <summary>
            /// The One-Handed Axes weapon skill
            /// </summary>
            AXES = 44,
            /// <summary>
            /// The Bows weapon skill
            /// </summary>
            BOWS = 45,
            /// <summary>
            /// The Guns weapon skill
            /// </summary>
            GUNS = 46,
            /// <summary>
            /// The Beast Mastery class skill
            /// </summary>
            BEAST_MASTERY = 50,
            /// <summary>
            /// The Survival class skill
            /// </summary>
            SURVIVAL = 51,
            /// <summary>
            /// The One-Handed Maces weapon skill
            /// </summary>
            MACES = 54,
            /// <summary>
            /// The Two-Handed Swords weapon skill
            /// </summary>
            TWOHAND_SWORDS = 55,
            /// <summary>
            /// The Holy class skill
            /// </summary>
            HOLY = 56,
            /// <summary>
            /// The Shadow class skill
            /// </summary>
            SHADOW = 78,
            /// <summary>
            /// The Defense weapon skill
            /// </summary>
            DEFENSE = 95,
            /// <summary>
            /// The Common Language skill
            /// </summary>
            LANG_COMMON = 98,
            /// <summary>
            /// A Dwarven Racial skill
            /// </summary>
            RACIAL_DWARVEN = 101,
            /// <summary>
            /// The Orcish Language skill
            /// </summary>
            LANG_ORCISH = 109,
            /// <summary>
            /// The Dwarven Language skill
            /// </summary>
            LANG_DWARVEN = 111,
            /// <summary>
            /// The Darnassian Language skill
            /// </summary>
            LANG_DARNASSIAN = 113,
            /// <summary>
            /// The Taurahe Language skill
            /// </summary>
            LANG_TAURAHE = 115,
            /// <summary>
            /// A Dual Wield skill
            /// </summary>
            DUAL_WIELD = 118,
            /// <summary>
            /// A Tauren Racial skill
            /// </summary>
            RACIAL_TAUREN = 124,
            /// <summary>
            /// The Orc Racial skill
            /// </summary>
            ORC_RACIAL = 125,
            /// <summary>
            /// The Night Elf Racial skill
            /// </summary>
            RACIAL_NIGHT_ELF = 126,
            /// <summary>
            /// The First Aid skill
            /// </summary>
            FIRST_AID = 129,
            /// <summary>
            /// The Feral Combat class skill
            /// </summary>
            FERAL_COMBAT = 134,
            /// <summary>
            /// The Thalassian Language skill
            /// </summary>
            LANG_THALASSIAN = 137,
            /// <summary>
            /// The Staves weapon skill
            /// </summary>
            STAVES = 136,
            /// <summary>
            /// The Draconic Language skill
            /// </summary>
            LANG_DRACONIC = 138,
            /// <summary>
            /// The Demon Tongue Language skill
            /// </summary>
            LANG_DEMON_TONGUE = 139,
            /// <summary>
            /// The Titan Language skill
            /// </summary>
            LANG_TITAN = 140,
            /// <summary>
            /// The Old Tongue Language skill
            /// </summary>
            LANG_OLD_TONGUE = 141,
            /// <summary>
            /// The Survival2 class skill
            /// </summary>
            SURVIVAL2 = 142,
            /// <summary>
            /// The Riding Horse skill
            /// </summary>
            RIDING_HORSE = 148,
            /// <summary>
            /// The Riding Wolf skill
            /// </summary>
            RIDING_WOLF = 149,
            /// <summary>
            /// The Riding Ram skill
            /// </summary>
            RIDING_RAM = 152,
            /// <summary>
            /// The Riding Tiger skill
            /// </summary>
            RIDING_TIGER = 150,
            /// <summary>
            /// The Swimming skills
            /// </summary>
            SWIMMING = 155,
            /// <summary>
            /// The Two-Handed Maces weapon skill
            /// </summary>
            TWOHAND_MACES = 160,
            /// <summary>
            /// The Unarmed weapon skill
            /// </summary>
            UNARMED = 162,
            /// <summary>
            /// The Marksmanship class skill
            /// </summary>
            MARKSMANSHIP = 163,
            /// <summary>
            /// The Blacksmithing skill
            /// </summary>
            BLACKSMITHING = 164,
            /// <summary>
            /// The Leatherworking skill
            /// </summary>
            LEATHERWORKING = 165,
            /// <summary>
            /// The Alchemy skill
            /// </summary>
            ALCHEMY = 171,
            /// <summary>
            /// The Two-Handed Axes weapon skill
            /// </summary>
            TWOHAND_AXES = 172,
            /// <summary>
            /// The Daggers weapon skill
            /// </summary>
            DAGGERS = 173,
            /// <summary>
            /// The Thrown weapon skill
            /// </summary>
            THROWN = 176,
            /// <summary>
            /// The Herbalism skill
            /// </summary>
            HERBALISM = 182,
            /// <summary>
            /// The Generic DND skill
            /// </summary>
            GENERIC_DND = 183,
            /// <summary>
            /// The Retribution class skill
            /// </summary>
            RETRIBUTION = 184,
            /// <summary>
            /// The Cooking skill
            /// </summary>
            COOKING = 185,
            /// <summary>
            /// The Mining skill
            /// </summary>
            MINING = 186,
            /// <summary>
            /// The Pet Imp skill
            /// </summary>
            PET_IMP = 188,
            /// <summary>
            /// The Pet Felhunter skill
            /// </summary>
            PET_FELHUNTER = 189,
            /// <summary>
            /// The Tailoring skill
            /// </summary>
            TAILORING = 197,
            /// <summary>
            /// The Engineering skill
            /// </summary>
            ENGINEERING = 202,
            /// <summary>
            /// The Pet Spider skill
            /// </summary>
            PET_SPIDER = 203,
            /// <summary>
            /// The Pet Voidwalker skill
            /// </summary>
            PET_VOIDWALKER = 204,
            /// <summary>
            /// The Pet Succubus skill
            /// </summary>
            PET_SUCCUBUS = 205,
            /// <summary>
            /// The Pet Infernal skill
            /// </summary>
            PET_INFERNAL = 206,
            /// <summary>
            /// The Pet Doomguard skill
            /// </summary>
            PET_DOOMGUARD = 207,
            /// <summary>
            /// The Pet Wolf skill
            /// </summary>
            PET_WOLF = 208,
            /// <summary>
            /// The Pet Cat skill
            /// </summary>
            PET_CAT = 209,
            /// <summary>
            /// The Pet Bear skill
            /// </summary>
            PET_BEAR = 210,
            /// <summary>
            /// The Pet Boar skill
            /// </summary>
            PET_BOAR = 211,
            /// <summary>
            /// The Pet Crocolisk skill
            /// </summary>
            PET_CROCOLISK = 212,
            /// <summary>
            /// The Pet Carrion Bird skill
            /// </summary>
            PET_CARRION_BIRD = 213,
            /// <summary>
            /// The Pet Gorilla skill
            /// </summary>
            PET_GORILLA = 215,
            /// <summary>
            /// The Pet Crab skill
            /// </summary>
            PET_CRAB = 214,
            /// <summary>
            /// The Pet Raptor skill
            /// </summary>
            PET_RAPTOR = 217,
            /// <summary>
            /// The Tallstrider skill
            /// </summary>
            PET_TALLSTRIDER = 218,
            /// <summary>
            /// The Undead Racial skill
            /// </summary>
            RACIAL_UNDEAD = 220,
            /// <summary>
            /// The Crossbows weapon skill
            /// </summary>
            CROSSBOWS = 226,
            /// <summary>
            /// The Spears weapon skill
            /// </summary>
            SPEARS = 227,
            /// <summary>
            /// The Wands weapon skill
            /// </summary>
            WANDS = 228,
            /// <summary>
            /// The Polearms weapon skill
            /// </summary>
            POLEARMS = 229,
            /// <summary>
            /// The Attribute Enhancements skill
            /// </summary>
            ATTRIBUTE_ENHANCEMENTS = 230,
            /// <summary>
            /// The Slayer Talents skill
            /// </summary>
            SLAYER_TALENTS = 231,
            /// <summary>
            /// The Magic Talents skill
            /// </summary>
            MAGIC_TALENTS = 233,
            /// <summary>
            /// The Defensive Talents skill
            /// </summary>
            DEFENSIVE_TALENTS = 234,
            /// <summary>
            /// The Pet Scorpid skill
            /// </summary>
            PET_SCORPID = 236,
            /// <summary>
            /// The Arcane skill
            /// </summary>
            ARCANE = 237,
            /// <summary>
            /// The Pet Turtle skill
            /// </summary>
            PET_TURTLE = 251,
            /// <summary>
            /// The Fury class skill
            /// </summary>
            FURY = 256,
            /// <summary>
            /// The Protection class skill
            /// </summary>
            PROTECTION = 257,
            /// <summary>
            /// The Beast Training skill
            /// </summary>
            BEAST_TRAINING = 261,
            /// <summary>
            /// The Protection2 class skill
            /// </summary>
            PROTECTION2 = 267,
            /// <summary>
            /// The Pet Talents skill
            /// </summary>
            PET_TALENTS = 270,
            /// <summary>
            /// The Plate Mail armor skill
            /// </summary>
            PLATE_MAIL = 293,
            /// <summary>
            /// The Assassination class skill
            /// </summary>
            ASSASSINATION = 253,
            /// <summary>
            /// The Gnomish Language skill
            /// </summary>
            LANG_GNOMISH = 313,
            /// <summary>
            /// The Troll Language skill
            /// </summary>
            LANG_TROLL = 315,
            /// <summary>
            /// The Enchanting skill
            /// </summary>
            ENCHANTING = 333,
            /// <summary>
            /// The Demonology class skill
            /// </summary>
            DEMONOLOGY = 354,
            /// <summary>
            /// The Affliction class skill
            /// </summary>
            AFFLICTION = 355,
            /// <summary>
            /// The Fishing skill
            /// </summary>
            FISHING = 356,
            /// <summary>
            /// The Enhancement class skill
            /// </summary>
            ENHANCEMENT = 373,
            /// <summary>
            /// The Restoration class skill
            /// </summary>
            RESTORATION = 374,
            /// <summary>
            /// The Elemental Combat class skill
            /// </summary>
            ELEMENTAL_COMBAT = 375,
            /// <summary>
            /// The Skinning skill
            /// </summary>
            SKINNING = 393,
            /// <summary>
            /// The Leather armor skill
            /// </summary>
            LEATHER = 414,
            /// <summary>
            /// The Cloth armor skill
            /// </summary>
            CLOTH = 415,
            /// <summary>
            /// The Mail armor skill
            /// </summary>
            MAIL = 413,
            /// <summary>
            /// The Shield weapon skill
            /// </summary>
            SHIELD = 433,
            /// <summary>
            /// The Fist Weapons weapon skill
            /// </summary>
            FIST_WEAPONS = 473,
            /// <summary>
            /// The Tracking Beast skill
            /// </summary>
            TRACKING_BEAST = 513,
            /// <summary>
            /// The Tracking Humanoid skill
            /// </summary>
            TRACKING_HUMANOID = 514,
            /// <summary>
            /// The Tracking Demon skill
            /// </summary>
            TRACKING_DEMON = 516,
            /// <summary>
            /// The Tracking Undead skill
            /// </summary>
            TRACKING_UNDEAD = 517,
            /// <summary>
            /// The Tracking Dragon skill
            /// </summary>
            TRACKING_DRAGON = 518,
            /// <summary>
            /// The Tracking Elemental skill
            /// </summary>
            TRACKING_ELEMENTAL = 519,
            /// <summary>
            /// The Riding Raptor skill
            /// </summary>
            RIDING_RAPTOR = 533,
            /// <summary>
            /// The Riding Mechanostrider skill
            /// </summary>
            RIDING_MECHANOSTRIDER = 553,
            /// <summary>
            /// The Riding Undead Horse skill
            /// </summary>
            RIDING_UNDEAD_HORSE = 554,
            /// <summary>
            /// The Restoration2 class skill
            /// </summary>
            RESTORATION2 = 573,
            /// <summary>
            /// The Balance class skill
            /// </summary>
            BALANCE = 574,
            /// <summary>
            /// The Destruction class skill
            /// </summary>
            DESTRUCTION = 593,
            /// <summary>
            /// The Holy2 class skill
            /// </summary>
            HOLY2 = 594,
            /// <summary>
            /// The Discipline class skill
            /// </summary>
            DISCIPLINE = 613,
            /// <summary>
            /// The Lockpicking skill
            /// </summary>
            LOCKPICKING = 633,
            /// <summary>
            /// The Pet Bat skill
            /// </summary>
            PET_BAT = 653,
            /// <summary>
            /// The Pet Hyena skill
            /// </summary>
            PET_HYENA = 654,
            /// <summary>
            /// The Pet Owl skill
            /// </summary>
            PET_OWL = 655,
            /// <summary>
            /// The Pet Wind Serpent skill
            /// </summary>
            PET_WIND_SERPENT = 656,
            /// <summary>
            /// The Gutterspeak Language skill
            /// </summary>
            LANG_GUTTERSPEAK = 673,
            /// <summary>
            /// The Riding Kodo skill
            /// </summary>
            RIDING_KODO = 713,
            /// <summary>
            /// The Troll Racial skill
            /// </summary>
            RACIAL_TROLL = 733,
            /// <summary>
            /// The Gnome Racial skill
            /// </summary>
            RACIAL_GNOME = 753,
            /// <summary>
            /// The Human Racial skill
            /// </summary>
            RACIAL_HUMAN = 754,
            /// <summary>
            /// The Jewelcrafting skill
            /// </summary>
            JEWELCRAFTING = 755,
            /// <summary>
            /// The Blood Elf Racial skill
            /// </summary>
            RACIAL_BLOODELF = 756,
            /// <summary>
            /// The Pet Event Remote Control skill (tonk?)
            /// </summary>
            PET_EVENT_REMOTECONTROL = 758,
            /// <summary>
            /// The Draenei Language skill
            /// </summary>
            LANG_DRAENEI = 759,
            /// <summary>
            /// The Draenei Racial skill
            /// </summary>
            DRAENEI_RACIAL = 760,
            /// <summary>
            /// The Pet Felguard skill
            /// </summary>
            PET_FELGUARD = 761,
            /// <summary>
            /// The Riding skill
            /// </summary>
            RIDING = 762,
            /// <summary>
            /// The Pet Drgonhawk skill
            /// </summary>
            PET_DRAGONHAWK = 763,
            /// <summary>
            /// The Pet Nether Ray skill
            /// </summary>
            PET_NETHER_RAY = 764,
            /// <summary>
            /// The Pet Sporebat skill
            /// </summary>
            PET_SPOREBAT = 765,
            /// <summary>
            /// The Pet Warp Stalker skill
            /// </summary>
            PET_WARP_STALKER = 766,
            /// <summary>
            /// The Pet Ravager skill
            /// </summary>
            PET_RAVAGER = 767,
            /// <summary>
            /// The Pet Serpent skill
            /// </summary>
            PET_SERPENT = 768,
            /// <summary>
            /// The Internal skill
            /// </summary>
            INTERNAL = 769,
        }

        /// <summary>
        /// Qualities of WoW Items
        /// </summary>
        public enum ItemQuality
        {
            /// <summary>
            /// The Poor item quality (grey)
            /// </summary>
            Poor = 0,
            /// <summary>
            /// The Common item quality (white)
            /// </summary>
            Common = 1,
            /// <summary>
            /// The Uncommon item quality (green)
            /// </summary>
            Uncommon = 2,
            /// <summary>
            /// The Rare item quality (blue)
            /// </summary>
            Rare = 3,
            /// <summary>
            /// The Epic item quality (purple)
            /// </summary>
            Epic = 4,
            /// <summary>
            /// The Legendary item quality (orange)
            /// </summary>
            Legendary = 5,
        }

        /// <summary>
        /// UnitFlags
        /// </summary>
        [Flags]
        public enum UnitFlags
        {
            /// <summary>
            ///     The unit is fleeing
            /// </summary>
            UNIT_FLAG_FLEEING = 0x00800000,
            /// <summary>
            ///     The unit is confused
            /// </summary>
            UNIT_FLAG_CONFUSED = 0x00400000,
            /// <summary>
            ///     The unit is in combat
            /// </summary>
            UNIT_FLAG_IN_COMBAT = 0x00080000,
            /// <summary>
            ///     The unit is skinnable
            /// </summary>
            UNIT_FLAG_SKINNABLE = 0x04000000,
            /// <summary>
            ///     The unit is stunned
            /// </summary>
            UNIT_FLAG_STUNNED = 0x00040000,
            /// <summary>
            ///     The unit is rooted
            /// </summary>
            UNIT_FLAG_DISABLE_MOVE = 0x00000004,
            /// <summary>
            ///     The unit is silenced
            /// </summary>
            UNIT_FLAG_SILENCED = 0x00002000
        }

        /// <summary>
        /// MovementFlags enum
        /// </summary>
        [Flags]
        public enum MovementFlags
        {
            /// <summary>
            /// Not moving
            /// </summary>
            None = 0x0,
            /// <summary>
            /// Moving forward
            /// </summary>
            Front = 0x00000001,
            /// <summary>
            /// Moving backwards
            /// </summary>
            Back = 0x00000002,
            /// <summary>
            /// Moving left
            /// </summary>
            Left = 0x00000010,
            /// <summary>
            /// Moving right
            /// </summary>
            Right = 0x00000020,
            /// <summary>
            /// Strafing left
            /// </summary>
            StrafeLeft = 0x00000004,
            /// <summary>
            /// Strafing right
            /// </summary>
            StrafeRight = 0x00000008,
            /// <summary>
            /// Swimming state
            /// </summary>
            Swimming = 0x00200000,
            /// <summary>
            /// Jumping state
            /// </summary>
            Jumping = 0x00002000,
            /// <summary>
            /// Falling state
            /// </summary>
            Falling = 0x0000A000,
            /// <summary>
            /// Levitate state
            /// </summary>
            Levitate = 0x70000000
        }

        /// <summary>
        ///     Classes of WoW
        /// </summary>
        public enum ClassId : byte
        {
            /// <summary>
            /// The Warrior class
            /// </summary>
            Warrior = 1,
            /// <summary>
            /// The Paladin class
            /// </summary>
            Paladin = 2,
            /// <summary>
            /// The Hunter class
            /// </summary>
            Hunter = 3,
            /// <summary>
            /// The Rogue class
            /// </summary>
            Rogue = 4,
            /// <summary>
            /// The Priest class
            /// </summary>
            Priest = 5,
            /// <summary>
            /// The Shaman class
            /// </summary>
            Shaman = 7,
            /// <summary>
            /// The Mage class
            /// </summary>
            Mage = 8,
            /// <summary>
            /// The Warlock class
            /// </summary>
            Warlock = 9,
            /// <summary>
            /// The Druid class
            /// </summary>
            Druid = 11
        }

        /// <summary>
        /// ControlBits used for movement
        /// </summary>
        [Flags]
        public enum ControlBits
        {
            /// <summary>
            /// No movement
            /// </summary>
            Nothing = 0x00000000,
            /// <summary>
            /// CTM movement
            /// </summary>
            CtmWalk = 0x00001000,
            /// <summary>
            /// Forward movement
            /// </summary>
            Front = 0x00000010,
            /// <summary>
            /// Backward movement
            /// </summary>
            Back = 0x00000020,
            /// <summary>
            /// Left movement
            /// </summary>
            Left = 0x00000100,
            /// <summary>
            /// Right movement
            /// </summary>
            Right = 0x00000200,
            /// <summary>
            /// Left strafing
            /// </summary>
            StrafeLeft = 0x00000040,
            /// <summary>
            /// Right strafing
            /// </summary>
            StrafeRight = 0x00000080
        }

        /// <summary>
        /// NpcFlags - taken straight from mangos (some might be incorrect in conclusion)
        /// </summary>
        [Flags]
        public enum NpcFlags
        {
            /// <summary>
            /// No interaction
            /// </summary>
            UNIT_NPC_FLAG_NONE = 0x00000000,
            /// <summary>
            /// NPC has a Gossip menu
            /// </summary>
            UNIT_NPC_FLAG_GOSSIP = 0x00000001,       // 100%
            /// <summary>
            /// NPC is a Questgiver
            /// </summary>
            UNIT_NPC_FLAG_QUESTGIVER = 0x00000002,       // guessed, probably ok
            /// <summary>
            /// The NPC is a Vendor
            /// </summary>
            UNIT_NPC_FLAG_VENDOR = 0x00000004,       // 100%
            /// <summary>
            /// The NPC is a Flightmaster
            /// </summary>
            UNIT_NPC_FLAG_FLIGHTMASTER = 0x00000008,       // 100%
            /// <summary>
            /// The NPC is a Trainer
            /// </summary>
            UNIT_NPC_FLAG_TRAINER = 0x00000010,       // 100%
            /// <summary>
            /// The NPC is a Spirit Healer
            /// </summary>
            UNIT_NPC_FLAG_SPIRITHEALER = 0x00000020,       // guessed
            /// <summary>
            /// The NPC is a Spirit Guide
            /// </summary>
            UNIT_NPC_FLAG_SPIRITGUIDE = 0x00000040,       // guessed
            /// <summary>
            /// The NPC is an Innkeeper
            /// </summary>
            UNIT_NPC_FLAG_INNKEEPER = 0x00000080,       // 100%
            /// <summary>
            /// The NPC is a Banker
            /// </summary>
            UNIT_NPC_FLAG_BANKER = 0x00000100,       // 100%
            /// <summary>
            /// The NPC is a Guild Master
            /// </summary>
            UNIT_NPC_FLAG_PETITIONER = 0x00000200,       // 100% 0xC0000 = guild petitions
            /// <summary>
            /// The NPC is a Tabard Designer
            /// </summary>
            UNIT_NPC_FLAG_TABARDDESIGNER = 0x00000400,       // 100%
            /// <summary>
            /// The NPC is a Battlemaster
            /// </summary>
            UNIT_NPC_FLAG_BATTLEMASTER = 0x00000800,       // 100%
            /// <summary>
            /// The NPC is an Auctioneer
            /// </summary>
            UNIT_NPC_FLAG_AUCTIONEER = 0x00001000,       // 100%
            /// <summary>
            /// The NPC is a Stable Master
            /// </summary>
            UNIT_NPC_FLAG_STABLEMASTER = 0x00002000,       // 100%
            /// <summary>
            /// The NPC is a Repairer
            /// </summary>
            UNIT_NPC_FLAG_REPAIR = 0x00004000,       // 100%
            /// <summary>
            /// The NPC is meant for PvP
            /// </summary>
            UNIT_NPC_FLAG_OUTDOORPVP = 0x20000000, // custom flag for outdoor pvp creatures || Custom flag
        }

        /// <summary>
        ///     The different ranks of creatures
        /// </summary>
        public enum CreatureRankTypes
        {
            /// <summary>
            /// The NPC is Normal
            /// </summary>
            Normal = 0,
            /// <summary>
            /// The NPC is Elite
            /// </summary>
            Elite = 1,
            /// <summary>
            /// The NPC is a Rare Elite
            /// </summary>
            RareElite = 2,
            /// <summary>
            /// The NPC is a Boss
            /// </summary>
            Boss = 3,
            /// <summary>
            /// The NPC is a Rare
            /// </summary>
            Rare = 4
        }

        /// <summary>
        /// Types of creatures
        /// </summary>
        public enum CreatureType
        {
            /// <summary>
            /// The creature is a Beast
            /// </summary>
            Beast = 1,
            /// <summary>
            /// The creature is a Dragonkin
            /// </summary>
            Dragonkin = 2,
            /// <summary>
            /// The creature is a Demon
            /// </summary>
            Demon = 3,
            /// <summary>
            /// The creature is an Elemental
            /// </summary>
            Elemental = 4,
            /// <summary>
            /// The creature is a Giant
            /// </summary>
            Giant = 5,
            /// <summary>
            /// The creature is Undead
            /// </summary>
            Undead = 6,
            /// <summary>
            /// The creature is Humanoid
            /// </summary>
            Humanoid = 7,
            /// <summary>
            /// The creature is a Critter
            /// </summary>
            Critter = 8,
            /// <summary>
            /// The creature is Mechanical
            /// </summary>
            Mechanical = 9,
            /// <summary>
            /// The creature type is not specified
            /// </summary>
            NotSpecified = 10,
            /// <summary>
            /// The creature is a Totem
            /// </summary>
            Totem = 11,
        }

        /// <summary>
        /// Creature families
        /// </summary>
        public enum CreatureFamily
        {
            /// <summary>
            /// The creature is a part of the Wolf family
            /// </summary>
            Wolf = 1,
            /// <summary>
            /// The creature is a part of the Cat family
            /// </summary>
            Cat = 2,
            /// <summary>
            /// The creature is a part of the Spider family
            /// </summary>
            Spider = 3,
            /// <summary>
            /// The creature is a part of the Bear family
            /// </summary>
            Bear = 4,
            /// <summary>
            /// The creature is a part of the Boar family
            /// </summary>
            Boar = 5,
            /// <summary>
            /// The creature is a part of the Crocolisk family
            /// </summary>
            Crocolisk = 6,
            /// <summary>
            /// The creature is a part of the Carrion Bird family
            /// </summary>
            CarrionBird = 7,
            /// <summary>
            /// The creature is a part of the Crab family
            /// </summary>
            Crab = 8,
            /// <summary>
            /// The creature is a part of the Gorilla family
            /// </summary>
            Gorilla = 9,
            /// <summary>
            /// The creature is a part of the Raptor family
            /// </summary>
            Raptor = 11,
            /// <summary>
            /// The creature is a part of the Tallstrider family
            /// </summary>
            Tallstrider = 12,
            /// <summary>
            /// The creature is a part of the Felhunter family
            /// </summary>
            Felhunter = 15,
            /// <summary>
            /// The creature is a part of the Voidwalker family
            /// </summary>
            Voidwalker = 16,
            /// <summary>
            /// The creature is a part of the Succubus family
            /// </summary>
            Succubus = 17,
            /// <summary>
            /// The creature is a part of the Doomguard family
            /// </summary>
            Doomguard = 19,
            /// <summary>
            /// The creature is a part of the Scorpid family
            /// </summary>
            Scorpid = 20,
            /// <summary>
            /// The creature is a part of the Turtle family
            /// </summary>
            Turtle = 21,
            /// <summary>
            /// The creature is a part of the Imp family
            /// </summary>
            Imp = 23,
            /// <summary>
            /// The creature is a part of the Bat family
            /// </summary>
            Bat = 24,
            /// <summary>
            /// The creature is a part of the Hyena family
            /// </summary>
            Hyena = 25,
            /// <summary>
            /// The creature is a part of the Owl family
            /// </summary>
            Owl = 26,
            /// <summary>
            /// The creature is a part of the Wind Serpent family
            /// </summary>
            WindSerpent = 27,
            /// <summary>
            /// The creature is a part of the Remote Control family (tonk?)
            /// </summary>
            RemoteControl = 28
        }

        /// <summary>
        ///     Character equipment slots
        /// </summary>
        public enum EquipSlot
        {
            /// <summary>
            /// The Ammo slot
            /// </summary>
            Ammo = 0, // 25% sure
            /// <summary>
            /// The Head slot
            /// </summary>
            Head = 1,
            /// <summary>
            /// The Neck slot
            /// </summary>
            Neck = 2,
            /// <summary>
            /// The Shoulders slot
            /// </summary>
            Shoulders = 3,
            /// <summary>
            /// The Back slot
            /// </summary>
            Back = 15,
            /// <summary>
            /// The Chest slot
            /// </summary>
            Chest = 5,
            /// <summary>
            /// The Shirt slot
            /// </summary>
            Shirt = 4,
            /// <summary>
            /// The Tabard slot
            /// </summary>
            Tabard = 19,
            /// <summary>
            /// The Wrist slot
            /// </summary>
            Wrist = 9,
            /// <summary>
            /// The Main Hand slot
            /// </summary>
            MainHand = 16,
            /// <summary>
            /// The Main Hand slot
            /// </summary>
            OffHand = 17,
            /// <summary>
            /// The Ranged slot
            /// </summary>
            Ranged = 18,
            /// <summary>
            /// The Hands slot
            /// </summary>
            Hands = 10,
            /// <summary>
            /// The Waist slot
            /// </summary>
            Waist = 6,
            /// <summary>
            /// The Legs slot
            /// </summary>
            Legs = 7,
            /// <summary>
            /// The Feet slot
            /// </summary>
            Feet = 8,
            /// <summary>
            /// The First Finger slot
            /// </summary>
            Finger1 = 11,
            /// <summary>
            /// The Second Finger slot
            /// </summary>
            Finger2 = 12,
            /// <summary>
            /// The First Trinket slot
            /// </summary>
            Trinket1 = 13,
            /// <summary>
            /// The Second Trinket slot
            /// </summary>
            Trinket2 = 14
        }

        /// <summary>
        /// Classes of items
        /// </summary>
        public enum ItemClass
        {
            /// <summary>
            /// The item is a Consumable
            /// </summary>
            Consumable = 0,
            /// <summary>
            /// The item is a Container
            /// </summary>
            Container = 1,
            /// <summary>
            /// The item is a Weapon
            /// </summary>
            Weapon = 2,
            /// <summary>
            /// The item is Armor
            /// </summary>
            Armor = 4,
            /// <summary>
            /// The item is a Reagent
            /// </summary>
            Reagent = 5,
            /// <summary>
            /// The item is a Projectile
            /// </summary>
            Projectile = 6,
            /// <summary>
            /// The item is a Trade Good
            /// </summary>
            TradeGoods = 7,
            /// <summary>
            /// The item is a Recipe
            /// </summary>
            Recipe = 9,
            /// <summary>
            /// The item is a Quiver
            /// </summary>
            Quiver = 11,
            /// <summary>
            /// The item is for a quest
            /// </summary>
            Quest = 12,
            /// <summary>
            /// The item is a Key
            /// </summary>
            Key = 13,
            /// <summary>
            /// The item is Miscellaneous
            /// </summary>
            Miscellaneous = 15
        }

        /// <summary>
        /// Subclasses of items
        /// </summary>
        public enum ItemSubclass
        {
            /// <summary>
            /// The item is a Consumable
            /// </summary>
            Consumable = 0,
            /// <summary>
            /// The item is a Bag
            /// </summary>
            Bag = 0,
            /// <summary>
            /// The item is a Soul Shard Bag
            /// </summary>
            SoulBag = 1,
            /// <summary>
            /// The item is an Herbalism Bag
            /// </summary>
            HerbBag = 2,
            /// <summary>
            /// The item is an Enchanting Bag
            /// </summary>
            EnchantingBag = 3,
            /// <summary>
            /// The item is an Engineering Bag
            /// </summary>
            EngineeringBag = 4,

            /// <summary>
            /// The item is a One-Handed Axe
            /// </summary>
            Axe1H = 0,
            /// <summary>
            /// The item is a Two-Handed Axe
            /// </summary>
            Axe2H = 1,
            /// <summary>
            /// The item is a Bow
            /// </summary>
            Bow = 2,
            /// <summary>
            /// The item is a Gun
            /// </summary>
            Gun = 3,
            /// <summary>
            /// The item is a One-Handed Mace
            /// </summary>
            Mace1H = 4,
            /// <summary>
            /// The item is a Two-Handed Mace
            /// </summary>
            Mace2H = 5,
            /// <summary>
            /// The item is a Polearm
            /// </summary>
            Polearm = 6,
            /// <summary>
            /// The item is a One-Handed Sword
            /// </summary>
            Sword1H = 7,
            /// <summary>
            /// The item is a Two-Handed Sword
            /// </summary>
            Sword2H = 8,
            /// <summary>
            /// The item is a Staff
            /// </summary>
            Staff = 10,
            /// <summary>
            /// The item is a Fist Weapon
            /// </summary>
            FistWeapon = 13,
            /// <summary>
            /// The Item is a Miscellaneous Weapon
            /// </summary>
            MiscellaneousWeapon = 14,
            /// <summary>
            /// The item is a Dagger
            /// </summary>
            Dagger = 15,
            /// <summary>
            /// The Item is a Throwing Weapon
            /// </summary>
            Thrown = 16,
            /// <summary>
            /// The item is a spear
            /// </summary>
            Spear = 17,
            /// <summary>
            /// The item is a Crossbow
            /// </summary>
            Crossbow = 18,
            /// <summary>
            /// The item is a Wand
            /// </summary>
            Wand = 19,
            /// <summary>
            /// The item is a Fishing Pole
            /// </summary>
            FishingPole = 20,

            /// <summary>
            /// The item is Miscellaneous Armor
            /// </summary>
            MiscellaneousArmor = 0,
            /// <summary>
            /// The item is Cloth
            /// </summary>
            Cloth = 1,
            /// <summary>
            /// The item is Leather
            /// </summary>
            Leather = 2,
            /// <summary>
            /// The item is Mail
            /// </summary>
            Mail = 3,
            /// <summary>
            /// The item is Plate
            /// </summary>
            Plate = 4,
            /// <summary>
            /// The item is a Shield
            /// </summary>
            Shield = 6,
            /// <summary>
            /// The item is a Libram
            /// </summary>
            Libram = 7,
            /// <summary>
            /// The item is an Idol
            /// </summary>
            Idol = 8,
            /// <summary>
            /// The item is a Totem
            /// </summary>
            Totem = 9,

            /// <summary>
            /// The item is a Reagent
            /// </summary>
            Reagent = 0,

            /// <summary>
            /// The item is an Arrow
            /// </summary>
            Arrow = 2,
            /// <summary>
            /// The item is a Bullet
            /// </summary>
            Bullet = 3,

            /// <summary>
            /// The item is a Trade Good
            /// </summary>
            TradeGoods = 0,
            /// <summary>
            /// The item is an Engineering Part
            /// </summary>
            Parts = 1,
            /// <summary>
            /// The item is an Explosive
            /// </summary>
            Explosives = 2,
            /// <summary>
            /// The item is an Engineering Device
            /// </summary>
            Devices = 3,

            /// <summary>
            /// The item is a Book
            /// </summary>
            Book = 0,
            /// <summary>
            /// The item is for Leatherworking
            /// </summary>
            Leatherworking = 1,
            /// <summary>
            /// The item is for Tailoring
            /// </summary>
            Tailoring = 2,
            /// <summary>
            /// The item is for Engineering
            /// </summary>
            Engineering = 3,
            /// <summary>
            /// The item is for Blacksmithing
            /// </summary>
            Blacksmithing = 4,
            /// <summary>
            /// The item is for Cooking
            /// </summary>
            Cooking = 5,
            /// <summary>
            /// The item is for Alchemy
            /// </summary>
            Alchemy = 6,
            /// <summary>
            /// The item is for First Aid
            /// </summary>
            FirstAid = 7,
            /// <summary>
            /// The item is for Enchanting
            /// </summary>
            Enchanting = 8,
            /// <summary>
            /// The item is for Fishing
            /// </summary>
            Fishing = 9,

            /// <summary>
            /// The item is a Quiver
            /// </summary>
            Quiver = 2,
            /// <summary>
            /// The item is an Ammo Pouch
            /// </summary>
            AmmoPouch = 3,

            /// <summary>
            /// The item is for a Quest
            /// </summary>
            Quest = 0,

            /// <summary>
            /// The item is a Key
            /// </summary>
            Key = 0,
            /// <summary>
            /// The item is a Lockpick
            /// </summary>
            Lockpick = 1,

            /// <summary>
            /// The item is Junk
            /// </summary>
            Junk = 0
        }

        /// <summary>
        /// The different types of Gossip Options in WoW
        /// </summary>
        public enum GossipTypes
        {
            /// <summary>
            /// The Gossip option
            /// </summary>
            Gossip = 0,
            /// <summary>
            /// The Vendor option
            /// </summary>
            Vendor = 1,
            /// <summary>
            /// The Taxi option
            /// </summary>
            Taxi = 2,
            /// <summary>
            /// The Trainer option
            /// </summary>
            Trainer = 3,
            /// <summary>
            /// The Healer option
            /// </summary>
            Healer = 4,
            /// <summary>
            /// The Binder option
            /// </summary>
            Binder = 5,
            /// <summary>
            /// The Banker option
            /// </summary>
            Banker = 6,
            /// <summary>
            /// The Petition option
            /// </summary>
            Petition = 7,
            /// <summary>
            /// The Tabard Designer option
            /// </summary>
            TabardDesigner = 8,
            /// <summary>
            /// The Battlemaster option
            /// </summary>
            Battlemaster = 9,
            /// <summary>
            /// The Auctioneer option
            /// </summary>
            Auctioneer = 10
        }

        /// <summary>
        /// Login states (login, charselect)
        /// </summary>
        public enum LoginStates
        {
            /// <summary>
            /// The Login screen
            /// </summary>
            Login,
            /// <summary>
            /// The Character Select screen
            /// </summary>
            CharacterSelect
        }

        /// <summary>
        /// Types of Quest-Frames (Accept, Continue, Complete, None)
        /// </summary>
        public enum QuestFrameState
        {
            /// <summary>
            /// Quest Frame for accepting or declining a quest
            /// </summary>
            Accept = 1,
            /// <summary>
            /// Quest Frame for continuing a dialog
            /// </summary>
            Continue = 2,
            /// <summary>
            /// Quest Frame for completing a quest
            /// </summary>
            Complete = 3,
            /// <summary>
            /// Quest Frame for a quest giver's greeting
            /// </summary>
            Greeting = 0
        }

        /// <summary>
        /// The state of a quest selectable in a gossip dialog (complete, accept etc.)
        /// </summary>
        public enum QuestGossipState
        {
            /// <summary>
            /// The quest has been accepted
            /// </summary>
            Accepted = 3,
            /// <summary>
            /// The quest is available
            /// </summary>
            Available = 5,
            /// <summary>
            /// The quest is able to be turned in
            /// </summary>
            Completeable = 4
        }

        /// <summary>
        /// Quest-objective types: Kill, Collect or Event
        /// </summary>
        public enum QuestObjectiveTypes : byte
        {
            /// <summary>
            /// Quest Objective involves killing NPCs
            /// </summary>
            Kill = 1,
            /// <summary>
            /// The Quest Objective involves collecting items
            /// </summary>
            Collect = 2,
            /// <summary>
            /// The Quest Objective is an event
            /// </summary>
            Event = 3
        }

        /// <summary>
        /// The possible states of an accepted quest
        /// </summary>
        public enum QuestState
        {
            /// <summary>
            /// The accepted quest is completed
            /// </summary>
            Completed = 1,
            /// <summary>
            /// The accepted quest is in progress
            /// </summary>
            InProgress = 0,
            /// <summary>
            /// The accepted quest is failed
            /// </summary>
            Failed = -1
        }

        /// <summary>
        ///     Possible reactions of units
        /// </summary>
        public enum UnitReaction
        {
            /// <summary>
            /// The unit is Neutral (yellow)
            /// </summary>
            Neutral = 3,
            /// <summary>
            /// The unit is Friendly (green) (blue?)
            /// </summary>
            Friendly = 4,

            // Guards of the other faction are for example hostile 2.
            // All other hostile mobs I met are just hostile.
            /// <summary>
            /// The unit is Hostile (red) (is a non-guard)
            /// </summary>
            Hostile = 1,
            /// <summary>
            /// The unit is hostile (red) (is a guard)
            /// </summary>
            Hostile2 = 0
        }

        /// <summary>
        ///     Different types of WoW objects
        /// </summary>
        public enum WoWObjectTypes : byte
        {
            /// <summary>
            /// The object has no type
            /// </summary>
            OT_NONE = 0,
            /// <summary>
            /// The object is an Item
            /// </summary>
            OT_ITEM = 1,
            /// <summary>
            /// The object is a Container
            /// </summary>
            OT_CONTAINER = 2,
            /// <summary>
            /// The object is a Unit
            /// </summary>
            OT_UNIT = 3,
            /// <summary>
            /// The object is a Player
            /// </summary>
            OT_PLAYER = 4,
            /// <summary>
            /// The object is a Game Object
            /// </summary>
            OT_GAMEOBJ = 5,
            /// <summary>
            /// The object is a Dynamic Object
            /// </summary>
            OT_DYNOBJ = 6,
            /// <summary>
            /// The object is a Corpse
            /// </summary>
            OT_CORPSE = 7
        }
    }
}
