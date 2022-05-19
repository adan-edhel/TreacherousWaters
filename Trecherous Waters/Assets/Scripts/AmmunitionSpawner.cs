using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

namespace TreacherousWaters
{
    /// <summary>
    /// Handles creation of ammunition spawn positions and the spawning 
    /// of ammunition pickups in the game.
    /// </summary>
    public class AmmunitionSpawner : MonoBehaviour
    {
        [Header("Debug")]
        // Shows casting grid and distribution through the level.
        [SerializeField] bool showCastVisuals;
        // Shows generated spawn positions.
        [SerializeField] bool showSpawnPositions;

        [Header("Cast Values")]
        [SerializeField] float castHeight = 240;
        [SerializeField, Range(1, 1000)] float castAreaSize = 900;
        [SerializeField, Range(20, 100)] float castSparsity = 40;
        [SerializeField] LayerMask navigableTerrain;

        [Header("Spawner Values")]
        [SerializeField] float spawnHeight = 5;
        [SerializeField] float spawnInterval = 4;
        [SerializeField] float pickupLifetime = 40;
        [SerializeField] float maxSpawnedPickups = 30;

        [Header("Lists")]
        [SerializeField] List<GameObject> prefabs = new List<GameObject>();
        [SerializeField] List<Vector3> spawnPositions = new List<Vector3>();
        [SerializeField] List<CollectibleItem> spawnedPickups = new List<CollectibleItem>();

        List<int> indexesToSpawnFrom = new List<int>();
        GameObject ammunitionContainer;
        int lastSpawnedPickupIndex;
        float counter;

        private void Start()
        {
            if (prefabs.Count != 3)
            {
                throw new System.Exception($"{name} script on the {gameObject.name} doesn't have all prefabs assigned!");
            }

            // If there are no spawn positions, generate them.
            if (spawnPositions.Count == 0) GenerateSpawnPositions();

            EventContainer.onDestroyedPickup += HandlePickupRegistry;

            ammunitionContainer = new GameObject();
            ammunitionContainer.name = "Ammunition Container";
            ammunitionContainer.transform.SetParent(transform);
        }

        private void OnValidate()
        {
            GenerateSpawnPositions();
        }

        private void Update()
        {
            if (counter > 0) counter -= Time.deltaTime;

            if (counter <= 0 && spawnedPickups.Count < maxSpawnedPickups)
            {
                SpawnPickup();
                counter = spawnInterval;
            }
        }

        /// <summary>
        /// Spawns pickups at intervals using a random position from the generated spawn positions.
        /// </summary>
        /// <exception cref="System.Exception"></exception>
        private void SpawnPickup()
        {
            if (spawnPositions.Count < 1)
            {
                throw new System.Exception($"{name} script on the {gameObject.name} has no spawn positions generated!");
            }

            indexesToSpawnFrom.Clear();
            for (int i = 0; i < 3; i++)
            {
                if (i == lastSpawnedPickupIndex) continue;
                indexesToSpawnFrom.Add(i);
            }
            lastSpawnedPickupIndex = indexesToSpawnFrom[Random.Range(0, indexesToSpawnFrom.Count)];
            var rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
            var position = spawnPositions[Random.Range(0, spawnPositions.Count - 1)];

            CollectibleItem item = Instantiate(prefabs[lastSpawnedPickupIndex], position, rotation).GetComponent<CollectibleItem>();
            item.transform.SetParent(ammunitionContainer.transform);
            item.lifetime = pickupLifetime;
            HandlePickupRegistry(item);
        }

        /// <summary>
        /// Adds and removes pickups from a list of spawned pickups.
        /// </summary>
        /// <param name="item"></param>
        private void HandlePickupRegistry(CollectibleItem item)
        {
            if (spawnedPickups.Contains(item))
            {
                spawnedPickups.Remove(item);
            }
            else { spawnedPickups.Add(item); }
        }

        /// <summary>
        /// Generates spawn positions to be used for pickups.
        /// </summary>
        private void GenerateSpawnPositions()
        {
            spawnPositions.Clear();

            NavMeshHit hit;
            RaycastHit[] hits;
            for (int x = 0; x < castAreaSize - castSparsity; x++)
            {
                for (int z = 0; z < castAreaSize - castSparsity; z++)
                {
                    Vector3 pos = new Vector3(x * castSparsity - (castAreaSize / 2), castHeight, z * castSparsity - (castAreaSize / 2)) + Vector3.zero;
                    if (pos.x < castAreaSize / 2 && pos.z < castAreaSize / 2)
                    {
                        hits = Physics.RaycastAll(pos, Vector3.down, Mathf.Infinity, navigableTerrain);
                        if (NavMesh.SamplePosition(hits[0].point, out hit, .05f, NavMesh.AllAreas))
                        {
                            spawnPositions.Add(hits[0].point + (Vector3.up * spawnHeight));
                        }
                    }
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (showCastVisuals)
            {
                Gizmos.color = Color.magenta;
                Gizmos.DrawWireCube(transform.up * castHeight, new Vector3(castAreaSize, 1, castAreaSize));

                for (int x = 0; x < castAreaSize - castSparsity; x++)
                {
                    for (int z = 0; z < castAreaSize - castSparsity; z++)
                    {
                        Vector3 pos = new Vector3(x * castSparsity - (castAreaSize / 2), castHeight, z * castSparsity - (castAreaSize / 2)) + Vector3.zero;
                        if (pos.x < castAreaSize / 2 && pos.z < castAreaSize / 2)
                        {
                            Gizmos.DrawWireSphere(pos, 5);
                            Gizmos.DrawLine(pos, pos + Vector3.down * castHeight);
                        }
                    }
                }
            }

            if (showSpawnPositions && spawnPositions.Count > 0)
            {
                Gizmos.color = Color.yellow;
                for (int i = 0; i < spawnPositions.Count; i++)
                {
                    Gizmos.DrawWireSphere(spawnPositions[i], 10);
                }
            }
        }

        private void OnDestroy()
        {
            EventContainer.onDestroyedPickup -= HandlePickupRegistry;
        }
    }
}
