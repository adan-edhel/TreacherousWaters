using UnityEngine.InputSystem;
using UnityEngine;

namespace TreacherousWaters
{
    public class InputHandler : MonoBehaviour
    {
        ISetWaypoint[] iSetWaypoint;
        ICameraInput iCameraInput;
        IAddBoost iAddBoost;
        IFire iFire;

        public Broadside currentSide { get; private set; }

        /// <summary>
        /// Layer mask for layers detected to set waypoints on.
        /// </summary>
        public LayerMask navigableTerrain;

        RaycastHit hit;
        bool rotating;
        bool setWaypoint;

        void Start()
        {
            iCameraInput = FreelookCamera.iCameraInput;
            iSetWaypoint = GetComponents<ISetWaypoint>();
            iAddBoost = GetComponent<IAddBoost>();
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

        /// <summary>
        /// Continuously sets a waypoint at the mouse location and delivers it through an interface.
        /// </summary>
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
        /// Catches the SetWaypoint input and sets condition to true or false.
        /// </summary>
        private void OnSetWaypoint(InputValue value)
        {
            setWaypoint = value.isPressed;
        }

        /// <summary>
        /// Catches the switch broadside input and delivers it through an interface.
        /// </summary>
        /// <param name="value"></param>
        private void OnSwitchBroadside(InputValue value)
        {
            if (value.Get<float>() == 0) return;
            currentSide = (value.Get<float>() < 0 ? Broadside.port : Broadside.starboard);
        }

        /// <summary>
        /// Catches the toggle rotate input and delivers it through a singleton.
        /// </summary>
        /// <param name="value"></param>
        private void OnToggleRotate(InputValue value)
        {
            rotating = value.isPressed;
            iCameraInput?.ToggleRotate(value.isPressed);


            Cursor.visible = value.isPressed ? false : true;
        }

        /// <summary>
        /// Catches the rotation input and delivers it through a singleton.
        /// </summary>
        /// <param name="value"></param>
        private void OnRotate(InputValue value)
        {
            iCameraInput?.Rotation(value.Get<Vector2>());

            Cursor.lockState = value.Get<Vector2>().magnitude > 4f && rotating ? CursorLockMode.Locked : CursorLockMode.None;
        }

        /// <summary>
        /// Catches the zoom input and delivers it through a singleton.
        /// </summary>
        /// <param name="value"></param>
        private void OnZoom(InputValue value)
        {
            iCameraInput?.Zoom(value.Get<float>());
        }

        private void OnBoost(InputValue value)
        {
            iAddBoost.AddBoost(value.isPressed);
        }

        /// <summary>
        /// Catches the Fire input and delivers it through an interface.
        /// </summary>
        /// <param name="value"></param>
        private void OnFire()
        {
            iFire?.Fire(currentSide);
        }

        /// <summary>
        /// Triggers OnGameOver event.
        /// </summary>
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
