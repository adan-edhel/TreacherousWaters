using System.Collections;
using System.Collections.Generic;
using UltimateClean;
using UnityEngine;

namespace TreacherousWaters
{
    /// <summary>
    /// Handles all GUI related tasks.
    /// </summary>
    public class GameUI : MonoBehaviour
    {
        void Start()
        {
            EventContainer.onGameOver += OnEndGame;
        }

        /// <summary>
        /// Starts endgame stats popup courotine.
        /// </summary>
        private void OnEndGame(bool delayed)
        {
            float delay = delayed ? 5 : 0;
            StartCoroutine(PopupCoroutine(delay));
            EventContainer.onGameOver -= OnEndGame;
        }

        /// <summary>
        /// Endgame stats popup coroutine.
        /// </summary>
        /// <returns></returns>
        IEnumerator PopupCoroutine(float delay)
        {
            yield return new WaitForSeconds(delay);
            GetComponent<PopupOpener>().OpenPopup();
        }
    }
}