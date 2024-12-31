using System;
using Other;
using UnityEngine;

namespace Battle
{
    /// <summary>
    ///     Has move counter and subtracts one move each finished cycle in battle.
    ///     Action done on each move can be attached. Stops invoking action if no moves left.
    /// </summary>
    [Serializable]
    public class MoveCounter : IChangeAble, IInitiated
    {
        [SerializeField] private int moves;
        [SerializeField] private bool delay;

        public MoveCounter(int moves,
            bool delay = false)
        {
            this.moves = moves;
            this.delay = delay;
        }

        public int Moves => moves;
        public string SubInfo => moves.ToString();

        public bool EndedWork => moves == 0;
        public event Action OnChanged;

        public void Init()
        {
            BattleFlowManager.Instance.OnCycleEnd += Move;
        }

        public event Action OnMove;


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