using ZzukBot.Core.Constants;

namespace ZzukBot.Core.Game.Frames.FrameObjects
{
    /// <summary>
    ///     Representing an objective of a quest (Kill 8/8 boars etc.)
    /// </summary>
    public class QuestObjective
    {
        /// <summary>
        /// Creates a new objective for a quest
        /// </summary>
        /// <param name="parType"></param>
        /// <param name="parObjectId"></param>
        /// <param name="parObjectsRequired"></param>
        /// <param name="parProgress"></param>
        /// <param name="parIsDone"></param>
        public QuestObjective(Enums.QuestObjectiveTypes parType, int parObjectId,
            int parObjectsRequired, int parProgress, bool parIsDone)
        {
            Type = parType;
            ObjectId = parObjectId;
            ObjectsRequired = parObjectsRequired;
            Progress = parProgress;
            IsDone = parIsDone;
        }

        /// <summary>
        ///     Is the objective done?
        /// </summary>
        public readonly bool IsDone;

        /// <summary>
        ///     The ID of the item to gather / unit to kill
        /// </summary>
        public readonly int ObjectId;

        /// <summary>
        ///     The number of objects required
        /// </summary>
        public readonly int ObjectsRequired;

        /// <summary>
        ///     How many objects we already killed / collected
        /// </summary>
        public readonly int Progress;

        /// <summary>
        ///     The objective-type
        /// </summary>
        public readonly Enums.QuestObjectiveTypes Type;
    }
}