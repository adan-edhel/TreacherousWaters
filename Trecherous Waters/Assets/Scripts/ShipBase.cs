using UnityEngine.AI;
using UnityEngine;

namespace TreacherousWaters
{
    public class ShipBase : MonoBehaviour, IAdjustIntegrity
    {
        [Tooltip("Default value: 100")]
        public float maxIntegrity = 100;
        public float integrity { get; private set; }

        private bool sunk;

        private Animator animator;
        private NavMeshAgent navAgent;

        protected virtual void Awake()
        {
            integrity = maxIntegrity;

            navAgent = GetComponent<NavMeshAgent>();
            animator = GetComponentInChildren<Animator>();

            animator.Play("Bobbing", -1, Random.Range(0f, 1f));
        }

        private void LateUpdate()
        {
            GameUI.Instance.HandleIntegrityBar(integrity, 100);
        }

        /// <summary>
        /// Changes the integrity value of the ship.
        /// </summary>
        public void AdjustIntegrity(float value)
        {
            integrity -= value;
            integrity = Mathf.Clamp(integrity, 0, maxIntegrity);

            if (integrity <= 0 && !sunk)
            {
                OnSink();
            }
        }

        /// <summary>
        /// Handles the sinking behavior of the ship.
        /// </summary>
        protected virtual void OnSink()
        {
            sunk = true;
            navAgent.isStopped = true;
            animator?.SetBool("Sunk", sunk);
            GetComponent<Collider>().enabled = false;
        }
    }
}

