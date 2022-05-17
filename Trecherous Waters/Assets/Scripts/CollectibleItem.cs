using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TreacherousWaters
{
    public class CollectibleItem : MonoBehaviour, IDealDamage
    {
        public float integrity { get; private set; } = 100;

        public float lifetime = 40;

        private void Start()
        {
            Destroy(gameObject, lifetime);
        }

        protected virtual void OnPickup(Collision collision)
        {
            Destroy(gameObject);
        }

        public void DealDamage(float value)
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
