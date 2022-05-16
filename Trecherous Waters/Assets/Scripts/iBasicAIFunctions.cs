using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TreacherousWaters
{
    public interface iBasicAIFunctions
    {
        public bool InAttackRange();
        public bool InAttackAngle();
        public float GetPlayerAngle();
        public void SetWaypoint(Vector3 destination);
    }
}

