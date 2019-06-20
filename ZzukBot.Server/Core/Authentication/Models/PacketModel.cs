using Newtonsoft.Json;

namespace ZzukBot.Server.Core.Authentication.Models
{
    internal class PacketModel
    {
        [JsonProperty]
        internal uint Opcode { get; set; }
        [JsonProperty]
        internal string[] Content { get; set; }
    }
}
