using System.Collections;
using UnityEngine;

namespace TreacherousWaters
{
    public enum Broadside
    {
        port,
        starboard
    }

    /// <summary>
    /// Handles base ship combat functions.
    /// </summary>
    public class ShipCombat : MonoBehaviour, IFire, ISwitchAmmunition
    {
        /// <summary>
        /// Transform where barrels are instantiated.
        /// </summary>
        [SerializeField] Transform barrelDropPoint;
        /// <summary>
        /// Array of transforms where cannonballs/chainballs are instantiated.
        /// </summary>
        [SerializeField] Transform[] cannons;
        /// <summary>
        /// Array of available ammunition data.
        /// </summary>
        [SerializeField] ProjectileScriptableObject[] projectiles;

        /// <summary>
        /// Currently equipped ammunition type.
        /// </summary>
        public AmmunitionType currentAmmo { get; private set; }

        /// <summary>
        /// Cooldown counters for weapons.
        /// </summary>
        [SerializeField] float[] weaponCooldowns = new float[2];
        /// <summary>
        /// Returns whether port side cannons are loaded.
        /// </summary>
        public bool portLoaded => weaponCooldowns[0] <= 0;
        /// <summary>
        /// returns whether starboard side cannons are loaded.
        /// </summary>
        public bool starboardLoaded => weaponCooldowns[1] <= 0;

        Collider shipCollider;

        private void Start()
        {
            shipCollider = GetComponent<Collider>();
        }

        protected virtual void Update()
        {
            // Counts down cannon cooldowns
            for (int i = 0; i < weaponCooldowns.Length; i++) { if (weaponCooldowns[i] > 0) { weaponCooldowns[i] -= Time.deltaTime; } }

            // If player, update the UI
            if (gameObject.CompareTag("Player"))
            {
                EventContainer.OnUpdateCombatUI?.Invoke(weaponCooldowns, projectiles[(int)currentAmmo].loadTime, currentAmmo);
            }
        }

        /// <summary>
        /// Switches ammunition type and resets loaded weapons.
        /// </summary>
        /// <param name="type"></param>
        public void SwitchAmmunition(AmmunitionType type)
        {
            if (currentAmmo != type)
            {
                // Reset cooldowns
                for (int i = 0; i < weaponCooldowns.Length; i++) { weaponCooldowns[i] = projectiles[(int)type].loadTime; }
            }
            currentAmmo = type;
        }

        /// <summary>
        /// Fires a volley on selected side or drops a barrel depending on selected ammunition.
        /// </summary>
        /// <param name="side"></param>
        public void Fire(Broadside side)
        {
            if ((int)currentAmmo != 2)
            {
                switch (side)
                {
                    case Broadside.port:
                        if (!portLoaded) return;
                        break;
                    case Broadside.starboard:
                        if (!starboardLoaded) return;
                        break;
                }

                ShootBroadside(side);
            }
            else
            {
                DropBarrel();
            }
        }

        /// <summary>
        /// Fires a volley of cannons in given side/direction.
        /// </summary>
        /// <param name="side"></param>
        private void ShootBroadside(Broadside side)
        {
            foreach (Transform cannon in cannons)
            {
                // Check if cannon is on portside (left side of the ship)
                bool portside = cannon.localPosition.x < 0;

                // Shoots only if cannon is on the currently selected side
                if (portside) { if (side == Broadside.starboard) { continue; } }
                else { if (side == Broadside.port) { continue; } }

                // Rotate cannon balls to match side
                float rotateDegrees = portside ? -90 : 90;

                StartCoroutine(ShootCannon(cannon.position, rotateDegrees));

                // Sets sides to load again, 0 = left, 1 = right.
                weaponCooldowns[portside ? 0 : 1] = projectiles[(int)currentAmmo].loadTime;
            }
        }

        /// <summary>
        /// Drops an explosive barrel behind the ship.
        /// </summary>
        private void DropBarrel()
        {
            if (!portLoaded) return;

            // Instantiate barrel with a random rotation
            var randomRotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
            Barrel barrel = Instantiate(projectiles[2].prefab, barrelDropPoint.position, randomRotation).GetComponent<Barrel>();

            barrel.data = projectiles[2];

            weaponCooldowns[0] = projectiles[(int)currentAmmo].loadTime;
        }

        /// <summary>
        /// A coroutine that shoots cannons with slight random delays.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        /// <returns></returns>
        IEnumerator ShootCannon(Vector3 position, float rotation)
        {
            yield return new WaitForSeconds(Random.Range(0, .15f));

            // Instantiate and create reference for canonballs
            Projectile shot = Instantiate(projectiles[(int)currentAmmo].prefab, position, Quaternion.identity).GetComponent<Projectile>();

            // Rotate the cannonballs to match ship rotation
            shot.transform.Rotate(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z));
            shot.transform.Rotate(shot.transform.up, rotation);

            // Initiate shot
            shot.OnShot(shipCollider, projectiles[(int)currentAmmo]);
        }

        private void OnDrawGizmosSelected()
        {
            // Cannon positions
            if (cannons.Length > 0)
            {
                Gizmos.color = Color.red;
                for (int i = 0; i < cannons.Length; i++)
                {
                    Gizmos.DrawSphere(cannons[i].position, .5f);
                }
            }
            // Barrel dropoff position
            if (barrelDropPoint != null)
            {
                Gizmos.DrawSphere(barrelDropPoint.position, .5f);
            }
        }
    }
}