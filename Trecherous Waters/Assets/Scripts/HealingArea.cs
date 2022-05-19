using UnityEngine;

namespace TreacherousWaters
{
    /// <summary>
    /// Handles player healing inside a given radius.
    /// </summary>
    public class HealingArea : MonoBehaviour
    {
        [SerializeField] float healingRadius = 50;
        [SerializeField] float healInterval = 5;
        [SerializeField] float healAmount = 10;

        [SerializeField] LayerMask player;

        ParticleSystem particle;

        float counter;

        void Start()
        {
            particle = GetComponentInChildren<ParticleSystem>();
        }

        void Update()
        {
            if (counter > 0) counter -= Time.deltaTime;

            if (counter <= 0)
            {
                Heal();
            }
        }

        private void Heal()
        {
            counter = healInterval;

            Collider[] hits;
            hits = Physics.OverlapSphere(transform.position, healingRadius, player);

            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[0])
                {
                    if (hits[i].GetComponent<ShipBase>().integrity != hits[i].GetComponent<ShipBase>().maxIntegrity)
                    {
                        if (hits[i].TryGetComponent<IAdjustIntegrity>(out IAdjustIntegrity ship))
                        {
                            ship.AdjustIntegrity(-healAmount);
                            particle.Play();
                        }
                        return;
                    }
                }
            }
            particle.Stop();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, healingRadius);
        }
    }
}
