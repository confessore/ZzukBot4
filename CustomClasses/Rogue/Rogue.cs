using Rogue.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using ZzukBot.Core.Constants;
using ZzukBot.Core.Framework.Classes;
using ZzukBot.Core.Game.Objects;
using ZzukBot.Core.Game.Statics;
using ZzukBot.Core.Utilities.DependencyInjection;

namespace Rogue
{
    [Export(typeof(CustomClass))]
    public class Rogue : CustomClass
    {
        private DependencyMap dm = new DependencyMap();

        public Rogue()
        {
            Author = "krycess";
            Name = "Rogue";
            Version = new Version("0.0.1.0");

            dm.Add(this);
            dm.Add(Common.Instance);
            dm.Add(Inventory.Instance);
            dm.Add(Lua.Instance);
            dm.Add(Navigation.Instance);
            dm.Add(ObjectManager.Instance);
            dm.Add(Spell.Instance);
            dm.Add(new RogueManager(
                dm.Get<Common>(),
                dm.Get<Inventory>(),
                dm.Get<Lua>(),
                dm.Get<Navigation>(),
                dm.Get<ObjectManager>(),
                dm.Get<Spell>()));
        }

        public override Enums.ClassId Class => Enums.ClassId.Rogue;

        public override Enums.CombatPosition GetCombatPosition()
            => dm.Get<RogueManager>().GetCombatPosition;

        public override bool CanBuffAnotherPlayer()
            => dm.Get<RogueManager>().CanBuffAnotherPlayer();

        public override bool CanWin(IEnumerable<WoWUnit> possibleTargets)
            => dm.Get<RogueManager>().CanWin(possibleTargets);

        public override void Fight(IEnumerable<WoWUnit> possibleTargets)
            => dm.Get<RogueManager>().Fight(possibleTargets);

        public override float GetKiteDistance()
            => dm.Get<RogueManager>().GetKiteDistance();

        public override int GetMaxPullCount()
            => dm.Get<RogueManager>().GetMaxPullCount();

        public override float GetPullDistance()
            => dm.Get<RogueManager>().GetPullDistance();

        public override bool IsBuffRequired()
            => dm.Get<RogueManager>().IsBuffRequired();

        public override bool IsReadyToFight(IEnumerable<WoWUnit> possibleTargets)
            => dm.Get<RogueManager>().IsReadyToFight(possibleTargets);

        public override void OnFightEnded()
            => SuppressBotMovement = false;

        public override void PrepareForFight()
            => dm.Get<RogueManager>().PrepareForFight();

        public override void Pull(WoWUnit target)
            => dm.Get<RogueManager>().Pull(target);

        public override void Rebuff()
            => dm.Get<RogueManager>().Rebuff();

        public override void TryToBuffAnotherPlayer(WoWUnit player)
            => dm.Get<RogueManager>().TryToBuffAnotherPlayer(player);
    }
}
