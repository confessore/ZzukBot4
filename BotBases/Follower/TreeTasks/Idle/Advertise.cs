using System;
using System.Collections.ObjectModel;
using System.Linq;
using TreeTaskCore;
using ZzukBot.Core.Game.Statics;

namespace Follower.TreeTasks.Idle
{
    public class Advertise : TTask
    {
        public override int Priority => 0;

        public override bool Activate()
        {
            if (ObjectManager.Instance.PartyLeader == null)
            {
                var lastMessage = Chat.Instance.ChatMessages
                    .Where(x => x.UnitName == ObjectManager.Instance.Player.Name)
                    .LastOrDefault();
                if (lastMessage != null)
                    return lastMessage.Time.AddMinutes(5.0) < DateTime.Now;
                return true;
            }
            return false;
        }

        public override void Execute()
        {
            Common.Instance.DebugMessage("ADVERTISE");
            Lua.Instance.SendSay("Invite me to your group and I will follow you.");
        }
    }
}
