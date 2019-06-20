using System.Collections.Generic;
using ZzukBot.Core.Game.Frames.FrameObjects;
using ZzukBot.Core.Game.Objects;

namespace Quester
{
    public class Quest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IReadOnlyList<QuestObjective> Objectives { get; set; }
        public IReadOnlyList<Location> Locations { get; set; }
        public IReadOnlyList<string> Targets { get; set; }
        public Location TurnIn { get; set; }
    }
}
