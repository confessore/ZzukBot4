using System.Collections.Generic;
using TreeTaskCore;
using WarriorRotation.TTasks;

namespace WarriorRotation
{
    public static class TTS
    {
        public static readonly TreeTask idle = new TreeTask(0,
            new List<TTask>
            {
                new Idle(),
                //new Charge(),
                //new Intercept(),
                new Pummel(),
                new Rend(),
                new BerserkerStance(),
                new Attack(),
                new SunderArmor(),
                new BloodRage(),
                new BattleShout(),
                new Bloodthirst(),
                new Whirlwind(),
                new HeroicStrike(),
                new X()
            });

        public static readonly TreeTask dead = new TreeTask(1,
            new List<TTask>
            {
            });

        public static readonly List<TreeTask> collection = new List<TreeTask>()
        {
            idle
        };
    }
}
