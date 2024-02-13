using System;

namespace Battle.BattleEventHandlers
{
    public class BattleEndThen : BattleEvent
    {
        private readonly Action onAppear;

        public BattleEndThen(Action onAppear) : base()
        {
            this.onAppear = onAppear;
        }

        protected override void Handle(Log log)
        {
            if (log is not BattleEndLog) return;
            onAppear?.Invoke();
        }
    }
}