using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Warlock.Engine;
using ZzukBot.Core.Constants;
using ZzukBot.Core.Framework.Classes;
using ZzukBot.Core.Game.Objects;
using ZzukBot.Core.Game.Statics;
using ZzukBot.Core.Utilities.DependencyInjection;

namespace Warlock
{
    [Export(typeof(CustomClass))]
    public class Warlock : CustomClass
    {
        private DependencyMap dm = new DependencyMap();

        public Warlock()
        {
            Author = "krycess";
            Name = "Warlock";
            Version = new Version("0.0.1.0");

            dm.Add(this);
            dm.Add(Common.Instance);
            dm.Add(Inventory.Instance);
            dm.Add(Lua.Instance);
            dm.Add(Navigation.Instance);
            dm.Add(ObjectManager.Instance);
            dm.Add(Spell.Instance);
            dm.Add(new WarlockManager(
                dm.Get<Common>(),
                dm.Get<Inventory>(),
                dm.Get<Lua>(),
                dm.Get<Navigation>(),
                dm.Get<ObjectManager>(),
                dm.Get<Spell>()));
        }

        public override Enums.ClassId Class => Enums.ClassId.Warlock;

        public override Enums.CombatPosition GetCombatPosition()
            => dm.Get<WarlockManager>().GetCombatPosition;

        public override bool CanBuffAnotherPlayer()
            => dm.Get<WarlockManager>().CanBuffAnotherPlayer();

        public override bool CanWin(IEnumerable<WoWUnit> possibleTargets)
            => dm.Get<WarlockManager>().CanWin(possibleTargets);

        public override void Fight(IEnumerable<WoWUnit> possibleTargets)
            => dm.Get<WarlockManager>().Fight(possibleTargets);

        public override float GetKiteDistance()
            => dm.Get<WarlockManager>().GetKiteDistance();

        public override int GetMaxPullCount()
            => dm.Get<WarlockManager>().GetMaxPullCount();

        public override float GetPullDistance()
            => dm.Get<WarlockManager>().GetPullDistance();

        public override bool IsBuffRequired()
            => dm.Get<WarlockManager>().IsBuffRequired();

        public override bool IsReadyToFight(IEnumerable<WoWUnit> possibleTargets)
            => dm.Get<WarlockManager>().IsReadyToFight(possibleTargets);

        public override void OnFightEnded()
            => SuppressBotMovement = false;

        public override void PrepareForFight()
            => dm.Get<WarlockManager>().PrepareForFight();

        public override void Pull(WoWUnit target)
            => dm.Get<WarlockManager>().Pull(target);

        public override void Rebuff()
            => dm.Get<WarlockManager>().Rebuff();

        public override void TryToBuffAnotherPlayer(WoWUnit player)
            => dm.Get<WarlockManager>().TryToBuffAnotherPlayer(player);
    }
}
