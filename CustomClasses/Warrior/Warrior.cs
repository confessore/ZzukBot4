using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Warrior.Engine;
using ZzukBot.Core.Constants;
using ZzukBot.Core.Framework.Classes;
using ZzukBot.Core.Game.Objects;
using ZzukBot.Core.Game.Statics;
using ZzukBot.Core.Utilities.DependencyInjection;

namespace Warrior
{
    [Export(typeof(CustomClass))]
    public class Warrior : CustomClass
    {
        private DependencyMap dm = new DependencyMap();

        public Warrior()
        {
            Author = "krycess";
            Name = "Warrior";
            Version = new Version("0.0.1.0");

            dm.Add(this);
            dm.Add(Common.Instance);
            dm.Add(Lua.Instance);
            dm.Add(Navigation.Instance);
            dm.Add(ObjectManager.Instance);
            dm.Add(Spell.Instance);
            dm.Add(new WarriorManager(
                dm.Get<Common>(),
                dm.Get<Lua>(),
                dm.Get<Navigation>(),
                dm.Get<ObjectManager>(),
                dm.Get<Spell>()));
        }

        public override Enums.ClassId Class => Enums.ClassId.Warrior;

        public override Enums.CombatPosition GetCombatPosition()
            => dm.Get<WarriorManager>().GetCombatPosition;

        public override bool CanBuffAnotherPlayer()
            => dm.Get<WarriorManager>().CanBuffAnotherPlayer();

        public override bool CanWin(IEnumerable<WoWUnit> possibleTargets)
            => dm.Get<WarriorManager>().CanWin(possibleTargets);

        public override void Fight(IEnumerable<WoWUnit> possibleTargets)
            => dm.Get<WarriorManager>().Fight(possibleTargets);

        public override float GetKiteDistance()
            => dm.Get<WarriorManager>().GetKiteDistance();

        public override int GetMaxPullCount()
            => dm.Get<WarriorManager>().GetMaxPullCount();

        public override float GetPullDistance()
            => dm.Get<WarriorManager>().GetPullDistance();

        public override bool IsBuffRequired()
            => dm.Get<WarriorManager>().IsBuffRequired();

        public override bool IsReadyToFight(IEnumerable<WoWUnit> possibleTargets)
            => dm.Get<WarriorManager>().IsReadyToFight(possibleTargets);

        public override void OnFightEnded()
            => SuppressBotMovement = false;

        public override void PrepareForFight()
            => dm.Get<WarriorManager>().PrepareForFight();

        public override void Pull(WoWUnit target)
            => dm.Get<WarriorManager>().Pull(target);

        public override void Rebuff()
            => dm.Get<WarriorManager>().Rebuff();

        public override void TryToBuffAnotherPlayer(WoWUnit player)
            => dm.Get<WarriorManager>().TryToBuffAnotherPlayer(player);
    }
}

