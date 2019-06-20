using Newtonsoft.Json;
using ZzukBot.Server.Core.Authentication.Models;
using ZzukBot.Server.Core.Authentication.Objects;

namespace ZzukBot.Server.Core.Utilities.Extensions
{
    internal static class AuthenticationExtensions
    {
        static string FormatJson(this string json, string endOfFile)
        {
            if (json.Contains(endOfFile))
                return json.Replace(endOfFile, string.Empty);
            else
                return json + endOfFile;
        }

        internal static string GetContentAtIndex(this string content, string endOfFile, int index)
        {
            return JsonConvert.DeserializeObject<PacketModel>(content.FormatJson(endOfFile)).Content[index];
        }

        internal static uint GetOpcode(this string content, string endOfFile)
        {
            return JsonConvert.DeserializeObject<PacketModel>(content.FormatJson(endOfFile)).Opcode;
        }

        internal static string NewPacket(this Opcodes opcode, string endOfFile, params string[] content)
        {
            return JsonConvert.SerializeObject(
                new PacketModel
                {
                    Opcode = (uint)opcode,
                    Content = content
                }).FormatJson(endOfFile);
        }
    }
}
