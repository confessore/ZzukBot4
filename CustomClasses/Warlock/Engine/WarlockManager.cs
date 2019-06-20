using System.Collections.Generic;
using System.Linq;
using ZzukBot.Core.Constants;
using ZzukBot.Core.Game.Objects;
using ZzukBot.Core.Game.Statics;

namespace Warlock.Engine
{
    public class WarlockManager
    {
        Common Common { get; }
        Inventory Inventory { get; }
        Lua Lua { get; }
        Navigation Navigation { get; }
        ObjectManager ObjectManager { get; }
        Spell Spell { get; }

        public WarlockManager(
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

        readonly string[] Healthstones =
        {
            "Minor Healthstone", "Lesser Healthstone", "Healthstone", "Greater Healthstone", "Major Healthstone"
        };

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

                    if (ShouldStopWand)
                        Spell.StopWand();
                    if (!ObjectManager.Player.IsCasting && !ObjectManager.Player.IsChanneling)
                    {
                        if (Spell.GCDReady("Shadow Bolt", "Immolate", ""))
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
            return 29;
        }

        public bool IsBuffRequired()
        {
            if (!ObjectManager.Player.IsInCombat)
            {
                if (!ObjectManager.Player.HasPet || ObjectManager.Pet.IsDead)
                {
                    if (Spell.IsKnown("Summon Voidwalker") && Inventory.GetItemCount("Soul Shard") > 0)
                        return true;
                    else if (Spell.IsKnown("Summon Imp"))
                        return true;
                }
                if (Inventory.CountFreeSlots(false) > 0)
                {
                    if (Spell.IsKnown("Create Healthstone (Major)") && Inventory.ExistingItems(Healthstones).Count < 1 && Inventory.GetItemCount("Soul Shard") > 0)
                        return true;
                    else if (Spell.IsKnown("Create Healthstone (Greater)") && Inventory.ExistingItems(Healthstones).Count < 1 && Inventory.GetItemCount("Soul Shard") > 0)
                        return true;
                    else if (Spell.IsKnown("Create Healthstone") && Inventory.ExistingItems(Healthstones).Count < 1 && Inventory.GetItemCount("Soul Shard") > 0)
                        return true;
                    else if (Spell.IsKnown("Create Healthstone (Lesser)") && Inventory.ExistingItems(Healthstones).Count < 1 && Inventory.GetItemCount("Soul Shard") > 0)
                        return true;
                    else if (Spell.IsKnown("Create Healthstone (Minor)") && Inventory.ExistingItems(Healthstones).Count < 1 && Inventory.GetItemCount("Soul Shard") > 0)
                        return true;
                }
            }
            if (Spell.IsKnown("Demon Armor") && !ObjectManager.Player.GotAura("Demon Armor"))
                return true;
            else if (!ObjectManager.Player.GotAura("Demon Skin") && !ObjectManager.Player.GotAura("Demon Armor"))
                return true;
            return false;
        }

        void FightHeal()
        {
            if (ShouldHealthstone)
                Inventory.FirstExistingItem(Healthstones).Use();
            if (ShouldDrainLife)
                Spell.Cast("Drain Life");
            if (ShouldHealthFunnel)
            {
                if (ObjectManager.Pet.DistanceToPlayer > 20)
                    ObjectManager.Pet.FollowPlayer();
                Spell.Cast("Health Funnel");
            }
        }

        bool ShouldAgony
        {
            get => !ShouldWand && (Spell.IsKnown("Curse of Agony") ? !ObjectManager.Target.GotDebuff("Curse of Agony") : false);
        }

        bool ShouldCorruption
        {
            get => !ShouldWand && (Spell.IsKnown("Corruption") ? !ObjectManager.Target.GotDebuff("Corruption") : false);
        }

        bool ShouldDrainLife
        {
            get => !ShouldWand && Spell.IsKnown("Drain Life") && ObjectManager.Player.HealthPercent < Common.EatAt;
        }

        bool ShouldDrainSoul
        {
            get => Spell.IsKnown("Drain Soul") && Inventory.GetItemCount("Soul Shard") < 3 && ObjectManager.Target.HealthPercent < 25 && ObjectManager.Player.ManaPercent > 10;
        }

        bool ShouldHealthFunnel
        {
            get => Spell.IsKnown("Health Funnel") && !ObjectManager.Pet.IsDead && ObjectManager.Pet.HealthPercent < Common.EatAt && ObjectManager.Player.HealthPercent > Common.EatAt;
        }

        bool ShouldHealthstone
        {
            get => Inventory.ExistingItems(Healthstones).Count > 0 && Inventory.FirstExistingItem(Healthstones).CanUse() && ObjectManager.Player.HealthPercent < Common.EatAt;
        }

        bool ShouldImmolate
        {
            get => !ShouldWand && (Spell.IsKnown("Immolate") && !Spell.IsKnown("Curse of Agony") ? !ObjectManager.Target.GotDebuff("Immolate") : false);
        }

        bool ShouldLifeTap
        {
            get
            {
                return Spell.IsKnown("Life Tap") &&
                    ObjectManager.Player.ManaPercent < Common.DrinkAt && ObjectManager.Player.HealthPercent > 80 &&
                    !ObjectManager.Player.GotAura("Drink") && !ObjectManager.Player.GotAura("Eat");
            }
        }

        bool ShouldStopWand
        {
            get
            {
                return ObjectManager.Player.IsWandEquipped() && (IsBuffRequired() || ShouldHealthstone || ShouldDrainSoul || 
                    ShouldImmolate || ShouldCorruption || ShouldAgony || ShouldLifeTap || ShouldDrainLife);
            }
        }

        bool ShouldWand
        {
            get => ObjectManager.Player.ManaPercent < 10 && ObjectManager.Target.HealthPercent < 10;
        }

        public bool IsReadyToFight(IEnumerable<WoWUnit> possibleTargets)
        {
            if (ObjectManager.Player.HealthPercent == 100 && ObjectManager.Player.ManaPercent == 100)
                return true;

            if (ObjectManager.Player.HealthPercent < Common.EatAt || ObjectManager.Player.ManaPercent < Common.DrinkAt)
                return false;

            if (ObjectManager.Player.HasPet)
            {
                if (ObjectManager.Pet.IsDead || ObjectManager.Pet.HealthPercent < Common.EatAt)
                    return false;
            }

            if ((ObjectManager.Player.GotAura("Food") && ObjectManager.Player.HealthPercent != 100) ||
                ObjectManager.Player.GotAura("Drink") && ObjectManager.Player.ManaPercent != 100)
                return false;

            return true;
        }

        public void PrepareForFight()
        {
            if (!ObjectManager.Player.IsCasting && !ObjectManager.Player.IsChanneling)
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

            if (!ObjectManager.Player.IsCasting && !ObjectManager.Player.IsChanneling)
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
                if (!ObjectManager.Player.HasPet || ObjectManager.Pet.IsDead)
                {
                    if (Spell.IsKnown("Summon Voidwalker") && Inventory.GetItemCount("Soul Shard") > 0)
                        Spell.Cast("Summon Voidwalker");
                    else if (Spell.IsKnown("Summon Imp"))
                        Spell.Cast("Summon Imp");
                }
                if (Inventory.CountFreeSlots(false) > 0)
                {
                    if (Spell.IsKnown("Create Healthstone (Major)") && Inventory.ExistingItems(Healthstones).Count < 1 && Inventory.GetItemCount("Soul Shard") > 0)
                        Lua.Execute("CastSpellByName('Create Healthstone (Major)()');");
                    else if (Spell.IsKnown("Create Healthstone (Greater)") && Inventory.ExistingItems(Healthstones).Count < 1 && Inventory.GetItemCount("Soul Shard") > 0)
                        Lua.Execute("CastSpellByName('Create Healthstone (Greater)()');");
                    else if (Spell.IsKnown("Create Healthstone") && Inventory.ExistingItems(Healthstones).Count < 1 && Inventory.GetItemCount("Soul Shard") > 0)
                        Spell.Cast("Create Healthstone");
                    else if (Spell.IsKnown("Create Healthstone (Lesser)") && Inventory.ExistingItems(Healthstones).Count < 1 && Inventory.GetItemCount("Soul Shard") > 0)
                        Lua.Execute("CastSpellByName('Create Healthstone (Lesser)()');");
                    else if (Spell.IsKnown("Create Healthstone (Minor)") && Inventory.ExistingItems(Healthstones).Count < 1 && Inventory.GetItemCount("Soul Shard") > 0)
                        Lua.Execute("CastSpellByName('Create Healthstone (Minor)()');");
                }
            }
            if (Spell.IsKnown("Demon Armor") && !ObjectManager.Player.GotAura("Demon Armor"))
                Spell.Cast("Demon Armor");
            else if (!ObjectManager.Player.GotAura("Demon Skin") && !ObjectManager.Player.GotAura("Demon Armor"))
                Spell.Cast("Demon Skin");
        }

