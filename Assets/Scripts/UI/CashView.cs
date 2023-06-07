using Battle.Units;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Text))]
    public class CashView : MonoBehaviour
    {
        private Text _text;

        public void Awake()
        {
            _text = GetComponent<Text>();
        }

    
        public void Update()
        {
            _text.text = $"Cash: {Player.data.money}";
        }
    }
}