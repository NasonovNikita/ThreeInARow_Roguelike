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
        public HealingConstMod(int value, bool save = false) : base(value, save)
        {
        }

        protected override Sprite SpritePositive => ModifierSpritesContainer.Instance.healing;

        protected override Sprite SpriteNegative => ModifierSpritesContainer.Instance.healing;

        protected override bool IsPositive => value > 0;

        protected override KnotTextKeyReference DescriptionKnotKeyReferencePositive =>
            ModDescriptionsContainer.Instance.healingPositive;

        protected override KnotTextKeyReference DescriptionKnotKeyReferenceNegative =>
            ModDescriptionsContainer.Instance.healingNegative;

        protected override bool CanConcat(Modifier other)
        {
            return other is HealingConstMod;
        }
    }
}