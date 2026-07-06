using UnityEngine;
using TMPro;

namespace TreacherousWaters
{
    /// <summary>
    /// Updates fields of stats on the endgame screen popup.
    /// </summary>
    public class EndgameStats : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI goldText;
        [SerializeField] TextMeshProUGUI sunkShipsText;

        private void Start()
        {
            goldText.text = GameStats.Instance.stolenGold.ToString();
            sunkShipsText.text = GameStats.Instance.shipsSunk.ToString();

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
