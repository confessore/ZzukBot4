using System.Collections.Generic;
using System.Linq;
using ZzukBot.Core.Constants;
using ZzukBot.Core.Game.Objects;
using ZzukBot.Core.Game.Statics;

namespace Priest.Engine
{
    public class PriestManager
    {
        Common Common { get; }
        Inventory Inventory { get; }
        Lua Lua { get; }
        Navigation Navigation { get; }
        ObjectManager ObjectManager { get; }
        Spell Spell { get; }

        public PriestManager(
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

        IEnumerable<WoWUnit> PossibleTargets { get; set; }

        public Enums.CombatPosition GetCombatPosition;

        public bool CanBuffAnotherPlayer()
        {
            if (Spell.IsKnown("Power Word: Fortitude"))
            {
                if (ObjectManager.Party1 != null)
                {
                    if (!ObjectManager.Party1.GotAura("Power Word: Fortitude") &&
                        !ObjectManager.Party1.GotDebuff("Ghost"))
                        return true;
                }

                if (ObjectManager.Party2 != null)
                {
                    if (!ObjectManager.Party2.GotAura("Power Word: Fortitude") &&
                        !ObjectManager.Party2.GotDebuff("Ghost"))
                        return true;
                }

                if (ObjectManager.Party3 != null)
                {
                    if (!ObjectManager.Party3.GotAura("Power Word: Fortitude") &&
                        !ObjectManager.Party3.GotDebuff("Ghost"))
                        return true;
                }

                if (ObjectManager.Party4 != null)
                {
                    if (!ObjectManager.Party4.GotAura("Power Word: Fortitude") &&
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
            PossibleTargets = Common.PossibleTargets(possibleTargets);

            if (PossibleTargets.Count() > 0)
            {
                var primaryTarget = PossibleTargets.Contains(PossibleTargets.Where(x => x.IsPlayer).FirstOrDefault()) ?
                    PossibleTargets.Where(x => x.IsPlayer).FirstOrDefault() : PossibleTargets.OrderBy(x => x.HealthPercent).FirstOrDefault();

                if (primaryTarget != null)
                {
                    ObjectManager.Player.SetTarget(primaryTarget);
                    ObjectManager.Player.Face(primaryTarget);
                    
                    if (ObjectManager.Player.CastingId == 0 && Spell.GCDReady("Smite", "Shadow Word: Pain", "Power Word: Fortitude"))
                    {
                        Rebuff();
                        FightHeal();
                        Rotation();

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
            if (Spell.IsKnown("Power Word: Fortitude") && !ObjectManager.Player.GotAura("Power Word: Fortitude"))
                return true;
            if (Spell.IsKnown("Divine Spirit") && !ObjectManager.Player.GotAura("Divine Spirit"))
                return true;
            if (Spell.IsKnown("Shadow Protection") && !ObjectManager.Player.GotAura("Shadow Protection"))
                return true;
            if (Spell.IsKnown("Inner Fire") && !ObjectManager.Player.GotAura("Inner Fire"))
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

        void FightHeal()
        {
            if (ObjectManager.Player.HealthPercent < 80 && Spell.IsKnown("Renew") && !ObjectManager.Player.GotAura("Renew"))
                Spell.Cast("Renew");
            else if (ObjectManager.Player.HealthPercent < 50)
            {
                if (Spell.IsKnown("Flash Heal"))
                    Spell.Cast("Flash Heal");
                else if (Spell.IsKnown("Lesser Heal"))
                    Spell.Cast("Lesser Heal");
            }
        }

        void RestHeal()
        {
            if (ObjectManager.Player.HealthPercent < Common.EatAt && ObjectManager.Player.ManaPercent > Common.DrinkAt &&
                !ObjectManager.Player.GotAura("Drink") && !ObjectManager.Player.GotAura("Food"))
            {
                if (Spell.IsKnown("Flash Heal"))
                {
                    if (ObjectManager.Player.HealthPercent > 70)
                        Spell.Cast("Lesser Heal");
                    else
                        Spell.Cast("Flash Heal");
                }
                else if (Spell.IsKnown("Heal"))
                {
                    if (ObjectManager.Player.HealthPercent > 70)
                        Spell.Cast("Lesser Heal");
                    else
                        Spell.Cast("Heal");
                }
                else if (Spell.IsKnown("Lesser Heal"))
                    Spell.Cast("Lesser Heal");
            }
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

            Rebuff();
            /*if (Spell.IsKnown("Holy Fire") && Spell.IsReady("Holy Fire"))
                Spell.Cast("Holy Fire");
            else*/
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
                if (Spell.IsKnown("Power Word: Fortitude") && !ObjectManager.Player.GotAura("Power Word: Fortitude"))
                    Spell.Cast("Power Word: Fortitude");
                if (Spell.IsKnown("Divine Spirit") && !ObjectManager.Player.GotAura("Divine Spirit"))
                    Spell.Cast("Divine Spirit");
                if (Spell.IsKnown("Shadow Protection") && !ObjectManager.Player.GotAura("Shadow Protection"))
                    Spell.Cast("Shadow Protection");
                if (Spell.IsKnown("Inner Fire") && !ObjectManager.Player.GotAura("Inner Fire"))
                    Spell.Cast("Inner Fire");
            }
            else
            {
                if (Spell.IsKnown("Power Word: Shield") && Spell.IsReady("Power Word: Shield") &&
                    !ObjectManager.Player.GotAura("Power Word: Shield") && !ObjectManager.Player.GotDebuff("Weakened Soul"))
                    Spell.Cast("Power Word: Shield");
                if (Spell.IsKnown("Inner Fire") && !ObjectManager.Player.GotAura("Inner Fire"))
                    Spell.Cast("Inner Fire");
            }
        }

        bool ShouldStopWand { get => !(ObjectManager.Player.ManaPercent < 20 || ObjectManager.Target.HealthPercent < 10) &&
                (Spell.IsKnown("Power Word: Shield") && !ObjectManager.Player.GotAura("Power Word: Shield") && !ObjectManager.Player.GotDebuff("Weakened Soul")) ||
                (ObjectManager.Player.HealthPercent < 80 && Spell.IsKnown("Renew") && !ObjectManager.Player.GotAura("Renew")) ||
                (Spell.IsKnown("Shadow Word: Pain") && !ObjectManager.Target.GotDebuff("Shadow Word: Pain")); }

        void Rotation()
        {
            if (ObjectManager.Player.ManaPercent < 20 || ObjectManager.Target.HealthPercent < 10)
            {
                if (ObjectManager.Player.IsWandEquipped())
                    Spell.StartWand();
                else
                    Spell.Attack();
            }
            else
            {
                if (Spell.IsKnown("Shadow Word: Pain") && !ObjectManager.Target.GotDebuff("Shadow Word: Pain"))
                    Spell.Cast("Shadow Word: Pain");
                else if (Spell.IsKnown("Mind Blast") && Spell.IsReady("Mind Blast"))
                {
                    if (Spell.IsKnown("Inner Focus") && Spell.IsReady("Inner Focus"))
                        Spell.Cast("Inner Focus");
                    Spell.Cast("Mind Blast");
                }
                else if (ObjectManager.Player.IsWandEquipped())
                    Spell.StartWand();
                else
                    Spell.Cast("Smite");
            }
        }

        public void TryToBuffAnotherPlayer(WoWUnit player)
        {
            player = PartyMembers.Where(x => NotNullOrDead(x) && !x.GotAura("Power Word: Fortitude")).FirstOrDefault();

            if (player.DistanceToPlayer > 29f)
                Navigation.Traverse(player.Position);
            else
            {
                ObjectManager.Player.SetTarget(player);
                Spell.Cast("Power Word: Fortitude");
                ObjectManager.Player.SetTarget(null);
            }
        }

        bool NotNullOrDead(WoWUnit partyMember) => partyMember != null && !partyMember.IsDead;

        List<WoWUnit> PartyMembers =>
            new List<WoWUnit>()
            {
                ObjectManager.PartyLeader,
                ObjectManager.Party1,
                ObjectManager.Party2,
                ObjectManager.Party3,
                ObjectManager.Party4,
            };
    }
}
