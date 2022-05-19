using UnityEngine;

namespace TreacherousWaters
{
    /// <summary>
    /// Base collectible class for all pickups. Handles collision, integrity and destruction.
    /// </summary>
    public class CollectibleItem : MonoBehaviour, IAdjustIntegrity
    {
        /// <summary>
        /// Integrity of the pickup item. Once it depletes, item is destroyed.
        /// </summary>
        public float integrity { get; private set; } = 100;

        public float lifetime = 40;

        private void Start()
        {
            Destroy(gameObject, lifetime);
        }

        /// <summary>
        /// Handles base pickup behavior (destruction).
        /// </summary>
        /// <param name="collision"></param>
        protected virtual void OnPickup(Collision collision)
        {
            Destroy(gameObject);
        }

        /// <summary>
        /// Delivers damage to integrity.
        /// </summary>
        /// <param name="value"></param>
        protected virtual void AdjustIntegrity(float value)
        {
            integrity -= value;
            if (integrity <= 0)
            {
                GameObject.Destroy(gameObject);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.CompareTag("Player"))
            {
                OnPickup(collision);
            }
        }

        private void OnDestroy()
        {
            EventContainer.onDestroyedPickup?.Invoke(this);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, 5);
        }
    }
}
