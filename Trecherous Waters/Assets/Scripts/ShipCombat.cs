using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TreacherousWaters
{
    public enum Broadside
    {
        port,
        starboard
    }

    public class ShipCombat : MonoBehaviour, IFire, ISwitchAmmo
    {
        [SerializeField] Transform barrelDropPoint;
        [SerializeField] Transform[] cannons;
        [SerializeField] ProjectileScriptableObject[] projectiles;

        public AmmunitionType currentAmmo { get; private set; }

        [SerializeField] float[] loadCounters = new float[2];
        public bool portLoaded => loadCounters[0] <= 0;
        public bool starboardLoaded => loadCounters[1] <= 0;

        private Collider shipCollider;

        private void Start()
        {
            shipCollider = GetComponent<Collider>();
        }

        protected virtual void Update()
        {
            for (int i = 0; i < loadCounters.Length; i++)
            {
                if (loadCounters[i] > 0)
                {
                    loadCounters[i] -= Time.deltaTime;
                }
            }

            if(PlayerShip.Instance.gameObject == gameObject) //TODO: Clean up
            {
                GameUI.Instance.GUIBottom(loadCounters, projectiles[(int)currentAmmo].loadTime, currentAmmo);
                GameUI.Instance.UpdateUIAmmo((int)currentAmmo);
            }
        }

        public void SwitchAmmunition(AmmunitionType type)
        {
            currentAmmo = type;
            for (int i = 0; i < loadCounters.Length; i++)
            {
                loadCounters[i] = projectiles[(int)currentAmmo].loadTime;
            }
        }

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

        private void ShootBroadside(Broadside side)
        {
            foreach (Transform cannon in cannons)
            {
                // Check if cannon is on portside (left side of the ship)
                bool portside = cannon.localPosition.x < 0;

                // Shoots only if cannon is on the currently selected side.
                if (portside) { if (side == Broadside.starboard) { continue; } }
                else { if (side == Broadside.port) { continue; } }

                // Rotate cannon balls to match side
                float rotateDegrees = portside ? -90 : 90;

                StartCoroutine(ShootCannon(cannon.position, rotateDegrees));

                // Sets sides to load.
                loadCounters[portside ? 0 : 1] = projectiles[(int)currentAmmo].loadTime;
            }
        }

        private void DropBarrel()
        {
            if (!portLoaded) return;

            var randomRotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
            Barrel barrel = Instantiate(projectiles[2].prefab, barrelDropPoint.position, randomRotation).GetComponent<Barrel>();

            barrel.data = projectiles[2];

            loadCounters[0] = projectiles[(int)currentAmmo].loadTime;
        }

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
            if (cannons.Length > 0)
            {
                Gizmos.color = Color.red;
                for (int i = 0; i < cannons.Length; i++)
                {
                    Gizmos.DrawSphere(cannons[i].position, .5f);
                }
            }
            if (barrelDropPoint != null)
            {
                Gizmos.DrawSphere(barrelDropPoint.position, .5f);
            }
        }
    }
}