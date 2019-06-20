using System;
using System.Collections.Generic;
using System.Linq;
using ZzukBot.Core.Constants;
using ZzukBot.Core.Game.Objects;
using ZzukBot.Core.Game.Statics;

namespace Rogue.Engine
{
    public class RogueManager
    {
        Common Common { get; }
        Inventory Inventory { get; }
        Lua Lua { get; }
        Navigation Navigation { get; }
        ObjectManager ObjectManager { get; }
        Spell Spell { get; }

        public RogueManager(
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
        readonly int[][] EviscerateDamage = new int[][]
        {
            new int[] {0, 0,0,0,0},
            new int[] {10, 10,15,20,25,30},
            new int[] {20, 22,33,44,55,66},
            new int[] {30, 39,58,77,96,115},
            new int[] {50, 61,92,123,154,185},
            new int[] {60, 60,135,180,225,270},
            new int[] {80, 143,220,297,374,451},
            new int[] {100, 212,322,432,542,652},
            new int[] {150, 295,446,597,748,899},
            new int[] {200, 332,502,672,842,1012},
        };

        private double GetSliceAndDiceDuration()
        {
            try
            {
                Lua.Execute("function getBuffDuration(name) GetSpellForBot = name " +
                "timeleft = -1 for i=0,31 do local id,cancel = GetPlayerBuff(i,'HELPFUL|HARMFUL|PASSIVE') if(name == GetPlayerBuffTexture(id)) then " +
                "timeleft = GetPlayerBuffTimeLeft(id) " +/*DEFAULT_CHAT_FRAME:AddMessage(timeleft) + */" return timeleft end end return timeleft end");
            }
            catch
            {
                return -3;
            }
            try
            {
                string tex = @"Interface\\Icons\\Ability_Rogue_SliceDice";
                Lua.Execute("points = getBuffDuration('" + tex + "')");
                return Convert.ToDouble(Lua.GetText("points"));
            }
            catch
            {
                return -3;
            }
        }

        readonly string[] Poisons = { "Instant Poison", "Instant Poison I", "Instant Poison II", "Instant Poison III", "Instant Poison IV", "Instant Poison V", "Instant Poison VI" };

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
            PossibleTargets = Common.PossibleTargets(possibleTargets);

            if (PossibleTargets.Count() > 0)
            {
                var primaryTarget = PossibleTargets.Contains(PossibleTargets.Where(x => x.IsPlayer).FirstOrDefault()) ?
                    PossibleTargets.Where(x => x.IsPlayer).FirstOrDefault() : PossibleTargets.OrderBy(x => x.HealthPercent).FirstOrDefault();

                if (primaryTarget != null)
                {
                    ObjectManager.Player.SetTarget(primaryTarget);
                    ObjectManager.Player.Face(primaryTarget);

                    if (ObjectManager.Player.CastingId == 0)
                    {
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
            return 5f;
        }

        public bool IsBuffRequired()
        {
            
            if (ObjectManager.Player.IsMainhandEnchanted() && ObjectManager.Player.IsOffhandEnchanted())
                return false;
            if (ObjectManager.Player.CastingId != 0 && ObjectManager.Player.CastingAsName != "Stealth")
                return true;
            if (Inventory.ExistingItems(Poisons).Count > 0)
                return true;
                
            return false;
        }

        public bool IsReadyToFight(IEnumerable<WoWUnit> possibleTargets)
        {
            if (ObjectManager.Player.HealthPercent == 100)
                return true;

            if (ObjectManager.Player.HealthPercent < Common.EatAt)
                return false;

            if (ObjectManager.Player.GotAura("Food"))
                return false;

            return true;
        }

        public void PrepareForFight()
        {
            if (ObjectManager.Player.HealthPercent < Common.EatAt)
                Common.TryUseFood();
        }

        public void Pull(WoWUnit target)
        {
            ObjectManager.Player.SetTarget(target);
            if (ObjectManager.Player.MovementState == Enums.MovementFlags.None)
                ObjectManager.Player.Face(target);

            Stealth();
            if (ObjectManager.Target.DistanceToPlayer >= 20 && Spell.IsKnown("Sprint") && Spell.IsReady("Sprint"))
                Spell.Cast("Sprint");
            if (Spell.IsKnown("Cheap Shot") && Spell.IsReady("Cheap Shot"))
                Spell.Cast("Cheap Shot");
            else
                Spell.Attack();

            if (target.DistanceToPlayer > GetPullDistance())
                Navigation.Traverse(target.Position);
        }

        public void Rebuff()
        {
            if (!ObjectManager.Player.IsMainhandEnchanted())
                Inventory.ExistingItems(Poisons).FirstOrDefault().UseOn(Inventory.GetEquippedItem(Enums.EquipSlot.MainHand));
            if (!ObjectManager.Player.IsOffhandEnchanted())
                Inventory.ExistingItems(Poisons).FirstOrDefault().UseOn(Inventory.GetEquippedItem(Enums.EquipSlot.OffHand));
        }

        void Rotation()
        {
            int Energy = ObjectManager.Player.Energy;
            int ComboPoint = ObjectManager.Player.ComboPoints;
            Spell.Attack();
            if (Spell.IsKnown("Berserking") && Spell.IsReady("Berserking"))
                Spell.Cast("Berserking");
            if (Spell.IsKnown("Blood Fury") && Spell.IsReady("Blood Fury"))
                Spell.Cast("Blood Fury");
            if (PossibleTargets.Where(x => x.TargetGuid == ObjectManager.Player.Guid).Count() >= 2)
            {
                if (Spell.IsKnown("Adrenaline Rush") && Spell.IsReady("Adrenaline Rush"))
                    Spell.Cast("Adrenaline Rush");
                if (Spell.IsKnown("Blood Fury") && Spell.IsReady("Blood Fury"))
                    Spell.Cast("Blood Fury");
                if (Spell.IsKnown("Blade Flurry") && Spell.IsReady("Blade Flurry") && Energy >= 25)
                    Spell.Cast("Blade Flurry");
                if (Spell.IsKnown("Evasion") && Spell.IsReady("Evasion"))
                    Spell.Cast("Evasion");
            }
            if (Spell.IsKnown("Riposte") && Spell.IsReady("Riposte") && Energy >= 10)
                Spell.Cast("Riposte");
            if (Energy <= 20)
                return;
            if (Energy >= 25 && (ObjectManager.Target.CastingId != 0 || ObjectManager.Target.ChannelingId != 0))
            {
                if (Spell.IsKnown("Kick") && Spell.IsReady("Kick"))
                    Spell.Cast("Kick");
            }
            if (Energy >= 35)
            {
                if (Spell.IsKnown("Eviscerate") && Spell.IsReady("Eviscerate"))
                {
                    if (ShouldEviscerate() || ComboPoint == 5)
                    {
                        Spell.Cast("Eviscerate");
                        return;
                    }
                }
            }
            if (Energy >= 25)
            {
                if (Spell.IsKnown("Slice and Dice"))
                {
                    if ((!ObjectManager.Player.GotAura("Slice and Dice") || GetSliceAndDiceDuration() <= 2.0) && !ShouldEviscerate() && ComboPoint > 0)
                    {
                        Spell.Cast("Slice and Dice");
                        return;
                    }
                }
                if (Energy >= 40)
                    Spell.Cast("Sinister Strike");
            }
        }

        bool ShouldEviscerate()
        {
                int rank = Spell.GetSpellRank("Eviscerate");
                int damage = 0;
                int comboPoints = ObjectManager.Player.ComboPoints;
                if (rank >= 1 && comboPoints >= 1)
                {
                    damage = (EviscerateDamage[rank][comboPoints] + EviscerateDamage[rank][0]);
                    if (ObjectManager.Target.Health <= damage)
                        return true;
                }
                return false;
        }

        void Stealth()
        {
            if (Spell.IsKnown("Stealth"))
            {
                if (!ObjectManager.Player.GotAura("Stealth") && Spell.IsReady("Stealth") && ObjectManager.Target.DistanceToPlayer < 25f)
                    Spell.CastWait("Stealth", 500);
            }
        }

        public void TryToBuffAnotherPlayer(WoWUnit player)
        {

        }
    }
}
