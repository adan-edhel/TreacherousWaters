using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UltimateClean;
using UnityEngine;
using TMPro;

namespace TreacherousWaters
{
    /// <summary>
    /// Handles all GUI related tasks.
    /// </summary>
    public class GameUI : MonoBehaviour
    {
        public static GameUI Instance { get; private set; }

        [SerializeField] TextMeshProUGUI timerText;

        [SerializeField, Range(0, 1)] float targetValue;
        [SerializeField] TextMeshProUGUI integrityPerc;
        [SerializeField] Image integrityBar;

        [SerializeField] TextMeshProUGUI guiGold;
        float goldAmount;
        int targetGold;

        [SerializeField] Image ammunitionIcon;
        [SerializeField] List<Image> broadsideIcons = new List<Image>();
        [SerializeField] Color loadedColor;
        Color broadsideStartColor;

        [SerializeField] List<Sprite> ammunitionIcons = new List<Sprite>();

        private void Awake()
        {
            Instance = this; 
        }

        void Start()
        {
            EventContainer.onGameOver += OnEndGame;
            EventContainer.OnUpdateGUIGold += UpdateGUIStats;
            EventContainer.onDestroyedGUIBroadside += UpdateBroadsideValues;

            broadsideStartColor = broadsideIcons[0].color;
        }

        private void LateUpdate()
        {
            float minutes = Mathf.FloorToInt(GameManager.instance.timeLeft / 60);
            float seconds = Mathf.FloorToInt(GameManager.instance.timeLeft % 60);

            timerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);

            integrityBar.fillAmount = Mathf.Lerp(integrityBar.fillAmount, targetValue, 3f * Time.deltaTime);
            integrityPerc.text = Mathf.Round(integrityBar.fillAmount * 100).ToString() + "%";

            goldAmount = Mathf.Lerp(goldAmount, targetGold, 3f * Time.deltaTime);
            guiGold.text = Mathf.Round(goldAmount).ToString();
        }

        public void HandleIntegrityBar(float integrity, float maxIntegrity)
        {
            targetValue = integrity / maxIntegrity;
        }

        private void UpdateGUIStats(int amount)
        {
            targetGold = amount;
        }

        public void UpdateUIBroadside(Broadside side)
        {
            switch (side)
            {
                case Broadside.port:
                    break;
                case Broadside.starboard:
                    break;
            }
        }

        public void UpdateUIAmmo(int index)
        {
            ammunitionIcon.sprite = ammunitionIcons[index];
        }

        public void UpdateBroadsideValues(float port, float starboard, float loadtime)
        {
            if (port > 0)
            {
                broadsideIcons[0].color = broadsideStartColor;
                broadsideIcons[0].fillAmount = (loadtime - port) / loadtime;
            }
            else
            {
                broadsideIcons[0].color = loadedColor;
            }

            if (starboard > 0)
            {
                broadsideIcons[1].color = broadsideStartColor;
                broadsideIcons[1].fillAmount = (loadtime - starboard) / loadtime;
            }
            else
            {
                broadsideIcons[1].color = loadedColor;
            }
        }

        /// <summary>
        /// Starts endgame stats popup courotine.
        /// </summary>
        private void OnEndGame(bool delayed)
        {
            float delay = delayed ? 5 : 0;
            StartCoroutine(PopupCoroutine(delay));
            EventContainer.onGameOver -= OnEndGame;
            EventContainer.OnUpdateGUIGold -= UpdateGUIStats;
            EventContainer.onDestroyedGUIBroadside -= UpdateBroadsideValues;
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