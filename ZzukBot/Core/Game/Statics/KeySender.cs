using System;
using System.Windows.Forms;
using ZzukBot.Core.Mem;

namespace ZzukBot.Core.Game.Statics
{
    /// <summary>
    ///     Class to send key-presses to WoW
    /// </summary>
    public class KeySender
    {
        /// <summary>
        ///     Key-modifiers
        /// </summary>
        public enum KeyModifier
        {
            /// <summary>
            /// None
            /// </summary>
            NONE = 0,
            /// <summary>
            /// System Key Down
            /// </summary>
            WM_SYSKEYDOWN = 260,
            /// <summary>
            /// System Key Up
            /// </summary>
            WM_SYSKEYUP = 261,
            /// <summary>
            /// Key Character
            /// </summary>
            WM_CHAR = 258,
            /// <summary>
            /// Key Down
            /// </summary>
            WM_KEYDOWN = 256,
            /// <summary>
            /// Key Up
            /// </summary>
            WM_KEYUP = 257
        }

        // ReSharper disable once InconsistentNaming
        private static readonly Lazy<KeySender> _instance = new Lazy<KeySender>(() => new KeySender());

        private KeySender()
        {
        }

        /// <summary>
        ///     Access to KeySender
        /// </summary>
        public static KeySender Instance => _instance.Value;

        /// <summary>
        ///     Sends a simple keypress (key down and up)
        /// </summary>
        /// <param name="parKey">The key</param>
        public void SendDownUp(Keys parKey)
        {
            MainThread.Instance.SendDownUp(parKey);
        }

        /// <summary>
        ///     Sends the key
        /// </summary>
        /// <param name="parKey">The key</param>
        /// <param name="parModifier">The modifier (up, down etc.)</param>
        public void Send(Keys parKey, KeyModifier parModifier = KeyModifier.NONE)
        {
            MainThread.Instance.Send(parKey, parModifier);
        }

        /// <summary>
        ///     Send a key down
        /// </summary>
        /// <param name="parKey">Key</param>
        public void SendDown(Keys parKey)
        {
            MainThread.Instance.Send(parKey, KeyModifier.WM_KEYDOWN);
        }

        /// <summary>
        ///     Send a key up
        /// </summary>
        /// <param name="parKey">Key</param>
        public void SendUp(Keys parKey)
        {
            MainThread.Instance.Send(parKey, KeyModifier.WM_KEYUP);
        }
    }
}