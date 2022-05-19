using System.Collections.Generic;
using UnityEngine;

namespace TreacherousWaters
{
    /// <summary>
    /// Handles merchant ship specific behaviors.
    /// </summary>
    public class MerchantShip : ShipBase
    {
        [SerializeField] GameObject goldChest;
        [SerializeField] bool destroyOnFinalDestination = true;

        /// <summary>
        /// The home pier the ship belongs to.
        /// </summary>
        public GameObject homePier { get; private set; }
        private Queue<GameObject> destinations = new Queue<GameObject>();

        ISetWaypoint iSetWaypoint;

        bool sunkByPlayer;
        bool docked;

        void Start()
        {
            iSetWaypoint = GetComponent<ISetWaypoint>();

            if (destinations.Count > 0)
            {
                iSetWaypoint.SetWaypoint(destinations.Peek().transform.position);
            }
        }

        private void Update()
        {
            if (destinations.Count > 0)
            {
                float distance = Vector3.Distance(transform.position, destinations.Peek().transform.position);
                if (distance <= 1f && !docked)
                {
                    docked = true;
                    Invoke("SailToNewPier", 1f);
                }
            }
        }

        /// <summary>
        /// Removes current pier from the queue and assigns next pier as destination.
        /// </summary>
        private void SailToNewPier()
        {
            destinations.Dequeue();
            if (destinations.Count == 0)
            {
                if (destroyOnFinalDestination)
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                iSetWaypoint.SetWaypoint(destinations.Peek().transform.position);
            }
            docked = false;
        }

        /// <summary>
        /// Assigns required pier references to the ship.
        /// </summary>
        /// <param name="home"></param>
        /// <param name="destination"></param>
        public void AssignPiers(GameObject home, List<GameObject> piers)
        {
            homePier = home;
            for (int i = 0; i < piers.Count; i++)
            {
                destinations.Enqueue(piers[i]);
            }
        }

        protected override void OnSink()
        {
            base.OnSink();
            sunkByPlayer = true;
            Destroy(gameObject, 5);
            GetComponent<ICombatFunctions>().IsSunk(sunkByPlayer);

            if (goldChest) Instantiate(goldChest, transform.position + (Vector3.up * 4), Quaternion.identity);
        }

        private void OnDestroy()
        {
            // Removes the ship from the list of active merchant ships and 
            // starts a revenge mission if the ship has a home pier
            bool revengeMission = homePier != null ? true : false;
            EventContainer.onDestroyedMerchant?.Invoke(this, sunkByPlayer && revengeMission);
        }

        private void OnDrawGizmos()
        {
            // Draws a sphere around the ship to see better in editor.
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, 20);
        }

        private void OnDrawGizmosSelected()
        {
            // Draws target piers.
            Gizmos.color = Color.yellow;
            foreach (GameObject obj in destinations)
            {
                Gizmos.DrawSphere(obj.transform.position, 20);
            }
        }
    }
}

