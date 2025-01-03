using System;
using Battle.Modifiers;
using Core.Singleton;
using Knot.Localization;
using UnityEngine;

namespace Battle.Units.StatModifiers
{
    [Serializable]
    public class HealingConstMod : ValuedStatModifier
    {
        public HealingConstMod(int value, bool isSaved = false) : base(value, isSaved)
        {
        }

        protected override Sprite SpritePositive =>
            ModSpritesContainer.Instance.healing;

        protected override Sprite SpriteNegative =>
            ModSpritesContainer.Instance.healing;

        protected override bool IsPositive => value > 0;

        protected override KnotTextKeyReference DescriptionKnotKeyReferencePositive =>
            ModDescriptionsContainer.Instance.healingPositive;

        protected override KnotTextKeyReference DescriptionKnotKeyReferenceNegative =>
            ModDescriptionsContainer.Instance.healingNegative;

        protected override bool HiddenCanConcat(Modifier other) =>
            other is HealingConstMod;
    }
}