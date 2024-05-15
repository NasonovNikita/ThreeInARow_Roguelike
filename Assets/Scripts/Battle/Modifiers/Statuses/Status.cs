using System;
using Battle.Units;

namespace Battle.Modifiers.Statuses
{
    [Serializable]
    public abstract class Status : Modifier
    {
        protected Unit belongingUnit;
        protected BattleFlowManager BattleFlowManager => BattleFlowManager.Instance;

        public virtual void Init(Unit unit) => belongingUnit = unit;

        protected Status(bool save = false) : base(save) {}
    }
}