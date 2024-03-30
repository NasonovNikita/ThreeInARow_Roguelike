using System;
using Battle.Units;
using UI.Battle;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Battle.Modifiers.Statuses
{
    [Serializable]
    public abstract class Status : IModIconAble
    {
        protected Unit belongingUnit;
        
        public abstract Sprite Sprite { get; }
        public abstract string Tag { get; }
        public abstract string Description { get; }
        public abstract string SubInfo { get; }
        public abstract bool ToDelete { get; }
        protected BattleManager Manager => Object.FindFirstObjectByType<BattleManager>();

        public virtual void Init(Unit unit) => belongingUnit = unit;
    }
}