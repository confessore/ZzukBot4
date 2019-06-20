using Gatherer.GUI;
using System;
using System.Linq;
using TreeTaskCore;
using ZzukBot.Core.Constants;
using ZzukBot.Core.Framework;
using ZzukBot.Core.Game.Statics;

namespace Gatherer.TreeTasks
{
    public class Pull : TTask
    {
        public override int Priority => 102;

        public void AddPosition()
        {
            CMD.positions.Add(Convert.ToInt32(ObjectManager.Instance.Player.Position.X).ToString()
                            + Convert.ToInt32(ObjectManager.Instance.Player.Position.Y).ToString()
                            + Convert.ToInt32(ObjectManager.Instance.Player.Position.Z).ToString());
        }

        public bool Stuck => CMD.positions.FindAll(x => x.Equals(CMD.positions.Last())).Count() > 20;

        public override bool Activate()
        {
            return !ObjectManager.Instance.Player.IsSwimming && !ObjectManager.Instance.Player.IsInCombat && ObjectManager.Instance.Units.Where(x =>
                !CMD.guidBlacklist.Any(y => y == x.Guid) && !x.IsPlayer && !x.IsDead && !x.IsSwimming
                    && !x.IsPet && !x.TappedByOther
                    && ObjectManager.Instance.Player.InLosWith(x)
                    && x.Reaction == Enums.UnitReaction.Hostile && x.DistanceToPlayer < 20f).Any();
        }

        public override void Execute()
        {
            AddPosition();
            var target = ObjectManager.Instance.Units.Where(x =>
                !CMD.guidBlacklist.Any(y => y == x.Guid) && !x.IsPlayer && !x.IsDead && !x.IsSwimming
                    && !x.IsPet && !x.TappedByOther
                    && ObjectManager.Instance.Player.InLosWith(x)
                    && x.Reaction == Enums.UnitReaction.Hostile && x.DistanceToPlayer < 20f)
                .OrderBy(x => x.DistanceToPlayer).FirstOrDefault();
            if (Stuck)
            {
                CMD.positions.Clear();
                CMD.guidBlacklist.Add(target.Guid);
            }
            else
                CustomClasses.Instance.Current.Pull(target);
            //Common.Instance.DebugMessage("PULL");
        }
    }
}
