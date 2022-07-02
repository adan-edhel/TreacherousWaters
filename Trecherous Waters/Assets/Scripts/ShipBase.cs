using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

namespace TreacherousWaters
{
    /// <summary>
    /// Handles basic ship functions such as taking damage and sinking.
    /// </summary>
    public class ShipBase : MonoBehaviour, IAdjustIntegrity
    {
        /// <summary>
        /// Maximum ship integrity.
        /// </summary>
        [Tooltip("Default value: 100")]
        public float maxIntegrity = 100;
        /// <summary>
        /// Concurrent ship integrity.
        /// </summary>
        public float integrity { get; private set; }

        /// <summary>
        /// Whether ship is sunk.
        /// </summary>
        private bool sunk;
        /// <summary>
        /// Whether ship is smoking.
        /// </summary>
        private bool smoking;

        private Animator animator;
        private NavMeshAgent navAgent;

        [SerializeField] GameObject explosionParticle;
        [SerializeField] List<ParticleSystem> smokes = new List<ParticleSystem>();

        protected virtual void Awake()
        {
            integrity = maxIntegrity;

            navAgent = GetComponent<NavMeshAgent>();
            animator = GetComponentInChildren<Animator>();

            animator.Play("Bobbing", -1, Random.Range(0f, 1f));
        }

        /// <summary>
        /// Changes the integrity value of the ship.
        /// </summary>
        protected virtual void AdjustIntegrity(float value)
        {
            integrity -= value;
            integrity = Mathf.Clamp(integrity, 0, maxIntegrity);

            if (integrity <= maxIntegrity / 2)
            {
                if (!smoking)
                {
                    if (smokes.Count > 0)
                    {
                        for (int i = 0; i < smokes.Count; i++)
                        {
                            smokes[i].Play();
                        }
                    }
                    smoking = true;
                }
            }
            else
            {
                if (smoking)
                {
                    if (smokes.Count > 0)
                    {
                        for (int i = 0; i < smokes.Count; i++)
                        {
                            smokes[i].Stop();
                        }
                    }
                    smoking = false;
                }
            }

            if (integrity <= 0 && !sunk)
            {
                OnSink();
                if (explosionParticle)
                {
                    Instantiate(explosionParticle, transform.position, Quaternion.identity);
                }
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

            AnimatedCharacter[] characters = GetComponentsInChildren<AnimatedCharacter>();
            for (int i = 0; i < characters.Length; i++)
            {
                characters[i].HandleDeath();
            }

            if (gameObject.tag != "Player")
            {
                GameStats.Instance.HandleSunkShip();
            }
        }
    }
}

