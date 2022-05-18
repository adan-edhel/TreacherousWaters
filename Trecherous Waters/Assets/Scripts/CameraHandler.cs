using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace TreacherousWaters
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class CameraHandler : MonoBehaviour, ICameraInput
    {
        CinemachineVirtualCamera vCam;
        CinemachineFramingTransposer fTransposer;

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
        }

        public void Rotation(Vector2 input) 
        {
            rotateInput = input;
        }

        public void ToggleRotate(bool input)
        {
            rotating = input;
        }

        public void Zoom(float input)
        {
            zoomInput = -input;
        }

    }
}
