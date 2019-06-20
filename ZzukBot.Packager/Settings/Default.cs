using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Reflection;

namespace ZzukBot.Packager.Settings
{
    internal static class Default
    {
        internal static string Version
        {
            get =>
                JObject.Parse(File.ReadAllText(Paths.ServerSettings)).SelectToken(typeof(Default).Name).Value<string>(MethodBase.GetCurrentMethod().Name.Replace("get_", string.Empty));

            set
            {
                JObject settingsJObject = JObject.Parse(File.ReadAllText(Paths.ServerSettings));
                JToken defaultJToken = settingsJObject.SelectToken(typeof(Default).Name);

                defaultJToken[MethodBase.GetCurrentMethod().Name.Replace("set_", string.Empty)] = value;

                File.WriteAllText(Paths.ServerSettings, JsonConvert.SerializeObject(settingsJObject, Formatting.Indented));
            }
        }
    }
}