        void Rotation()
        {
            Spell.Attack();
            if (PossibleTargets.Count() > 1 && Spell.IsKnown("Corruption") && Spell.IsKnown("Curse of Agony"))
            {
                var dotlessTargets = PossibleTargets.Where(x => !x.GotDebuff("Corruption") || !x.GotDebuff("Curse of Agony"));
                if (dotlessTargets.Any())
                    ObjectManager.Player.SetTarget(dotlessTargets.FirstOrDefault());
            }
            if (ObjectManager.Player.HasPet && !ObjectManager.Pet.IsDead)
            {
                var currentTarget = ObjectManager.Target;
                var targettingPlayer = PossibleTargets.Where(x => x.TargetGuid == ObjectManager.Player.Guid);
                if (targettingPlayer.Any())
                {
                    if (ObjectManager.Pet.TargetGuid != targettingPlayer.FirstOrDefault().Guid)
                    {
                        ObjectManager.Player.SetTarget(targettingPlayer.FirstOrDefault());
                        ObjectManager.Pet.Attack();
                        ObjectManager.Player.SetTarget(currentTarget);
                    }
                }
                else
                    ObjectManager.Pet.Attack();
            }
            if (ShouldLifeTap)
                Spell.Cast("Life Tap");
            if (ShouldWand)
            {
                if (ObjectManager.Player.IsWandEquipped())
                    Spell.StartWand();
                else
                    Spell.Attack();
            }
            else if (ShouldDrainSoul)
                Spell.Cast("Drain Soul");
            else if (ShouldImmolate)
                Spell.Cast("Immolate");
            else if (ShouldCorruption)
                Spell.Cast("Corruption");
            else if (ShouldAgony)
                Spell.Cast("Curse of Agony");
            else if (ObjectManager.Player.IsWandEquipped())
                Spell.StartWand();
            else
                Spell.Cast("Shadow Bolt");
        }

        public void TryToBuffAnotherPlayer(WoWUnit player)
        {

        }
    }
}
