using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace ZzukBot.Settings
{
    internal static class Default
    {
        internal static bool Activatecleverbot
        {
            get =>
                JObject.Parse(File.ReadAllText(Paths.Settings)).SelectToken(typeof(Default).Name).Value<bool>(MethodBase.GetCurrentMethod().Name.Replace("get_", string.Empty));

            set
            {
                JObject settingsJObject = JObject.Parse(File.ReadAllText(Paths.Settings));
                JToken defaultJToken = settingsJObject.SelectToken(typeof(Default).Name);

                defaultJToken[MethodBase.GetCurrentMethod().Name.Replace("set_", string.Empty)] = value;

                File.WriteAllText(Paths.Settings, JsonConvert.SerializeObject(settingsJObject, Formatting.Indented));
            }
        }

        internal static bool BeepName
        {
            get =>
                JObject.Parse(File.ReadAllText(Paths.Settings)).SelectToken(typeof(Default).Name).Value<bool>(MethodBase.GetCurrentMethod().Name.Replace("get_", string.Empty));

            set
            {
                JObject settingsJObject = JObject.Parse(File.ReadAllText(Paths.Settings));
                JToken defaultJToken = settingsJObject.SelectToken(typeof(Default).Name);

                defaultJToken[MethodBase.GetCurrentMethod().Name.Replace("set_", string.Empty)] = value;

                File.WriteAllText(Paths.Settings, JsonConvert.SerializeObject(settingsJObject, Formatting.Indented));
            }
        }

        internal static bool BeepSay
        {
            get =>
                JObject.Parse(File.ReadAllText(Paths.Settings)).SelectToken(typeof(Default).Name).Value<bool>(MethodBase.GetCurrentMethod().Name.Replace("get_", string.Empty));

            set
            {
                JObject settingsJObject = JObject.Parse(File.ReadAllText(Paths.Settings));
                JToken defaultJToken = settingsJObject.SelectToken(typeof(Default).Name);

                defaultJToken[MethodBase.GetCurrentMethod().Name.Replace("set_", string.Empty)] = value;

                File.WriteAllText(Paths.Settings, JsonConvert.SerializeObject(settingsJObject, Formatting.Indented));
            }
        }

        internal static bool BeepWhisper
        {
            get =>
                JObject.Parse(File.ReadAllText(Paths.Settings)).SelectToken(typeof(Default).Name).Value<bool>(MethodBase.GetCurrentMethod().Name.Replace("get_", string.Empty));

            set
            {
                JObject settingsJObject = JObject.Parse(File.ReadAllText(Paths.Settings));
                JToken defaultJToken = settingsJObject.SelectToken(typeof(Default).Name);

                defaultJToken[MethodBase.GetCurrentMethod().Name.Replace("set_", string.Empty)] = value;

                File.WriteAllText(Paths.Settings, JsonConvert.SerializeObject(settingsJObject, Formatting.Indented));
            }
        }

        internal static bool BeepYell
        {
            get =>
                JObject.Parse(File.ReadAllText(Paths.Settings)).SelectToken(typeof(Default).Name).Value<bool>(MethodBase.GetCurrentMethod().Name.Replace("get_", string.Empty));

            set
            {
                JObject settingsJObject = JObject.Parse(File.ReadAllText(Paths.Settings));
                JToken defaultJToken = settingsJObject.SelectToken(typeof(Default).Name);

                defaultJToken[MethodBase.GetCurrentMethod().Name.Replace("set_", string.Empty)] = value;

                File.WriteAllText(Paths.Settings, JsonConvert.SerializeObject(settingsJObject, Formatting.Indented));
            }
        }

        internal static string BotAccount
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

        internal static string BotPassword
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

        internal static string GameAccount
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

        internal static string GamePassword
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

        internal static HashSet<string> LoadedPlugins
        {
            get =>
                JObject.Parse(File.ReadAllText(Paths.Settings)).SelectToken(typeof(Default).Name).SelectToken(MethodBase.GetCurrentMethod().Name.Replace("get_", string.Empty))
                .ToObject<HashSet<string>>();

            set
            {
                JObject settingsJObject = JObject.Parse(File.ReadAllText(Paths.Settings));
                JToken defaultJToken = settingsJObject.SelectToken(typeof(Default).Name);

                defaultJToken[MethodBase.GetCurrentMethod().Name.Replace("set_", string.Empty)] = JToken.FromObject(value);

                File.WriteAllText(Paths.Settings, JsonConvert.SerializeObject(settingsJObject, Formatting.Indented));
            }
        }

        internal static bool LogChat
        {
            get =>
                JObject.Parse(File.ReadAllText(Paths.Settings)).SelectToken(typeof(Default).Name).Value<bool>(MethodBase.GetCurrentMethod().Name.Replace("get_", string.Empty));

            set
            {
                JObject settingsJObject = JObject.Parse(File.ReadAllText(Paths.Settings));
                JToken defaultJToken = settingsJObject.SelectToken(typeof(Default).Name);

                defaultJToken[MethodBase.GetCurrentMethod().Name.Replace("set_", string.Empty)] = value;

                File.WriteAllText(Paths.Settings, JsonConvert.SerializeObject(settingsJObject, Formatting.Indented));
            }
        }

        internal static bool LogInvites
        {
            get =>
                JObject.Parse(File.ReadAllText(Paths.Settings)).SelectToken(typeof(Default).Name).Value<bool>(MethodBase.GetCurrentMethod().Name.Replace("get_", string.Empty));

            set
            {
                JObject settingsJObject = JObject.Parse(File.ReadAllText(Paths.Settings));
                JToken defaultJToken = settingsJObject.SelectToken(typeof(Default).Name);

                defaultJToken[MethodBase.GetCurrentMethod().Name.Replace("set_", string.Empty)] = value;

                File.WriteAllText(Paths.Settings, JsonConvert.SerializeObject(settingsJObject, Formatting.Indented));
            }
        }

        internal static string PathToWoW
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

        internal static string ProcessName
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

        internal static string Realmlist
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

        internal static bool RememberPassword
        {
            get =>
                JObject.Parse(File.ReadAllText(Paths.Settings)).SelectToken(typeof(Default).Name).Value<bool>(MethodBase.GetCurrentMethod().Name.Replace("get_", string.Empty));

            set
            {
                JObject settingsJObject = JObject.Parse(File.ReadAllText(Paths.Settings));
                JToken defaultJToken = settingsJObject.SelectToken(typeof(Default).Name);
            
                defaultJToken[MethodBase.GetCurrentMethod().Name.Replace("set_", string.Empty)] = value;

                File.WriteAllText(Paths.Settings, JsonConvert.SerializeObject(settingsJObject, Formatting.Indented));
            }
        }

        internal static string ServerAddress
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
    }
}
