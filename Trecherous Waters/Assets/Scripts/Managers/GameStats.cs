using UnityEngine;

namespace TreacherousWaters
{
    /// <summary>
    /// Registers and holds earnt stats during gameplay.
    /// </summary>
    public class GameStats : MonoBehaviour
    {
        /// <summary>
        /// Singleton instance.
        /// </summary>
        public static GameStats Instance { get; private set; }

        /// <summary>
        /// Amount of gold stolen by player.
        /// </summary>
        public int stolenGold { get; private set; }
        /// <summary>
        /// Amount of ships sunk by player.
        /// </summary>
        public int shipsSunk { get; private set; }

        private void Awake() { Instance = this; }

        /// <summary>
        /// Adds up on sunk ship count.
        /// </summary>
        public void HandleSunkShip()
        {
            shipsSunk += 1;
        }

        /// <summary>
        /// Adds up on stolen gold count.
        /// </summary>
        /// <param name="amount"></param>
        public void HandleEarntGold(int amount)
        {
            stolenGold += amount;
            EventContainer.OnUpdateGameUI?.Invoke(amount);
        }
    }
}
