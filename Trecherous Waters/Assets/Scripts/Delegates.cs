namespace TreacherousWaters
{
    /// <summary>
    /// A container holding static delegates
    /// </summary>
    public static class Delegates
    {
        public delegate void OnGameOver(bool delayed);
        public delegate void OnUpdateGUIGold(int gold);
        public delegate void OnUpdateGUIBroadside(float port, float starboard, float loadtime);
        public delegate void OnDestroyedPickup(CollectibleItem item);
        public delegate void OnDestroyedMerchant(MerchantShip ship, bool sunk);
    }
}