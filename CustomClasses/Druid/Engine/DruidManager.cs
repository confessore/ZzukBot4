using System;
using System.Collections.Generic;
using System.Linq;
using ZzukBot.Core.Constants;
using ZzukBot.Core.Game.Objects;
using ZzukBot.Core.Game.Statics;

namespace Druid.Engine
{
    public class DruidManager
    {
        Common Common { get; }
        Inventory Inventory { get; }
        Lua Lua { get; }
        Navigation Navigation { get; }
        ObjectManager ObjectManager { get; }
        Spell Spell { get; }

        public DruidManager(
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
                        if (Spell.GCDReady("Healing Touch", "Moonfire", "Bear Form"))
                        {
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
            if (CanShift)
                return 5f;
            return 29f;
        }

        public bool IsBuffRequired()
        {
            if (!ObjectManager.Player.IsInCombat)
            {
                if (Spell.IsKnown("Mark of the Wild") && !ObjectManager.Player.GotAura("Mark of the Wild"))
                    return true;
                if (Spell.IsKnown("Thorns") && !ObjectManager.Player.GotAura("Thorns"))
                    return true;
                if (Spell.IsKnown("Omen of Clarity") && !ObjectManager.Player.GotAura("Omen of Clarity"))
                    return true;
            }
            return false;
        }

        void FightHeal()
        {
            if (ObjectManager.Player.ManaPercent < 50 && Spell.IsKnown("Innervate") && Spell.IsReady("Innervate") && !IsShifted)
                Spell.Cast("Innervate");
            if (ObjectManager.Player.HealthPercent < 50 && Spell.IsKnown("Barkskin") && Spell.IsReady("Barkskin"))
                Spell.Cast("Barkskin");
            if (ObjectManager.Player.HealthPercent < 50 && Spell.IsKnown("Rejuvenation") && !ObjectManager.Player.GotAura("Rejuvenation"))
                Spell.Cast("Rejuvenation");
            else if (ObjectManager.Player.HealthPercent < 50)
            {
                if (Spell.IsKnown("Regrowth") && !ObjectManager.Player.GotAura("Regrowth"))
                    Spell.Cast("Regrowth");
                else if (Spell.IsKnown("Healing Touch"))
                    Spell.Cast("Healing Touch");
            }
        }

        void RestHeal()
        {
            if (ObjectManager.Player.HealthPercent < Common.EatAt && ObjectManager.Player.ManaPercent > Common.DrinkAt &&
                !ObjectManager.Player.GotAura("Drink") && !ObjectManager.Player.GotAura("Food"))
            {
                if (Spell.IsKnown("Regrowth") && !ObjectManager.Player.GotAura("Regrowth"))
                    Spell.Cast("Regrowth");
                else if (Spell.IsKnown("Healing Touch"))
                    Spell.Cast("Healing Touch");
            }
        }

        public bool IsReadyToFight(IEnumerable<WoWUnit> possibleTargets)
        {
            if (ObjectManager.Player.HealthPercent == 100 && ObjectManager.Player.ManaPercent == 100)
                return true;

            if (ObjectManager.Player.HealthPercent < Common.EatAt || (ObjectManager.Player.ManaPercent < Common.DrinkAt && !IsShifted))
                return false;

            if (ObjectManager.Player.GotAura("Food") || ObjectManager.Player.GotAura("Drink"))
                return false;

            return true;
        }

        public void PrepareForFight()
        {
            RestHeal();
            if (!ObjectManager.Player.IsCasting && !ObjectManager.Player.IsChanneling)
            {
                if (ObjectManager.Player.ManaPercent < Common.DrinkAt)
                    Common.TryUseDrink();
                if (ObjectManager.Player.HealthPercent < Common.EatAt)
                    Common.TryUseFood();
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
            if (!ObjectManager.Player.IsInCombat)
            {
                if (Spell.IsKnown("Mark of the Wild") && !ObjectManager.Player.GotAura("Mark of the Wild"))
                    Spell.Cast("Mark of the Wild");
                if (Spell.IsKnown("Thorns") && !ObjectManager.Player.GotAura("Thorns"))
                    Spell.Cast("Thorns");
                if (Spell.IsKnown("Omen of Clarity") && !ObjectManager.Player.GotAura("Omen of Clarity"))
                    Spell.Cast("Omen of Clarity");
            }
        }

        bool CanShift { get => Spell.IsKnown("Bear Form") || Spell.IsKnown("Dire Bear Form"); }

        bool IsShifted { get => ObjectManager.Player.GotAura("Bear Form") || ObjectManager.Player.GotAura("Cat Form") || ObjectManager.Player.GotAura("Dire Bear Form"); }

        void Rotation()
        {
            Spell.Attack();
            if (CanShift && !IsShifted)
            {
                if (PossibleTargets.Count() < 2)
                {
                    if (Spell.IsKnown("Cat Form"))
                        Spell.Cast("Cat Form");
                    else
                        Spell.Cast("Bear Form");
                }
                else
                {
                    if (Spell.IsKnown("Dire Bear Form"))
                        Spell.Cast("Dire Bear Form");
                    else
                        Spell.Cast("Bear Form");
                }
            }
            else if (IsShifted)
            {
                Spell.Attack();
                if (Spell.IsKnown("Faerie Fire (Feral)") && Spell.IsReady("Faerie Fire (Feral)") && !ObjectManager.Target.GotDebuff("Faerie Fire (Feral)"))
                    Lua.Execute("CastSpellByName('Faerie Fire (Feral)()');");
                else
                {
                    if (ObjectManager.Player.GotAura("Bear Form") || ObjectManager.Player.GotAura("Dire Bear Form"))
                    {
                        if (Spell.IsKnown("Enrage") && Spell.IsReady("Enrage") &&
                            ObjectManager.Player.IsInCombat && ObjectManager.Target.IsInCombat
                            && ObjectManager.Target.HealthPercent > 50)
                            Spell.Cast("Enrage");
                        if (ObjectManager.Player.Rage > 9 && (ObjectManager.Player.HealthPercent < 60 || ObjectManager.Target.CastingId != 0 || ObjectManager.Target.ChannelingId != 0))
                            Spell.Cast("Bash");
                        if (PossibleTargets.Count() < 2)
                        {
                            if (ObjectManager.Player.Rage > 9 && !ObjectManager.Target.GotDebuff("Demoralizing Roar"))
                                Spell.Cast("Demoralizing Roar");
                            else if (ObjectManager.Player.Rage > 14 - Ferocity.CurrentRank && ObjectManager.Target.GotDebuff("Demoralizing Roar"))
                                Spell.Cast("Maul");
                        }
                        else
                        {
                            if (ObjectManager.Player.HealthPercent < 65 && Spell.IsKnown("Frenzied Regeneration") && Spell.IsReady("Frenzied Regeneration"))
                                Spell.Cast("Frenzied Regeneration");
                            if (ObjectManager.Player.Rage > 9 && !ObjectManager.Target.GotDebuff("Demoralizing Roar"))
                                Spell.Cast("Demoralizing Roar");
                            /*else if (Spell.IsKnown("Swipe") && ObjectManager.Player.Rage > 19 - Ferocity.CurrentRank && ObjectManager.Target.GotDebuff("Demoralizing Roar"))
                                Spell.Cast("Swipe");*/
                            else if (!ObjectManager.Player.GotAura("Frenzied Regeneration") && ObjectManager.Player.Rage > 14 - Ferocity.CurrentRank && ObjectManager.Target.GotDebuff("Demoralizing Roar"))
                                Spell.Cast("Maul");
                        }
                    }
                    else
                    {
                        if (PossibleTargets.Count() < 2)
                        {
                            if (!ObjectManager.Player.IsInCombat && Spell.IsKnown("Dash") && Spell.IsReady("Dash"))
                                Spell.Cast("Dash");
                            if (!Spell.IsKnown("Ferocious Bite") && !ObjectManager.Target.GotDebuff("Rip") &&
                                ObjectManager.Player.ComboPoints >= 3 && ObjectManager.Player.Energy > 39 &&
                                ObjectManager.Target.CreatureType != Enums.CreatureType.Elemental && ObjectManager.Target.CreatureType != Enums.CreatureType.Mechanical)
                                Spell.Cast("Rip");
                            if (ShouldBite || (Spell.IsKnown("Ferocious Bite") && ObjectManager.Player.ComboPoints == 5 && ObjectManager.Player.Energy > 34))
                                Spell.Cast("Ferocious Bite");
                            else if (ObjectManager.Player.Energy > 44 - Ferocity.CurrentRank)
                                Spell.Cast("Claw");
                        }
                        else
                            Spell.CancelShapeshift();
                    }
                }
            }
            else
            {
                if (ObjectManager.Player.ManaPercent < 10 || ObjectManager.Target.HealthPercent < 10)
                    Spell.Attack();
                else
                {
                    if (Spell.IsKnown("Moonfire") && !ObjectManager.Target.GotDebuff("Moonfire"))
                        Spell.Cast("Moonfire");
                    else
                        Spell.Cast("Wrath");
                }
            }
        }

        public void TryToBuffAnotherPlayer(WoWUnit player)
        {

        }

        Lua.Talent Ferocity { get => Lua.GetTalents().Where(x => x.Name == "Ferocity").FirstOrDefault(); }
        Lua.Talent FeralAggression { get => Lua.GetTalents().Where(x => x.Name == "Feral Aggression").FirstOrDefault(); }
        Lua.Talent NaturalWeapons { get => Lua.GetTalents().Where(x => x.Name == "Natural Weapons").FirstOrDefault(); }

        bool ShouldBite
        {
            get
            {
                var rank = Spell.GetSpellRank("Ferocious Bite");
                var cp = ObjectManager.Player.ComboPoints;
                var energy = ObjectManager.Player.Energy;

                if (rank > 0 && cp > 0 && energy > 34)
                {
                    var power = ObjectManager.Player.AttackPower * 0.1526;
                    var excess = energy - 35;
                    var aggression = 1 + (0.3 * FeralAggression.CurrentRank);
                    var weapons = 1 + (0.2 * NaturalWeapons.CurrentRank);
                    var damage = (BiteDamage[rank][cp] + power + excess) * aggression * weapons;

                    if (damage > ObjectManager.Target.Health * 10)
                        return true;
                }
                return false;
            }
        }

        readonly int[][] BiteDamage =
        {
            new int[] {0, 0, 0, 0, 0},
            new int[] {0, 50, 86, 122, 158, 194},
            new int[] {0, 79, 138, 197, 256, 315},
            new int[] {0, 122, 214, 306, 398, 490},
            new int[] {0, 173, 301, 429, 557, 685},
            new int[] {0, 199, 346, 493, 640, 787}
        };
    }
}
