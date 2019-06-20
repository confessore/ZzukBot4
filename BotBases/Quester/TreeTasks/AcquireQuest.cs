using System.Linq;
using TreeTaskCore;
using ZzukBot.Core.Game.Frames;
using ZzukBot.Core.Game.Statics;
using static ZzukBot.Core.Constants.Enums;

namespace Quester.TreeTasks
{
    public class AcquireQuest : TTask
    {
        public override int Priority => 100;

        public override bool Activate()
        {
            return ObjectManager.Instance.Units
                .Where(x => x.QuestState == NpcQuestOfferState.OffersQuest)
                .Any();
        }

        public override void Execute()
        {
            var questNPC = ObjectManager.Instance.Units
                .Where(x => x.QuestState == NpcQuestOfferState.OffersQuest)
                .OrderBy(x => x.DistanceToPlayer)
                .FirstOrDefault();
            if (questNPC.DistanceToPlayer < 5f)
            {
                if (GossipFrame.IsOpen)
                {
                    var quests = GossipFrame.Instance.Quests
                        .Where(x => x.State == QuestGossipState.Available);
                    if (quests.Any())
                    {
                        GossipFrame.Instance.AcceptQuest(
                            GossipFrame.Instance.Quests
                            .Where(x => x.State == QuestGossipState.Available)
                            .FirstOrDefault()
                            .Id);
                    }
                }
                else if (QuestGreetingFrame.IsOpen)
                {
                    var quests = QuestGreetingFrame.Instance.Quests
                        .Where(x => x.State == QuestGossipState.Available);
                    if (quests.Any())
                    {
                        QuestGreetingFrame.Instance.AcceptQuest(
                        QuestGreetingFrame.Instance.Quests
                        .Where(x => x.State == QuestGossipState.Available)
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
    }
}
