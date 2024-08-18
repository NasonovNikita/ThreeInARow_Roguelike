using System;
using Battle.Modifiers;
using Battle.UI.ModsDisplaying;
using UnityEngine;

namespace Battle.Units.Statuses
{
    /// <summary>
    ///     Modifier that belongs to <see cref="Unit"/>. Is usually used as a mark
    ///     or has some additional behaviour such as <see cref="Burning"/>.
    /// </summary>
    [Serializable]
    public abstract class Status : Modifier, IModIconModifier
    {
        protected Unit BelongingUnit;

        protected Status(bool save = false) : base(save)
        {
        }

        protected BattleFlowManager BattleFlowManager => BattleFlowManager.Instance;

        public override bool EndedWork => ToDelete;

        public abstract Sprite Sprite { get; }

        public abstract string Description { get; }
        public abstract string SubInfo { get; }

        public abstract bool ToDelete { get; }

        public virtual void Init(Unit unit)
        {
            BelongingUnit = unit;
        }
    }
}