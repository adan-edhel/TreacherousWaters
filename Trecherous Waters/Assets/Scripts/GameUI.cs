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
        private void OnEndGame()
        {
            StartCoroutine(PopupCoroutine());
            EventContainer.onGameOver -= OnEndGame;
        }

        /// <summary>
        /// Endgame stats popup coroutine.
        /// </summary>
        /// <returns></returns>
        IEnumerator PopupCoroutine()
        {
            yield return new WaitForSeconds(5);
            GetComponent<PopupOpener>().OpenPopup();
        }
    }
}