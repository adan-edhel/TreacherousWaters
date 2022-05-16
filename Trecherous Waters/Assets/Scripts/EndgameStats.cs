using UnityEngine;
using TMPro;

namespace TreacherousWaters
{
    /// <summary>
    /// A class that updates fields of stats on the endgame screen popup.
    /// </summary>
    public class EndgameStats : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI goldText;
        [SerializeField] TextMeshProUGUI sunkShipsText;

        private void Start()
        {
            goldText.text = GameStats.Instance.stolenGold.ToString();
            sunkShipsText.text = GameStats.Instance.shipsSunk.ToString();
        }
    }
}
