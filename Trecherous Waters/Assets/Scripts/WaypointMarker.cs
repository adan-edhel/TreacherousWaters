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
        [SerializeField] GameObject waypointMarkerPrefab;
        [SerializeField] Color color = new Color(207, 181, 59);
        Color invisibleColor;

        bool visible => image.color == color;
        bool moving => agent.velocity.magnitude > 1;

        Image image;
        Canvas canvas;
        NavMeshAgent agent;

        void Start()
        {
            agent = GetComponent<ShipMovement>().agent;
            canvas = Instantiate(waypointMarkerPrefab, transform.position, Quaternion.identity).GetComponentInChildren<Canvas>();
            canvas.transform.parent.Rotate(90, 0, 0);

            invisibleColor = new Color(color.r, color.g, color.b, 0);

            image = canvas.GetComponentInChildren<Image>();
            image.color = invisibleColor;
        }

        private void LateUpdate()
        {
            if (moving)
            {
                image.color = color;
                canvas.transform.parent.position = agent.destination;
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
    }
}
