using System;
using Core.Singleton;
using Knot.Localization;
using UI.Battle.ModsDisplaying;
using UnityEngine;

namespace Battle.Modifiers.StatModifiers
{
    [Serializable]
    public class HealingConstMod : ValuedStatModifier
    {
        public HealingConstMod(int value, bool save = false) : base(value, save) {}

        protected override Sprite SpritePositive => ModifierSpritesContainer.Instance.healing;

        protected override Sprite SpriteNegative => ModifierSpritesContainer.Instance.healing;

        protected override bool IsPositive => value > 0;

        protected override KnotTextKeyReference DescriptionKnotKeyReferencePositive => 
            ModDescriptionsContainer.Instance.healingPositive;

        protected override KnotTextKeyReference DescriptionKnotKeyReferenceNegative =>
            ModDescriptionsContainer.Instance.healingNegative;

        protected override bool CanConcat(Modifier other) => 
            other is HealingConstMod;
    }
}