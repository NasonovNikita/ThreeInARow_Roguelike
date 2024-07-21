using Battle.Units;
using Battle.Units.Stats;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.UI
{
    public abstract class StatBar : MonoBehaviour
    {
        [SerializeField] protected Unit unit;

        [SerializeField] private Slider slider;
        [SerializeField] private Text text;

        protected abstract Stat Stat { get; }

        private void Start()
        {
            if (Stat.BorderUp == 0) OnEmptyStat();
            SetUp();

            UpdateValue();
        }

        private void SetUp()
        {
            slider.maxValue = Stat.BorderUp;
            Stat.OnValueChanged += _ => UpdateValue();
        }

        private void UpdateValue()
        {
            slider.value = Stat.Value;
            text.text = $"{Stat.Value}/{Stat.BorderUp}";
        }

        private void OnEmptyStat()
        {
            Destroy(gameObject);
        }
    }
}