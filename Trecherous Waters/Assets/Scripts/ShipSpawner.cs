using System.Collections.Generic;
using UnityEngine;

public class ShipSpawner : MonoBehaviour
{
    public static ShipSpawner Instance { get; private set; }

    [Header("Values")]
    [SerializeField] int maxMerchantShipCount = 5;
    [SerializeField] Vector2 spawnShipIntervalRange = new Vector2(3, 10);

    [Header("Prefabs")]
    [SerializeField] GameObject MerchantShipPrefab;
    [SerializeField] GameObject MilitaryShipPrefab;

    [Header("Lists")]
    [SerializeField] private List<GameObject> piers = new List<GameObject>();
    [SerializeField] private List<MerchantShip> merchantShips = new List<MerchantShip>();

    private float intervalCounter;

    private void Awake()
    {
        Instance = this;
    }

    private void OnValidate()
    {
        AssignPiersAutomatically();
    }

    private void Update()
    {
        if (intervalCounter > 0) { intervalCounter -= Time.deltaTime; }
        if (intervalCounter <= 0 && merchantShips.Count < maxMerchantShipCount)
        {
            SpawnMerchantShip();
        }
    }

    private void SpawnMerchantShip()
    {
        if (piers.Count <= 1) { throw new System.Exception("There aren't enough piers in the scene!"); }
        if (!MerchantShipPrefab) { throw new System.Exception("Ship prefabs not assigned to Ship Spawner!"); }

        int randomHomePier = Random.Range(0, piers.Count - 1);

        List<GameObject> otherPiers = new List<GameObject>();
        for (int i = 0; i < piers.Count; i++)
        {
            if (piers[i] == piers[randomHomePier])
            {
                continue;
            }
            else
            {
                otherPiers.Add(piers[i]);
            }
        }
        int randomTargetPier = Random.Range(0, otherPiers.Count - 1);

        MerchantShip ship = Instantiate(MerchantShipPrefab, piers[randomHomePier].transform.position, Quaternion.identity).GetComponent<MerchantShip>();
        ship.AssignPiers(piers[randomHomePier], otherPiers[randomTargetPier]);
        ship.transform.SetParent(transform);

        intervalCounter = Random.Range(spawnShipIntervalRange.x, spawnShipIntervalRange.y);
    }

    /// <summary>
    /// Spawns a military ship at the home pier of given merchant ship and removes the merchant ship from the registry.
    /// </summary>
    /// <param name="ship"></param>
    public void SpawnMilitaryShip(MerchantShip ship)
    {
        HandleMerchantShipRegistry(ship);
    }

    /// <summary>
    /// Adds merchant ship to the registry or removes it from it if it's already included.
    /// </summary>
    /// <param name="ship"></param>
    public void HandleMerchantShipRegistry(MerchantShip ship)
    {
        if (merchantShips.Contains(ship))
        {
            merchantShips.Remove(ship);
        }
        else
        {
            merchantShips.Add(ship);
        }
    }

    /// <summary>
    /// Assigns game objects with the "Pier" tag to the list of piers.
    /// </summary>
    private void AssignPiersAutomatically()
    {
        GameObject[] tempPiers = GameObject.FindGameObjectsWithTag("Pier");
        if (tempPiers.Length <= 1) {  }

        if (tempPiers.Length == piers.Count) return;

        piers.Clear();
        for (int i = 0; i < tempPiers.Length; i++)
        {
            piers.Add(tempPiers[i]);
        }
    }
}
