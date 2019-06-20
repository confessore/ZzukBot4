using System;
using System.Collections.Generic;
using System.Linq;
using ZzukBot.Core.Constants;
using ZzukBot.Core.Game.Objects;

namespace ZzukBot.Core.Game.Statics
{
    /// <summary>
    ///     Commonly used methods and variables for botbases, plugins and custom classes
    /// </summary>
    public sealed class Common
    {
        static readonly Lazy<Common> instance = new Lazy<Common>(() => new Common());

        Inventory Inventory { get; }
        Lua Lua { get; }
        Navigation Navigation { get; }
        ObjectManager ObjectManager { get; }
        Spell Spell { get; }

        private Common()
        {
            Inventory = Inventory.Instance;
            Lua = Lua.Instance;
            Navigation = Navigation.Instance;
            ObjectManager = ObjectManager.Instance;
            Spell = Spell.Instance;
            WoWEventHandler.Instance.OnErrorMessage += (sender, args) => HandleErrorMessage(args);
        }

        /// <summary>
        ///     Access to the Common instance
        /// </summary>
        public static Common Instance => instance.Value;

        /// <summary>
        ///     Prints a message to the WoW chat window that is only visible to the player
        /// </summary>
        /// <param name="String"></param>
        public void DebugMessage(string String)
        {
            Lua.Execute("DEFAULT_CHAT_FRAME:AddMessage(\"DEBUG: " + String + "\");");
        }

        /// <summary>
        ///     Mana percentage at which the player should drink
        /// </summary>
        public int DrinkAt { get; set; } = 50;

        /// <summary>
        ///     An array of drink names
        /// </summary>
        public readonly string[] Drinks =
        {
            "Refreshing Spring Water", "Conjured Water",
            "Ice Cold Milk", "Conjured Fresh Water",
            "Melon Juice", "Conjured Purified Water",
            "Sweet Nectar", "Conjured Spring Water",
            "Moonberry Juice", "Conjured Mineral Water",
            "Morning Glory Dew", "Conjured Sparkling Water",
            "Conjured Crystal Water"
        };

        /// <summary>
        ///     Tries to drink 
        /// </summary>
        public void TryUseDrink()
        {
            if (ObjectManager.Player.CastingId != 0 || ObjectManager.Player.ChannelingId != 0) return;
            if (Inventory.ExistingItems(Drinks).Count > 0 && !ObjectManager.Player.GotAura("Drink"))
                Inventory.ExistingItems(Drinks).FirstOrDefault().Use();
            else
                Lua.Execute("DoEmote('sit')");
        }

        /// <summary>
        ///     Health percentage at which the player should eat
        /// </summary>
        public int EatAt { get; set; } = 50;

        /// <summary>
        ///     An array of food names
        /// </summary>
        public readonly string[] Foods =
        {
            "Tough Hunk of Bread", "Darnassian Bleu", "Slitherskin Mackeral", "Shiny Red Apple", "Forest Mushroom Cap", "Tough Jerky", "Conjured Muffin",
            "Freshly Baked Bread", "Dalaran Sharp", "Longjaw Mud Snapper", "Tel'Abim Banana", "Red-speckled Mushroom", "Haunch of Meat", "Conjured Bread",
            "Moist Cornbread", "Dwarven Mild", "Bristle Whisker Catfish", "Snapvine Watermelon", "Spongy Morel", "Mutton Chop", "Conjured Rye",
            "Mulgore Spice Bread", "Stormwind Brie", "Rockscale Cod", "Goldenbark Apple", "Delicious Cave Mold", "Wild Hog Shank", "Conjured Pumpernickel",
            "Soft Banana Bread", "Fine Aged Cheddar", "Spotted Yellowtail", "Striped Yellowtail", "Moon Harvest Pumpkin", "Raw Black Truffle", "Cured Ham Steak", "Conjured Sourdough",
            "Homemade Cherry Pie", "Alterac Swiss", "Spinefin Halibut", "Deep Fried Plantains", "Dried King Bolete", "Roasted Quail", "Conjured Sweet Roll",
            "Conjured Cinnamon Roll"
        };

        /// <summary>
        ///     An array of bread names
        /// </summary>
        public readonly string[] Bread =
        {
            "Tough Hunk of Bread", "Conjured Muffin",
            "Freshly Baked Bread", "Conjured Bread",
            "Moist Cornbread", "Conjured Rye",
            "Mulgore Spice Bread", "Conjured Pumpernickel",
            "Soft Banana Bread", "Conjured Sourdough",
            "Homemade Cherry Pie", "Conjured Sweet Roll",
            "Conjured Cinnamon Roll"
        };

        /// <summary>
        ///     An array of cheese names
        /// </summary>
        public readonly string[] Cheese =
        {
            "Darnassian Bleu", "Dalaran Sharp", "Dwarven Mild", "Stormwind Brie", "Fine Aged Cheddar", "Alterac Swiss"
        };

        /// <summary>
        ///     An array of fish names
        /// </summary>
        public readonly string[] Fish =
        {
            "Slitherskin Mackeral", "Longjaw Mud Snapper", "Bristle Whisker Catfish", "Rockscale Cod", "Spotted Yellowtail", "Striped Yellowtail", "Spinefin Halibut"
        };

