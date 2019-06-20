using Grinderv2.TreeTasks;
using System.Collections.Generic;
using TreeTaskCore;

namespace Grinderv2
{
    public class TTS
    {
        public static readonly TreeTask idle = new TreeTask(0,
            new List<TTask>
            {
                new Idle(),
                new SearchCreature(),
                new Fight(),
                new Pull(),
                new Loot(),
                new Buff(),
                new Skin(),
                new Rest(),
                new Harvest(),
                new ShouldVendor(),
                new SearchVendor(),
                new Vendor()
                //new Vendor()
                /*new Buff(),
                new Gather(),
                new Fight(),
                new Search(),
                new Rest(),
                new Pull()*/
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
