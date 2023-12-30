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
            _slider.maxValue = unit.Hp.borderUp;
            _slider.minValue = unit.Hp.borderDown;
            _slider.value = unit.Hp.value;
        }

        private void Update()
        {
            _slider.value = unit.Hp.value;
            text.text = $"{_slider.value}/{unit.Hp.borderUp}";
        }
    }
}