using UnityEngine.InputSystem;
using UnityEngine;

namespace TreacherousWaters
{
    public class InputHandler : MonoBehaviour
    {
        ISetWaypoint iSetWaypoint;
        IFire iFire;

        /// <summary>
        /// Layer mask for layers detected to set waypoints on.
        /// </summary>
        public LayerMask navigableTerrain;

        void Start()
        {
            iSetWaypoint = GetComponent<ISetWaypoint>();
            iFire = GetComponent<IFire>();

            EventContainer.onGameOver += OnGameOver;
        }

        /// <summary>
        /// Catches the SetWaypoint input and delivers it through an interface.
        /// </summary>
        private void OnSetWaypoint()
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, navigableTerrain))
            {
                if (hit.point.y < (transform.position.y - 5)) { return; }
                iSetWaypoint?.SetWaypoint(hit.point);
            }
        }

        /// <summary>
        /// Catches the Fire input and delivers it through an interface.
        /// </summary>
        /// <param name="value"></param>
        private void OnFire()
        {
            iFire?.Fire();
        }

        /// <summary>
        /// Deactivates player input & NavMeshAgent.
        /// </summary>
        private void OnGameOver()
        {
            GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = true;
            GetComponent<PlayerInput>().DeactivateInput();
            EventContainer.onGameOver -= OnGameOver;
        }
    }
}
