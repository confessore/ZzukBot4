using System.Collections.Generic;
using System.Linq;
using ZzukBot.Core.Game.Objects;
using ZzukBot.Core.Game.Statics;

namespace Grinder.Engine.Modules
{
    public class ConsumablesModule
    {
        Inventory Inventory { get; }
        ObjectManager ObjectManager { get; }

        public ConsumablesModule(Inventory inventory, ObjectManager objectManager)
        {
            Inventory = inventory;
            ObjectManager = objectManager;
        }

        public List<string> foodNames = new List<string>(new string[]
        {
            "Tough Hunk of Bread", "Darnassian Bleu", "Slitherskin Mackeral", "Shiny Red Apple", "Forest Mushroom Cap", "Tough Jerky",
            "Freshly Baked Bread", "Dalaran Sharp", "Longjaw Mud Snapper", "Tel'Abim Banana", "Red-speckled Mushroom", "Haunch of Meat",
            "Moist Cornbread", "Dwarven Mild", "Bristle Whisker Catfish", "Snapvine Watermelon", "Spongy Morel", "Mutton Chop",
            "Mulgore Spice Bread", "Stormwind Brie", "Rockscale Cod", "Goldenbark Apple", "Delicious Cave Mold", "Wild Hog Shank",
            "Soft Banana Bread", "Fine Aged Cheddar", "Spotted Yellowtail", "Striped Yellowtail", "Moon Harvest Pumpkin", "Raw Black Truffle", "Cured Ham Steak",
            "Homemade Cherry Pie", "Alterac Swiss", "Spinefin Halibut", "Deep Fried Plantains", "Dried King Bolete", "Roasted Quail",
            "Conjured Sweet Roll", "Conjured Cinnamon Roll"
        });

        public List<string> drinkNames = new List<string>(new string[]
        {
            "Refreshing Spring Water", "Conjured Water",
            "Ice Cold Milk", "Conjured Fresh Water",
            "Melon Juice", "Conjured Purified Water",
            "Moonberry Juice", "Conjured Mineral Water",
            "Sweet Nectar", "Conjured Sparkling Water",
            "Morning Glory Dew", "Conjured Crystal Water",
        });

        public List<string> mountNames = new List<string>(new string[]
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
        });

        public WoWItem Food()
        {
            return Inventory.GetItem(foodNames.Where(x => Inventory.GetItemCount(x) > 0).FirstOrDefault());
        }

        public WoWItem Drink()
        {
            return Inventory.GetItem(drinkNames.Where(x => Inventory.GetItemCount(x) > 0).FirstOrDefault());
        }

        public WoWItem Mount()
        {
            return Inventory.GetItem(mountNames.Where(x => Inventory.GetItemCount(x) > 0).FirstOrDefault());
        }
    }
}
