using UnityEngine;

namespace TreacherousWaters
{
    public interface INavAgentFunctions
    {
        public void SetWaypoint(Vector3 destination);
        public void SetStoppingDistance(float distance);
        public float GetRemainingDistance();
    }
}

