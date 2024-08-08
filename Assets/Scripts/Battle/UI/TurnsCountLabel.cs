using Battle.Units;
using Knot.Localization;
using Other;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.UI
{
    public class TurnsCountLabel : MonoBehaviour
    {
        [SerializeField] private Text turnsLeftText;
        
        [SerializeField] private KnotTextKeyReference leftTurns;

        public void Start()
        {
            Player.Instance.OnMovesCountChanged += WriteLeftTurns;
            WriteLeftTurns();
        }
        
        private void WriteLeftTurns()
        {
            turnsLeftText.text =
                leftTurns.Value.IndexErrorProtectedFormat(Player.Instance.CurrentMovesCount);
        }
    }
}