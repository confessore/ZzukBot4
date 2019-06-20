using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Reflection;

namespace ZzukBot.Server.Settings
{
    internal static class Default
    {
        internal static string CertificateName
        {
            get =>
                JObject.Parse(File.ReadAllText(Paths.Settings)).SelectToken(typeof(Default).Name).Value<string>(MethodBase.GetCurrentMethod().Name.Replace("get_", string.Empty));

            set
            {
                JObject settingsJObject = JObject.Parse(File.ReadAllText(Paths.Settings));
                JToken defaultJToken = settingsJObject.SelectToken(typeof(Default).Name);

                defaultJToken[MethodBase.GetCurrentMethod().Name.Replace("set_", string.Empty)] = value;

                File.WriteAllText(Paths.Settings, JsonConvert.SerializeObject(settingsJObject, Formatting.Indented));
            }
        }

        internal static string CertificatePassword
        {
            get =>
                JObject.Parse(File.ReadAllText(Paths.Settings)).SelectToken(typeof(Default).Name).Value<string>(MethodBase.GetCurrentMethod().Name.Replace("get_", string.Empty));

            set
            {
                JObject settingsJObject = JObject.Parse(File.ReadAllText(Paths.Settings));
                JToken defaultJToken = settingsJObject.SelectToken(typeof(Default).Name);

                defaultJToken[MethodBase.GetCurrentMethod().Name.Replace("set_", string.Empty)] = value;

                File.WriteAllText(Paths.Settings, JsonConvert.SerializeObject(settingsJObject, Formatting.Indented));
            }
        }

        internal static string EndOfFile
        {
            get =>
                JObject.Parse(File.ReadAllText(Paths.Settings)).SelectToken(typeof(Default).Name).Value<string>(MethodBase.GetCurrentMethod().Name.Replace("get_", string.Empty));

            set
            {
                JObject settingsJObject = JObject.Parse(File.ReadAllText(Paths.Settings));
                JToken defaultJToken = settingsJObject.SelectToken(typeof(Default).Name);

                defaultJToken[MethodBase.GetCurrentMethod().Name.Replace("set_", string.Empty)] = value;

                File.WriteAllText(Paths.Settings, JsonConvert.SerializeObject(settingsJObject, Formatting.Indented));
            }
        }

        internal static int MaxClients
        {
            get =>
                JObject.Parse(File.ReadAllText(Paths.Settings)).SelectToken(typeof(Default).Name).Value<int>(MethodBase.GetCurrentMethod().Name.Replace("get_", string.Empty));

            set
            {
                JObject settingsJObject = JObject.Parse(File.ReadAllText(Paths.Settings));
                JToken defaultJToken = settingsJObject.SelectToken(typeof(Default).Name);

                defaultJToken[MethodBase.GetCurrentMethod().Name.Replace("set_", string.Empty)] = value;

                File.WriteAllText(Paths.Settings, JsonConvert.SerializeObject(settingsJObject, Formatting.Indented));
            }
        }

        internal static int MaxWaitingSockets
        {
            get =>
                JObject.Parse(File.ReadAllText(Paths.Settings)).SelectToken(typeof(Default).Name).Value<int>(MethodBase.GetCurrentMethod().Name.Replace("get_", string.Empty));

            set
            {
                JObject settingsJObject = JObject.Parse(File.ReadAllText(Paths.Settings));
                JToken defaultJToken = settingsJObject.SelectToken(typeof(Default).Name);

                defaultJToken[MethodBase.GetCurrentMethod().Name.Replace("set_", string.Empty)] = value;

                File.WriteAllText(Paths.Settings, JsonConvert.SerializeObject(settingsJObject, Formatting.Indented));
            }
        }

        internal static int ServerPort
        {
            get =>
                JObject.Parse(File.ReadAllText(Paths.Settings)).SelectToken(typeof(Default).Name).Value<int>(MethodBase.GetCurrentMethod().Name.Replace("get_", string.Empty));

            set
            {
                JObject settingsJObject = JObject.Parse(File.ReadAllText(Paths.Settings));
                JToken defaultJToken = settingsJObject.SelectToken(typeof(Default).Name);

                defaultJToken[MethodBase.GetCurrentMethod().Name.Replace("set_", string.Empty)] = value;

                File.WriteAllText(Paths.Settings, JsonConvert.SerializeObject(settingsJObject, Formatting.Indented));
            }
        }

        internal static int Timeout
        {
            get =>
                JObject.Parse(File.ReadAllText(Paths.Settings)).SelectToken(typeof(Default).Name).Value<int>(MethodBase.GetCurrentMethod().Name.Replace("get_", string.Empty));

            set
            {
                JObject settingsJObject = JObject.Parse(File.ReadAllText(Paths.Settings));
                JToken defaultJToken = settingsJObject.SelectToken(typeof(Default).Name);

                defaultJToken[MethodBase.GetCurrentMethod().Name.Replace("set_", string.Empty)] = value;

                File.WriteAllText(Paths.Settings, JsonConvert.SerializeObject(settingsJObject, Formatting.Indented));
            }
        }

        internal static string Version
        {
            get =>
                JObject.Parse(File.ReadAllText(Paths.Settings)).SelectToken(typeof(Default).Name).Value<string>(MethodBase.GetCurrentMethod().Name.Replace("get_", string.Empty));

            set
            {
                JObject settingsJObject = JObject.Parse(File.ReadAllText(Paths.Settings));
                JToken defaultJToken = settingsJObject.SelectToken(typeof(Default).Name);

                defaultJToken[MethodBase.GetCurrentMethod().Name.Replace("set_", string.Empty)] = value;

                File.WriteAllText(Paths.Settings, JsonConvert.SerializeObject(settingsJObject, Formatting.Indented));
            }
        }
    }
}
