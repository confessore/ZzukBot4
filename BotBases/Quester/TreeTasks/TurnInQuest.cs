using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TreeTaskCore;
using ZzukBot.Core.Game.Frames;
using ZzukBot.Core.Game.Objects;
using ZzukBot.Core.Game.Statics;
using static ZzukBot.Core.Constants.Enums;

namespace Quester.TreeTasks
{
    public class TurnInQuest : TTask
    {
        Quest Quest { get; set; }

        public override int Priority => 100;

        public override bool Activate()
        {
            return ObjectManager.Instance.Units
                .Where(x => x.QuestState == NpcQuestOfferState.CanTurnIn)
                .Any();
        }

        public override void Execute()
        {
            if (QuestExecutable(out string path))
            {
                var questNPC = ObjectManager.Instance.Units
                .Where(x => x.QuestState == NpcQuestOfferState.CanTurnIn)
                .FirstOrDefault();
                if (questNPC.DistanceToPlayer < 5f)
                {
                    if (GossipFrame.IsOpen)
                    {
                        var quests = GossipFrame.Instance.Quests
                            .Where(x => x.State == QuestGossipState.Completeable);
                        if (quests.Any())
                        {
                            GossipFrame.Instance.CompleteQuest(
                                GossipFrame.Instance.Quests
                                .Where(x => x.State == QuestGossipState.Completeable)
                                .FirstOrDefault()
                                .Id);
                        }
                    }
                    else if (QuestGreetingFrame.IsOpen)
                    {
                        var quests = QuestGreetingFrame.Instance.Quests
                            .Where(x => x.State == QuestGossipState.Completeable);
                        if (quests.Any())
                        {
                            QuestGreetingFrame.Instance.CompleteQuest(
                            QuestGreetingFrame.Instance.Quests
                            .Where(x => x.State == QuestGossipState.Completeable)
                            .FirstOrDefault()
                            .Id);
                        }
                    }
                    else if (QuestFrame.IsOpen)
                    {
                        switch (QuestFrame.Instance.QuestFrameState)
                        {
                            case QuestFrameState.Complete:
                                QuestFrame.Instance.Complete();
                                break;
                            default:
                                QuestFrame.Instance.Proceed();
                                break;
                        }
                    }
                    else
                        questNPC.Interact(true);
                }
                else
                    Navigation.Instance.Traverse(questNPC.Position);
            }
            else
                Common.Instance.DebugMessage($"failed to turn in {Quest.Id} - {Quest.Name}");
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
