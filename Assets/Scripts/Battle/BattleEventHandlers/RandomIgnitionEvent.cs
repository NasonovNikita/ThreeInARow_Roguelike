using Battle.Units;
using UnityEngine;

namespace Battle.BattleEventHandlers
{
    public class RandomIgnitionEvent : BattleEvent
    {
        private readonly int chance;
        private readonly Unit unit;
        private int moves;

        public RandomIgnitionEvent(int chance, Unit unit, int moves) : base()
        {
            this.chance = chance;
            this.unit = unit;
            this.moves = moves;
        }

        protected override void Handle(ILog log)
        {
            base.Handle(log);
            if (log is not TurnLog) return;
            
            moves -= 1;
            if (chance <= Random.Range(0, 101))
            {
                unit.StartBurning(1);
            }
        }
    }
}