using Hunter.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using ZzukBot.Core.Constants;
using ZzukBot.Core.Framework.Classes;
using ZzukBot.Core.Game.Objects;
using ZzukBot.Core.Game.Statics;
using ZzukBot.Core.Utilities.DependencyInjection;

namespace Hunter
{
    [Export(typeof(CustomClass))]
    public class Hunter : CustomClass
    {
        private DependencyMap dm = new DependencyMap();

        public Hunter()
        {
            Author = "krycess";
            Name = "Hunter";
            Version = new Version("0.0.1.0");

            dm.Add(this);
            dm.Add(Common.Instance);
            dm.Add(Inventory.Instance);
            dm.Add(Lua.Instance);
            dm.Add(Navigation.Instance);
            dm.Add(ObjectManager.Instance);
            dm.Add(Spell.Instance);
            dm.Add(new HunterManager(
                dm.Get<Common>(),
                dm.Get<Inventory>(),
                dm.Get<Lua>(),
                dm.Get<Navigation>(),
                dm.Get<ObjectManager>(),
                dm.Get<Spell>()));
        }

        public override Enums.ClassId Class => Enums.ClassId.Hunter;

        public override Enums.CombatPosition GetCombatPosition()
            => dm.Get<HunterManager>().GetCombatPosition;

        public override bool CanBuffAnotherPlayer()
            => dm.Get<HunterManager>().CanBuffAnotherPlayer();

        public override bool CanWin(IEnumerable<WoWUnit> possibleTargets)
            => dm.Get<HunterManager>().CanWin(possibleTargets);

        public override void Fight(IEnumerable<WoWUnit> possibleTargets)
            => dm.Get<HunterManager>().Fight(possibleTargets);

        public override float GetKiteDistance()
            => dm.Get<HunterManager>().GetKiteDistance();

        public override int GetMaxPullCount()
            => dm.Get<HunterManager>().GetMaxPullCount();

        public override float GetPullDistance()
            => dm.Get<HunterManager>().GetPullDistance();

        public override bool IsBuffRequired()
            => dm.Get<HunterManager>().IsBuffRequired();

        public override bool IsReadyToFight(IEnumerable<WoWUnit> possibleTargets)
            => dm.Get<HunterManager>().IsReadyToFight(possibleTargets);

        public override void OnFightEnded()
            => SuppressBotMovement = false;

        public override void PrepareForFight()
            => dm.Get<HunterManager>().PrepareForFight();

        public override void Pull(WoWUnit target)
            => dm.Get<HunterManager>().Pull(target);

        public override void Rebuff()
            => dm.Get<HunterManager>().Rebuff();

        public override void TryToBuffAnotherPlayer(WoWUnit player)
            => dm.Get<HunterManager>().TryToBuffAnotherPlayer(player);
    }
}
