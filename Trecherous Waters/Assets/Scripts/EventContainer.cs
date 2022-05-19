namespace TreacherousWaters
{
    /// <summary>
    /// A container holding static custom events.
    /// </summary>
    public static class EventContainer
    {
        public static Delegates.OnGameOver onGameOver;

        public static Delegates.OnUpdateGameUI OnUpdateGameUI;
        public static Delegates.OnUpdateCombatUI OnUpdateCombatUI;
        public static Delegates.OnPlayerIntegrityChanged onPlayerIntegrityChanged;

        public static Delegates.OnDestroyedPickup onDestroyedPickup;
        public static Delegates.OnDestroyedMerchant onDestroyedMerchant;
    }
}