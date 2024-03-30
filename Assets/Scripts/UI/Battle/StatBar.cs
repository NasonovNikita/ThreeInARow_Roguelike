using Battle.Units;
using Battle.Units.Stats;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Battle
{
    public abstract class StatBar : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] protected Unit unit;
        [SerializeField] private Text text;

        protected abstract Stat Stat { get; }

        private void Start()
        {
            if (Stat.BorderUp == 0) Destroy(gameObject);
        
            slider.maxValue = Stat.BorderUp;
            slider.minValue = Stat.BorderDown;
            slider.value = Stat.Value;
        }

        private void Update()
        {
            slider.value = Stat.Value;
            text.text = $"{Stat.Value}/{Stat.BorderUp}";
        }
    }
}