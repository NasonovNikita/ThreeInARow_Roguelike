using System;
using Battle.Modifiers;
using Knot.Localization;
using UI.Battle.ModsDisplaying;
using UnityEngine;

namespace Battle.Units.StatModifiers
{
    [Serializable]
    public abstract class ValuedStatModifier : Modifier, IIntModifier, IModIconModifier
    {
        [SerializeField] protected int value;

        public override bool EndedWork => ToDelete;

        protected ValuedStatModifier(int value, bool save) : base(save)
        {
            this.value = value;
        }

        public virtual string SubInfo => value.ToString();
        public virtual bool ToDelete => value == 0;

        protected abstract bool IsPositive { get; }

        protected abstract KnotTextKeyReference DescriptionKnotKeyReferencePositive { get; }
        protected abstract KnotTextKeyReference DescriptionKnotKeyReferenceNegative { get; }

        public string Description =>
            IsPositive switch
            {
                true => IModIconModifier.SimpleFormatDescription(DescriptionKnotKeyReferencePositive.Value, 
                    Math.Abs(value)),
                false => IModIconModifier.SimpleFormatDescription(DescriptionKnotKeyReferenceNegative.Value, 
                    Math.Abs(value))
            };

        protected abstract Sprite SpritePositive { get; }
        protected abstract Sprite SpriteNegative { get; }

        public Sprite Sprite =>
            IsPositive switch
            {
                true => SpritePositive,
                false => SpriteNegative
            };


        int IIntModifier.Modify(int val)
        {
            return val + value;
        }

        public override void Concat(Modifier other)
        {
            value += ((ValuedStatModifier)other).value;
        }
    }
}