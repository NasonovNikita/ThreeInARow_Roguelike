using System;
using Battle.Units;

namespace Battle.BattleEventHandlers
{
    public class PlayerGettingHitThen : BattleEvent
    {
        private Action onAppear;

        public PlayerGettingHitThen(Action onAppear) : base()
        {
            this.onAppear = onAppear;
        }

        protected override void Handle(Log log)
        {
            if ((log as GotDamageLog)?.GetData.Item1 is not Player) return;
            onAppear?.Invoke();
        }
    }
}