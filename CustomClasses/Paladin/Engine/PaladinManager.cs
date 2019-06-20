using System.Collections.Generic;
using System.Linq;
using ZzukBot.Core.Constants;
using ZzukBot.Core.Game.Objects;
using ZzukBot.Core.Game.Statics;

namespace Paladin.Engine
{
    public class PaladinManager
    {
        Common Common { get; }
        Lua Lua { get; }
        Navigation Navigation { get; }
        ObjectManager ObjectManager { get; }
        Spell Spell { get; }

        public PaladinManager(
            Common common,
            Lua lua,
            Navigation navigation,
            ObjectManager objectManager,
            Spell spell)
        {
            Common = common;
            Lua = lua;
            Navigation = navigation;
            ObjectManager = objectManager;
            Spell = spell;
        }

        IEnumerable<WoWUnit> PossibleTargets { get; set; }

        public Enums.CombatPosition GetCombatPosition;

        void BuffAura()
        {
            if (Spell.CanCast("Sanctity Aura"))
            {
                if (!ObjectManager.Player.GotAura("Sanctity Aura"))
                    Spell.Cast("Sanctity Aura");
            }
            else if (Spell.CanCast("Retribution Aura"))
            {
                if (!ObjectManager.Player.GotAura("Retribution Aura"))
                    Spell.Cast("Retribution Aura");
            }
            else if (Spell.CanCast("Devotion Aura"))
            {
                if (!ObjectManager.Player.GotAura("Devotion Aura"))
                    Spell.Cast("Devotion Aura");
            }
        }

        public void BuffBlessing()
        {
            if (ObjectManager.Player.ManaPercent >= 30)
            {
                if (Spell.CanCast("Blessing of Wisdom"))
                {
                    if (!ObjectManager.Player.GotAura("Blessing of Wisdom"))
                        Spell.Cast("Blessing of Wisdom");
                }
                else if (Spell.CanCast("Blessing of Might"))
                {
                    if (!ObjectManager.Player.GotAura("Blessing of Might"))
                        Spell.Cast("Blessing of Might");
                }
            }
        }

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

