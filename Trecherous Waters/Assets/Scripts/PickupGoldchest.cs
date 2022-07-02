using UnityEngine;

namespace TreacherousWaters
{
    /// <summary>
    /// Handles collection of goldchests and addition of stolen gold to game stats.
    /// </summary>
    public class PickupGoldchest : CollectibleItem
    {
        /// <summary>
        /// The amount of gold assigned to this pickup.
        /// </summary>
        public int goldAmount = 50;

        protected override void OnPickup(Collision collision)
        {
            if (collision.gameObject.layer == 29)
            {
                GameStats.Instance.HandleEarntGold(goldAmount);
            }
            base.OnPickup(collision);
        }
    }
}
