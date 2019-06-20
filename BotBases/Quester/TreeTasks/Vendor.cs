using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using TreeTaskCore;
using ZzukBot.Core.Game.Statics;

namespace Quester.TreeTasks
{
    public class Vendor : TTask
    {
        public override int Priority => 11000;

        public override bool Activate()
        {
            return Inventory.Instance.CountFreeSlots(false) < 1;
        }

        public override void Execute()
        {
            AddUnit(out string path);
        }

        bool AddUnit(out string path)
        {
            var gg = new List<NPC>
            {
                new NPC
                {
                    Name = ObjectManager.Instance.Target.Name,
                    Position = ObjectManager.Instance.Target.Position
                }
            };
            path = Paths.Quester + $"\\npcs";
            if (!File.Exists(path))
                File.Create(path);
            var json = JsonConvert.SerializeObject(new NPCs
            {
                GeneralGoods = gg
            }, Formatting.Indented);
            File.WriteAllText(path, json);
            return false;
        }
    }
}
