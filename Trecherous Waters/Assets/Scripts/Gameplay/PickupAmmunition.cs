using UnityEngine;

namespace TreacherousWaters
{
    public enum AmmunitionType
    {
        Cannonball,
        Chainball,
        Barrel
    }

    /// <summary>
    /// Triggers player to switch ammunition type.
    /// </summary>
    public class PickupAmmunition : CollectibleItem
    {
        /// <summary>
        /// The type of ammunition assigned to the pickup.
        /// </summary>
        [SerializeField] AmmunitionType type;

        protected override void OnPickup(Collision collision)
        {
            if (collision.transform.TryGetComponent<ISwitchAmmunition>(out ISwitchAmmunition ship))
            {
                ship.SwitchAmmunition(type);
            }
            base.OnPickup(collision);
        }
    }
}
