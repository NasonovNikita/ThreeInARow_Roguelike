using Battle.Units;
using TMPro;
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
        private TMP_Text text;

        private void Start()
        {
            _slider = GetComponent<Slider>();
            _slider.maxValue = unit.hp.borderUp;
            _slider.minValue = unit.hp.borderDown;
            _slider.value = unit.hp.GetValue();
        }

        private void Update()
        {
            _slider.value = unit.hp.GetValue();
            text.text = $"{_slider.value}/{unit.hp.borderUp}";
        }
    }
}