namespace TreacherousWaters
{
    /// <summary>
    /// A container holding static custom delegates.
    /// </summary>
    public static class Delegates
    {
        public delegate void OnGameOver(bool delayed);

        public delegate void OnUpdateGameUI(int gold);
        public delegate void OnPlayerIntegrityChanged(float integrity, float maxIntegrity);
        public delegate void OnUpdateCombatUI(float[] loads, float loadtime, AmmunitionType type);

        public delegate void OnDestroyedPickup(CollectibleItem item);
        public delegate void OnDestroyedMerchant(MerchantShip ship, bool sunk);
    }
}