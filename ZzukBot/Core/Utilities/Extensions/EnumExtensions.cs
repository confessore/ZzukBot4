using System;

namespace ZzukBot.Core.Utilities.Extensions
{
    internal static class EnumExtensions
    {
        internal static bool IsFlag(this Enum keys, Enum flag)
        {
            try
            {
                var keysVal = Convert.ToUInt64(keys);
                var flagVal = Convert.ToUInt64(flag);

                return (keysVal | flagVal) == flagVal;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
