using Other;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Slider))]
    [RequireComponent(typeof(GlobalsReference))]
    public class ConnectedSlider : MonoBehaviour
    {
        private Slider _slider;
        private GlobalsReference _value;

        public void Awake()
        {
            _slider = GetComponent<Slider>();
            _value = GetComponent<GlobalsReference>();
            _slider.value = _value.GetValue<float>();
        }

        public void Update()
        {
            _value.SetValue(_slider.value);
        }
    }
}