using System;
using System.Collections.Generic;
using System.Linq;
using ZzukBot.Core.Constants;
using ZzukBot.Core.Mem;

namespace ZzukBot.Core.Game.Statics
{
    /// <summary>
    ///     Class regarding Lua stuff
    /// </summary>
    public class Lua
    {
        private static readonly Lazy<Lua> _instance = new Lazy<Lua>(() => new Lua());

        private static readonly Random Random = new Random();

        private Lua()
        {
        }

        /// <summary>
        ///     Access to the current instance
        /// </summary>
        /// <value>
        ///     The instance.
        /// </value>
        public static Lua Instance => _instance.Value;

        /// <summary>
        ///     Executes Lua code
        /// </summary>
        /// <example>
        ///     <code>Execute("DoEmote('dance')");</code>
        /// </example>
        /// <param name="parScript">The code</param>
        public void Execute(string parScript)
        {
            Functions.DoString(parScript);
        }

        /// <summary>
        ///     Will execute a Lua script and return values assigned to all variables created by placeholders:
        ///     {0} = 'hello', {1} = 'world' => Result will be { "hello", "world" }
        /// </summary>
        /// <param name="parScript">The Lua script</param>
        /// <returns>The string return values</returns>
        public string[] ExecuteWithResult(string parScript)
        {
            var luaVarNames = new List<string>();
            for (var i = 0; i < 6; i++)
            {
                var currentPlaceHolder = "{" + i + "}";
                if (!parScript.Contains(currentPlaceHolder)) break;
                var randomName = GetRandomLuaVarName();
                parScript = parScript.Replace(currentPlaceHolder, randomName);
                luaVarNames.Add(randomName);
            }
            return MainThread.Instance.Invoke(() =>
            {
                Functions.DoString(parScript);
                return Functions.GetText(luaVarNames.ToArray());
            });
        }

        /// <summary>
        /// Gets text from the string returned from an Lua execution
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string GetText(string text)
        {
            return Functions.GetText(text);
        }

        private static string GetRandomLuaVarName()
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz";
            return new string(chars.Select(c => chars[Random.Next(chars.Length)]).Take(8).ToArray());
        }

        /// <summary>
        ///     Accepts a group invitation
        /// </summary>
        public void AcceptGroup()
        {
            Execute("AcceptGroup();");
        }

        /// <summary>
        /// The player's current attack power
        /// </summary>
        public int PlayerAttackPower
        {
            get
            {
                Execute(string.Format("base, posBuff, negBuff = UnitAttackPower('player');"));
                return Convert.ToInt32(GetText("base")) + Convert.ToInt32(GetText("posBuff")) + Convert.ToInt32(GetText("negBuff"));
            }
        }

        /// <summary>
        /// The family that the player's pet belongs to
        /// </summary>
        public string PetFamily
        {
            get
            {
                Execute(string.Format("petIcon, petName, petLevel, petType, petLoyalty = GetStablePetInfo(0);"));
                return Convert.ToString(GetText("petType"));
            }
        }

        /// <summary>
        ///     Sends a message to the 'say' channel
        /// </summary>
        /// <param name="message"></param>
        public void SendSay(string message)
        {
            Execute($"SendChatMessage(\"{message}\", \"say\", nil, nil);");
        }

        /// <summary>
        ///     Sends a message to the 'yell' channel
        /// </summary>
        /// <param name="message"></param>
        public void SendYell(string message)
        {
            Execute($"SendChatMessage(\"{message}\", \"yell\", nil, nil);");
        }

        /// <summary>
        ///     Sends a message to the specified player
        /// </summary>
        /// <param name="message"></param>
        /// <param name="playerName"></param>
        public void SendWhisper(string message, string playerName)
        {
            Execute($"SendChatMessage(\"{message}\", \"whisper\", nil, \"{playerName}\");");
        }

        /*/// <summary>
        ///     Gets the cost of the specified spell using the spell's id
        /// </summary>
        /// <param name="spell"></param>
        /// <returns></returns>
        public int GetSpellCost(int spell)
        {
            Execute(string.Format($"name, rank, icon, cost, isFunnel, powerType, castTime, minRange, maxRange = GetSpellInfo('Pounce');", spell));
            return Convert.ToInt32(GetText("cost"));
        }

        /// <summary>
        ///     Gets the cost of the specified spell using the spell's name
        /// </summary>
        /// <param name="spell"></param>
        /// <returns></returns>
        public int GetSpellCost(string spell)
        {
            Execute(string.Format($"name, rank, icon, cost, isFunnel, powerType, castTime, minRange, maxRange = GetSpellInfo({0});", spell));
            return Convert.ToInt32(GetText("cost"));
        }*/

        /// <summary>
        ///     The list of the talents available to the player's class
        /// </summary>
        /// <returns></returns>
        public IList<Talent> GetTalents()
        {
            var talents = new List<Talent>();
            Execute("TM_numberOfTabs = GetNumTalentTabs()");
            int tabCount = Convert.ToInt32(GetText("TM_numberOfTabs"));
            for (int i = 1; i <= tabCount; i++)
            {
                Execute(string.Format("TM_numberOfTalents = GetNumTalents({0})", i));
                int talentCount = Convert.ToInt32(GetText("TM_numberOfTalents"));
                for (int j = 1; j <= talentCount; j++)
                {
                    Execute(
                        string.Format(
                            "TM_nameTalent, TM_icon, TM_tier, TM_column, TM_currRank, TM_maxRank = GetTalentInfo({0},{1});",
                            i, j));
                    var talent = new Talent(GetText("TM_nameTalent"),
                        Convert.ToInt32(GetText("TM_currRank")),
                        Convert.ToInt32(GetText("TM_maxRank")), i, j);
                    talents.Add(talent);
                }
            }
            return talents;
        }
        
        /// <summary>
        /// The model for a talent
        /// </summary>
        public class Talent
        {
            /// <summary>
            /// The model for a talent
            /// </summary>
            /// <param name="name"></param>
            /// <param name="currentRank"></param>
            /// <param name="maxRank"></param>
            /// <param name="tab"></param>
            /// <param name="index"></param>
            public Talent(string name, int currentRank, int maxRank, int tab, int index)
            {
                Name = name;
                CurrentRank = currentRank;
                MaxRank = maxRank;
                Tab = tab;
                Index = index;
            }

            /// <summary>
            /// The name of the talent
            /// </summary>
            public string Name { get; private set; }
            /// <summary>
            /// The current rank of the talent
            /// </summary>
            public int CurrentRank { get; private set; }
            /// <summary>
            /// The maximum rank of the talent
            /// </summary>
            public int MaxRank { get; private set; }
            /// <summary>
            /// The tab that the talent can be found on
            /// </summary>
            public int Tab { get; private set; }
            /// <summary>
            /// The position of the talent on the tab (left to right, top to bottom)
            /// </summary>
            public int Index { get; private set; }
        }
    }
}