using UnityEngine;

namespace TreacherousWaters
{
    /// <summary>
    /// Delivers waypoint destinations between classes.
    /// </summary>
    public interface IShipControls
    {
        /// <summary>
        /// Sets a navigation waypoint on given position.
        /// </summary>
        /// <param name="destination"></param>
        public void SetWaypoint(Vector3 destination);

        /// <summary>
        /// Adds boost to the navigation speed.
        /// </summary>
        /// <param name="toggle"></param>
        public void AddBoost(bool toggle) { }
    }
}

