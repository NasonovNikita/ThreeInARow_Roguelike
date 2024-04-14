using System;
using Core.SingletonContainers;
using Knot.Localization;
using UnityEngine;

namespace Battle.Modifiers.StatModifiers
{
    [Serializable]
    public class ManaWastingMod : ValuedStatModifier
    {
        public ManaWastingMod(int value, bool save = false) : base(value, save) {}

        public override Sprite Sprite => throw new NotImplementedException();

        protected override KnotTextKeyReference DescriptionKnotKeyReferencePositive => ModDescriptionsContainer.Instance.;

        protected override bool CanConcat(Modifier other) => other is ManaWastingMod;
    }
}