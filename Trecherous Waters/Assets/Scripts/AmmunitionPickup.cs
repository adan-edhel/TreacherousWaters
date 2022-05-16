using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TreacherousWaters
{
    public enum AmmunitionType
    {
        Cannonball,
        Chainball,
        Barrel
    }

    public class AmmunitionPickup : CollectibleItem
    {
        [SerializeField] AmmunitionType type;

        protected override void OnPickup(Collision collision)
        {
            if (collision.transform.TryGetComponent<ISwitchAmmo>(out ISwitchAmmo ship))
            {
                ship.SwitchAmmunition(type);
            }
            base.OnPickup(collision);
        }
    }
}
