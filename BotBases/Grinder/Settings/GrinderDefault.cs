using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using ZzukBot.Settings;

namespace Grinder.Settings
{
    internal static class GrinderDefault
    {

        internal static bool CorpseLoot
        {
            get =>
                JObject.Parse(File.ReadAllText(Paths.Settings)).SelectToken(typeof(GrinderDefault).Name).Value<bool>(MethodBase.GetCurrentMethod().Name.Replace("get_", string.Empty));

            set
            {
                JObject settingsJObject = JObject.Parse(File.ReadAllText(Paths.Settings));
                JToken defaultJToken = settingsJObject.SelectToken(typeof(GrinderDefault).Name);

                defaultJToken[MethodBase.GetCurrentMethod().Name.Replace("set_", string.Empty)] = value;

                File.WriteAllText(Paths.Settings, JsonConvert.SerializeObject(settingsJObject, Formatting.Indented));
            }
        }

        internal static bool CorpseSkin
        {
            get =>
                JObject.Parse(File.ReadAllText(Paths.Settings)).SelectToken(typeof(GrinderDefault).Name).Value<bool>(MethodBase.GetCurrentMethod().Name.Replace("get_", string.Empty));

            set
            {
                JObject settingsJObject = JObject.Parse(File.ReadAllText(Paths.Settings));
                JToken defaultJToken = settingsJObject.SelectToken(typeof(GrinderDefault).Name);

                defaultJToken[MethodBase.GetCurrentMethod().Name.Replace("set_", string.Empty)] = value;

                File.WriteAllText(Paths.Settings, JsonConvert.SerializeObject(settingsJObject, Formatting.Indented));
            }
        }

        internal static int DrinkAt
        {
            get =>
                JObject.Parse(File.ReadAllText(Paths.Settings)).SelectToken(typeof(GrinderDefault).Name).Value<int>(MethodBase.GetCurrentMethod().Name.Replace("get_", string.Empty));

            set
            {
                JObject settingsJObject = JObject.Parse(File.ReadAllText(Paths.Settings));
                JToken defaultJToken = settingsJObject.SelectToken(typeof(GrinderDefault).Name);

                defaultJToken[MethodBase.GetCurrentMethod().Name.Replace("set_", string.Empty)] = value;

                File.WriteAllText(Paths.Settings, JsonConvert.SerializeObject(settingsJObject, Formatting.Indented));
            }
        }

        internal static int EatAt
        {
            get =>
                JObject.Parse(File.ReadAllText(Paths.Settings)).SelectToken(typeof(GrinderDefault).Name).Value<int>(MethodBase.GetCurrentMethod().Name.Replace("get_", string.Empty));

            set
            {
                JObject settingsJObject = JObject.Parse(File.ReadAllText(Paths.Settings));
                JToken defaultJToken = settingsJObject.SelectToken(typeof(GrinderDefault).Name);

                defaultJToken[MethodBase.GetCurrentMethod().Name.Replace("set_", string.Empty)] = value;

                File.WriteAllText(Paths.Settings, JsonConvert.SerializeObject(settingsJObject, Formatting.Indented));
            }
        }

        internal static int FreeSlotsToVendor
        {
            get =>
                JObject.Parse(File.ReadAllText(Paths.Settings)).SelectToken(typeof(GrinderDefault).Name).Value<int>(MethodBase.GetCurrentMethod().Name.Replace("get_", string.Empty));

            set
            {
                JObject settingsJObject = JObject.Parse(File.ReadAllText(Paths.Settings));
                JToken defaultJToken = settingsJObject.SelectToken(typeof(GrinderDefault).Name);

                defaultJToken[MethodBase.GetCurrentMethod().Name.Replace("set_", string.Empty)] = value;

                File.WriteAllText(Paths.Settings, JsonConvert.SerializeObject(settingsJObject, Formatting.Indented));
            }
        }

