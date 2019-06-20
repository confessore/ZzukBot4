using System.Linq;
using ZzukBot.Core.Framework;
using ZzukBot.Core.Game.Objects;
using ZzukBot.Core.Game.Statics;

namespace Grinder.Engine.Modules
{
    public class CombatModule
    {
        CustomClasses CustomClasses { get; }
        ObjectManager ObjectManager { get; }
        PathModule PathModule { get; }

        public CombatModule(
            CustomClasses customClasses,
            ObjectManager objectManager,
            PathModule pathModule)
        {
            CustomClasses = customClasses;
            ObjectManager = objectManager;
            PathModule = pathModule;
        }

        public void Fight()
        {
            if (ObjectManager.Units.Count() > 0)
                CustomClasses.Current.Fight(ObjectManager.Units.Where(x => x.IsInCombat || x.GotDebuff("Polymorph")));
        }

        public bool IsBuffRequired()
        {
            return CustomClasses.Current.IsBuffRequired();
        }

        public bool IsReadyToFight()
        {
            return CustomClasses.Current.IsReadyToFight(ObjectManager.Units);
        }

        public void PrepareForFight()
        {
            CustomClasses.Current.PrepareForFight();
        }

        public void Pull(WoWUnit target)
        {
            if (target != null)
                CustomClasses.Current.Pull(target);
        }

        public void Rebuff()
        {
            CustomClasses.Current.Rebuff();
        }
    }
}
