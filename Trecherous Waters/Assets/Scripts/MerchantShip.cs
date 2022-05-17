using UnityEngine;

namespace TreacherousWaters
{
    public class MerchantShip : ShipBase
    {
        public GameObject homePier { get; private set; }
        public GameObject destinationPier;

        ISetWaypoint iSetWaypoint;

        bool sunkByPlayer;

        void Start()
        {
            iSetWaypoint = GetComponent<ISetWaypoint>();

            if (destinationPier != null)
            {
                iSetWaypoint.SetWaypoint(destinationPier.transform.position);
            }
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
            destinationPier = destination;
            homePier = home;
        }

        protected override void OnSink()
        {
            base.OnSink();
            sunkByPlayer = true;
            Destroy(gameObject, 5);
        }

        private void OnDestroy()
        {
            EventContainer.onDestroyedMerchant?.Invoke(this, sunkByPlayer);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, 20);
        }
    }
}

