using System;
using Battle.Modifiers;
using Battle.UI.ModsDisplaying;
using Knot.Localization;
using UnityEngine;

namespace Battle.Units.StatModifiers
{
    [Serializable]
    public abstract class ValuedStatModifier : Modifier, IIntModifier, IModIconModifier
    {
        [SerializeField] protected int value;

        protected ValuedStatModifier(int value, bool save) : base(save)
        {
            this.value = value;
        }

        protected abstract bool IsPositive { get; }

        protected abstract KnotTextKeyReference DescriptionKnotKeyReferencePositive { get; }
        protected abstract KnotTextKeyReference DescriptionKnotKeyReferenceNegative { get; }

        protected abstract Sprite SpritePositive { get; }
        protected abstract Sprite SpriteNegative { get; }


        int IIntModifier.Modify(int val)
        {
            return val + value;
        }

        public override bool EndedWork => ToDelete;

        public virtual string SubInfo => value.ToString();
        public virtual bool ToDelete => value == 0;

        public string Description =>
            IsPositive switch
            {
                true => IModIconModifier.SimpleFormatDescription(
                    DescriptionKnotKeyReferencePositive.Value,
                    Math.Abs(value)),
                false => IModIconModifier.SimpleFormatDescription(
                    DescriptionKnotKeyReferenceNegative.Value,
                    Math.Abs(value))
            };

        public Sprite Sprite =>
            IsPositive switch
            {
                true => SpritePositive,
                false => SpriteNegative
            };

        public override void Concat(Modifier other)
        {
            value += ((ValuedStatModifier)other).value;
        }
    }
}