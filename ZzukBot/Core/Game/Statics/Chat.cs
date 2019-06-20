using System;
using System.Collections.ObjectModel;

namespace ZzukBot.Core.Game.Statics
{
    /// <summary>
    ///     Everything to do with Chat and Chat Messages
    /// </summary>
    public sealed class Chat
    {
        static readonly Lazy<Chat> instance = new Lazy<Chat>(() => new Chat());

        /// <summary>
        ///     Creates a new instance of Chat
        /// </summary>
        public Chat()
        {
            ChatMessages = new ObservableCollection<WoWEventHandler.ChatMessageArgs>();
            WoWEventHandler.Instance.OnChatMessage += (sender, args) => OnChatMessage(args);
        }

        /// <summary>
        ///     Access to the Common instance
        /// </summary>
        public static Chat Instance => instance.Value;

        /// <summary>
        ///     All chat messages
        /// </summary>
        public ObservableCollection<WoWEventHandler.ChatMessageArgs> ChatMessages { get; set; }

        void OnChatMessage(WoWEventHandler.ChatMessageArgs args)
        {
            ChatMessages.Add(args);
        }
    }
}
