using System.IO;

namespace Grinderv2
{
    public class Paths
    {
        public static string Settings = ZzukBot.Settings.Paths.Data + "\\grinder.json";
        public static string Creature = Directory.GetCurrentDirectory() + "\\creature.json";
        public static string CreatureTemplate = Directory.GetCurrentDirectory() + "\\creature_template.json";
    }
}
