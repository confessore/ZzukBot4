using Shaman.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZzukBot.Core.Constants;
using ZzukBot.Core.Framework.Classes;
using ZzukBot.Core.Game.Objects;
using ZzukBot.Core.Game.Statics;
using ZzukBot.Core.Utilities.DependencyInjection;

namespace Shaman
{
    [Export(typeof(CustomClass))]
    public class Shaman : CustomClass
    {
        private DependencyMap dm = new DependencyMap();

        public Shaman()
        {
            Author = "krycess";
            Name = "Shaman";
            Version = new Version("0.0.1.0");

            dm.Add(this);
            dm.Add(Common.Instance);
            dm.Add(Inventory.Instance);
            dm.Add(Lua.Instance);
            dm.Add(Navigation.Instance);
            dm.Add(ObjectManager.Instance);
            dm.Add(Spell.Instance);
            dm.Add(new ShamanManager(
                dm.Get<Common>(),
                dm.Get<Inventory>(),
                dm.Get<Lua>(),
                dm.Get<Navigation>(),
                dm.Get<ObjectManager>(),
                dm.Get<Spell>()));
        }

        public override Enums.ClassId Class => Enums.ClassId.Shaman;

        public override Enums.CombatPosition GetCombatPosition()
            => dm.Get<ShamanManager>().GetCombatPosition;

        public override bool CanBuffAnotherPlayer()
            => dm.Get<ShamanManager>().CanBuffAnotherPlayer();

        public override bool CanWin(IEnumerable<WoWUnit> possibleTargets)
            => dm.Get<ShamanManager>().CanWin(possibleTargets);

        public override void Fight(IEnumerable<WoWUnit> possibleTargets)
            => dm.Get<ShamanManager>().Fight(possibleTargets);

        public override float GetKiteDistance()
            => dm.Get<ShamanManager>().GetKiteDistance();

        public override int GetMaxPullCount()
            => dm.Get<ShamanManager>().GetMaxPullCount();

        public override float GetPullDistance()
            => dm.Get<ShamanManager>().GetPullDistance();

        public override bool IsBuffRequired()
            => dm.Get<ShamanManager>().IsBuffRequired();

        public override bool IsReadyToFight(IEnumerable<WoWUnit> possibleTargets)
            => dm.Get<ShamanManager>().IsReadyToFight(possibleTargets);

        public override void OnFightEnded()
            => SuppressBotMovement = false;

        public override void PrepareForFight()
            => dm.Get<ShamanManager>().PrepareForFight();

        public override void Pull(WoWUnit target)
            => dm.Get<ShamanManager>().Pull(target);

        public override void Rebuff()
            => dm.Get<ShamanManager>().Rebuff();

        public override void TryToBuffAnotherPlayer(WoWUnit player)
            => dm.Get<ShamanManager>().TryToBuffAnotherPlayer(player);
    }
}
