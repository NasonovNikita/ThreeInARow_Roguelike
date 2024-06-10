using UnityEngine;

namespace Battle.Units
{
    public class ManaByTimeIncreaser : MonoBehaviour
    {
        [SerializeField] private Unit unit;

        [SerializeField] private float timeBetweenIncrements;
        [SerializeField] private int incrementAmount;

        private bool doIncrease;
        private float lastIncreaseTime;

        public void Awake()
        {
            unit.OnDied += StopIncreasing;
        }

        public void Update()
        {
            if (!doIncrease || !(Time.time - lastIncreaseTime > timeBetweenIncrements)) return;


            unit.mana.Refill(incrementAmount);
            lastIncreaseTime = Time.time;
        }

        public void StartIncreasing()
        {
            lastIncreaseTime = Time.time;
            doIncrease = true;
        }

        public void StopIncreasing() // purposely public for possible future interactions
        {
            doIncrease = false;
        }
    }
}