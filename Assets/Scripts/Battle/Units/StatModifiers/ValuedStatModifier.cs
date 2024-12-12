using System;
using Battle.Modifiers;
using Battle.UI.ModsDisplaying;
using Knot.Localization;
using UnityEngine;

namespace Battle.Units.StatModifiers
{
    /// <summary>
    ///     Created because of <a href="https://en.wikipedia.org/wiki/Don%27t_repeat_yourself">DRY</a>.
    /// </summary>
    [Serializable]
    public abstract class ValuedStatModifier : UnitModifier, IIntModifier
    {
        [SerializeField] protected int value;

        protected ValuedStatModifier(int value, bool isSaved) : base(isSaved) =>
            this.value = value;

        protected abstract bool IsPositive { get; }

        protected abstract KnotTextKeyReference DescriptionKnotKeyReferencePositive
        {
            get;
        }

        protected abstract KnotTextKeyReference DescriptionKnotKeyReferenceNegative
        {
            get;
        }

        protected abstract Sprite SpritePositive { get; }
        protected abstract Sprite SpriteNegative { get; }


        int IIntModifier.Modify(int val)
        {
            if (EndedWork) return val;
            
            return val + value;
        }

        public override string SubInfo => value.ToString();
        protected override bool HiddenEndedWork => value == 0;

        public override string Description =>
            IsPositive switch
            {
                true => IModIconModifier.SimpleFormatDescription(
                    DescriptionKnotKeyReferencePositive.Value,
                    Math.Abs(value)),
                false => IModIconModifier.SimpleFormatDescription(
                    DescriptionKnotKeyReferenceNegative.Value,
                    Math.Abs(value))
            };

        public override Sprite Sprite =>
            IsPositive switch
            {
                true => SpritePositive,
                false => SpriteNegative
            };

        protected override void HiddenConcat(Modifier other)
        {
            value += ((ValuedStatModifier)other).value;
        }
    }
}