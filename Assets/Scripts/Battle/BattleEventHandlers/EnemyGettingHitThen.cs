using System;

namespace Battle.BattleEventHandlers
{
    public class EnemyGettingHitThen : BattleEvent
    {
        private Action onAppear;

        public EnemyGettingHitThen(Action onAppear) : base()
        {
            this.onAppear = onAppear;
        }

        protected override void Handle(ILog log)
        {
            if (log is not PToEDamageLog) return;
            onAppear?.Invoke();
        }
    }
}