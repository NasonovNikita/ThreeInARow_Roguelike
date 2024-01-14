using Battle.Units;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Battle
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
            _slider.maxValue = unit.hp.borderUp;
            _slider.minValue = unit.hp.borderDown;
            _slider.value = unit.hp.value;
        }

        private void Update()
        {
            _slider.value = unit.hp.value;
            text.text = $"{_slider.value}/{unit.hp.borderUp}";
        }
    }
}