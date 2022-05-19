using UnityEngine.InputSystem;
using System.Linq;
using UnityEngine;

namespace TreacherousWaters
{
    public class InputHandler : MonoBehaviour
    {
        ISetWaypoint[] iSetWaypoint;
        ICameraInput iCameraInput;
        IFire iFire;

        Broadside currentSide;

        /// <summary>
        /// Layer mask for layers detected to set waypoints on.
        /// </summary>
        public LayerMask navigableTerrain;
        RaycastHit hit;

        bool rotating;
        bool setWaypoint;

        Vector2 mousePos;

        void Start()
        {
            iCameraInput = FreelookCamera.iCameraInput;
            iSetWaypoint = GetComponents<ISetWaypoint>();
            iFire = GetComponent<IFire>();

            EventContainer.onGameOver += OnGameOver;
        }

        private void Update()
        {
            if (setWaypoint && !rotating)
            {
                SetWaypointContinuous();
            }
        }

        private void SetWaypointContinuous()
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, navigableTerrain))
            {
                if (hit.point.y < (transform.position.y - 5)) { return; }
                for (int i = 0; i < iSetWaypoint.Length; i++) { iSetWaypoint[i]?.SetWaypoint(hit.point); }
            }
        }

        // -----------------------------------------------------------------------

        /// <summary>
        /// Catches the SetWaypoint input and delivers it through an interface.
        /// </summary>
        private void OnSetWaypoint(InputValue value)
        {
            setWaypoint = value.isPressed;
        }

        private void OnSwitchBroadside(InputValue value)
        {
            if (value.Get<float>() == 0) return;
            currentSide = (value.Get<float>() < 0 ? Broadside.port : Broadside.starboard);
            GameUI.Instance.UpdateUIBroadside(currentSide);
        }

        private void OnToggleRotate(InputValue value)
        {
            rotating = value.isPressed;
            iCameraInput?.ToggleRotate(value.isPressed);

            Cursor.visible = value.isPressed ? false : true;
            Cursor.lockState = value.isPressed ? CursorLockMode.Locked : CursorLockMode.None;
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
