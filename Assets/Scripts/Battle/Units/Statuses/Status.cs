using System;
using Battle.Modifiers;
using Battle.UI.ModsDisplaying;
using UnityEngine;

namespace Battle.Units.Statuses
{
    [Serializable]
    public abstract class Status : Modifier, IModIconModifier
    {
        protected Unit belongingUnit;

        protected Status(bool save = false) : base(save)
        {
        }

        protected BattleFlowManager BattleFlowManager => BattleFlowManager.Instance;

        public abstract Sprite Sprite { get; }

        public abstract string Description { get; }
        public abstract string SubInfo { get; }

        public abstract bool ToDelete { get; }

        public override bool EndedWork => ToDelete;

        public virtual void Init(Unit unit)
        {
            belongingUnit = unit;
        }
    }
}