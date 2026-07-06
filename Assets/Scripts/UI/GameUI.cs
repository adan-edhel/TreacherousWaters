using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UltimateClean;
using UnityEngine;
using TMPro;

namespace TreacherousWaters
{
    /// <summary>
    /// Handles all UI related tasks.
    /// </summary>
    public class GameUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI timerText;

        [SerializeField, Range(0, 1)] float targetValue = 1f;
        [SerializeField] TextMeshProUGUI integrityPerc;
        [SerializeField] Image integrityBar;

        [SerializeField] TextMeshProUGUI guiGold;
        float goldAmount;
        int targetGold;

        [SerializeField] GameObject[] broadsideFrames;
        [SerializeField] Image ammunitionIcon;
        [SerializeField] Image[] broadsideIcons;
        [SerializeField] Color loadingColor;
        Color broadsideStartColor;

        [SerializeField] List<Sprite> ammunitionIcons = new List<Sprite>();

        void Start()
        {
            EventContainer.onGameOver += OnEndGame;

            EventContainer.OnUpdateGameUI += HandleGoldValue;
            EventContainer.OnUpdateCombatUI += HandleCombatUI;

            EventContainer.onPlayerIntegrityChanged += HandleIntegrityValues;

            broadsideStartColor = broadsideIcons[0].color;
        }

        private void LateUpdate()
        {
            HandleGameUIElements();
        }

        /// <summary>
        /// Updates all non-combat UI elements to their concurrent values.
        /// </summary>
        private void HandleGameUIElements()
        {
            // Timer
            float timeLeft = GameManager.instance.timeLeft;
            float minutes = Mathf.FloorToInt(timeLeft / 60);
            float seconds = Mathf.FloorToInt(timeLeft % 60);

            timerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);

            // Integrity bar
            integrityBar.fillAmount = Mathf.Lerp(integrityBar.fillAmount, targetValue, 3f * Time.deltaTime);
            integrityPerc.text = Mathf.Round(integrityBar.fillAmount * 100).ToString() + "%";

            // Gold amount
            goldAmount = Mathf.Lerp(goldAmount, targetGold, 3f * Time.deltaTime);
            guiGold.text = Mathf.Round(goldAmount).ToString();
        }

        /// <summary>
        /// Updates target gold value for the game UI.
        /// </summary>
        /// <param name="amount"></param>
        private void HandleGoldValue(int amount)
        {
            targetGold += amount;
        }

        /// <summary>
        /// Handles activation & deactivation, animation and alteration of 
        /// combat elements on the game UI.
        /// </summary>
        /// <param name="loads"></param>
        /// <param name="loadtime"></param>
        /// <param name="type"></param>
        private void HandleCombatUI(float[] loads, float loadtime, AmmunitionType type)
        {
            // Update middle icon
            ammunitionIcon.sprite = ammunitionIcons[(int)type];

            if (type != AmmunitionType.Barrel)
            {
                // Fill middle icon
                broadsideIcons[2].fillAmount = 1;
                // Set broadside icons to active
                for (int i = 0; i < broadsideFrames.Length; i++) { broadsideFrames[i].SetActive(true); }

                // Update & fill port icon
                if (loads[0] > 0)
                {
                    broadsideIcons[0].color = loadingColor;
                    broadsideIcons[0].fillAmount = (loadtime - loads[0]) / loadtime;
                }
                else
                {
                    broadsideIcons[0].color = broadsideStartColor;
                }

                // Update & fill starboard icon
                if (loads[1] > 0)
                {
                    broadsideIcons[1].color = loadingColor;
                    broadsideIcons[1].fillAmount = (loadtime - loads[1]) / loadtime;
                }
                else
                {
                    broadsideIcons[1].color = broadsideStartColor;
                }

                // Set middle icon to blank color
                broadsideIcons[2].color = loadingColor;
            }
            else
            {
                // Set broadside icons to inactive
                for (int i = 0; i < broadsideFrames.Length; i++) { broadsideFrames[i].SetActive(false); }

                // Update & fill middle/barrel icon
                if (loads[0] > 0)
                {
                    broadsideIcons[2].color = loadingColor;
                    broadsideIcons[2].fillAmount = (loadtime - loads[0]) / loadtime;
                }
                else
                {
                    broadsideIcons[2].color = broadsideStartColor;
                }
            }
        }

        /// <summary>
        /// Updates target integrity value for the game UI.
        /// </summary>
        /// <param name="integrity"></param>
        /// <param name="maxIntegrity"></param>
        private void HandleIntegrityValues(float integrity, float maxIntegrity)
        {
            targetValue = integrity / maxIntegrity;
        }

        /// <summary>
        /// Starts Endscreen popup coroutine with a delay and unsubscribes all functions 
        /// from their respective events.
        /// </summary>
        private void OnEndGame(bool delayed)
        {
            float delay = delayed ? 5 : 0;
            StartCoroutine(PopupCoroutine(delay));
            EventContainer.onGameOver -= OnEndGame;
            EventContainer.OnUpdateCombatUI -= HandleCombatUI;
            EventContainer.OnUpdateGameUI -= HandleGoldValue;
            EventContainer.onPlayerIntegrityChanged -= HandleIntegrityValues;
        }

        /// <summary>
        /// Opens the end screen popup with a delay.
        /// </summary>
        /// <returns></returns>
        IEnumerator PopupCoroutine(float delay)
        {
            yield return new WaitForSeconds(delay);
            GetComponent<PopupOpener>().OpenPopup();
        }
    }
}