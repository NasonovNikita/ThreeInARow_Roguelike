using Battle.Units;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Text))]
    public class StatsView : MonoBehaviour
    {
        private Text text;
        
        public void Awake()
        {
            text = GetComponent<Text>();
        }

        public void Update()
        {
            text.text =
                $"Hp/Mana: {Player.data.unitHp.value}/{Player.data.mana.value}\n" +
                $"Cash: {Player.data.money}\n" +
                $"Element: {Player.data.chosenElement}";
        }
    }
}
