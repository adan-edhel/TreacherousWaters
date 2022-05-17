using UnityEngine;

namespace TreacherousWaters
{
    /// <summary>
    /// Singleton class registering and holding earnt stats during gameplay.
    /// </summary>
    public class GameStats : MonoBehaviour
    {
        public static GameStats Instance { get; private set; }

        public int stolenGold { get; private set; }
        public int shipsSunk { get; private set; }

        private void Awake() { Instance = this; }

        public void HandleSunkShip()
        {
            shipsSunk += 1;
        }

        public void HandleEarntGold(int amount)
        {
            stolenGold += amount;
            EventContainer.OnUpdateGUIGold?.Invoke(amount);
        }
    }
}
