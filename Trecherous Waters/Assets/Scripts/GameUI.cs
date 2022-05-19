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

        [SerializeField, Range(0, 1)] float targetValue = 1f;
        [SerializeField] TextMeshProUGUI integrityPerc;
        [SerializeField] Image integrityBar;

        [SerializeField] TextMeshProUGUI guiGold;
        float goldAmount;
        int targetGold;

        [SerializeField] Image ammunitionIcon;
        [SerializeField] Image[] broadsideIcons = new Image[3];
        [SerializeField] Color loadingColor;
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

        private void OnValidate()
        {
            GUIValues();
        }

        private void LateUpdate()
        {
            float minutes = Mathf.FloorToInt(GameManager.instance.timeLeft / 60);
            float seconds = Mathf.FloorToInt(GameManager.instance.timeLeft % 60);

            timerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);

            GUIValues();
        }

        private void GUIValues()
        {
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
            targetGold += amount;
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

        public void UpdateBroadsideValues(float[] loads, float loadtime, AmmunitionType type)
        {
            if (type != AmmunitionType.Barrel)
            {
                if (loads[0] > 0)
                {
                    broadsideIcons[0].color = loadingColor;
                    broadsideIcons[0].fillAmount = (loadtime - loads[0]) / loadtime;
                }
                else
                {
                    broadsideIcons[0].color = broadsideStartColor;
                }

                if (loads[1] > 0)
                {
                    broadsideIcons[1].color = loadingColor;
                    broadsideIcons[1].fillAmount = (loadtime - loads[1]) / loadtime;
                }
                else
                {
                    broadsideIcons[1].color = broadsideStartColor;
                }

                broadsideIcons[2].color = loadingColor;
            }
            else
            {
                for (int i = 0; i < 2; i++)
                {
                    broadsideIcons[i].color = loadingColor;
                    broadsideIcons[i].fillAmount = 1;
                }

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