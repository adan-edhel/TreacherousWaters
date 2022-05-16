using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace TreacherousWaters
{
    public class ShipSpawner : MonoBehaviour
    {
        public static ShipSpawner Instance { get; private set; }

        [Header("Values")]
        [SerializeField] int maxMerchantShips = 5;
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

            // Catch exceptions
            if (!MerchantShipPrefab) { throw new System.Exception($"MerchantShip prefab not assigned to the {name} script on the {gameObject.name}!"); }
            if (!MilitaryShipPrefab) { throw new System.Exception($"MilitaryShip prefab not assigned to the {name} script on the {gameObject.name}!"); }
        }

        private void OnValidate()
        {
            AssignPiersAutomatically();
        }

        private void Update()
        {
            // Count down
            if (intervalCounter > 0) { intervalCounter -= Time.deltaTime; }

            // If counter reaches 0, spawn merchant ship
            if (intervalCounter <= 0 && merchantShips.Count < maxMerchantShips)
            {
                SpawnMerchantShip();
            }
        }

        /// <summary>
        /// Spawns a merchant ship at a random pier, assigning required home pier and
        /// target pier references to the merchant ship.
        /// </summary>
        /// <exception cref="System.Exception"></exception>
        private void SpawnMerchantShip()
        {
            // Select random pier from list
            int homePier = Random.Range(0, piers.Count - 1);

            // Select a random other pier from list
            List<GameObject> otherPiers = piers.Where(pier => pier != piers[homePier]).ToList();
            int targetPier = Random.Range(0, otherPiers.Count - 1);

            // Instantiate ship & assign required references
            MerchantShip ship = Instantiate(MerchantShipPrefab, piers[homePier].transform.position, Quaternion.identity).GetComponent<MerchantShip>();
            ship.AssignPiers(piers[homePier], otherPiers[targetPier]);
            ship.transform.SetParent(transform);

            // Reset spawn interval counter
            intervalCounter = Random.Range(spawnShipIntervalRange.x, spawnShipIntervalRange.y);
        }

        /// <summary>
        /// Spawns a military ship at the home pier of given merchant ship and removes the merchant ship from the registry.
        /// </summary>
        /// <param name="ship"></param>
        public void SpawnMilitaryShip(MerchantShip ship)
        {
            // Removes merchant ship from list
            HandleMerchantShipRegistry(ship);

            // Instatiate military ship
            MilitaryShip mShip = Instantiate(MilitaryShipPrefab, ship.homePier.transform.position, Quaternion.identity).GetComponent<MilitaryShip>();
            mShip.transform.SetParent(transform);
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
        /// Assigns objects to the list of piers using the "Pier" tag.
        /// </summary>
        private void AssignPiersAutomatically()
        {
            // Find all objects with tag "Pier"
            GameObject[] tempPiers = GameObject.FindGameObjectsWithTag("Pier");

            // If there are enough piers & number of found piers matches piers in the list, return
            if (piers.Count > 1 && tempPiers.Length == piers.Count)
            {
                return;
            }

            // Clear and refill the list
            piers.Clear();
            for (int i = 0; i < tempPiers.Length; i++)
            {
                piers.Add(tempPiers[i]);
            }

            // If there aren't enough piers in the scene, throw exception
            if (piers.Count <= 1) { throw new System.Exception("There aren't enough piers in the scene!"); }
        }

        private void OnDrawGizmos()
        {
            if (piers.Count > 0)
            {
                for (int i = 0; i < piers.Count; i++)
                {
                    Gizmos.color = Color.blue;
                    Gizmos.DrawCube(piers[i].transform.position, Vector3.one * 12);
                }
            }

        }
    }
}
