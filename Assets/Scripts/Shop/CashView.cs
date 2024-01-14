using Battle.Units;
using UnityEngine;
using UnityEngine.UI;

namespace Shop
{
    [RequireComponent(typeof(Text))]
    public class CashView : MonoBehaviour
    {
        private Text text;

        public void Awake()
        {
            text = GetComponent<Text>();
        }

    
        public void Update()
        {
            text.text = $"Cash: {Player.data.money}";
        }
    }
}