        internal static bool KeepBlues
        {
            get =>
                JObject.Parse(File.ReadAllText(Paths.Settings)).SelectToken(typeof(GrinderDefault).Name).Value<bool>(MethodBase.GetCurrentMethod().Name.Replace("get_", string.Empty));

            set
            {
                JObject settingsJObject = JObject.Parse(File.ReadAllText(Paths.Settings));
                JToken defaultJToken = settingsJObject.SelectToken(typeof(GrinderDefault).Name);

                defaultJToken[MethodBase.GetCurrentMethod().Name.Replace("set_", string.Empty)] = value;

                File.WriteAllText(Paths.Settings, JsonConvert.SerializeObject(settingsJObject, Formatting.Indented));
            }
        }

        internal static bool KeepGreens
        {
            get =>
                JObject.Parse(File.ReadAllText(Paths.Settings)).SelectToken(typeof(GrinderDefault).Name).Value<bool>(MethodBase.GetCurrentMethod().Name.Replace("get_", string.Empty));

            set
            {
                JObject settingsJObject = JObject.Parse(File.ReadAllText(Paths.Settings));
                JToken defaultJToken = settingsJObject.SelectToken(typeof(GrinderDefault).Name);

                defaultJToken[MethodBase.GetCurrentMethod().Name.Replace("set_", string.Empty)] = value;

                File.WriteAllText(Paths.Settings, JsonConvert.SerializeObject(settingsJObject, Formatting.Indented));
            }
        }

        internal static bool KeepPurples
        {
            get =>
                JObject.Parse(File.ReadAllText(Paths.Settings)).SelectToken(typeof(GrinderDefault).Name).Value<bool>(MethodBase.GetCurrentMethod().Name.Replace("get_", string.Empty));

            set
            {
                JObject settingsJObject = JObject.Parse(File.ReadAllText(Paths.Settings));
                JToken defaultJToken = settingsJObject.SelectToken(typeof(GrinderDefault).Name);

                defaultJToken[MethodBase.GetCurrentMethod().Name.Replace("set_", string.Empty)] = value;

                File.WriteAllText(Paths.Settings, JsonConvert.SerializeObject(settingsJObject, Formatting.Indented));
            }
        }

        internal static bool NinjaSkin
        {
            get =>
                JObject.Parse(File.ReadAllText(Paths.Settings)).SelectToken(typeof(GrinderDefault).Name).Value<bool>(MethodBase.GetCurrentMethod().Name.Replace("get_", string.Empty));

            set
            {
                JObject settingsJObject = JObject.Parse(File.ReadAllText(Paths.Settings));
                JToken defaultJToken = settingsJObject.SelectToken(typeof(GrinderDefault).Name);

                defaultJToken[MethodBase.GetCurrentMethod().Name.Replace("set_", string.Empty)] = value;

                File.WriteAllText(Paths.Settings, JsonConvert.SerializeObject(settingsJObject, Formatting.Indented));
            }
        }

        internal static List<string> ProtectedItems
        {
            get =>
                JObject.Parse(File.ReadAllText(Paths.Settings)).SelectToken(typeof(GrinderDefault).Name).SelectToken(MethodBase.GetCurrentMethod().Name.Replace("get_", string.Empty))
                .ToObject<List<string>>();

            set
            {
                JObject settingsJObject = JObject.Parse(File.ReadAllText(Paths.Settings));
                JToken defaultJToken = settingsJObject.SelectToken(typeof(GrinderDefault).Name);

                defaultJToken[MethodBase.GetCurrentMethod().Name.Replace("set_", string.Empty)] = JToken.FromObject(value);

                File.WriteAllText(Paths.Settings, JsonConvert.SerializeObject(settingsJObject, Formatting.Indented));
            }
        }

        internal static int SearchRadius
        {
            get =>
                JObject.Parse(File.ReadAllText(Paths.Settings)).SelectToken(typeof(GrinderDefault).Name).Value<int>(MethodBase.GetCurrentMethod().Name.Replace("get_", string.Empty));

            set
            {
                JObject settingsJObject = JObject.Parse(File.ReadAllText(Paths.Settings));
                JToken defaultJToken = settingsJObject.SelectToken(typeof(GrinderDefault).Name);

                defaultJToken[MethodBase.GetCurrentMethod().Name.Replace("set_", string.Empty)] = value;

                File.WriteAllText(Paths.Settings, JsonConvert.SerializeObject(settingsJObject, Formatting.Indented));
            }
        }
    }
}
