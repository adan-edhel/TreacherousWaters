using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TreacherousWaters
{
    public class MilitaryShip : ShipBase
    {
        iBasicAIFunctions iBasicAI;
        Transform player;

        [Header("Rotation Values")]
        [SerializeField] float rotationSpeed = 50;

        void Start()
        {
            player = PlayerShip.Instance.transform;
            iBasicAI = GetComponent<iBasicAIFunctions>();
        }

        void Update()
        {
            iBasicAI?.SetWaypoint(player.position);
            RotateShip();
        }

        /// <summary>
        /// Rotates the ship to face one of its sides to its target
        /// depending on the angle once stopping distance is reached.
        /// </summary>
        private void RotateShip()
        {
            float angle = iBasicAI.GetPlayerAngle();
            // Player's absolute angle
            float absAngle = Mathf.Abs(angle);

            // Check when to rotate left or right
            bool rotateLeft = IsBetween(angle, 0, 90) || IsBetween(angle, -90, -180);

            if (iBasicAI.InAttackRange())
            {
                // Check if player ship angle is outside bounds
                if (!iBasicAI.InAttackAngle())
                {
                    // Rotate according to the angle of the player ship
                    float relativeRotationSpeed = rotateLeft ? rotationSpeed : -rotationSpeed;
                    transform.Rotate((Vector3.up * relativeRotationSpeed) * Time.deltaTime);
                }
            }
        }

        /// <summary>
        /// Returns a bool depending on whether a value finds itself between two bounds.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="bound1"></param>
        /// <param name="bound2"></param>
        /// <returns></returns>
        private bool IsBetween(float value, float bound1, float bound2) // TODO: study this one
        {
            if (bound1 > bound2) return value >= bound2 && value <= bound1;

            return value >= bound1 && value <= bound2;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 20);
        }
    }
}

