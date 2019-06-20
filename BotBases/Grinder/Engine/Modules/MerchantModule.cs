using Grinder.Settings;
using System.Linq;
using ZzukBot.Core.Constants;
using ZzukBot.Core.Game.Frames;
using ZzukBot.Core.Game.Statics;

namespace Grinder.Engine.Modules
{
    public class MerchantModule
    {
        Inventory Inventory { get; }
        MerchantFrame MerchantFrame { get; set; }
        ObjectManager ObjectManager { get; }
        PathModule PathModule { get; }

        public MerchantModule(
            Inventory inventory,
            ObjectManager objectManager,
            PathModule pathModule)
        {
            Inventory = inventory;
            ObjectManager = objectManager;
            PathModule = pathModule;
        }

        public bool NeedToVendor()
        {
            return Inventory.CountFreeSlots(false) <= GrinderDefault.FreeSlotsToVendor;
        }

        public void RepairAndVendor()
        {
            if (MerchantFrame.IsOpen)
            {
                MerchantFrame = MerchantFrame.Instance;

                if (MerchantFrame.CanRepair && MerchantFrame.TotalRepairCost < ObjectManager.Player.Money)
                    MerchantFrame.RepairAll();

                var item = Inventory.GetAllItems().Where(x =>
                (GrinderDefault.KeepGreens ? !x.Quality.Equals(Enums.ItemQuality.Uncommon) : true) &&
                (GrinderDefault.KeepBlues ? !x.Quality.Equals(Enums.ItemQuality.Rare) : true) &&
                (GrinderDefault.KeepPurples ? !x.Quality.Equals(Enums.ItemQuality.Epic) : true) &&
                !GrinderDefault.ProtectedItems.Contains(x.Name) && !x.Info.SellPrice.Equals(0)).FirstOrDefault();

                if (item != null)
                    MerchantFrame.VendorByGuid(item.Guid);
                else
                    Vendoring = false;
            }
        }

        public bool Vendoring { get; set; }
    }
}
