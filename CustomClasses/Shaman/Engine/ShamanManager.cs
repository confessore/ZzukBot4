using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZzukBot.Core.Constants;
using ZzukBot.Core.Game.Objects;
using ZzukBot.Core.Game.Statics;

namespace Shaman.Engine
{
    public class ShamanManager
    {
        Common Common { get; }
        Inventory Inventory { get; }
        Lua Lua { get; }
        Navigation Navigation { get; }
        ObjectManager ObjectManager { get; }
        Spell Spell { get; }

        public ShamanManager(
            Common common,
            Inventory inventory,
            Lua lua,
            Navigation navigation,
            ObjectManager objectManager,
            Spell spell)
        {
            Common = common;
            Inventory = inventory;
            Lua = lua;
            Navigation = navigation;
            ObjectManager = objectManager;
            Spell = spell;
        }

        IList<WoWUnit> PossibleTargets { get; set; }

        public Enums.CombatPosition GetCombatPosition;

        public bool CanBuffAnotherPlayer()
        {
            return false;
        }

        public bool CanWin(IEnumerable<WoWUnit> possibleTargets)
        {
            if (possibleTargets.Count() < 3 && ObjectManager.Player.HealthPercent > Common.EatAt)
                return true;

            return false;
        }

        public void Fight(IEnumerable<WoWUnit> possibleTargets)
        {
            PossibleTargets = Common.PossibleTargets(possibleTargets).ToList();

            if (PossibleTargets.Count() > 0)
            {
                PossibleTargets = PossibleTargets.Where(x => x.IsPlayer).Any() ?
                    PossibleTargets.Where(x => x.IsPlayer).ToList() : PossibleTargets.OrderBy(x => x.HealthPercent).ToList();
                var primaryTarget = PossibleTargets.FirstOrDefault();

                if (primaryTarget != null)
                {
                    ObjectManager.Player.SetTarget(primaryTarget);
                    ObjectManager.Player.Face(primaryTarget);

                    if (ObjectManager.Player.CastingId == 0)
                    {
                        if (Spell.GCDReady("Healing Wave", "Flame Shock", "Frost Shock"))
                        {
                            Rebuff();
                            FightHeal();
                            Rotation();
                        }

                        if (primaryTarget.DistanceToPlayer > GetPullDistance())
                            Navigation.Traverse(primaryTarget.Position);
                    }
                }
            }
        }

        public float GetKiteDistance()
        {
            return 20;
        }

        public int GetMaxPullCount()
        {
            return 2;
        }

        public float GetPullDistance()
        {
            return 4;
        }

        public bool IsBuffRequired()
        {
            if (Spell.IsKnown("Lightning Shield") && !ObjectManager.Player.GotAura("Lightning Shield"))
                return true;
            if (Spell.IsKnown("Rockbiter Weapon") && !ObjectManager.Player.IsMainhandEnchanted())
                return true;
            return false;
        }

        void FightHeal()
        {
            if (ObjectManager.Player.HealthPercent < 50)
                Spell.Cast("Healing Wave");
        }

        void RestHeal()
        {
            if (ObjectManager.Player.HealthPercent < Common.EatAt && ObjectManager.Player.ManaPercent > Common.DrinkAt &&
                !ObjectManager.Player.GotAura("Drink") && !ObjectManager.Player.GotAura("Food"))
                Spell.Cast("Healing Wave");
        }

        public bool IsReadyToFight(IEnumerable<WoWUnit> possibleTargets)
        {
            if (ObjectManager.Player.HealthPercent == 100 && ObjectManager.Player.ManaPercent == 100)
                return true;

            if (ObjectManager.Player.HealthPercent < Common.EatAt || ObjectManager.Player.ManaPercent < Common.DrinkAt)
                return false;

            if ((ObjectManager.Player.GotAura("Food") && ObjectManager.Player.HealthPercent != 100) ||
                ObjectManager.Player.GotAura("Drink") && ObjectManager.Player.ManaPercent != 100)
                return false;

            return true;
        }

