namespace TreacherousWaters
{
    /// <summary>
    /// A container holding custom events.
    /// </summary>
    public static class EventContainer
    {
        public static Delegates.OnGameOver onGameOver;
        public static Delegates.OnDestroyedPickup onDestroyedPickup;
        public static Delegates.OnDestroyedMerchant onDestroyedMerchant;
    }
}