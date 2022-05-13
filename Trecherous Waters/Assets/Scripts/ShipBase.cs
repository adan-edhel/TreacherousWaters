using UnityEngine.AI;
using UnityEngine;

public class ShipBase : MonoBehaviour, IDealDamage
{
    public float integrity { get; private set; } = 100;

    protected bool sunk;

    protected Animator animator;
    private NavMeshAgent navAgent;

    protected virtual void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    public void DealDamage(float value)
    {
        integrity -= value;

        if (integrity <= 0)
        {
            OnSink();
        }
    }

    protected virtual void OnSink()
    {
        sunk = true;
        navAgent.isStopped = true;
        animator.SetBool("Sunk", sunk);
    }
}
