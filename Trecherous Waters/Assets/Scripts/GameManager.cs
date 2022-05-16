using UnityEngine;

namespace TreacherousWaters
{
    /// <summary>
    /// A singleton class that handles the game state.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance { get; private set; }

        public float timeLeft = 300; // 5 minutes
        bool gameOver;

        private void Awake() { instance = this; }

        private void Update()
        {
            timeLeft -= Time.deltaTime;

            if (timeLeft <= 0 && !gameOver)
            {
                EndGame();
            }
        }

        /// <summary>
        /// Ends the game by invoking the Game Over event.
        /// </summary>
        public void EndGame()
        {
            EventContainer.onGameOver.Invoke();
            gameOver = true;
        }
    }
}