using System.Collections.Generic;

namespace Quester
{
    public class NPCs
    {
        public IReadOnlyList<NPC> GeneralGoods { get; set; }
        public IReadOnlyList<NPC> Repair { get; set; }
        public IReadOnlyList<NPC> Ammo { get; set; }
        public IReadOnlyList<NPC> FoodDrink { get; set; }
    }
}
