using UI.Battle;
using UnityEngine;

namespace Battle.Modifiers.StatModifiers
{
    public abstract class BaseStatModifier : IStatModifier, IConcatAble, ISaveAble, IModIconAble
    {
        [SerializeField] protected int value;

        protected BaseStatModifier(int value, bool save = false)
        {
            this.value = value;
            Save = save;
        }

        public abstract Sprite Sprite { get; }
        public abstract string Tag { get; }
        public abstract string Description { get; }
        public abstract string SubInfo { get; }
        public abstract bool ToDelete { get; }

        int IStatModifier.Modify(int val) => val + value;

        public bool ConcatAbleWith(IConcatAble other) =>
            other is BaseStatModifier basicStatModifier &&
            Save == basicStatModifier.Save &&
            ConcatAbleWith(basicStatModifier);

        protected abstract bool ConcatAbleWith(BaseStatModifier other);

        public void Concat(IConcatAble other) =>
            value += ((BaseStatModifier)other).value;

        public bool Save { get; }
    }
}