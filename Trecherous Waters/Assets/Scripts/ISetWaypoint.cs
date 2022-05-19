using UnityEngine;

namespace TreacherousWaters
{
    /// <summary>
    /// Delivers waypoint destinations between classes.
    /// </summary>
    public interface ISetWaypoint
    {
        /// <summary>
        /// Sets a navigation waypoint on given position.
        /// </summary>
        /// <param name="destination"></param>
        public void SetWaypoint(Vector3 destination);
    }
}

