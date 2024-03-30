using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Battle.Modifiers
{
    [Serializable]
    public class MoveCounter : IConcatAble
    {
        [SerializeField] public int moves;
        [SerializeField] public bool delay;
        public Action onMove;


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
            
            onMove?.Invoke();
            moves -= 1;
        }

        public bool ConcatAbleWith(IConcatAble other) => other is MoveCounter;
        public void Concat(IConcatAble other) => moves += ((MoveCounter)other).moves;
        
        private void Init() => Object.FindFirstObjectByType<BattleManager>().onTurnEnd += Move;
    }
}