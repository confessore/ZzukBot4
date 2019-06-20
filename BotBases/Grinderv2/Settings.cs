using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Grinderv2
{
    public class Settings
    {
        internal static bool CorpseLoot
        {
            get =>
                JObject.Parse(File.ReadAllText(Paths.Settings)).SelectToken(typeof(Settings).Name).Value<bool>(MethodBase.GetCurrentMethod().Name.Replace("get_", string.Empty));

            set
            {
                JObject settingsJObject = JObject.Parse(File.ReadAllText(Paths.Settings));
                JToken defaultJToken = settingsJObject.SelectToken(typeof(Settings).Name);

                defaultJToken[MethodBase.GetCurrentMethod().Name.Replace("set_", string.Empty)] = value;

                File.WriteAllText(Paths.Settings, JsonConvert.SerializeObject(settingsJObject, Formatting.Indented));
            }
        }

        internal static bool CorpseSkin
        {
            get =>
                JObject.Parse(File.ReadAllText(Paths.Settings)).SelectToken(typeof(Settings).Name).Value<bool>(MethodBase.GetCurrentMethod().Name.Replace("get_", string.Empty));

            set
            {
                JObject settingsJObject = JObject.Parse(File.ReadAllText(Paths.Settings));
                JToken defaultJToken = settingsJObject.SelectToken(typeof(Settings).Name);

                defaultJToken[MethodBase.GetCurrentMethod().Name.Replace("set_", string.Empty)] = value;

                File.WriteAllText(Paths.Settings, JsonConvert.SerializeObject(settingsJObject, Formatting.Indented));
            }
        }

        internal static List<string> Creatures
        {
            get =>
                JObject.Parse(File.ReadAllText(Paths.Settings)).SelectToken(typeof(Settings).Name).SelectToken(MethodBase.GetCurrentMethod().Name.Replace("get_", string.Empty))
                .ToObject<List<string>>();

            set
            {
                JObject settingsJObject = JObject.Parse(File.ReadAllText(Paths.Settings));
                JToken defaultJToken = settingsJObject.SelectToken(typeof(Settings).Name);

                defaultJToken[MethodBase.GetCurrentMethod().Name.Replace("set_", string.Empty)] = JToken.FromObject(value);

                File.WriteAllText(Paths.Settings, JsonConvert.SerializeObject(settingsJObject, Formatting.Indented));
            }
        }

        internal static int DrinkAt
        {
            get =>
                JObject.Parse(File.ReadAllText(Paths.Settings)).SelectToken(typeof(Settings).Name).Value<int>(MethodBase.GetCurrentMethod().Name.Replace("get_", string.Empty));

            set
            {
                JObject settingsJObject = JObject.Parse(File.ReadAllText(Paths.Settings));
                JToken defaultJToken = settingsJObject.SelectToken(typeof(Settings).Name);

                defaultJToken[MethodBase.GetCurrentMethod().Name.Replace("set_", string.Empty)] = value;

                File.WriteAllText(Paths.Settings, JsonConvert.SerializeObject(settingsJObject, Formatting.Indented));
            }
        }

        internal static int EatAt
        {
            get =>
                JObject.Parse(File.ReadAllText(Paths.Settings)).SelectToken(typeof(Settings).Name).Value<int>(MethodBase.GetCurrentMethod().Name.Replace("get_", string.Empty));

            set
            {
                JObject settingsJObject = JObject.Parse(File.ReadAllText(Paths.Settings));
                JToken defaultJToken = settingsJObject.SelectToken(typeof(Settings).Name);

                defaultJToken[MethodBase.GetCurrentMethod().Name.Replace("set_", string.Empty)] = value;

                File.WriteAllText(Paths.Settings, JsonConvert.SerializeObject(settingsJObject, Formatting.Indented));
            }
        }

        internal static int FreeSlotsToVendor
        {
            get =>
                JObject.Parse(File.ReadAllText(Paths.Settings)).SelectToken(typeof(Settings).Name).Value<int>(MethodBase.GetCurrentMethod().Name.Replace("get_", string.Empty));

            set
            {
                JObject settingsJObject = JObject.Parse(File.ReadAllText(Paths.Settings));
                JToken defaultJToken = settingsJObject.SelectToken(typeof(Settings).Name);

                defaultJToken[MethodBase.GetCurrentMethod().Name.Replace("set_", string.Empty)] = value;

                File.WriteAllText(Paths.Settings, JsonConvert.SerializeObject(settingsJObject, Formatting.Indented));
            }
        }

        internal static bool Harvest
        {
            get =>
                JObject.Parse(File.ReadAllText(Paths.Settings)).SelectToken(typeof(Settings).Name).Value<bool>(MethodBase.GetCurrentMethod().Name.Replace("get_", string.Empty));

            set
            {
                JObject settingsJObject = JObject.Parse(File.ReadAllText(Paths.Settings));
                JToken defaultJToken = settingsJObject.SelectToken(typeof(Settings).Name);

                defaultJToken[MethodBase.GetCurrentMethod().Name.Replace("set_", string.Empty)] = value;

                File.WriteAllText(Paths.Settings, JsonConvert.SerializeObject(settingsJObject, Formatting.Indented));
            }
        }

        internal static bool KeepBlues
        {
            get =>
                JObject.Parse(File.ReadAllText(Paths.Settings)).SelectToken(typeof(Settings).Name).Value<bool>(MethodBase.GetCurrentMethod().Name.Replace("get_", string.Empty));

            set
            {
                JObject settingsJObject = JObject.Parse(File.ReadAllText(Paths.Settings));
                JToken defaultJToken = settingsJObject.SelectToken(typeof(Settings).Name);

                defaultJToken[MethodBase.GetCurrentMethod().Name.Replace("set_", string.Empty)] = value;

                File.WriteAllText(Paths.Settings, JsonConvert.SerializeObject(settingsJObject, Formatting.Indented));
            }
        }

        internal static bool KeepGreens
        {
            get =>
                JObject.Parse(File.ReadAllText(Paths.Settings)).SelectToken(typeof(Settings).Name).Value<bool>(MethodBase.GetCurrentMethod().Name.Replace("get_", string.Empty));

            set
            {
                JObject settingsJObject = JObject.Parse(File.ReadAllText(Paths.Settings));
                JToken defaultJToken = settingsJObject.SelectToken(typeof(Settings).Name);

                defaultJToken[MethodBase.GetCurrentMethod().Name.Replace("set_", string.Empty)] = value;

                File.WriteAllText(Paths.Settings, JsonConvert.SerializeObject(settingsJObject, Formatting.Indented));
            }
        }

        internal static bool KeepPurples
        {
            get =>
                JObject.Parse(File.ReadAllText(Paths.Settings)).SelectToken(typeof(Settings).Name).Value<bool>(MethodBase.GetCurrentMethod().Name.Replace("get_", string.Empty));

            set
            {
                JObject settingsJObject = JObject.Parse(File.ReadAllText(Paths.Settings));
                JToken defaultJToken = settingsJObject.SelectToken(typeof(Settings).Name);

                defaultJToken[MethodBase.GetCurrentMethod().Name.Replace("set_", string.Empty)] = value;

                File.WriteAllText(Paths.Settings, JsonConvert.SerializeObject(settingsJObject, Formatting.Indented));
            }
        }

        internal static bool NinjaSkin
        {
            get =>
                JObject.Parse(File.ReadAllText(Paths.Settings)).SelectToken(typeof(Settings).Name).Value<bool>(MethodBase.GetCurrentMethod().Name.Replace("get_", string.Empty));

            set
            {
                JObject settingsJObject = JObject.Parse(File.ReadAllText(Paths.Settings));
                JToken defaultJToken = settingsJObject.SelectToken(typeof(Settings).Name);

                defaultJToken[MethodBase.GetCurrentMethod().Name.Replace("set_", string.Empty)] = value;

                File.WriteAllText(Paths.Settings, JsonConvert.SerializeObject(settingsJObject, Formatting.Indented));
            }
        }

        internal static List<string> ProtectedItems
        {
            get =>
                JObject.Parse(File.ReadAllText(Paths.Settings)).SelectToken(typeof(Settings).Name).SelectToken(MethodBase.GetCurrentMethod().Name.Replace("get_", string.Empty))
                .ToObject<List<string>>();

            set
            {
                JObject settingsJObject = JObject.Parse(File.ReadAllText(Paths.Settings));
                JToken defaultJToken = settingsJObject.SelectToken(typeof(Settings).Name);

                defaultJToken[MethodBase.GetCurrentMethod().Name.Replace("set_", string.Empty)] = JToken.FromObject(value);

                File.WriteAllText(Paths.Settings, JsonConvert.SerializeObject(settingsJObject, Formatting.Indented));
            }
        }

        internal static int PulseRate
        {
            get =>
                JObject.Parse(File.ReadAllText(Paths.Settings)).SelectToken(typeof(Settings).Name).Value<int>(MethodBase.GetCurrentMethod().Name.Replace("get_", string.Empty));

            set
            {
                JObject settingsJObject = JObject.Parse(File.ReadAllText(Paths.Settings));
                JToken defaultJToken = settingsJObject.SelectToken(typeof(Settings).Name);

                defaultJToken[MethodBase.GetCurrentMethod().Name.Replace("set_", string.Empty)] = value;

                File.WriteAllText(Paths.Settings, JsonConvert.SerializeObject(settingsJObject, Formatting.Indented));
            }
        }

        internal static bool Vendor
        {
            get =>
                JObject.Parse(File.ReadAllText(Paths.Settings)).SelectToken(typeof(Settings).Name).Value<bool>(MethodBase.GetCurrentMethod().Name.Replace("get_", string.Empty));

            set
            {
                JObject settingsJObject = JObject.Parse(File.ReadAllText(Paths.Settings));
                JToken defaultJToken = settingsJObject.SelectToken(typeof(Settings).Name);

                defaultJToken[MethodBase.GetCurrentMethod().Name.Replace("set_", string.Empty)] = value;

                File.WriteAllText(Paths.Settings, JsonConvert.SerializeObject(settingsJObject, Formatting.Indented));
            }
        }
    }
}
