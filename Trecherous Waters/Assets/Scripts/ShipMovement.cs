using UnityEngine.AI;
using UnityEngine;

namespace TreacherousWaters
{
    /// <summary>
    /// Allows to move ships using a NavMeshAgent component.
    /// </summary>
    [RequireComponent(typeof(NavMeshAgent)), DisallowMultipleComponent]
    public class ShipMovement : MonoBehaviour, ISetWaypoint
    {
        NavMeshAgent agent;

        /// <summary>
        /// Dictates how close NavMesh needs to have been baked for a waypoint to be set.
        /// Set player's value lower (0.2f) for a better game feel.
        /// </summary>
        [SerializeField] private float navSampleRadius = 3f;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            agent.avoidancePriority = Random.Range(10, 50);
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
        /// Stops the NavMeshAgent of ship
        /// </summary>
        public void SetStoppingDistance(float distance)
        {
            agent.stoppingDistance = distance;
        }

        public float GetRemainingDistance()
        {
            // If stopping distance is reached, stop NavMeshAgent rotation
            agent.updateRotation = agent.remainingDistance <= agent.stoppingDistance ? false : true;

            return agent.remainingDistance;
        }
    }
}

