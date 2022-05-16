using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TreacherousWaters
{
    public class MerchantShip : ShipBase
    {
        public GameObject homePier { get; private set; }
        public GameObject destinationPier;

        iBasicAIFunctions iBasicAI;

        void Start()
        {
            iBasicAI = GetComponent<iBasicAIFunctions>();

            if (destinationPier != null)
            {
                iBasicAI.SetWaypoint(destinationPier.transform.position);
            }

            ShipSpawner.Instance.HandleMerchantShipRegistry(this);
        }

        private void Update()
        {
            if (destinationPier)
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

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, 20);
        }
    }
}

