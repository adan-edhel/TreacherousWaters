namespace TreacherousWaters
{
    /// <summary>
    /// A container holding static custom events.
    /// </summary>
    public static class EventContainer
    {
        /// <summary>
        /// Event to fire when game is over.
        /// </summary>
        public static Delegates.OnGameOver onGameOver;

        /// <summary>
        /// Event to fire when gold is collected.
        /// </summary>
        public static Delegates.OnUpdateGameUI OnUpdateGameUI;
        /// <summary>
        /// Event to fire to update combat UI elements.
        /// </summary>
        public static Delegates.OnUpdateCombatUI OnUpdateCombatUI;
        /// <summary>
        /// Event to fire to update ship integrity on the UI.
        /// </summary>
        public static Delegates.OnPlayerIntegrityChanged onPlayerIntegrityChanged;

        /// <summary>
        /// Event to fire when a pickup is destroyed.
        /// </summary>
        public static Delegates.OnDestroyedPickup onDestroyedPickup;
        /// <summary>
        /// Event to fire when a merchant ship is destroyed.
        /// </summary>
        public static Delegates.OnDestroyedMerchant onDestroyedMerchant;
    }
}