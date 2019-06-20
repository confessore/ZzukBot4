using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;

namespace ZzukBot.Core.Utilities.Extensions
{
    /// <summary>
    /// Extensions for Assemblies
    /// </summary>
    public static class AssemblyExtensions
    {
        /// <summary>
        /// Jumps to a specified directory level
        /// </summary>
        /// <param name="value"></param>
        /// <param name="parLevels"></param>
        /// <returns>The location of the directory as a string</returns>
        public static string ExtJumpUp(this Assembly value, int parLevels)
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
