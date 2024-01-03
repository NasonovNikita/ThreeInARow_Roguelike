using System;
using UnityEngine;

namespace Battle.Modifiers
{
    [Serializable]
    public class Modifier
    {
        public int moves;

        public ModType type;
        public ModClass workPattern;
        public DmgType dmgType;
        private bool always;
    
        public float value;
        
        public bool IsPositive { get; private set; }

        [SerializeField] private bool delay;

        private Action onMove;

        public Modifier(int moves, ModType type, ModClass workPattern = ModClass.Standard, DmgType dmgType = DmgType.Physic, bool isPositive = true, float value = 1, Action onMove = null,
            bool delay = false, bool always = false)
        {
            this.moves = moves;
            this.type = type;
            this.workPattern = workPattern;
            this.dmgType = dmgType;
            this.value = value;
            this.onMove = onMove;
            this.delay = delay;
            this.always = always;
            IsPositive = isPositive;
            Log.OnLog += Move;
        }

        public float Use()
        {
            return moves != 0 ? value : 0;
        }

        public void TurnOff()
        {
            moves = 0;
        }
        
        private void Move(Log log)
        {
            if (log is BattleEndLog && !always)
            {
                TurnOff();
                return;
            } 
            if (log is not TurnLog) return;
            
            if (delay)
            {
                delay = false;
                return;
            }
            if (moves == 0) return;
            onMove?.Invoke();
            if (moves <= 0) return;
            moves -= 1;
        }
    }

    public enum ModClass
    {
        DamageBase,
        DamageTyped,
        DamageTypedStat,
        HpHealing,
        ManaRefill,
        ManaWaste,
        Standard
    }
}