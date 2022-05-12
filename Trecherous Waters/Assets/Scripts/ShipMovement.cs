using UnityEngine.AI;
using UnityEngine;

public class ShipMovement : MonoBehaviour, ISetWaypoint
{
    NavMeshAgent navAgent;

    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    public void SetWaypoint(Vector3 destination)
    {
        navAgent.SetDestination(destination);
    }
}
