using System;
using TreeTaskCore;
using ZzukBot.Core.Game.Statics;

namespace Grinderv2.TreeTasks
{
    public class Idle : TTask
    {
        public override int Priority => 0;

        public override bool Activate()
        {
            return true;
        }

        public override void Execute()
        {
            //Common.Instance.DebugMessage($"IDLE {DateTime.Now}");
        }
    }
}
