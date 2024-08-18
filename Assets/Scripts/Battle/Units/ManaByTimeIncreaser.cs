using UnityEngine;

namespace Battle.Units
{
    /// <summary>
    ///     Increases Enemy's mana by time.
    /// </summary>
    public class ManaByTimeIncreaser : MonoBehaviour
    {
        [SerializeField] private Unit unit;

        [SerializeField] private float timeBetweenIncrements;
        [SerializeField] private int incrementAmount;

        private bool _doIncrease;
        private float _lastIncreaseTime;

        public void Awake()
        {
            unit.OnDied += StopIncreasing;
        }

        public void Update()
        {
            if (!_doIncrease ||
                !(Time.time - _lastIncreaseTime > timeBetweenIncrements)) return;


            unit.mana.Refill(incrementAmount);
            _lastIncreaseTime = Time.time;
        }

        public void StartIncreasing()
        {
            _lastIncreaseTime = Time.time;
            _doIncrease = true;
        }

        // ReSharper disable once MemberCanBePrivate.Global
        // purposely public for possible future interactions
        public void StopIncreasing()
        {
            _doIncrease = false;
        }
    }
}