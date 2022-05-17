using UnityEngine;

namespace TreacherousWaters
{
    public class MerchantShip : ShipBase
    {
        public GameObject homePier { get; private set; }
        public GameObject destinationPier;

        ISetWaypoint iSetWaypoint;

        [SerializeField] GameObject goldChest;

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

            if (goldChest) Instantiate(goldChest, transform.position + (Vector3.up * 1), Quaternion.identity);
        }

        private void OnDestroy()
        {
            bool revengeMission = homePier != null ? true : false;
            EventContainer.onDestroyedMerchant?.Invoke(this, sunkByPlayer && revengeMission);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, 20);
        }
    }
}

