using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TreacherousWaters
{
    public class ShipCombat : MonoBehaviour, IFire, ISwitchAmmo
    {
        [SerializeField] Transform barrelDropPoint;
        [SerializeField] Transform[] cannons;
        [SerializeField] ProjectileScriptableObject[] projectiles;

        [SerializeField] AmmunitionType currentAmmo = AmmunitionType.Cannonball;

        private float loadCounter;
        private bool canFire => loadCounter <= 0;

        private Collider shipCollider;

        private void Start()
        {
            shipCollider = GetComponent<Collider>();
        }

        protected virtual void Update()
        {
            if (loadCounter > 0) loadCounter -= Time.deltaTime;
        }

        public void SwitchAmmunition(AmmunitionType type)
        {
            currentAmmo = type;
            loadCounter = projectiles[(int)currentAmmo].loadTime;
        }

        public void Fire()
        {
            if (!canFire) return;

            if ((int)currentAmmo == 2)
            {
                DropBarrel();
            }
            else
            {
                ShootBroadside();
            }

            loadCounter = projectiles[0].loadTime;
        }

        private void ShootBroadside()
        {
            foreach (Transform cannon in cannons)
            {
                // Check if cannon is on portside (left side of the ship)
                bool portside = cannon.localPosition.x < 0;

                // Rotate cannon balls to match side
                float rotateDegrees = portside ? -90 : 90;

                StartCoroutine(ShootCannon(cannon.position, rotateDegrees));
            }
        }

        private void DropBarrel()
        {
            var randomRotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
            Barrel barrel = Instantiate(projectiles[2].prefab, barrelDropPoint.position, randomRotation).GetComponent<Barrel>();
            barrel.data = projectiles[2];
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