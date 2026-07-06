namespace TreacherousWaters
{
    /// <summary>
    /// A container holding static custom delegates.
    /// </summary>
    public static class Delegates
    {
        /// <summary>
        /// Event to fire when game is over.
        /// </summary>
        /// <param name="delayed"></param>
        public delegate void OnGameOver(bool delayed);

        /// <summary>
        /// Event to fire when gold is collected.
        /// </summary>
        /// <param name="gold"></param>
        public delegate void OnUpdateGameUI(int gold);
        /// <summary>
        /// Event to fire to update ship integrity on the UI.
        /// </summary>
        /// <param name="integrity"></param>
        /// <param name="maxIntegrity"></param>
        public delegate void OnPlayerIntegrityChanged(float integrity, float maxIntegrity);
        /// <summary>
        /// Event to fire to update combat UI elements.
        /// </summary>
        /// <param name="loads"></param>
        /// <param name="loadtime"></param>
        /// <param name="type"></param>
        public delegate void OnUpdateCombatUI(float[] loads, float loadtime, AmmunitionType type);

        /// <summary>
        /// Event to fire when a pickup is destroyed.
        /// </summary>
        /// <param name="item"></param>
        public delegate void OnDestroyedPickup(CollectibleItem item);
        /// <summary>
        /// Event to fire when a merchant ship is destroyed.
        /// </summary>
        /// <param name="ship"></param>
        /// <param name="sunk"></param>
        public delegate void OnDestroyedMerchant(MerchantShip ship, bool sunk);
    }
}