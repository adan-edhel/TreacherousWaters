using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TreacherousWaters
{
    public class PickupGoldchest : CollectibleItem
    {
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
