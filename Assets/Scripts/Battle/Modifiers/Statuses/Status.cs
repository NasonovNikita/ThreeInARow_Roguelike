using System;
using Battle.Units;
using Object = UnityEngine.Object;

namespace Battle.Modifiers.Statuses
{
    [Serializable]
    public abstract class Status : Modifier
    {
        protected Unit belongingUnit;
        protected BattleManager Manager => Object.FindFirstObjectByType<BattleManager>();

        public virtual void Init(Unit unit) => belongingUnit = unit;

        protected Status(bool save = false) : base(save) {}
    }
}