using ZzukBot.Core.Constants;

namespace ZzukBot.Core.Utilities.Extensions
{
    internal static class MovementFlagsExtensions
    {
        internal static bool HasFlag(this Enums.MovementFlags value, Enums.MovementFlags flag)
        {
            return (value & flag) != 0;
        }
    }
}
