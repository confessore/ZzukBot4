using System;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using ZzukBot.Settings;

namespace ZzukBot.Core.Utilities.Extensions
{
    /// <summary>
    ///     Extensions for strings
    /// </summary>
    public static class StringExtensions
    {
        internal static string BToString(this byte[] value)
        {
            return Encoding.Unicode.GetString(value);
        }

        internal static void ClearLog(this string value)
        {
            var logFile = Paths.Logs + $"\\{value}";

            if (!File.Exists(logFile)) return;

            File.Delete(logFile);
        }

        internal static void CreateFolderStructure(this string value)
        {
            if (!Directory.Exists(value))
            {
                $"Directory {value} doesnt exist yet. Creating it".Log(Logs.PreInject);
                Directory.CreateDirectory(value);
            }
        }

        /// <summary>
        /// Creates a new file
        /// </summary>
        /// <param name="value"></param>
        /// <param name="parArr"></param>
        public static void FileCreate(this string value, byte[] parArr)
        {
            if (File.Exists(value))
            {
                var bytes = File.ReadAllBytes(value);

                if (bytes.SequenceEqual(parArr)) return;

                File.WriteAllBytes(value, parArr);
            }
            else
                File.WriteAllBytes(value, parArr);
        }

        /// <summary>
        /// Checks the integrity of a file
        /// </summary>
        /// <param name="value"></param>
        /// <param name="parArr"></param>
        /// <returns></returns>
        public static bool FileEqualTo(this string value, byte[] parArr)
        {
            if (File.Exists(value))
            {
                var bytes = File.ReadAllBytes(value);

                if (bytes.SequenceEqual(parArr)) return true;
            }

            return false;
        }

        /// <summary>
        ///     Extension method to write a  datetime.now + value + newline to the bots log folder (file: ChatLog.txt => Content
        ///     will be written to ZzukBot\Logs\ChatLog.txt
        /// </summary>
        /// <param name="value">this</param>
        /// <param name="file"></param>
        /// <param name="showDate"></param>
        public static void Log(this string value, string file, bool showDate = true)
        {
            var input = (showDate ? "[" + DateTime.Now + "] " : "") + value + Environment.NewLine;

            if (!Directory.Exists(Paths.Logs))
                Directory.CreateDirectory(Paths.Logs);

            File.AppendAllText(Paths.Logs + "\\" + file, input);
        }

        internal static byte[] ToByte(this string value)
        {
            return Encoding.Unicode.GetBytes(value);
        }

        internal static SecureString ToSecure(this string value)
        {
            var secure = new SecureString();
            foreach (var c in value)
                secure.AppendChar(c);
            return secure;
        }
    }
}