        public void PrepareForFight()
        {
            RestHeal();
            if (ObjectManager.Player.CastingId == 0 && ObjectManager.Player.ChannelingId == 0)
            {
                if (ObjectManager.Player.HealthPercent < Common.EatAt)
                    Common.TryUseFood();
                if (ObjectManager.Player.ManaPercent < Common.DrinkAt)
                    Common.TryUseDrink();
            }
        }

        public void Pull(WoWUnit target)
        {
            ObjectManager.Player.SetTarget(target);
            if (ObjectManager.Player.MovementState == Enums.MovementFlags.None)
                ObjectManager.Player.Face(target);
            PossibleTargets = new List<WoWUnit> { target };

            if (ObjectManager.Player.CastingId == 0)
            {
                Rebuff();
                Rotation();

                if (target.DistanceToPlayer > GetPullDistance())
                    Navigation.Traverse(target.Position);
            }
        }

        public void Rebuff()
        {
            if (Spell.IsKnown("Lightning Shield") && !ObjectManager.Player.GotAura("Lightning Shield"))
                Spell.Cast("Lightning Shield");
            if (Spell.IsKnown("Windfury Weapon") && !ObjectManager.Player.IsMainhandEnchanted())
                Spell.Cast("Windfury Weapon");
            else if (Spell.IsKnown("Flametongue Weapon") && !ObjectManager.Player.IsMainhandEnchanted())
                Spell.Cast("Flametongue Weapon");
            else if (Spell.IsKnown("Rockbiter Weapon") && !ObjectManager.Player.IsMainhandEnchanted())
                Spell.Cast("Rockbiter Weapon");
        }

        void Rotation()
        {
            Spell.Attack();
            if (Spell.CanCast("Stormstrike"))
                Spell.Cast("Stormstrike");
            Shocks();
            if (ObjectManager.Player.IsInCombat)
                Totems();
        }

        public void TryToBuffAnotherPlayer(WoWUnit player)
        {

        }

        void Shocks()
        {
            if (!Spell.IsKnown("Stormstrike") && Spell.CanCast("Flame Shock") && ObjectManager.Player.ManaPercent > 50
                && !ObjectManager.Target.IsCasting && !ObjectManager.Target.IsChanneling)
                Spell.Cast("Flame Shock");
            if (Spell.CanCast("Earth Shock") && (ObjectManager.Player.ManaPercent > 50 || ObjectManager.Target.IsCasting || ObjectManager.Target.IsChanneling))
                Spell.Cast("Earth Shock");
        }

        void Totems()
        {
            AirTotems();
            WaterTotems();
            EarthTotems();
            FireTotems();
        }

        void AirTotems()
        {
            if (Spell.CanCast("Grounding Totem") && ObjectManager.Target.IsCasting || ObjectManager.Target.IsChanneling && !Spell.IsReady("Earth Shock"))
                Spell.Cast("Grounding Totem");
            else if (Spell.IsKnown("Grace of Air Totem") && !ObjectManager.Player.GotAura("Grace of Air"))
                Spell.Cast("Grace of Air Totem");
        }

        void EarthTotems()
        {
            if (PossibleTargets.Count() > 1 && Spell.CanCast("Stoneclaw Totem"))
                Spell.Cast("Stoneclaw Totem");
            else if (Spell.IsKnown("Strength of Earth Totem") && !ObjectManager.Player.GotAura("Strength of Earth"))
                Spell.Cast("Strength of Earth Totem");
        }

        void FireTotems()
        {
            if (Spell.IsKnown("Searing Totem") && (ObjectManager.Player.IsTotemSpawned("Searing Totem") == -1 || ObjectManager.Player.IsTotemSpawned("Searing Totem") > 20))
                Spell.Cast("Searing Totem");
        }

        void WaterTotems()
        {
            if (Spell.IsKnown("Mana Spring Totem") && !ObjectManager.Player.GotAura("Mana Spring"))
                Spell.Cast("Mana Spring Totem");
        }
    }
}
