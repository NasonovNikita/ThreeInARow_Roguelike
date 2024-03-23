using UnityEngine;

namespace Battle.Units.Modifiers.StatModifiers
{
    public class Shield : Counter, IStatModifier
    {
        public Shield(int count, bool permanent = false) : base(count, permanent) {}

        public Sprite Sprite => throw new System.NotImplementedException();

        public string Description => throw new System.NotImplementedException();
        bool IModifier.ConcatAble(IModifier other) => other is Shield && BothPermanent(other);

        int IStatModifier.Modify(int val) => Decrease(val);
    }
}