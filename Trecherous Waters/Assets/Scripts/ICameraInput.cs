using UnityEngine;

namespace TreacherousWaters
{
    /// <summary>
    /// Delivers input for camera functions.
    /// </summary>
    public interface ICameraInput
    {
        /// <summary>
        /// Delivers a bool to allow for camera rotation.
        /// </summary>
        /// <param name="input"></param>
        public void ToggleRotate(bool input);
        /// <summary>
        /// Delivers rotation values using mouse delta movement.
        /// </summary>
        /// <param name="input"></param>
        public void Rotation(Vector2 input);
        /// <summary>
        /// Delivers zoom values using mouse scroll YAxis.
        /// </summary>
        /// <param name="input"></param>
        public void Zoom(float input);
    }
}
