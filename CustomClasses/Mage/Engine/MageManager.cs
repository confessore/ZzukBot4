using System.Collections.Generic;
using System.Linq;
using ZzukBot.Core.Constants;
using ZzukBot.Core.Game.Objects;
using ZzukBot.Core.Game.Statics;

namespace Mage.Engine
{
    public class MageManager
    {
        Common Common { get; }
        Inventory Inventory { get; }
        Lua Lua { get; }
        Navigation Navigation { get; }
        ObjectManager ObjectManager { get; }
        Spell Spell { get; }

        public MageManager(
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

        List<WoWUnit> PossibleTargets { get; set; }

        public Enums.CombatPosition GetCombatPosition;

        public bool CanBuffAnotherPlayer()
        {
            if (Spell.IsKnown("Arcane Intellect"))
            {
                if (ObjectManager.Party1 != null)
                {
                    if (!ObjectManager.Party1.GotAura("Arcane Intellect") &&
                        !ObjectManager.Party1.GotDebuff("Ghost"))
                        return true;
                }

                if (ObjectManager.Party2 != null)
                {
                    if (!ObjectManager.Party2.GotAura("Arcane Intellect") &&
                        !ObjectManager.Party2.GotDebuff("Ghost"))
                        return true;
                }

                if (ObjectManager.Party3 != null)
                {
                    if (!ObjectManager.Party3.GotAura("Arcane Intellect") &&
                        !ObjectManager.Party3.GotDebuff("Ghost"))
                        return true;
                }

                if (ObjectManager.Party4 != null)
                {
                    if (!ObjectManager.Party4.GotAura("Arcane Intellect") &&
                        !ObjectManager.Party4.GotDebuff("Ghost"))
                        return true;
                }
            }
            return false;
        }

        public bool CanWin(IEnumerable<WoWUnit> possibleTargets)
        {
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
                if (PossibleTargets.Count() > 1  && Spell.IsKnown("Polymorph") && !PossibleTargets.Any(x => x.GotDebuff("Polymorph")))
                {
                    if (PossibleTargets.Any(x => (x.CreatureType == Enums.CreatureType.Beast || x.CreatureType == Enums.CreatureType.Humanoid) && !x.GotDebuff("Polymorph")))
                    {
                        PossibleTargets.Remove(primaryTarget);
                        ObjectManager.Player.SetTarget(
                            PossibleTargets.Where(x => (x.CreatureType == Enums.CreatureType.Beast || x.CreatureType == Enums.CreatureType.Humanoid) &&
                            !x.GotDebuff("Polymorph")).FirstOrDefault());
                        Spell.Cast("Polymorph");
                        PossibleTargets.Add(primaryTarget);
                    }
                }

                if (primaryTarget != null)
                {
                    ObjectManager.Player.SetTarget(primaryTarget);
                    if (ObjectManager.Player.MovementState == Enums.MovementFlags.None)
                        ObjectManager.Player.Face(primaryTarget);

                    ConsumeManaGem();
                    Rebuff();
                    Rotation();

                    if (ObjectManager.Player.CastingId == 0)
                    {
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
            return 29;
        }

        readonly string[] ConjuredFood =
        {
            string.Empty, "Conjured Muffin", "Conjured Bread",
            "Conjured Rye", "Conjured Pumpernickel",
            "Conjured Sourdough", "Conjured Sweet Roll",
            "Conjured Cinnamon Roll"
        };

        readonly string[] ConjuredGems =
        {
            "Mana Agate", "Mana Jade", "Mana Citrine", "Mana Ruby"
        };

        readonly string[] ConjuredWater =
        {
            string.Empty, "Conjured Water", "Conjured Fresh Water",
            "Conjured Purified Water", "Conjured Spring Water",
            "Conjured Mineral Water", "Conjured Sparkling Water",
            "Conjured Crystal Water"
        };

        void ConjureManaGem()
        {
            if (Spell.IsKnown("Conjure Mana Ruby") && Inventory.ExistingItems(ConjuredGems).Count == 0)
            {
                Spell.Cast("Conjure Mana Ruby");
                return;
            }
            if (Spell.IsKnown("Conjure Mana Citrine") && Inventory.ExistingItems(ConjuredGems).Count == 0)
            {
                Spell.Cast("Conjure Mana Citrine");
                return;
            }
            if (Spell.IsKnown("Conjure Mana Jade") && Inventory.ExistingItems(ConjuredGems).Count == 0)
            {
                Spell.Cast("Conjure Mana Jade");
                return;
            }
            if (Spell.IsKnown("Conjure Mana Agate") && Inventory.ExistingItems(ConjuredGems).Count == 0)
            {
                Spell.Cast("Conjure Mana Agate");
                return;
            }
        }

        void ConsumeManaGem()
        {
            if (Inventory.ExistingItems(ConjuredGems).Count > 0 && ObjectManager.Player.ManaPercent < 25)
                Inventory.ExistingItems(ConjuredGems).FirstOrDefault().Use();
        }

        bool NeedManaGem()
        {
            if ((Spell.IsKnown("Conjure Mana Ruby") || Spell.IsKnown("Conjure Mana Citrine") ||
                Spell.IsKnown("Conjure Mana Jade") || Spell.IsKnown("Conjure Mana Agate")) &&
                Inventory.ExistingItems(ConjuredGems).Count == 0)
                return true;

            return false;
        }

        public bool IsBuffRequired()
        {
            if (Spell.IsKnown("Conjure Water") && Inventory.GetItemCount(ConjuredWater[Spell.GetSpellRank("Conjure Water")]) <= 5)
                return true;
            if (Spell.IsKnown("Conjure Food") && Inventory.GetItemCount(ConjuredFood[Spell.GetSpellRank("Conjure Food")]) <= 5)
                return true;
            if (NeedManaGem())
                return true;
            if (Spell.IsKnown("Arcane Intellect") && !ObjectManager.Player.GotAura("Arcane Intellect"))
                return true;
            if (Spell.IsKnown("Ice Armor") && !ObjectManager.Player.GotAura("Ice Armor"))
                return true;
            else if (!Spell.IsKnown("Ice Armor") && Spell.IsKnown("Frost Armor") && !ObjectManager.Player.GotAura("Frost Armor"))
                return true;
            if (Spell.IsKnown("Dampen Magic") && !ObjectManager.Player.GotAura("Dampen Magic"))
                return true;

            return false;
        }

        public bool IsReadyToFight(IEnumerable<WoWUnit> possibleTargets)
        {
            if (ObjectManager.Player.HealthPercent == 100 && ObjectManager.Player.ManaPercent == 100)
                return true;
            if (ObjectManager.Player.CastingId != 0 || ObjectManager.Player.ChannelingId != 0)
                return false;
            if (ObjectManager.Player.HealthPercent < Common.EatAt || ObjectManager.Player.ManaPercent < Common.DrinkAt)
                return false;
            if (ObjectManager.Player.GotAura("Food") || ObjectManager.Player.GotAura("Drink"))
                return false;

            return true;
        }

        public void PrepareForFight()
        {
            if (Spell.IsKnown("Evocation") && Spell.IsReady("Evocation") && ObjectManager.Player.ManaPercent < 25 &&
                ObjectManager.Player.CastingId == 0 && ObjectManager.Player.ChannelingId == 0 &&
                !ObjectManager.Player.GotAura("Eat") && !ObjectManager.Player.GotAura("Drink"))
                Spell.CastWait("Evocation", 10000);
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

            Rebuff();
            Rotation();

            if (ObjectManager.Player.CastingId == 0)
            {
                if (target.DistanceToPlayer > GetPullDistance())
                    Navigation.Traverse(target.Position);
            }
        }

        public void Rebuff()
        {
            if (!ObjectManager.Player.IsInCombat)
            {
                if (Spell.IsKnown("Conjure Water") && Inventory.GetItemCount(ConjuredWater[Spell.GetSpellRank("Conjure Water")]) <= 5)
                    Spell.Cast("Conjure Water");
                if (Spell.IsKnown("Conjure Food") && Inventory.GetItemCount(ConjuredFood[Spell.GetSpellRank("Conjure Food")]) <= 5)
                    Spell.Cast("Conjure Food");
                if (NeedManaGem())
                    ConjureManaGem();
                if (Spell.IsKnown("Arcane Intellect") && !ObjectManager.Player.GotAura("Arcane Intellect"))
                    Spell.Cast("Arcane Intellect");
                if (Spell.IsKnown("Ice Armor") && !ObjectManager.Player.GotAura("Ice Armor"))
                    Spell.Cast("Ice Armor");
                else if (!Spell.IsKnown("Ice Armor") && Spell.IsKnown("Frost Armor") && !ObjectManager.Player.GotAura("Frost Armor"))
                    Spell.Cast("Frost Armor");
                if (Spell.IsKnown("Dampen Magic") && !ObjectManager.Player.GotAura("Dampen Magic"))
                    Spell.Cast("Dampen Magic");
                if (Spell.IsKnown("Ice Barrier") && Spell.IsReady("Ice Barrier"))
                    Spell.Cast("Ice Barrier");
            }
            else
            {
                if (Spell.IsKnown("Ice Barrier") && Spell.IsReady("Ice Barrier"))
                    Spell.Cast("Ice Barrier");
                if (ObjectManager.Player.HealthPercent < 15 && Spell.IsKnown("Mana Shield") && !ObjectManager.Player.GotAura("Mana Shield"))
                    Spell.Cast("Mana Shield");
            }
        }

        void Rotation()
        {
            if (((ObjectManager.Target.GotDebuff("Frost Nova") || ObjectManager.Target.GotDebuff("Frostbite")) &&
                ObjectManager.Target.Position.DistanceToPlayer() < 7) || Navigation.BackingUp)
                Navigation.Backup(7);
            else
            {
                if (ObjectManager.Player.ManaPercent < 10 || ObjectManager.Target.HealthPercent < 10)
                {
                    if (Spell.IsKnown("Fire Blast") && Spell.IsReady("Fire Blast"))
                        Spell.Cast("Fire Blast");
                    if (ObjectManager.Player.IsWandEquipped())
                        Spell.StartWand();
                    else if (ObjectManager.Target.DistanceToPlayer > 3f)
                        Navigation.Traverse(ObjectManager.Target.Position);
                    else
                        Spell.Attack();
                }
                else
                {
                    if (Spell.IsKnown("Counterspell") && Spell.IsReady("Counterspell") && (ObjectManager.Target.CastingId != 0 || ObjectManager.Target.ChannelingId != 0))
                    {
                        Spell.StopCasting();
                        Spell.Cast("Counterspell");
                        return;
                    }
                    if (Spell.IsKnown("Frost Nova") && Spell.IsReady("Frost Nova") && ObjectManager.Target.DistanceToPlayer < 7 &&
                        !ObjectManager.Target.GotDebuff("Frost Nova") && !ObjectManager.Target.GotDebuff("Frostbite") &&
                        ObjectManager.Player.IsAoeSafe(10))
                    {
                        Spell.StopCasting();
                        Spell.Cast("Frost Nova");
                        return;
                    }
                    if (Spell.IsKnown("Berserking") && Spell.IsReady("Berserking"))
                        Spell.Cast("Berserking");
                    if (Spell.IsKnown("Frostbolt"))
                    {
                        if ((ObjectManager.Target.GotDebuff("Frost Nova") || ObjectManager.Target.GotDebuff("Frostbite")) && ObjectManager.Player.IsAoeSafe(10))
                        {
                            if (Spell.IsKnown("Cone of Cold") && Spell.IsReady("Cone of Cold") &&
                                ObjectManager.Player.MovementState == Enums.MovementFlags.None && 
                                ObjectManager.Player.IsFacing(ObjectManager.Target.Position))
                            {
                                ObjectManager.Player.CtmFace(ObjectManager.Target);
                                Spell.CastWait("Frostbolt", 3000);
                                Spell.CastAtPos("Cone of Cold", ObjectManager.Target.Position);
                            }
                            else if (Spell.IsKnown("Fire Blast") && Spell.IsReady("Fire Blast") && ObjectManager.Target.HealthPercent < 15)
                            {
                                Spell.CastWait("Frostbolt", 3000);
                                Spell.Cast("Fire Blast");
                            }
                            else
                                Spell.CastWait("Frostbolt", 3000);
                        }
                        else
                            Spell.Cast("Frostbolt");
                    }
                    else
                        Spell.Cast("Fireball");
                }
            }
        }

        public void TryToBuffAnotherPlayer(WoWUnit player)
        {
            if (ObjectManager.Party1 != null)
            {
                if (!ObjectManager.Party1.GotAura("Arcane Intellect"))
                {
                    ObjectManager.Player.SetTarget(ObjectManager.Party1.Guid);
                    Spell.Cast("Arcane Intellect");
                }
            }

            if (ObjectManager.Party2 != null)
            {
                if (!ObjectManager.Party2.GotAura("Arcane Intellect"))
                {
                    ObjectManager.Player.SetTarget(ObjectManager.Party2.Guid);
                    Spell.Cast("Arcane Intellect");
                }
            }

            if (ObjectManager.Party3 != null)
            {
                if (!ObjectManager.Party3.GotAura("Arcane Intellect"))
                {
                    ObjectManager.Player.SetTarget(ObjectManager.Party3.Guid);
                    Spell.Cast("Arcane Intellect");
                }
            }

            if (ObjectManager.Party4 != null)
            {
                if (!ObjectManager.Party4.GotAura("Arcane Intellect"))
                {
                    ObjectManager.Player.SetTarget(ObjectManager.Party4.Guid);
                    Spell.Cast("Arcane Intellect");
                }
            }
        }
    }
}
