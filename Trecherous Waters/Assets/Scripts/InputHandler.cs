using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

namespace TreacherousWaters
{
    public class InputHandler : MonoBehaviour
    {
        INavAgentFunctions iNavAgent;
        IFire iFire;

        /// <summary>
        /// Layer mask for layers detected to set waypoints on.
        /// </summary>
        public LayerMask navigableTerrain;

        void Start()
        {
            iNavAgent = GetComponent<INavAgentFunctions>();
            iFire = GetComponent<IFire>();

            EventContainer.onGameOver += OnGameOver;
        }

        /// <summary>
        /// Catches the SetWaypoint input.
        /// </summary>
        private void OnSetWaypoint()
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, navigableTerrain))
            {
                if (hit.point.y < (transform.position.y - 5)) { return; }
                iNavAgent?.SetWaypoint(hit.point);
            }
        }

        /// <summary>
        /// Catches the Fire input.
        /// </summary>
        /// <param name="value"></param>
        private void OnFire()
        {
            iFire?.Fire();
        }

        private void OnGameOver()
        {
            GetComponent<PlayerInput>().DeactivateInput();
            GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = true;
            EventContainer.onGameOver -= OnGameOver;
        }
    }
}
