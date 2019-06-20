using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TreeTaskCore;
using ZzukBot.Core.Framework;
using ZzukBot.Core.Game.Objects;
using ZzukBot.Core.Game.Statics;
using static ZzukBot.Core.Constants.Enums;

namespace Quester.TreeTasks
{
    public class ExecuteQuest : TTask
    {
        Quest Quest { get; set; }

        public override int Priority => 90;

        public override bool Activate()
        {
            return QuestLog.Instance.Quests
                .Where(x => x.State == QuestState.InProgress)
                .Count() > 0;
        }

        public override void Execute()
        {
            if (QuestExecutable(out string path))
            {
                Common.Instance.DebugMessage($"{Quest.Id} - {Quest.Name}");
                var targets = ObjectManager.Instance.Units
                    .Where(x => ObjectManager.Instance.Player.InLosWith(x)
                        && !x.IsDead
                        && Quest.Targets.Where(y => y == x.Name || y == x.Id.ToString())
                    .Any());
                var gameObjects = ObjectManager.Instance.GameObjects
                    .Where(x => 
                        Quest.Targets.Where(y => y == x.Name || y == x.Id.ToString())
                    .Any());
                if (targets != null && targets.Any())
                {
                    var target = targets.OrderBy(x => x.DistanceToPlayer).FirstOrDefault();

                        Common.Instance.DebugMessage("pulling");
                        CustomClasses.Instance.Current.Pull(target);

                }
                else if (gameObjects != null && gameObjects.Any())
                {
                    var gameObject = gameObjects.OrderBy(x => x.Position.DistanceToPlayer()).FirstOrDefault();
                    if (gameObject.Position.DistanceToPlayer() > 5f)
                        Navigation.Instance.Traverse(gameObject.Position);
                    else
                        gameObject.Interact(true);
                }
                else
                    Navigation.Instance.Traverse(Quest.Locations.FirstOrDefault());
            }
            else
                Common.Instance.DebugMessage($"failed to execute {Quest.Id} - {Quest.Name}");
        }

        bool QuestExecutable(out string path)
        {
            var quest = QuestLog.Instance.Quests
                .Where(x =>
                    x.State == QuestState.InProgress)
                .FirstOrDefault();
            path = Paths.Quests + $"\\{quest.Id} - {quest.Name}";
            if (!File.Exists(path))
            {
                var json =
                    JsonConvert.SerializeObject(new Quest
                    {
                        Id = quest.Id,
                        Name = quest.Name,
                        Objectives = quest.Objectives.Any()
                            ? quest.Objectives
                            : null,
                        Targets = quest.Objectives.Any()
                            ? quest.Objectives
                            .Select(x => x.ObjectId.ToString())
                            .ToList()
                            : null,
                        Locations = null,
                        TurnIn = null
                    }, Formatting.Indented);
                File.WriteAllText(path, json);
            }
            Quest = JsonConvert.DeserializeObject<Quest>(File.ReadAllText(path));
            return Quest.Locations != null && Quest.Locations.Any()
                && Quest.Targets != null && Quest.Targets.Any()
                && Quest.TurnIn != null;
        }
    }
}
