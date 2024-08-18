using Knot.Localization;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Battle.UI
{
    public class TurnLabel : MonoBehaviour
    {
        [FormerlySerializedAs("text")] [SerializeField]
        private Text turningUnitText;


        [SerializeField] private KnotTextKeyReference playerTurn;
        [SerializeField] private KnotTextKeyReference enemyTurn;
        public static TurnLabel Instance { get; private set; }

        public void Awake()
        {
            Instance = this;
        }

        public void SetPlayerTurn()
        {
            turningUnitText.text = playerTurn.Value;
        }

        public void SetEnemyTurn()
        {
            turningUnitText.text = enemyTurn.Value;
        }
    }
}