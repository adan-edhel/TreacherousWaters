using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TreacherousWaters
{
    public class Barrel : MonoBehaviour
    {
        public ProjectileScriptableObject data;

        [SerializeField] float checkRadius = 50f;
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
                        if (hits[x].TryGetComponent<IDealDamage>(out IDealDamage ship))
                        {
                            ship.DealDamage(data.damage);
                        }
                    }
                    Instantiate(data.particleShipImpact, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                    return;
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, checkRadius);
        }
    }
}
