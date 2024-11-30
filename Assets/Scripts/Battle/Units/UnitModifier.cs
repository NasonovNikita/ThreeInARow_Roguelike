using Battle.Modifiers;
using Battle.UI.ModsDisplaying;
using UnityEngine;

namespace Battle.Units
{
    public abstract class UnitModifier : Modifier, IModIconModifier
    {
        protected Unit BelongingUnit;
        
        protected UnitModifier(bool isSaved = false) : base(isSaved) {}

        public virtual void Init(Unit unit)
        {
            BelongingUnit = unit;
        }

        public abstract Sprite Sprite { get; }

        public abstract string Description { get; }
        public abstract string SubInfo { get; }

        public bool ToDelete => EndedWork;
    }
}