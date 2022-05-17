using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TreacherousWaters
{
    /// <summary>
    /// Delivers input for camera functions.
    /// </summary>
    public interface ICameraInput
    {
        public void ToggleRotate(bool input);
        public void Rotation(Vector2 input);
        public void Zoom(float input);
    }
}
