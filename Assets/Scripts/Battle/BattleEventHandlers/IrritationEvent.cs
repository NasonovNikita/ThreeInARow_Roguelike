using Battle.Modifiers;
using Battle.Units;
using Battle.Units.Stats;

namespace Battle.BattleEventHandlers
{
    public class IrritationEvent : BattleEvent
    {
        private readonly float increaseValue;
        private readonly Unit unit;
        
        public IrritationEvent(float value, Unit unit) : base()    // Ilog.onLog += Handle
        {
            increaseValue = value;
            this.unit = unit;
        }
        
        protected override void Handle(ILog log)
        {
            base.Handle(log);

            if (log is not TurnLog) return;
            if (!BattleLog.GetLastTurn().Exists(v => v is DeathLog))
                unit.AddDamageMod(new DamageMod(-1, ModType.Add, value: increaseValue));
            ILog.OnLog -= Handle;
        }
    }
}