using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace TreacherousWaters
{
    public class FreelookCamera : MonoBehaviour, ICameraInput
    {
        public static ICameraInput iCameraInput { get; private set; }

        CinemachineFreeLook VCam;
        CinemachineFreeLook.Orbit[] originalOrbits;

        [Range(0.3f, 1f)]
        public float minScale = 0.4f;

        [Range(1f, 10f)]
        public float maxScale = 5f;

        public float movementSpeed;
        public float movementTime;

        public Vector3 newPosition;

        public AxisState zAxis = new AxisState(0, 1, false, true, 50f, 0.1f, 0.1f, "", false);

        bool canRotate;

        Vector3 moveInput;

        private void Awake()
        {
            iCameraInput = GetComponent<ICameraInput>();
            VCam = GetComponentInChildren<CinemachineFreeLook>();

            if (VCam)
            {
                originalOrbits = new CinemachineFreeLook.Orbit[VCam.m_Orbits.Length];
                for (int i = 0; i < VCam.m_Orbits.Length; i++)
                {
                    originalOrbits[i].m_Height = VCam.m_Orbits[i].m_Height;
                    originalOrbits[i].m_Radius = VCam.m_Orbits[i].m_Radius;
                }
            }
        }

        private void OnValidate()
        {
            minScale = Mathf.Max(0.01f, minScale);
            maxScale = Mathf.Max(minScale, maxScale);
        }

        private void Update()
        {
            UpdateOrbit();
        }

        void UpdateOrbit()
        {
            if (originalOrbits != null)
            {
                zAxis.Update(Time.deltaTime);
                float scale = Mathf.Lerp(minScale, maxScale, zAxis.Value);
                for (int i = 0; i < originalOrbits.Length; i++)
                {
                    VCam.m_Orbits[i].m_Height = originalOrbits[i].m_Height * scale;
                    VCam.m_Orbits[i].m_Radius = originalOrbits[i].m_Radius * scale;
                }
            }
        }

        public void ToggleRotate(bool input)
        {
            canRotate = input;
        }

        public void Rotation(Vector2 input)
        {
            VCam.m_XAxis.m_InputAxisValue = canRotate ? input.x : 0;
            VCam.m_YAxis.m_InputAxisValue = canRotate ? input.y : 0;

            transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);
        }

        public void Zoom(float input)
        {
            zAxis.m_InputAxisValue = -input / 1000;
        }
    }
}
