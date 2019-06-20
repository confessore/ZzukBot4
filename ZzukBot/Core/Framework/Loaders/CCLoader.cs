using System;
using ZzukBot.Core.Constants;
using ZzukBot.Core.Framework.Classes;

namespace ZzukBot.Core.Framework.Loaders
{
    /// <summary>
    ///     Loader for v4 custom classes
    /// </summary>
    public sealed class CCLoader
    {
        static readonly Lazy<CCLoader> instance = new Lazy<CCLoader>(() => new CCLoader());

        /// <summary>
        ///     Access to the profile loader
        /// </summary>
        public static CCLoader Instance => instance.Value;

        /// <summary>
        ///     Loads a custom class
        /// </summary>
        /// <param name="playerClass"></param>
        /// <returns></returns>
        public bool LoadCustomClass(Enums.ClassId playerClass)
        {
            bool loaded = false;
            int index = 0;

            CustomClasses.Instance.Refresh();
            foreach (CustomClass CC in CustomClasses.Instance.Enumerator)
            {
                if (CC.Class == playerClass)
                {
                    CustomClasses.Instance.SetCurrent(index);
                    loaded = true;
                    break;
                }
                ++index;
            }

            return loaded;
        }
    }
}