        /// <summary>
        ///     An array of fruit names
        /// </summary>
        public readonly string[] Fruit =
        {
            "Shiny Red Apple", "Tel'Abim Banana", "Snapvine Watermelon", "Goldenbark Apple", "Moon Harvest Pumpkin", "Deep Fried Plantains"
        };

        /// <summary>
        ///     An array of fungus names
        /// </summary>
        public readonly string[] Fungus =
        {
            "Forest Mushroom Cap", "Red-speckled Mushroom", "Spongy Morel", "Delicious Cave Mold", "Raw Black Truffle", "Dried King Bolete"
        };

        /// <summary>
        ///     An array of meat names
        /// </summary>
        public readonly string[] Meat =
        {
            "Tough Jerky", "Haunch of Meat", "Mutton Chop", "Wild Hog Shank", "Cured Ham Steak", "Roasted Quail"
        };

        /// <summary>
        ///     Tries to eat
        /// </summary>
        public void TryUseFood()
        {
            if (ObjectManager.Player.CastingId != 0 || ObjectManager.Player.ChannelingId != 0) return;
            if (Inventory.ExistingItems(Foods).Count > 0 && !ObjectManager.Player.GotAura("Food"))
                Inventory.ExistingItems(Foods).FirstOrDefault().Use();
            else
                Lua.Execute("DoEmote('sit')");
        }
        
        string[] PetFoodByFamily()
        {
            var family = ObjectManager.Pet.CreatureFamily;
            var food = new List<string>();

            if (family == Enums.CreatureFamily.Bat ||
                family == Enums.CreatureFamily.Gorilla ||
                family == Enums.CreatureFamily.Turtle)
            {
                foreach (var item in Fruit)
                    food.Add(item);
                foreach (var item in Fungus)
                    food.Add(item);
            }
            if (family == Enums.CreatureFamily.Cat ||
                family == Enums.CreatureFamily.Crocolisk ||
                family == Enums.CreatureFamily.CarrionBird)
            {
                foreach (var item in Fish)
                    food.Add(item);
                foreach (var item in Meat)
                    food.Add(item);
            }
            if (family == Enums.CreatureFamily.Owl ||
                family == Enums.CreatureFamily.Raptor ||
                family == Enums.CreatureFamily.Spider ||
                family == Enums.CreatureFamily.Scorpid ||
                family == Enums.CreatureFamily.Wolf)
            {
                foreach (var item in Meat)
                    food.Add(item);
            }
            if (family == Enums.CreatureFamily.WindSerpent)
            {
                foreach (var item in Bread)
                    food.Add(item);
                foreach (var item in Cheese)
                    food.Add(item);
                foreach (var item in Fish)
                    food.Add(item);
            }
            if (family == Enums.CreatureFamily.Hyena)
            {
                foreach (var item in Fruit)
                    food.Add(item);
                foreach (var item in Meat)
                    food.Add(item);
            }
            if (family == Enums.CreatureFamily.Bear ||
                family == Enums.CreatureFamily.Boar)
            {
                food = Foods.ToList();
            }
            if (family == Enums.CreatureFamily.Crab)
            {
                foreach (var item in Bread)
                    food.Add(item);
                foreach (var item in Fish)
                    food.Add(item);
            }
            if (family == Enums.CreatureFamily.Tallstrider)
            {
                foreach (var item in Cheese)
                    food.Add(item);
                foreach (var item in Fruit)
                    food.Add(item);
                foreach (var item in Fungus)
                    food.Add(item);
            }
            return food.ToArray();
        }

        /// <summary>
        ///     Determines if food exists in the inventory that the player's pet will eat
        /// </summary>
        public bool HavePetFood => Inventory.ExistingItems(PetFoodByFamily()).Count > 0;

        /// <summary>
        ///     Tries to feed the player's pet
        /// </summary>
        public void TryFeedPet()
        {
            if (!ObjectManager.Pet.IsHappy() && !ObjectManager.Pet.GotAura("Feed Pet Effect"))
                ObjectManager.Pet.Feed(Inventory.FirstExistingItem(PetFoodByFamily()).Name);
        }

