using Battle.Units;
using UnityEngine;
using UnityEngine.UI;

namespace UI
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
            _text.text = $"ХП/Мана: {Player.data.hp.GetValue()}/{Player.data.mana.GetValue()}  Золото: {Player.data.money}";
        }
    }
}