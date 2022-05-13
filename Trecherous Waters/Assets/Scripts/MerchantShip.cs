using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantShip : ShipBase
{
    public GameObject homePier { get; private set; }
    public GameObject destinationPier;

    ISetWaypoint iSetWaypoint;

    private void Awake()
    {
        iSetWaypoint = GetComponent<ISetWaypoint>();
    }

    protected override void Start()
    {
        base.Start();

        if (destinationPier != null)
        {
            iSetWaypoint.SetWaypoint(destinationPier.transform.position);
        }
        else
        {
            Debug.Log("MerchantShip has no destination!");
        }

        ShipSpawner.Instance.HandleMerchantShipRegistry(this);
    }

    private void Update()
    {
        if (destinationPier && !sunk)
        {
            float distance = Vector3.Distance(transform.position, destinationPier.transform.position);
            if (distance <= 1f)
            {
                Destroy(gameObject, 1);
            }
        }
    }

    /// <summary>
    /// Assign required pier references to the ship.
    /// </summary>
    /// <param name="home"></param>
    /// <param name="destination"></param>
    public void AssignPiers(GameObject home, GameObject destination)
    {
        homePier = home;
        destinationPier = destination;
    }

    protected override void OnSink()
    {
        base.OnSink();
        Destroy(gameObject, 5);
    }

    private void OnDestroy()
    {
        ShipSpawner.Instance.HandleMerchantShipRegistry(this);
    }
}
