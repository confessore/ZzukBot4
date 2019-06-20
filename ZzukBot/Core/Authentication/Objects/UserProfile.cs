namespace ZzukBot.Core.Authentication.Objects
{
    /// <summary>
    ///     The model for an authenticated user
    /// </summary>
    public static class UserProfile
    {
        /// <summary>
        ///     The type of subscription (full or trial)
        /// </summary>
        public static uint SubscriptionType { get; set; }
        /// <summary>
        ///     The id of the user
        /// </summary>
        public static string Id { get; set; }
        /// <summary>
        ///     The username of the user
        /// </summary>
        public static string UserName { get; set; }
        /// <summary>
        ///     The subscription expiration of the user
        /// </summary>
        public static string SubscriptionExpiration { get; set; }
    }
}
