using Druid.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using ZzukBot.Core.Constants;
using ZzukBot.Core.Framework.Classes;
using ZzukBot.Core.Game.Objects;
using ZzukBot.Core.Game.Statics;
using ZzukBot.Core.Utilities.DependencyInjection;

namespace Druid
{
    [Export(typeof(CustomClass))]
    public class Druid : CustomClass
    {
        private DependencyMap dm = new DependencyMap();

        public Druid()
        {
            Author = "krycess";
            Name = "Druid";
            Version = new Version("0.0.1.0");

            dm.Add(this);
            dm.Add(Common.Instance);
            dm.Add(Inventory.Instance);
            dm.Add(Lua.Instance);
            dm.Add(Navigation.Instance);
            dm.Add(ObjectManager.Instance);
            dm.Add(Spell.Instance);
            dm.Add(new DruidManager(
                dm.Get<Common>(),
                dm.Get<Inventory>(),
                dm.Get<Lua>(),
                dm.Get<Navigation>(),
                dm.Get<ObjectManager>(),
                dm.Get<Spell>()));
        }

        public override Enums.ClassId Class => Enums.ClassId.Druid;

        public override Enums.CombatPosition GetCombatPosition()
            => dm.Get<DruidManager>().GetCombatPosition;

        public override bool CanBuffAnotherPlayer()
            => dm.Get<DruidManager>().CanBuffAnotherPlayer();

        public override bool CanWin(IEnumerable<WoWUnit> possibleTargets)
            => dm.Get<DruidManager>().CanWin(possibleTargets);

        public override void Fight(IEnumerable<WoWUnit> possibleTargets)
            => dm.Get<DruidManager>().Fight(possibleTargets);

        public override float GetKiteDistance()
            => dm.Get<DruidManager>().GetKiteDistance();

        public override int GetMaxPullCount()
            => dm.Get<DruidManager>().GetMaxPullCount();

        public override float GetPullDistance()
            => dm.Get<DruidManager>().GetPullDistance();

        public override bool IsBuffRequired()
            => dm.Get<DruidManager>().IsBuffRequired();

        public override bool IsReadyToFight(IEnumerable<WoWUnit> possibleTargets)
            => dm.Get<DruidManager>().IsReadyToFight(possibleTargets);

        public override void OnFightEnded()
            => SuppressBotMovement = false;

        public override void PrepareForFight()
            => dm.Get<DruidManager>().PrepareForFight();

        public override void Pull(WoWUnit target)
            => dm.Get<DruidManager>().Pull(target);

        public override void Rebuff()
            => dm.Get<DruidManager>().Rebuff();

        public override void TryToBuffAnotherPlayer(WoWUnit player)
            => dm.Get<DruidManager>().TryToBuffAnotherPlayer(player);
    }
}
