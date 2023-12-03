using Battle.Modifiers;
using Battle.Units;

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
            {

                unit.cDmg.AddMod(new Modifier(-1, ModType.Add, value: increaseValue), ModAffect.ValueGet);
                unit.fDmg.AddMod(new Modifier(-1, ModType.Add, value: increaseValue), ModAffect.ValueGet);
                unit.pDmg.AddMod(new Modifier(-1, ModType.Add, value: increaseValue), ModAffect.ValueGet);
                unit.lDmg.AddMod(new Modifier(-1, ModType.Add, value: increaseValue), ModAffect.ValueGet);
                unit.phDmg.AddMod(new Modifier(-1, ModType.Add, value: increaseValue), ModAffect.ValueGet);
                unit.mDmg.AddMod(new Modifier(-1, ModType.Add, value: increaseValue), ModAffect.ValueGet);
            }
            ILog.OnLog -= Handle;
        }
    }
}