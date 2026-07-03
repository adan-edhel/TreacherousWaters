using UnityEngine;

namespace TreacherousWaters
{
    /// <summary>
    /// Allows UI buttons to quit the application.
    /// </summary>
    public class QuitButton : MonoBehaviour
    {
        /// <summary>
        /// Closes the game.
        /// </summary>
        public void QuitGame()
        {
            Application.Quit();
        }
    }
}

