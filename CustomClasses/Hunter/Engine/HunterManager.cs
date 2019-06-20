using System.Collections.Generic;
using System.Linq;
using ZzukBot.Core.Constants;
using ZzukBot.Core.Game.Objects;
using ZzukBot.Core.Game.Statics;

namespace Hunter.Engine
{
    public class HunterManager
    {
        Common Common { get; }
        Inventory Inventory { get; }
        Lua Lua { get; }
        Navigation Navigation { get; }
        ObjectManager ObjectManager { get; }
        Spell Spell { get; }

        public HunterManager(
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
            WoWEventHandler.Instance.OnErrorMessage += (sender, args) => HandleErrorMessage(args);
        }

        IList<WoWUnit> PossibleTargets { get; set; }

        readonly string[] Arrows =
        {
            "Rough Arrow", "Sharp Arrow", "Razor Arrow", "Feathered Arrow", "Precision Arrow",
            "Jagged Arrow", "Ice Threaded Arrow", "Thorium Headed Arrow", "Doomshot"
        };

        readonly string[] Bullets =
        {
            "Light Shot", "Flash Pellet", "Crafted Light Shot", "Heavy Shot", "Smooth Pebble",
            "Crafted Heavy Shot", "Solid Shot", "Crafted Solid Shot", "Exploding Shot", "Hi-Impact Mithril Slugs",
            "Accurate Slugs", "Mithril Gyro-Shot", "Rockshard Pellets", "Ice Threaded Bullet", "Thorium Shells",
            "Miniature Cannon Balls"
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
                    if (ObjectManager.Player.MovementState == Enums.MovementFlags.None)
                        ObjectManager.Player.Face(primaryTarget);
                    if (!ObjectManager.Player.IsCasting && !ObjectManager.Player.IsChanneling)
                    {
                        if (Spell.GCDReady("Raptor Strike", "Serpent Sting", "Aspect of the Monkey"))
                        {
                            Rebuff();
                            FightHeal();
                            Rotation();
                        }

                        /*if (primaryTarget.DistanceToPlayer > GetPullDistance())
                            Navigation.Traverse(primaryTarget.Position);*/
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
            if (ShouldMelee) return 4f;
            else return 34f;
        }

        public bool IsBuffRequired()
        {
            if (AmmoDepleted)
                return true;
            if (ObjectManager.Player.HasPet)
            {
                if (ObjectManager.Pet.IsDead)
                    return true;
            }
            return false;
        }

        void FightHeal()
        {
            if (ObjectManager.Player.HasPet && !ObjectManager.Pet.IsDead)
            {
                if (ObjectManager.Pet.HealthPercent < Common.EatAt && ObjectManager.Player.ManaPercent > Common.DrinkAt &&
                    Spell.IsKnown("Mend Pet") && ObjectManager.Pet.GotAura("Mend Pet"))
                    Spell.Cast("Mend Pet");
            }
        }

        public bool IsReadyToFight(IEnumerable<WoWUnit> possibleTargets)
        {
            if (ObjectManager.Player.HasPet && !ObjectManager.Pet.IsDead)
            {
                if ((!ObjectManager.Pet.IsHappy() && Common.HavePetFood) || ObjectManager.Pet.GotAura("Feed Pet Effect"))
                    return false;
                if (ObjectManager.Pet.HealthPercent < Common.EatAt)
                    return false;
            }

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
            if (!ObjectManager.Player.IsCasting && !ObjectManager.Player.IsChanneling)
            {
                if (ObjectManager.Player.HasPet && !ObjectManager.Pet.IsDead)
                {
                    if (!ObjectManager.Pet.IsHappy() && Common.HavePetFood && !ObjectManager.Pet.GotAura("Feed Pet Effect"))
                        Common.TryFeedPet();
                    if (ObjectManager.Pet.HealthPercent < Common.EatAt  && ObjectManager.Player.ManaPercent > Common.DrinkAt &&
                        Spell.IsKnown("Mend Pet") && ObjectManager.Pet.GotAura("Mend Pet"))
                        Spell.Cast("Mend Pet");
                }

                if (ObjectManager.Player.HealthPercent < Common.EatAt)
                    Common.TryUseFood();
                if (ObjectManager.Player.ManaPercent < Common.DrinkAt)
                    Common.TryUseDrink();
            }
        }

        public bool ShouldMelee { get; set; } = false;

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
                if (AmmoDepleted)
                    Lua.Execute("Quit();");
                if (ObjectManager.Player.HasPet)
                {
                    if (ObjectManager.Pet.IsDead)
                        Spell.Cast("Revive Pet");
                }
            }
        }

