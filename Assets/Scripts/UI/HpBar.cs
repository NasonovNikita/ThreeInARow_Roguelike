using Battle.Units;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HpBar : MonoBehaviour
    {
        private Slider _slider;
    
        [SerializeField]
        private Unit unit;

        [SerializeField]
        private Text text;

        private void Start()
        {
            _slider = GetComponent<Slider>();
            _slider.maxValue = unit.unitHp.borderUp;
            _slider.minValue = unit.unitHp.borderDown;
            _slider.value = unit.unitHp.value;
        }

        private void Update()
        {
            _slider.value = unit.unitHp.value;
            text.text = $"{_slider.value}/{unit.unitHp.borderUp}";
        }
    }
}