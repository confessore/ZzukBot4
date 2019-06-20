using Mage.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using ZzukBot.Core.Constants;
using ZzukBot.Core.Framework.Classes;
using ZzukBot.Core.Game.Objects;
using ZzukBot.Core.Game.Statics;
using ZzukBot.Core.Utilities.DependencyInjection;

namespace Mage
{
    [Export(typeof(CustomClass))]
    public class Mage : CustomClass
    {
        private DependencyMap dm = new DependencyMap();

        public Mage()
        {
            Author = "krycess";
            Name = "Mage";
            Version = new Version("0.0.1.0");

            dm.Add(this);
            dm.Add(Common.Instance);
            dm.Add(Inventory.Instance);
            dm.Add(Lua.Instance);
            dm.Add(Navigation.Instance);
            dm.Add(ObjectManager.Instance);
            dm.Add(Spell.Instance);
            dm.Add(new MageManager(
                dm.Get<Common>(),
                dm.Get<Inventory>(),
                dm.Get<Lua>(),
                dm.Get<Navigation>(),
                dm.Get<ObjectManager>(),
                dm.Get<Spell>()));
        }

        public override Enums.ClassId Class => Enums.ClassId.Mage;

        public override Enums.CombatPosition GetCombatPosition()
            => dm.Get<MageManager>().GetCombatPosition;

        public override bool CanBuffAnotherPlayer()
            => dm.Get<MageManager>().CanBuffAnotherPlayer();

        public override bool CanWin(IEnumerable<WoWUnit> possibleTargets)
            => dm.Get<MageManager>().CanWin(possibleTargets);

        public override void Fight(IEnumerable<WoWUnit> possibleTargets)
            => dm.Get<MageManager>().Fight(possibleTargets);

        public override float GetKiteDistance()
            => dm.Get<MageManager>().GetKiteDistance();

        public override int GetMaxPullCount()
            => dm.Get<MageManager>().GetMaxPullCount();

        public override float GetPullDistance()
            => dm.Get<MageManager>().GetPullDistance();

        public override bool IsBuffRequired()
            => dm.Get<MageManager>().IsBuffRequired();

        public override bool IsReadyToFight(IEnumerable<WoWUnit> possibleTargets)
            => dm.Get<MageManager>().IsReadyToFight(possibleTargets);

        public override void OnFightEnded()
            => SuppressBotMovement = false;

        public override void PrepareForFight()
            => dm.Get<MageManager>().PrepareForFight();

        public override void Pull(WoWUnit target)
            => dm.Get<MageManager>().Pull(target);

        public override void Rebuff()
            => dm.Get<MageManager>().Rebuff();

        public override void TryToBuffAnotherPlayer(WoWUnit player)
            => dm.Get<MageManager>().TryToBuffAnotherPlayer(player);
    }
}
