using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TreacherousWaters
{
    public interface iCombatAIFunctions
    {
        /// <summary>
        /// Compares distance of player to attack range.
        /// </summary>
        /// <returns></returns>
        public bool InAttackRange();
        /// <summary>
        /// Compares the angle of player in relation to ship to attack angle bounds.
        /// </summary>
        /// <returns></returns>
        public bool InAttackAngle();
        /// <summary>
        /// Returns player angle in relation to ship.
        /// </summary>
        /// <returns></returns>
        public float GetPlayerAngle();
    }
}

