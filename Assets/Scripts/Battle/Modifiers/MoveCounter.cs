using System;
using Other;
using UnityEngine;

namespace Battle.Modifiers
{
    [Serializable]
    public class MoveCounter : IChangeAble, IInit
    {
        [SerializeField] private int moves;
        [SerializeField] private bool delay;

        public int Moves => moves;
        
        public event Action OnMove;
        public event Action OnChanged;

        public bool EndedWork => moves == 0;
        public string SubInfo => moves.ToString();
        
        public MoveCounter(int moves,
            bool delay = false)
        {
            
            this.moves = moves;
            this.delay = delay;
            Init();
        }

        public void Init()
        {
            BattleFlowManager.Instance.OnCycleEnd += Move;
        }


        protected void Move()
        {
            if (delay)
            {
                delay = false;
                return;
            }
            if (EndedWork) return;
            
            OnMove?.Invoke();
            moves -= 1;
            OnChanged?.Invoke();
        }
        public void Concat(MoveCounter other)
        {
            if (other.moves == 0) return;
            
            moves += other.moves;
            OnChanged?.Invoke();
        }
    }
}