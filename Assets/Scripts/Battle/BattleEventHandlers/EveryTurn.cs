using System;

namespace Battle.BattleEventHandlers
{
    public class EveryTurn : BattleEvent
    {
        private Action onAppear;
        
        public EveryTurn(Action onAppear) : base()
        {
            this.onAppear = onAppear;
        }

        protected override void Handle(Log log)
        {
            if (log is not TurnLog) return;
            
            onAppear?.Invoke();
        }
    }
}