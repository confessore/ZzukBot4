using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TreeTaskCore;
using ZzukBot.Core.Game.Objects;
using ZzukBot.Core.Game.Statics;
using static ZzukBot.Core.Constants.Enums;

namespace Quester.TreeTasks
{
    public class GoToTurnIn : TTask
    {
        Quest Quest { get; set; }

        public override int Priority => 80;

        public override bool Activate()
        {
            return QuestLog.Instance.Quests.Where(x => x.State == QuestState.Completed).Any();
        }

        public override void Execute()
        {
            if (QuestExecutable(out string path))
                Navigation.Instance.Traverse(Quest.TurnIn);
            else
                Common.Instance.DebugMessage($"failed to go to {Quest.Id} - {Quest.Name}");
        }

        bool QuestExecutable(out string path)
        {
            var quest = QuestLog.Instance.Quests
                .Where(x =>
                    x.State == QuestState.InProgress
                    || x.State == QuestState.Completed)
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
