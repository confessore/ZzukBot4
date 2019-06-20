using TreeTaskCore;
using ZzukBot.Core.Game.Statics;

namespace Follower.TreeTasks.Idle
{
    public class Wait : TTask
    {
        public Wait()
        {
            WoWEventHandler.Instance.OnPartyInvite += (sender, args) => OnPartyInvite(args);
        }

        public override int Priority => -1;

        public override bool Activate()
        {
            return true;
        }

        public override void Execute()
        {
            Common.Instance.DebugMessage("WAIT");
        }

        void OnPartyInvite(WoWEventHandler.OnRequestArgs args)
        {
            Lua.Instance.Execute("AcceptGroup();");
            Lua.Instance.Execute("ReloadUI()");
        }
    }
}
