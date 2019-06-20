using Follower.TreeTasks.Idle;
using Follower.TreeTasks.Party;
using System.Collections.Generic;
using TreeTaskCore;

namespace Follower
{
    public static class TTS
    {
        public static readonly TreeTask idle = new TreeTask(0, 
            new List<TTask>
            {
                //new Advertise(),
                new Fight(),
                new Wait()
            });

        public static readonly TreeTask party = new TreeTask(1,
            new List<TTask>
            {
                new Buff(),
                new Follow(),
                new Heal(),
                new Loot(),
            });

        public static readonly List<TreeTask> alive = new List<TreeTask>()
        {
            idle, party
        };
    }
}
