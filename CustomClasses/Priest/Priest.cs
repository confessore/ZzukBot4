using Priest.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using ZzukBot.Core.Constants;
using ZzukBot.Core.Framework.Classes;
using ZzukBot.Core.Game.Objects;
using ZzukBot.Core.Game.Statics;
using ZzukBot.Core.Utilities.DependencyInjection;

namespace Priest
{
    [Export(typeof(CustomClass))]
    public class Priest : CustomClass
    {
        private DependencyMap dm = new DependencyMap();

        public Priest()
        {
            Author = "krycess";
            Name = "Priest";
            Version = new Version("0.0.1.0");

            dm.Add(this);
            dm.Add(Common.Instance);
            dm.Add(Inventory.Instance);
            dm.Add(Lua.Instance);
            dm.Add(Navigation.Instance);
            dm.Add(ObjectManager.Instance);
            dm.Add(Spell.Instance);
            dm.Add(new PriestManager(
                dm.Get<Common>(),
                dm.Get<Inventory>(),
                dm.Get<Lua>(),
                dm.Get<Navigation>(),
                dm.Get<ObjectManager>(),
                dm.Get<Spell>()));
        }

        public override Enums.ClassId Class => Enums.ClassId.Priest;

        public override Enums.CombatPosition GetCombatPosition()
            => dm.Get<PriestManager>().GetCombatPosition;

        public override bool CanBuffAnotherPlayer()
            => dm.Get<PriestManager>().CanBuffAnotherPlayer();

        public override bool CanWin(IEnumerable<WoWUnit> possibleTargets)
            => dm.Get<PriestManager>().CanWin(possibleTargets);

        public override void Fight(IEnumerable<WoWUnit> possibleTargets)
            => dm.Get<PriestManager>().Fight(possibleTargets);

        public override float GetKiteDistance()
            => dm.Get<PriestManager>().GetKiteDistance();

        public override int GetMaxPullCount()
            => dm.Get<PriestManager>().GetMaxPullCount();

        public override float GetPullDistance()
            => dm.Get<PriestManager>().GetPullDistance();

        public override bool IsBuffRequired()
            => dm.Get<PriestManager>().IsBuffRequired();

        public override bool IsReadyToFight(IEnumerable<WoWUnit> possibleTargets)
            => dm.Get<PriestManager>().IsReadyToFight(possibleTargets);

        public override void OnFightEnded()
            => SuppressBotMovement = false;

        public override void PrepareForFight()
            => dm.Get<PriestManager>().PrepareForFight();

        public override void Pull(WoWUnit target)
            => dm.Get<PriestManager>().Pull(target);

        public override void Rebuff()
            => dm.Get<PriestManager>().Rebuff();

        public override void TryToBuffAnotherPlayer(WoWUnit player)
            => dm.Get<PriestManager>().TryToBuffAnotherPlayer(player);
    }
}
