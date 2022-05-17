using UnityEngine;

namespace TreacherousWaters
{
    public interface ISetWaypoint
    {
        /// <summary>
        /// Sets a navigation waypoint on given position.
        /// </summary>
        /// <param name="destination"></param>
        public void SetWaypoint(Vector3 destination);
    }
}

