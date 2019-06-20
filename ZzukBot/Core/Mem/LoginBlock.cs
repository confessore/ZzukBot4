using ZzukBot.Core.Utilities.Extensions;
using ZzukBot.Settings;
using funcs = ZzukBot.Core.Constants.Offsets.Functions;

namespace ZzukBot.Core.Mem
{
    internal static class LoginBlock
    {
        internal static void Enable()
        {
            "Enabling Login Block".Log(Logs.Injected);
            Memory.Reader.WriteBytes(funcs.DefaultServerLogin, new byte[] { 0xc3 });
        }

        internal static void Disable()
        {
            Memory.Reader.WriteBytes(funcs.DefaultServerLogin, new byte[] { 0x56 });
        }
    }
}
