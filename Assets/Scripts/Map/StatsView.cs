using Battle.Units;
using UnityEngine;
using UnityEngine.UI;

namespace Map
{
    [RequireComponent(typeof(Text))]
    public class StatsView : MonoBehaviour
    {
        private Text _text;
        public void Awake()
        {
            _text = GetComponent<Text>();
        }

        public void Start()
        {
            _text.text = $"Hp/Mana: {Player.data.hp.GetValue()}/{Player.data.mana.GetValue()}  Cash: {Player.data.money}";
        }
    }
}