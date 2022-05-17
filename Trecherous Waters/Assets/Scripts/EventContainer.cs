namespace TreacherousWaters
{
    /// <summary>
    /// A container holding custom events.
    /// </summary>
    public static class EventContainer
    {
        public static Delegates.OnGameOver onGameOver;
        public static Delegates.OnUpdateGUIGold OnUpdateGUIGold;
        public static Delegates.OnDestroyedPickup onDestroyedPickup;
        public static Delegates.OnDestroyedMerchant onDestroyedMerchant;
        public static Delegates.OnUpdateGUIBroadside onDestroyedGUIBroadside;
    }
}