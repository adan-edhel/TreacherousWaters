using Cinemachine;
using UnityEngine;

namespace TreacherousWaters
{
    /// <summary>
    /// Handles camera rotation and zoom through inputs.
    /// </summary>
    public class CameraHandler : MonoBehaviour, ICameraInput
    {
        CinemachineVirtualCamera vCam;
        CinemachineFramingTransposer fTransposer;

        /// <summary>
        /// Singleton reference of the interface through which input is received.
        /// </summary>
        public static ICameraInput iCameraInput { get; private set; }

        Vector3 rotation;

        Vector2 rotateInput;
        float zoomInput;
        bool rotating;

        private void Awake()
        { 
            iCameraInput = GetComponent<ICameraInput>();

            vCam = GetComponent<CinemachineVirtualCamera>();
            fTransposer = vCam.GetCinemachineComponent<CinemachineFramingTransposer>();
        }

        private void Update()
        {
            fTransposer.m_CameraDistance += zoomInput;
            fTransposer.m_CameraDistance = Mathf.Clamp(fTransposer.m_CameraDistance, 20, 300);

            if (rotating)
            {
                rotation = new Vector3(-rotateInput.y, rotateInput.x, 0);
            }
            else
            {
                rotation = Vector3.Lerp(rotation, Vector3.zero, .05f);
            }
            transform.eulerAngles += rotation * Time.deltaTime;
            Vector3 clampedRotation = new Vector3(transform.eulerAngles.x, Mathf.Clamp(transform.eulerAngles.y, -30, 160), transform.eulerAngles.z);
            transform.eulerAngles = clampedRotation;
        }

        /// <summary>
        /// Receives rotation input and delivers it for translation into camera rotation.
        /// </summary>
        /// <param name="input"></param>
        public void Rotation(Vector2 input) 
        {
            rotateInput = input;
        }

        /// <summary>
        /// Receives rotation toggle input.
        /// </summary>
        /// <param name="input"></param>
        public void ToggleRotate(bool input)
        {
            rotating = input;
        }

        /// <summary>
        /// Receives zoom input and delivers it for translation into camera zoom.
        /// </summary>
        /// <param name="input"></param>
        public void Zoom(float input)
        {
            zoomInput = -input;
        }

    }
}
