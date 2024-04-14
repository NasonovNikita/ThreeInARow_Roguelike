using System;
using Knot.Localization;
using UnityEngine;

namespace Battle.Modifiers.StatModifiers
{
    [Serializable]
    public abstract class ValuedStatModifier : StatModifier
    {
        [SerializeField] protected int value;
        
        protected ValuedStatModifier(int value, bool save) : base(save) => this.value = value;
        
        public override string SubInfo => value.ToString();
        public override bool ToDelete => value == 0;
        
        protected abstract KnotTextKeyReference DescriptionKnotKeyReferencePositive { get; }
        protected abstract KnotTextKeyReference DescriptionKnotKeyReferenceNegative { get; }

        public override string Description
        {
            get
            {
                return string.Format(DescriptionKnotKeyReferencePositive.Value, value);
            }
        }

        protected override int Modify(int val) => val + value;

        public override void Concat(Modifier other) => value += ((ValuedStatModifier)other).value;
    }
}