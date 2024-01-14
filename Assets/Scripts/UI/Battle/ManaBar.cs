using Battle.Units;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Battle
{
    public class ManaBar : MonoBehaviour
    {
        private Slider _slider;
    
        [SerializeField]
        private Unit unit;

        [SerializeField]
        private Text text;

        private void Start()
        {
            if (unit.mana.borderUp == 0)
            {
                Destroy(gameObject);
            }
        
            _slider = GetComponent<Slider>();
            _slider.maxValue = unit.mana.borderUp;
            _slider.minValue = unit.mana.borderDown;
            _slider.value = unit.mana.value;
        }

        private void Update()
        {
            _slider.value = unit.mana.value;
            text.text = $"{_slider.value}/{unit.mana.borderUp}";
        }
    }
}