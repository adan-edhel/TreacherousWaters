using UnityEngine;

namespace TreacherousWaters
{
    /// <summary>
    /// Handles all player ship specific functions.
    /// </summary>
    public class PlayerShip : ShipBase
    {
        /// <summary>
        /// Singleton instance for NPC use.
        /// </summary>
        public static GameObject Instance { get; private set; }

        [SerializeField] bool randomizePositionOnStart;

        protected override void Awake()
        {
            base.Awake();
            Instance = gameObject;
        }

        private void Start()
        {
            // If checked, randomizes player start position.
            if (randomizePositionOnStart) transform.position = GameManager.instance.GetRandomSpawnPosition();
        }

        protected override void AdjustIntegrity(float value)
        {
            base.AdjustIntegrity(value);
            EventContainer.onPlayerIntegrityChanged?.Invoke(integrity, maxIntegrity);
        }

        protected override void OnSink()
        {
            base.OnSink();
            gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
            EventContainer.onGameOver?.Invoke(true);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, 20);
        }
    }
}
