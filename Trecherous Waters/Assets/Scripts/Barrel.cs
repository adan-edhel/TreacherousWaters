using UnityEngine;

namespace TreacherousWaters
{
    /// <summary>
    /// Handles explosive barrel behavior
    /// </summary>
    public class Barrel : MonoBehaviour
    {
        /// <summary>
        /// Contains all ammunition data.
        /// </summary>
        public ProjectileScriptableObject data;

        /// <summary>
        /// The radius of ship detection.
        /// </summary>
        [SerializeField] float checkRadius = 50f;
        /// <summary>
        /// The intervals in which barrel checks for detection.
        /// </summary>
        [SerializeField] float sphereCastInterval = 10;
        float counter;

        void Start()
        {
            counter = sphereCastInterval;
            Destroy(gameObject, data.lifeTime);
        }

        void Update()
        {
            if (counter > 0) counter -= Time.deltaTime;

            if (counter <= 0)
            {
                CheckForShips();
            }
        }

        /// <summary>
        /// Checks for enemy ships within a radius and deals damage
        /// in an explosion in the event of finding one.
        /// </summary>
        private void CheckForShips()
        {
            Collider[] hits;
            hits = Physics.OverlapSphere(transform.position, checkRadius);

            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].CompareTag("Enemy"))
                {
                    for (int x = 0; x < hits.Length; x++)
                    {
                        if (hits[x].TryGetComponent<IAdjustIntegrity>(out IAdjustIntegrity ship))
                        {
                            ship.AdjustIntegrity(data.damage);
                        }
                    }
                    Instantiate(data.particleShipImpact, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                    return;
                }
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == 31)
            {
                if (data.particleWaterImpact) Instantiate(data.particleWaterImpact, transform.position, Quaternion.identity);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, checkRadius);
        }
    }
}
