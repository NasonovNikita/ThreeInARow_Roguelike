using System;
using Other;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Battle.Modifiers
{
    [Serializable]
    public class MoveCounter : IChangeAble
    {
        [SerializeField] private int moves;
        [SerializeField] private bool delay;

        public int Moves => moves;
        
        public event Action OnMove;
        public event Action OnChanged;


        public MoveCounter(int moves,
            bool delay = false)
        {
            this.moves = moves;
            this.delay = delay;
            BattleManager.onBattleStart += Init;
        }

        public bool EndedWork => moves == 0;
        public string SubInfo => moves.ToString();


        protected void Move()
        {
            if (delay)
            {
                delay = false;
                return;
            }
            if (EndedWork) return;
            
            OnMove?.Invoke();
            OnChanged?.Invoke();
            moves -= 1;
        }
        public void Concat(MoveCounter other)
        {
            moves += other.moves;
            OnChanged?.Invoke();
        }

        private void Init() => Object.FindFirstObjectByType<BattleManager>().onTurnEnd += Move;
    }
}