using UI.Battle;
using UnityEngine;

namespace Battle.Units.Modifiers.Statuses
{
    public abstract class Status : IModIconAble
    {
        protected Unit belongingUnit;
        
        public abstract Sprite Sprite { get; }
        public abstract string Tag { get; }
        public abstract string Description { get; }
        public abstract string SubInfo { get; }
        public abstract bool ToDelete { get; }

        public virtual void Init(Unit unit) => belongingUnit = unit;
    }
}