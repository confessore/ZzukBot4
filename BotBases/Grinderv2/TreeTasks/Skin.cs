using System.Collections.Generic;
using System.Linq;
using TreeTaskCore;
using ZzukBot.Core.Constants;
using ZzukBot.Core.Game.Objects;
using ZzukBot.Core.Game.Statics;

namespace Grinderv2.TreeTasks
{
    public class Skin : TTask
    {
        public override int Priority => 140;

        public List<WoWUnit> SkinnableUnits()
        {
            return ObjectManager.Instance.Units.Where(x => x.IsDead && x.IsSkinnable &&
            (Skills.Instance.GetAllPlayerSkills().Where(y => y.Id == Enums.Skills.SKINNING).Any() ?
            Skills.Instance.GetAllPlayerSkills().Where(y => y.Id == Enums.Skills.SKINNING).FirstOrDefault().CurrentLevel >= x.RequiredSkinningLevel : false) &&
            (Settings.NinjaSkin ? (x.TappedByMe || x.TappedByOther) : x.TappedByMe)).ToList();
        }

        public override bool Activate()
        {
            return Settings.CorpseSkin && Inventory.Instance.CountFreeSlots(false) > 0 && SkinnableUnits().Any();
        }

        public override void Execute()
        {
            var target = SkinnableUnits().FirstOrDefault();
            if (target.DistanceToPlayer > 5f)
                Navigation.Instance.Traverse(target.Position);
            else
                target.Interact(true);
        }
    }
}
