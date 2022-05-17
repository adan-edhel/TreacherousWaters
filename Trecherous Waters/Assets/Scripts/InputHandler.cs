using UnityEngine.InputSystem;
using System.Linq;
using UnityEngine;

namespace TreacherousWaters
{
    public class InputHandler : MonoBehaviour
    {
        ISetWaypoint iSetWaypoint;
        ICameraInput iCameraInput;
        IFire iFire;

        Broadside currentSide;

        /// <summary>
        /// Layer mask for layers detected to set waypoints on.
        /// </summary>
        public LayerMask navigableTerrain;

        void Start()
        {
            iCameraInput = CameraHandler.iCameraInput;
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

        private void OnSwitchBroadside(InputValue value)
        {
            if (value.Get<float>() == 0) return;
            currentSide = (value.Get<float>() < 0 ? Broadside.port : Broadside.starboard);
            GameUI.Instance.UpdateUIBroadside(currentSide);
        }

        private void OnToggleRotate(InputValue value)
        {
            iCameraInput?.ToggleRotate(value.isPressed);
        }

        private void OnRotate(InputValue value)
        {
            iCameraInput?.Rotation(value.Get<Vector2>());
        }

        private void OnZoom(InputValue value)
        {
            iCameraInput?.Zoom(value.Get<float>());
        }

        /// <summary>
        /// Catches the Fire input and delivers it through an interface.
        /// </summary>
        /// <param name="value"></param>
        private void OnFire()
        {
            iFire?.Fire(currentSide);
        }

        private void OnQuit()
        {
            EventContainer.onGameOver.Invoke(false);
        }

        /// <summary>
        /// Deactivates player input & NavMeshAgent.
        /// </summary>
        private void OnGameOver(bool delayed)
        {
            GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = true;
            GetComponent<PlayerInput>().DeactivateInput();
            EventContainer.onGameOver -= OnGameOver;
        }
    }
}