        void Emergency()
        {
            if (!Spell.CanCast("Hammer of Justice") && !ObjectManager.Target.GotDebuff("Hammer of Justice"))
            {
                if (Spell.CanCast("Lay on Hands") && ObjectManager.Player.HealthPercent <= 15)
                {
                    Spell.StopCasting();
                    Spell.Cast("Lay on Hands");
                }
                else if (Spell.CanCast("Divine Shield") && !ObjectManager.Player.GotDebuff("Forbearance") && ObjectManager.Player.HealthPercent <= 20)
                {
                    Spell.StopCasting();
                    Spell.Cast("Divine Shield");
                }
                else if (Spell.CanCast("Blessing of Protection") && !ObjectManager.Player.GotDebuff("Forbearance") && ObjectManager.Player.HealthPercent <= 25)
                {
                    Spell.StopCasting();
                    Spell.Cast("Blessing of Protection");
                }
                else if (Spell.CanCast("Divine Protection") && !ObjectManager.Player.GotDebuff("Forbearance") && ObjectManager.Player.HealthPercent <= 30)
                {
                    Spell.StopCasting();
                    Spell.Cast("Divine Protection");
                }
            }
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

                    Emergency();
                    if (ObjectManager.Player.CastingId == 0)
                    {
                        if (Spell.GCDReady("Retribution Aura", "Devotion Aura", "Holy Light"))
                        {
                            FightHeal();
                            Rebuff();
                            Rotation();
                        }

                        if (primaryTarget.DistanceToPlayer > GetPullDistance())
                            Navigation.Traverse(primaryTarget.Position);
                    }
                }
            }
        }

        void FightHeal()
        {
            if (ObjectManager.Player.HealthPercent <= 50)
            {
                if (Spell.CanCast("Hammer of Justice") && ObjectManager.Target.HealthPercent > 20)
                    Spell.Cast("Hammer of Justice");
                Spell.Cast("Holy Light");
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
            return 3;
        }

        public bool IsBuffRequired()
        {
            return false;
        }

        public bool IsReadyToFight(IEnumerable<WoWUnit> possibleTargets)
        {
            if (ObjectManager.Player.HealthPercent == 100 && ObjectManager.Player.ManaPercent == 100)
                return true;

            if (ObjectManager.Player.HealthPercent < Common.EatAt || ObjectManager.Player.ManaPercent < Common.DrinkAt)
                return false;

            if (ObjectManager.Player.GotAura("Food") || ObjectManager.Player.GotAura("Drink"))
                return false;

            return true;
        }

        public void PrepareForFight()
        {
            RestHeal();
            RestRegen();
        }

        public void Pull(WoWUnit target)
        {
            ObjectManager.Player.SetTarget(target);
            if (ObjectManager.Player.MovementState == Enums.MovementFlags.None)
                ObjectManager.Player.Face(target);

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
            BuffAura();
            BuffBlessing();
        }

        void RestHeal()
        {
            if (ObjectManager.Player.ManaPercent > Common.DrinkAt && !ObjectManager.Player.GotAura("Drink") && !ObjectManager.Player.GotAura("Food"))
            {
                if (ObjectManager.Player.HealthPercent > Common.EatAt && ObjectManager.Player.HealthPercent < 90 && Spell.IsKnown("Flash of Light"))
                    Spell.Cast("Flash of Light");
                else if (ObjectManager.Player.HealthPercent < Common.EatAt)
                    Spell.Cast("Holy Light");
            }
        }

        void RestRegen()
        {
            if (ObjectManager.Player.ManaPercent < Common.DrinkAt && !ObjectManager.Player.GotAura("Drink"))
                Common.TryUseDrink();
            if (ObjectManager.Player.HealthPercent < Common.EatAt && ObjectManager.Player.ManaPercent < Common.DrinkAt && !ObjectManager.Player.GotAura("Food"))
                Common.TryUseFood();
        }

        void Rotation()
        {
            Spell.Attack();

            if (ObjectManager.Player.ManaPercent <= 30 && Spell.CanCast("Seal of Wisdom") &&
                !ObjectManager.Player.GotAura("Seal of Righteousness") && !ObjectManager.Player.GotAura("Seal of Command") && !ObjectManager.Player.GotAura("Seal of Wisdom"))
                Spell.Cast("Seal of Wisdom");
            else if (ObjectManager.Player.ManaPercent >= 30)
            {
                if (!Spell.CanCast("Seal of the Crusader") && !ObjectManager.Player.GotAura("Seal of Righteousness"))
                    Spell.Cast("Seal of Righteousness");
                else if (!Spell.CanCast("Seal of Wisdom"))
                {
                    if (!ObjectManager.Player.GotAura("Seal of the Crusader") && !ObjectManager.Target.GotDebuff("Judgement of the Crusader"))
                        Spell.Cast("Seal of the Crusader");
                    else if (!Spell.CanCast("Seal of Command") && !ObjectManager.Player.GotAura("Seal of Righteousness") && ObjectManager.Target.GotDebuff("Judgement of the Crusader"))
                        Spell.Cast("Seal of Righteousness");
                    else if (Spell.CanCast("Seal of Command") && !ObjectManager.Player.GotAura("Seal of Command") && ObjectManager.Target.GotDebuff("Judgement of the Crusader"))
                        Spell.Cast("Seal of Command");
                }
                else if (Spell.CanCast("Seal of Wisdom"))
                {
                    if (!ObjectManager.Player.GotAura("Seal of Wisdom") && !ObjectManager.Target.GotDebuff("Judgement of Wisdom"))
                        Spell.Cast("Seal of Wisdom");
                    else if (!Spell.CanCast("Seal of Command") && !ObjectManager.Player.GotAura("Seal of Righteousness") && ObjectManager.Target.GotDebuff("Judgement of Wisdom"))
                        Spell.Cast("Seal of Righteousness");
                    else if (Spell.CanCast("Seal of Command") && !ObjectManager.Player.GotAura("Seal of Command") && ObjectManager.Target.GotDebuff("Judgement of Wisdom"))
                        Spell.Cast("Seal of Command");
                }
                if (Spell.CanCast("Judgement") && ObjectManager.Player.GotAura("Seal of Righteousness") || ObjectManager.Player.GotAura("Seal of the Crusader") ||
                    ObjectManager.Player.GotAura("Seal of Command") || ObjectManager.Player.GotAura("Seal of Wisdom"))
                    Spell.Cast("Judgement");
                if (Spell.CanCast("Consecration") && ObjectManager.Player.ManaPercent >= 50)
                    Spell.Cast("Consecration");
                if (Spell.CanCast("Hammer of Wrath") && ObjectManager.Target.HealthPercent <= 20)
                    Spell.Cast("Hammer of Wrath");
            }
        }

        public void TryToBuffAnotherPlayer(WoWUnit player)
        {

        }
    }
}
