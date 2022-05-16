using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TreacherousWaters
{
    public class ShipAI : ShipCombat, iBasicAIFunctions
    {
        INavAgentFunctions iNavAgent;

        [Header("AI values")]
        [SerializeField] LayerMask playerLayer;

        [SerializeField] float aimArc = 7;
        [SerializeField] float distToFire = 90;

        public bool hostileShip = true;

        /// <summary>
        /// Compares attack range with distance to player.
        /// </summary>
        public bool inAttackRange => GetPlayerDistance() <= distToFire;
        /// <summary>
        /// Checks if target angle is inside the aiming arc.
        /// </summary>
        public bool inAttackAngle => Mathf.Abs(GetPlayerAngle()) > (90 - aimArc / 2) && Mathf.Abs(GetPlayerAngle()) < (90 + aimArc / 2);
        /// <summary>
        /// Returns whether player is on the left side of the ship.
        /// </summary>
        bool playerOnLeft => GetPlayerAngle() < 0;

        private void Awake()
        {
            iNavAgent = GetComponent<INavAgentFunctions>();
        }

        protected override void Update()
        {
            base.Update();

            if (!hostileShip) return;

            CheckToFire();
        }

        /// <summary>
        /// Casts rays to detect player on sides and when detected, fires cannons.
        /// </summary>
        private void CheckToFire()
        {
            if (inAttackRange)
            {
                Vector3 dir = Vector3.zero;

                if (playerOnLeft) { dir = transform.right; }
                else { dir = -transform.right; }

                if (Physics.Raycast(transform.position, dir, distToFire, playerLayer))
                {
                    Debug.DrawRay(transform.position, dir * distToFire, Color.red);
                    if (inAttackAngle) Fire();

                    return;
                }
                Debug.DrawRay(transform.position, dir * distToFire, Color.green);
            }
        }
        public bool InAttackRange() { return inAttackRange; }
        public bool InAttackAngle() { return inAttackAngle; }

        public float GetPlayerAngle()
        {
            // Get player ship direction
            Vector3 playerDirection = (PlayerShip.Instance.transform.position - transform.localPosition).normalized;

            // Get player ship angle
            return Vector3.SignedAngle(playerDirection, transform.forward, Vector3.up);
        }

        public float GetPlayerDistance()
        {
            return Vector3.Distance(PlayerShip.Instance.transform.position, transform.position);
        }

        public void SetWaypoint(Vector3 destination)
        {
            iNavAgent.SetWaypoint(destination);
        }
    }
}