        void Rotation()
        {
            if (!ObjectManager.Player.HasPet || ObjectManager.Pet.IsDead)
            {
                if (ObjectManager.Target.DistanceToPlayer > 11f)
                {
                    ShouldMelee = false;
                    if (Spell.IsKnown("Hunter's Mark") && !ObjectManager.Target.GotDebuff("Hunter's Mark"))
                        Spell.Cast("Hunter's Mark");
                    if (Spell.IsKnown("Concussive Shot") && Spell.IsReady("Concussive Shot") && !ObjectManager.Player.IsInCombat)
                        Spell.Cast("Concussive Shot");
                    if (Spell.IsKnown("Serpent Sting") && !ObjectManager.Target.GotDebuff("Serpent Sting"))
                        Spell.Cast("Serpent Sting");
                    else if (Spell.IsKnown("Arcane Shot") && Spell.IsReady("Arcane Shot"))
                        Spell.Cast("Arcane Shot");
                    else
                        Spell.StartRangedAttack();
                }
                else
                {
                    ShouldMelee = true;
                    if (Spell.IsKnown("Aspect of the Monkey") && !ObjectManager.Player.GotAura("Aspect of the Monkey"))
                        Spell.Cast("Aspect of the Monkey");
                    if (Spell.IsReady("Raptor Strike"))
                        Spell.Cast("Raptor Strike");
                    else
                        Spell.Attack();
                }
            }
            else
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
                if (ObjectManager.Target.DistanceToPlayer > 11f)
                {
                    ShouldMelee = false;
                    if (Spell.IsKnown("Aspect of the Hawk") && !ObjectManager.Player.GotAura("Aspect of the Hawk"))
                        Spell.Cast("Aspect of the Hawk");
                    if (Spell.IsKnown("Hunter's Mark") && !ObjectManager.Target.GotDebuff("Hunter's Mark"))
                        Spell.Cast("Hunter's Mark");
                    if (Spell.IsKnown("Rapid Fire") && Spell.IsReady("Rapid Fire") && ObjectManager.Player.IsInCombat)
                        Spell.Cast("Rapid Fire");
                    if (Spell.IsKnown("Intimidation") && Spell.IsReady("Intimidation"))
                        Spell.Cast("Intimidation");
                    if (Spell.IsKnown("Bestial Wrath") && Spell.IsReady("Bestial Wrath"))
                        Spell.Cast("Bestial Wrath");
                    if (Spell.IsKnown("Serpent Sting") && !ObjectManager.Target.GotDebuff("Serpent Sting"))
                        Spell.Cast("Serpent Sting");
                    else if (Spell.IsKnown("Arcane Shot") && Spell.IsReady("Arcane Shot"))
                        Spell.Cast("Arcane Shot");
                    else
                        Spell.StartRangedAttack();
                }
                else if (ObjectManager.Target.DistanceToPlayer < 11f && ObjectManager.Target.TargetGuid == ObjectManager.Player.Guid)
                {
                    ShouldMelee = true;
                    if (Spell.IsReady("Raptor Strike"))
                        Spell.Cast("Raptor Strike");
                    else
                        Spell.Attack();
                }
                else
                {
                    ShouldMelee = false;
                    Navigation.Backup(12f);
                }
            }
        }

        public void TryToBuffAnotherPlayer(WoWUnit player)
        {

        }

        bool AmmoDepleted
        {
            get
            {
                if (Inventory.ItemEquipped(Enums.EquipSlot.Ranged) && !ObjectManager.Player.IsInCombat)
                {
                    if (Inventory.GetEquippedItem(Enums.EquipSlot.Ranged).Subclass == Enums.ItemSubclass.Gun)
                    {
                        if (!Inventory.ExistingItems(Bullets).Any())
                            return true;
                    }
                    else
                    {
                        if (!Inventory.ExistingItems(Arrows).Any())
                            return true;
                    }
                }
                return false;
            }
        }

        void HandleErrorMessage(WoWEventHandler.OnUiMessageArgs args)
        {
            if (args.Message == "Ammo needs to be in the paper doll ammo slot before it can be fired")
            {
                if (Inventory.GetEquippedItem(Enums.EquipSlot.Ranged).Subclass == Enums.ItemSubclass.Gun)
                {
                    if (Inventory.ExistingItems(Bullets).Any())
                    {
                        var bullets = Inventory.FirstExistingItemWithPosition(Bullets);
                        Inventory.Equip(bullets.Item2, bullets.Item3);
                    }
                }
                else
                {
                    if (Inventory.ExistingItems(Arrows).Any())
                    {
                        var arrows = Inventory.FirstExistingItemWithPosition(Arrows);
                        Inventory.Equip(arrows.Item2, arrows.Item3);
                    }
                }
            }
        }
    }
}
