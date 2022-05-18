using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine;

namespace TreacherousWaters
{
    [RequireComponent(typeof(ShipMovement))]
    public class WaypointMarker : MonoBehaviour
    {
        [SerializeField] GameObject waypointMarker;
        [SerializeField] Color color = new Color(207, 181, 59);
        Color invisibleColor;

        Image image;
        Canvas canvas;

        bool visible => image.color == color;

        NavMeshAgent agent;

        void Start()
        {
            agent = GetComponent<ShipMovement>().agent;
            canvas = Instantiate(waypointMarker, transform.position, Quaternion.identity).GetComponent<Canvas>();

            invisibleColor = new Color(color.r, color.g, color.b, 0);

            image = canvas.GetComponentInChildren<Image>();
            image.color = invisibleColor;
        }

        private void LateUpdate()
        {
            if (agent.isStopped && visible)
            {
                image.color = invisibleColor;
            }
            else
            {
                image.color = color;
            }
        }

        public void SetWaypoint(Vector3 destination)
        {
            canvas.transform.position = destination;
            image.color = color;
        }
    }
}
