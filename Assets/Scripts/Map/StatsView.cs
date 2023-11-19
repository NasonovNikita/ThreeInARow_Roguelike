using Battle.Units;
using UnityEngine;
using UnityEngine.UI;

namespace Map
{
    [RequireComponent(typeof(Text))]
    public class StatsView : MonoBehaviour
    {
        private Text text;
        public void Awake()
        {
            text = GetComponent<Text>();
        }

        public void Start()
        {
            text.text = $"Hp/Mana: {Player.data.hp.GetValue()}/{Player.data.mana.GetValue()}  Cash: {Player.data.money}";
        }
    }
}
