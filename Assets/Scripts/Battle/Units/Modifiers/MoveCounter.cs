using System;
using UnityEngine;

namespace Battle.Units.Modifiers
{
    [Serializable]
    public abstract class MoveCounter : Modifier
    {
        [SerializeField] public int moves;
        [SerializeField] public bool delay;

        protected MoveCounter(int moves,
            bool delay = false,
            bool permanent = false) : base(permanent)
        {
            this.moves = moves;
            this.delay = delay;
            BattleManager.onTurnEnd += Move;
            BattleManager.onBattleEnd += TurnOff;
        }

        public bool IsZero => moves == 0;

        public string SubInfo => permanent || moves < 0 ? "-" : moves.ToString();

        public void Concat(IModifier other) =>
            moves += ((MoveCounter)other).moves;

        private void TurnOff()
        {
            if (!permanent) moves = 0;
        }

        protected virtual void Move()
        {
            if (delay)
            {
                delay = false;
                return;
            }
            if (moves <= 0) return;
            moves -= 1;
        }
    }
}