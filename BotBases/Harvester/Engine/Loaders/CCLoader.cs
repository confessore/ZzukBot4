using ZzukBot.Core.Constants;
using ZzukBot.Core.Framework;
using ZzukBot.Core.Framework.Classes;

namespace Harvester.Engine.Loaders
{
    public class CCLoader
    {
        private CustomClasses CustomClasses { get; }

        public CCLoader(CustomClasses customClasses)
        {
            CustomClasses = customClasses;
        }

        public bool LoadCustomClass(Enums.ClassId playerClass)
        {
            bool loaded = false;
            int index = 0;

            CustomClasses.Refresh();
            foreach (CustomClass CC in CustomClasses.Enumerator)
            {
                if (CC.Class == playerClass)
                {
                    CustomClasses.SetCurrent(index);
                    loaded = true;
                    break;
                }
                ++index;
            }

            return loaded;
        }
    }
}
