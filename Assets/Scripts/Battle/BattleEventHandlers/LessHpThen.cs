using System;
using Unit = Battle.Units.Unit;

namespace Battle.BattleEventHandlers
{
    public class LessHpThen : BattleEvent
    {
        private readonly int value;
        private readonly Unit unit;
        private readonly Action onAppear;
        private bool worked;


        public LessHpThen(int value, Unit unit, Action onAppear) : base()
        {
            this.value = value;
            this.unit = unit;
            this.onAppear = onAppear;
        }

        protected override void Handle(Log log)
        {
            if (log is not DamageLog) return;
            
            switch (worked)
            {
                case false when unit.hp <= value:
                    onAppear?.Invoke();
                    worked = true;
                    break;
                case true when unit.hp > value:
                    worked = false;
                    break;
            }
        }
    }
}