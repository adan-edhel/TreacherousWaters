using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine;

namespace TreacherousWaters
{
    /// <summary>
    /// Handles spawning and management of a waypoint marker to visualize 
    /// player destination.
    /// </summary>
    [RequireComponent(typeof(ShipMovement))]
    public class WaypointMarker : MonoBehaviour, ISetWaypoint
    {
        [SerializeField] GameObject waypointMarkerPrefab;
        [SerializeField] Color color = new Color(207, 181, 59);
        Color invisibleColor;

        bool visible => image.color == color;
        bool moving => agent.velocity.magnitude > 1;

        Image image;
        Canvas canvas;
        NavMeshHit hit;
        NavMeshAgent agent;
        ShipMovement movement;

        void Start()
        {
            canvas = Instantiate(waypointMarkerPrefab, transform.position, Quaternion.identity).GetComponentInChildren<Canvas>();
            canvas.transform.parent.Rotate(90, 0, 0);

            invisibleColor = new Color(color.r, color.g, color.b, 0);

            image = canvas.GetComponentInChildren<Image>();
            image.color = invisibleColor;

            movement = GetComponent<ShipMovement>();
            agent = movement.agent;
        }

        private void LateUpdate()
        {
            if (moving)
            {
                image.color = color;
            }
            else
            {
                if (visible)
                {
                    image.color = invisibleColor;
                    return;
                }
            }
        }

        /// <summary>
        /// Receives destination from player input.
        /// </summary>
        /// <param name="destination"></param>
        public void SetWaypoint(Vector3 destination)
        {
            if (NavMesh.SamplePosition(destination, out hit, movement.navSampleRadius, NavMesh.AllAreas))
            {
                canvas.transform.parent.position = destination;
            }
        }
    }
}
