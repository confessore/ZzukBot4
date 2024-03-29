﻿using System.IO;
using System.Reflection;

namespace ZzukBot.Server.Core.Utilities.Extensions
{
    internal static class AssemblyExtensions
    {
        internal static string ExtJumpUp(this Assembly value, int parLevels)
        {
            var tmp = value.Location;

            for (var i = 0; i < parLevels; i++)
                tmp = Path.GetDirectoryName(tmp);

            return tmp;
        }
    }
}
