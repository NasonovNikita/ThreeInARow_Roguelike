using System;
using Core.SingletonContainers;
using Knot.Localization;
using UnityEngine;

namespace Battle.Modifiers.StatModifiers
{
    [Serializable]
    public class HealingMod : ValuedStatModifier
    {
        public HealingMod(int value, bool save = false) : base(value, save) {}

        public override Sprite Sprite => SpritesContainer.instance.hpHealing;

        protected override KnotTextKeyReference DescriptionKnotKeyReferencePositive => 
            ModDescriptionsContainer.Instance.healingModDescription;

        protected override bool CanConcat(Modifier other) => 
            other is HealingMod;
    }
}