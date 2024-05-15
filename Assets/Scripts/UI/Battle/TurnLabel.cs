using Knot.Localization;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Battle
{
    public class TurnLabel : MonoBehaviour
    {
        public static TurnLabel Instance { get; private set; }
        
        [SerializeField] private Text text;

        [SerializeField] private KnotTextKeyReference playerTurn;
        [SerializeField] private KnotTextKeyReference enemyTurn;

        public void Awake() => 
            Instance = this;

        public void SetPlayerTurn() => 
            text.text = playerTurn.Value;

        public void SetEnemyTurn() => 
            text.text = enemyTurn.Value;
    }
}