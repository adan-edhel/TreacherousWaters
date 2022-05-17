using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TreacherousWaters
{
    /// <summary>
    /// A class that inherits from the ShipBase class and handles all player ship specific functions.
    /// </summary>
    public class PlayerShip : ShipBase
    {
        public static GameObject Instance { get; private set; }

        [SerializeField] bool randomizePositionOnStart;

        protected override void Awake()
        {
            base.Awake();

            Instance = gameObject;
        }

        private void Start()
        {
            if (randomizePositionOnStart) transform.position = GameManager.instance.GetRandomSpawnPosition();
        }

        protected override void OnSink()
        {
            base.OnSink();
            gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
            GameManager.instance.EndGame();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, 20);
        }
    }
}
