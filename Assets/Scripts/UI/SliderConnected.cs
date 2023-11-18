using Other;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Slider))]
    [RequireComponent(typeof(FieldReferenceOld))]
    public class ConnectedSlider : MonoBehaviour
    {
        private Slider _slider;
        private FieldReferenceOld _value;

        public void Awake()
        {
            _slider = GetComponent<Slider>();
            _value = GetComponent<FieldReferenceOld>();
            _slider.value = _value.GetValue<float>();
        }

        public void Update()
        {
            _value.SetValue(_slider.value);
        }
    }
}