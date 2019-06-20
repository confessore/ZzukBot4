using Gatherer.TreeTasks;
using System.Collections.Generic;
using TreeTaskCore;

namespace Gatherer
{
    public class TTS
    {
        public static readonly TreeTask idle = new TreeTask(0,
            new List<TTask>
            {
                new Idle(),
                new Buff(),
                new Gather(),
                new Fight(),
                new Search(),
                new Rest(),
                new Mount()
                //new Pull()
            });

        public static readonly TreeTask dead = new TreeTask(1,
            new List<TTask>
            {
                new Dead(),
                new Ghost()
            });

        public static readonly List<TreeTask> collect = new List<TreeTask>()
        {
            idle, dead
        };
    }
}
