using UnityEngine;

namespace TreacherousWaters
{
    /// <summary>
    /// Handles projectile behavior (cannonballs & chainballs).
    /// </summary>
    public class Projectile : MonoBehaviour
    {
        /// <summary>
        /// Data assigned to the projectile.
        /// </summary>
        private ProjectileScriptableObject data;
        /// <summary>
        /// Active rotation values if the object is supposed to rotate.
        /// </summary>
        private Vector3 activeRotation;

        private void Start()
        {
            Destroy(gameObject, data.lifeTime);
            activeRotation = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
        }

        private void Update()
        {
            if (data.type == AmmunitionType.Chainball)
            {
                transform.Rotate(activeRotation * Time.deltaTime);
            }
        }

        /// <summary>
        /// Makes the projectile ignore collisions with owner, assigns projectile data 
        /// and adds force to the object in forward direction.
        /// </summary>
        /// <param name="ownerCollider"></param>
        /// <param name="projectileData"></param>
        public void OnShot(Collider ownerCollider, ProjectileScriptableObject projectileData)
        {
            Physics.IgnoreCollision(ownerCollider, GetComponent<Collider>());

            data = projectileData;

            GetComponent<Rigidbody>().AddForce(transform.forward * data.forwardForce, ForceMode.Impulse);
            GetComponent<Rigidbody>().AddForce(transform.up * data.upForce, ForceMode.Impulse);

            if (data.particleShot) Instantiate(data.particleShot, transform.position, transform.rotation);
        }

        /// <summary>
        /// Handles collisions with all layers of objects and calls
        /// the proper functions & instantiates the proper particles.
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.TryGetComponent(out IAdjustIntegrity ship))
            {
                float damage = collision.transform.CompareTag("Player") ? data.damage / 2 : data.damage;
                ship.AdjustIntegrity(damage);
                Destroy(gameObject, 1f);


                Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
            }

            if (collision.gameObject.layer == 31)
            {
                if (data.particleWaterImpact) Instantiate(data.particleWaterImpact, transform.position, Quaternion.identity);
            }
            else if (collision.gameObject.layer == 28 || collision.gameObject.layer == 29)
            {
                if (data.particleShipImpact) Instantiate(data.particleShipImpact, transform.position, Quaternion.Inverse(transform.rotation));
            }
            else
            {
                if (data.particleDefaultImpact) Instantiate(data.particleDefaultImpact, transform.position, Quaternion.identity);
            }

            data.survivableImpactCount--;
            if (data.survivableImpactCount <= 0)
            {
                if (transform.childCount > 0)
                {
                    transform.GetChild(0).SetParent(null);
                }
                Destroy(gameObject);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, 1.5f);
        }
    }
}

