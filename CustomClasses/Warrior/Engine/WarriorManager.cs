using System.Collections.Generic;
using System.Linq;
using ZzukBot.Core.Constants;
using ZzukBot.Core.Game.Objects;
using ZzukBot.Core.Game.Statics;

namespace Warrior.Engine
{
    public class WarriorManager
    {
        Common Common { get; }
        Lua Lua { get; }
        Navigation Navigation { get; }
        ObjectManager ObjectManager { get; }
        Spell Spell { get; }

        public WarriorManager(
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
            /*else if (ObjectManager.Player.GotAura("Bloodrage"))
                for (int x = 0; x < 16; x++)
                    Lua.Execute($"CancelPlayerBuff(GetPlayerBuff({x}, 'HELPFUL'))");*/
        }

        public float GetKiteDistance()
        {
            return 20f;
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

            if (!ObjectManager.Player.GotAura("Battle Stance") && Spell.IsReady("Charge")
                && ObjectManager.Player.Rage < 25)
            {
                Spell.Cast("Battle Stance");

                return;
            }

            if (ObjectManager.Player.GotAura("Battle Stance")
                && ObjectManager.Target.DistanceToPlayer > 7
                && ObjectManager.Target.DistanceToPlayer < 26)
                Spell.Cast("Charge");

            Spell.Attack();

            if (target.DistanceToPlayer > GetPullDistance())
                Navigation.Traverse(target.Position);
        }

        public void Rebuff()
        {

        }

        void Rotation()
        {
            Spell.Attack();

            if (Spell.IsReady("Pummel") && ObjectManager.Player.GotAura("Berserker Stance")
                    && ObjectManager.Target.CastingId != 0)
                Spell.Cast("Pummel");

            if (Spell.IsReady("Bloodrage") && ObjectManager.Player.HealthPercent > 10
                && ObjectManager.Target.Level >= ObjectManager.Player.Level - 10)
                Spell.Cast("Bloodrage");

            if (Spell.IsReady("Battle Shout") && !ObjectManager.Player.GotAura("Battle Shout"))
                Spell.Cast("Battle Shout");

            if (Spell.IsReady("Berserker Stance") && !ObjectManager.Player.GotAura("Berserker Stance")
                && ObjectManager.Player.Rage < 25)
                Spell.Cast("Berserker Stance");

            if (Spell.IsReady("Berserker Rage") && ObjectManager.Player.GotAura("Berserker Stance"))
                Spell.Cast("Berserker Rage");

            if (ObjectManager.Player.GotAura("Battle Shout"))
            {
                if (Spell.IsReady("Execute") && ObjectManager.Target.HealthPercent < 20)
                    Spell.Cast("Execute");

                if (!Spell.IsReady("Execute") || ObjectManager.Target.HealthPercent >= 20)
                {
                    if (Spell.IsReady("Bloodthirst"))
                        Spell.Cast("Bloodthirst");

                    if (Spell.IsReady("Mortal Strike"))
                        Spell.Cast("Mortal Strike");

                    if (Spell.IsReady("Whirlwind") && ObjectManager.Player.GotAura("Berserker Stance")
                        && (!Spell.IsReady("Bloodthirst") && (!Spell.IsReady("Mortal Strike")))
                        && (ObjectManager.Player.Rage > 50))
                        Spell.Cast("Whirlwind");
                }
            }

            if (ObjectManager.Player.Rage > 50)
                Spell.Cast("Heroic Strike");
        }

        public void TryToBuffAnotherPlayer(WoWUnit player)
        {

        }
    }
}
