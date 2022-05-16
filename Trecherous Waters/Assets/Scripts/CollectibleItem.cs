using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TreacherousWaters
{
    public class CollectibleItem : MonoBehaviour
    {
        [SerializeField] float lifetime = 50;

        private void Start()
        {
            Destroy(gameObject, lifetime);
        }

        protected virtual void OnPickup(Collision collision)
        {
            Destroy(gameObject);
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
            EventContainer.onPickedUpItem.Invoke(this);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, 5);
        }
    }
}
