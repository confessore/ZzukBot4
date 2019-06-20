namespace Grinderv2.Models
{
    public class CreatureTemplate
    {
        public int Entry { get; set; }
        public string Name { get; set; }
        public int MinLevel { get; set; }
        public int MaxLevel { get; set; }
        public int MinHealth { get; set; }
        public int MaxHealth { get; set; }
        public int Faction_A { get; set; }
        public int Faction_H { get; set; }
        public int NpcFlag { get; set; }
    }
}
