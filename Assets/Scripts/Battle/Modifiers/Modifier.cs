using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Battle.Modifiers
{
    [Serializable]
    public class Modifier
    {
        public int moves;

        public ModType type;
    
        public float value;

        private bool delay;

        private Action onMove;

        public Modifier(int moves, ModType type, float value = 1, Action onMove = null, bool delay = false)
        {
            this.moves = moves;
            this.type = type;
            this.value = value;
            this.onMove = onMove;
            this.delay = delay;
            ILog.OnLog += Move;
        }

        public float Use()
        {
            return moves != 0 ? value : 0;
        }
        private void Move(ILog log)
        {
            if (log is not TurnLog) return;
            
            if (delay)
            {
                delay = false;
                return;
            }
            if (moves == 0) return;
            onMove?.Invoke();
            moves -= 1;
        }
    }
}