        /// <summary>
        /// An array of mount names
        /// </summary>
        public readonly string[] Mounts =
        {
            "Black Stallion Bridle", "Whistle of the Violet Raptor", "Whistle of the Turquoise Raptor",
            "Whistle of the Emerald Raptor", "Horn of the Brown Wolf", "Horn of the Dire Wolf",
            "Horn of the Timber Wolf", "Brown Horse Bridle", "Chestnut Mare Bridle", "Pinto Bridle",
            "Reins of the Spotted Frostsaber", "Reins of the Striped Frostsaber", "Reins of the Striped Nightsaber",
            "Unpainted Mechanostrider", "Green Mechanostrider", "Blue Mechanostrider", "Red Mechanostrider",
            "White Ram", "Brown Ram", "Gray Ram", "Gray Kodo", "Brown Kodo", "Brown Skeletal Horse",
            "Blue Skeletal Horse", "Red Skeletal Horse", "Reins of the Winterspring Frostsaber", "Great Gray Kodo",
            "Great Brown Kodo", "Great White Kodo", "Swift Blue Raptor", "Swift Brown Ram", "Swift Brown Steed",
            "Horn of the Swift Brown Wolf", "Reins of the Swift Frostsaber", "Swift Gray Ram",
            "Horn of the Swift Gray Wolf", "Swift Green Mechanostrider", "Reins of the Swift Mistsaber",
            "Swift Olive Raptor", "Swift Orange Raptor", "Swift Palomino", "Swift Razzashi Raptor",
            "Reins of the Swift Stormsaber", "Horn of the Swift Timber Wolf", "Swift White Mechanostrider",
            "Swift White Ram", "Swift White Steed", "Swift Yellow Mechanostrider", "Swift Zulian Tiger",
            "Whistle of the Ivory Raptor", "Whistle of the Mottled Red Raptor", "Horn of the Arctic Wolf",
            "Horn of the Red Wolf", "Palomino Bridle", "White Stallion Bridle", "Reins of the Nightsaber",
            "Reins of the Frostsaber", "Icy Blue Mechanostrider Mod A", "White Mechanostrider Mod A",
            "Frost Ram", "Black Ram", "Green Kodo", "Teal Kodo", "Deathcharger's Reins", "Purple Skeletal Warhorse",
            "Green Skeletal Warhorse", "Black War Ram", "Black Battlestrider", "Reins of the Black War Tiger",
            "Black War Steed Bridle", "Red Skeletal Warhorse", "Whistle of the Black War Raptor",
            "Horn of the Black War Wolf", "Black War Kodo", "Black Qiraji Resonating Crystal",
            "Stormpike Battle Charger", "Horn of the Frostwolf Howler"
        };

        /// <summary>
        /// Tries to mount
        /// </summary>
        public void TryUseMount()
        {
            if (ObjectManager.Player.CastingId != 0 || ObjectManager.Player.ChannelingId != 0) return;
            if (Inventory.ExistingItems(Mounts).Count > 0 && !ObjectManager.Player.IsMounted)
                Inventory.ExistingItems(Mounts).FirstOrDefault().Use();
        }

        /// <summary>
        ///     An enumerable of mounts in the player's inventory
        /// </summary>
        /// <returns></returns>
        public IEnumerable<WoWItem> ExistingMounts()
        {
            return Inventory.ExistingItems(Mounts);
        }

        /// <summary>
        ///     Units that the player could possible engage
        /// </summary>
        /// <param name="possibleTargets"></param>
        /// <returns>IEnumerable of WoWUnit</returns>
        public IEnumerable<WoWUnit> PossibleTargets(IEnumerable<WoWUnit> possibleTargets)
        {
            return possibleTargets.Where(x =>
                !FactionBlacklist().Where(y => y == x.FactionId).Any() &&
                x.Guid != ObjectManager.Player.Guid && x.IsInCombat && !x.IsDead && ObjectManager.Player.InLosWith(x) &&
                (!x.IsPlayerPet && x.IsMob || (x.IsPlayer && x.IsInCombat && x.NearbyPlayers.Where(y => y.IsInCombat && y.Guid == x.TargetGuid &&
                !(y.Reaction.Equals(Enums.UnitReaction.Neutral) || y.Reaction.Equals(Enums.UnitReaction.Hostile) || y.Reaction.Equals(Enums.UnitReaction.Hostile2))).Any())) &&
                (x.DistanceToPlayer > 5 ? !x.TappedByOther : true) &&
                (x.Reaction.Equals(Enums.UnitReaction.Neutral) || x.Reaction.Equals(Enums.UnitReaction.Hostile) || x.Reaction.Equals(Enums.UnitReaction.Hostile2)) &&
                !x.Reaction.Equals(Enums.UnitReaction.Friendly));
        }

        /// <summary>
        ///     Factions that we do not want to engage with
        /// </summary>
        /// <returns></returns>
        public IEnumerable<int> FactionBlacklist()
        {
            return new List<int>() { 29, 43, 85, 126, 188, 1625 };
        }

        void HandleErrorMessage(WoWEventHandler.OnUiMessageArgs args)
        {
            if (args.Message == "You must be standing to do that")
                Lua.Execute("DoEmote('stand')");
            if (args.Message == "You need to be standing up to loot something!")
                Lua.Execute("DoEmote('stand')");
            if (args.Message == "Target not in line of sight")
                Navigation.Traverse(ObjectManager.Target.Position);
            /*if (args.Message == "Target needs to be in front of you")
                ObjectManager.Instance.Player.StartMovement(Enums.ControlBits.Back);
            if (args.Message == "You are facing the wrong way!")
                ObjectManager.Instance.Player.StartMovement(Enums.ControlBits.Back);*/
            if (args.Message == "You are in shapeshift form")
                Spell.CancelShapeshift();
            if (args.Message == "Can't use items while shapeshifted.")
                Spell.CancelShapeshift();
            if (args.Message == "Can't speak while shapeshifted.")
                Spell.CancelShapeshift();
        }
    }
}
