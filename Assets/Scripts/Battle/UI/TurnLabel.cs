using Knot.Localization;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.UI
{
    public class TurnLabel : MonoBehaviour
    {
        [SerializeField] private Text text;

        [SerializeField] private KnotTextKeyReference playerTurn;
        [SerializeField] private KnotTextKeyReference enemyTurn;
        public static TurnLabel Instance { get; private set; }

        public void Awake()
        {
            Instance = this;
        }

        public void SetPlayerTurn()
        {
            text.text = playerTurn.Value;
        }

        public void SetEnemyTurn()
        {
            text.text = enemyTurn.Value;
        }
    }
}