using UnityEngine;

namespace TreacherousWaters
{
    /// <summary>
    /// A singleton class that handles the game state.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        /// <summary>
        /// Singleton instance.
        /// </summary>
        public static GameManager instance { get; private set; }

        /// <summary>
        /// Time left until game ends.
        /// </summary>
        public float timeLeft { get; private set; } = 300; // 5 minutes
        bool gameOver;

        [SerializeField] Transform[] playerSpawns = new Transform[4];

        private void Awake() { instance = this; }

        private void Update()
        {
            if (gameOver) return;

            timeLeft -= Time.deltaTime;

            if (timeLeft <= 0)
            {
                EndGame();
            }
        }

        /// <summary>
        /// Returns a random position for the player to start at.
        /// </summary>
        /// <returns></returns>
        public Vector3 GetRandomSpawnPosition()
        {
            return playerSpawns[Random.Range(0, playerSpawns.Length - 1)].position;
        }

        /// <summary>
        /// Ends the game by invoking the Game Over event.
        /// </summary>
        public void EndGame()
        {
            EventContainer.onGameOver.Invoke(false);
            gameOver = true;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            if (playerSpawns.Length > 0)
            {
                for (int i = 0; i < playerSpawns.Length; i++)
                {
                    Gizmos.DrawSphere(playerSpawns[i].position, 2);
                }
            }
        }
    }
}