using Quester.TreeTasks;
using System.Collections.Generic;
using TreeTaskCore;

namespace Quester
{
    public static class TTS
    {
        public static readonly TreeTask idle = new TreeTask(0,
            new List<TTask>
            {
                new AcquireQuest(),
                new Fight(),
                new ExecuteQuest(),
                new GoToTurnIn(),
                new Loot(),
                new Rest(),
                new TurnInQuest(),
                new Vendor(),
                new Wait(),
            });

        public static readonly TreeTask party = new TreeTask(1,
            new List<TTask>
            {
            });

        public static readonly List<TreeTask> alive = new List<TreeTask>()
        {
            idle, party
        };
    }
}
