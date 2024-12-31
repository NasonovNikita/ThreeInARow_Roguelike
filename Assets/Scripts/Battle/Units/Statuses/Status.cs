using System;
using Battle.UI.ModsDisplaying;
using UnityEngine;

namespace Battle.Units.Statuses
{
    /// <summary>
    ///     Modifier that belongs to <see cref="Unit"/>. Is usually used as a mark
    ///     or has some additional behaviour such as <see cref="Burning"/>.
    /// </summary>
    [Serializable]
    public abstract class Status : UnitModifier
    {
        protected Status(bool isSaved = false) : base(isSaved)
        {
        }

        protected BattleFlowManager BattleFlowManager => BattleFlowManager.Instance;
    }
}