using System;

namespace Battle.Modifiers
{
    [Serializable]
    public class Modifier
    {
        public int moves;

        public ModType type;
    
        public float value;
        
        public bool IsPositive { get; private set; }

        private bool delay;

        private Action onMove;

        public Modifier(int moves, ModType type, bool isPositive = true, float value = 1, Action onMove = null, bool delay = false)
        {
            this.moves = moves;
            this.type = type;
            this.value = value;
            this.onMove = onMove;
            this.delay = delay;
            IsPositive = isPositive;
            ILog.OnLog += Move;
        }

        public float Use()
        {
            return moves != 0 ? value : 0;
        }

        public void TurnOff()
        {
            moves = 0;
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