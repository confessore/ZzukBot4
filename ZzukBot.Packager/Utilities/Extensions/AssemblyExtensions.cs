using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;

namespace ZzukBot.Packager.Utilities.Extensions
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

        internal static string GetMd5AsBase64(this Assembly assembly)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(assembly.Location))
                {
                    return Convert.ToBase64String(md5.ComputeHash(stream));
                }
            }
        }
    }
}
