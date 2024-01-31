using System;
using Battle.Units;

namespace Battle.BattleEventHandlers
{
    public class EnemyGettingHitThen : BattleEvent
    {
        private readonly Action onAppear;

        public EnemyGettingHitThen(Action onAppear) : base()
        {
            this.onAppear = onAppear;
        }

        protected override void Handle(Log log)
        {
            if ((log as GotDamageLog)?.GetData.Item1 is not Enemy) return;
            onAppear?.Invoke();
        }
    }
}