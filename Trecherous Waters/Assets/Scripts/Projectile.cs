using UnityEngine;

namespace TreacherousWaters
{
    public class Projectile : MonoBehaviour
    {
        private ProjectileScriptableObject data;
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

        public void OnShot(Collider ownerCollider, ProjectileScriptableObject projectileData)
        {
            Physics.IgnoreCollision(ownerCollider, GetComponent<Collider>());

            data = projectileData;

            GetComponent<Rigidbody>().AddForce(transform.forward * data.forwardForce, ForceMode.Impulse);
            GetComponent<Rigidbody>().AddForce(transform.up * data.upForce, ForceMode.Impulse);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.TryGetComponent(out IDealDamage ship))
            {
                ship.DealDamage(data.damage);
                Destroy(gameObject, 1f);

                Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, 1.5f);
        }
    }
}

