using ZzukBot.Core.Constants;

namespace ZzukBot.Core.Utilities.Extensions
{
    internal static class ControlBitsExtensions
    {
        internal static bool HasFlag(this Enums.ControlBits value, Enums.ControlBits flag)
        {
            return (value & flag) != 0;
        }
    }
}
