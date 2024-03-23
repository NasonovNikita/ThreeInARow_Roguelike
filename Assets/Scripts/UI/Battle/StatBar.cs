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

        protected abstract Stat stat { get; }

        private void Start()
        {
            if (unit.mana.BorderUp == 0)
            {
                Destroy(gameObject);
            }
        
            slider.maxValue = stat.BorderUp;
            slider.minValue = stat.BorderDown;
            slider.value = stat.Value;
        }

        private void Update()
        {
            slider.value = stat.Value;
            text.text = $"{stat.Value}/{stat.BorderUp}";
        }
    }
}