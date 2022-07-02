using UnityEngine.AI;
using UnityEngine;

namespace TreacherousWaters
{
    /// <summary>
    /// Allows to move ships using a NavMeshAgent component.
    /// </summary>
    [RequireComponent(typeof(NavMeshAgent)), DisallowMultipleComponent]
    public class ShipMovement : MonoBehaviour, ISetWaypoint, IAddBoost
    {
        public NavMeshAgent agent { get; private set; }
        ParticleSystem wakeParticle;
        AudioSource audioSource;

        /// <summary>
        /// Target volume value for the sailing sound effect.
        /// </summary>
        float targetVolume;

        /// <summary>
        /// Original ship speed.
        /// </summary>
        float originalSpeed;
        /// <summary>
        /// Target ship speed (to switch between either boost value or original value).
        /// </summary>
        float targetSpeed;

        /// <summary>
        /// Dictates how close NavMesh needs to have been baked for a waypoint to be set.
        /// Set player's value lower (0.2f) for a better game feel.
        /// </summary>
        public float navSampleRadius { get; private set; } = 3f;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            agent.avoidancePriority = Random.Range(10, 50);

            audioSource = GetComponent<AudioSource>();
            wakeParticle = GetComponentInChildren<ParticleSystem>();

            originalSpeed = agent.speed;
            targetSpeed = agent.speed;
        }

        private void LateUpdate()
        {
            if (agent.velocity.magnitude > 0)
            {
                if (wakeParticle.isStopped)
                {
                    wakeParticle.Play();
                    targetVolume = 1f;
                }
            }
            else
            {
                if (wakeParticle.isPlaying)
                {
                    wakeParticle.Stop();
                    targetVolume = 0;
                }
            }
            audioSource.volume = Mathf.Lerp(audioSource.volume, targetVolume, 1f * Time.deltaTime);
            if (agent.speed != targetSpeed) agent.speed = Mathf.Lerp(agent.speed, targetSpeed, 1f * Time.deltaTime);
        }

        /// <summary>
        /// Sets a destination on the NavMeshAgent of ship.
        /// </summary>
        /// <param name="destination"></param>
        public void SetWaypoint(Vector3 destination)
        {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(destination, out hit, navSampleRadius, NavMesh.AllAreas))
            {
                agent.SetDestination(hit.position);
            }
        }

        /// <summary>
        /// Sets the stopping distance of a NavMeshAgent.
        /// </summary>
        public void SetStoppingDistance(float distance)
        {
            agent.stoppingDistance = distance;
        }

        /// <summary>
        /// Returns the remaining distance to destination.
        /// </summary>
        /// <returns></returns>
        public float GetRemainingDistance()
        {
            // If stopping distance is reached, stop NavMeshAgent rotation
            agent.updateRotation = agent.remainingDistance <= agent.stoppingDistance ? false : true;

            return agent.remainingDistance;
        }

        /// <summary>
        /// Applies speed boost to the ship while boost condition is true.
        /// </summary>
        /// <param name="toggle"></param>
        public void AddBoost(bool toggle)
        {
            if (toggle)
            {
                targetSpeed = originalSpeed * 1.5f;
            }
            else
            {
                targetSpeed = originalSpeed;
            }
        }
    }
}

