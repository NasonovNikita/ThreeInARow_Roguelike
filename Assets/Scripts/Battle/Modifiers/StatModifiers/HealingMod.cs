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

        protected override Sprite SpritePositive => SpritesContainer.Instance.hpHealing;

        protected override Sprite SpriteNegative => SpritesContainer.Instance.hpHealing;

        protected override bool IsPositive => value > 0;

        protected override KnotTextKeyReference DescriptionKnotKeyReferencePositive => 
            ModDescriptionsContainer.Instance.healingModDescriptionPositive;

        protected override KnotTextKeyReference DescriptionKnotKeyReferenceNegative =>
            ModDescriptionsContainer.Instance.healingModDescriptionNegative;

        protected override bool CanConcat(Modifier other) => 
            other is HealingMod;
    }
}