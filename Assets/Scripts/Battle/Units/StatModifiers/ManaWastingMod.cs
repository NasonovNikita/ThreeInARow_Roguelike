using Core.Singleton;
using Knot.Localization;
using UnityEngine;

namespace Battle.Units.StatModifiers
{
    public abstract class ManaWastingMod : ValuedStatModifier
    {
        protected ManaWastingMod(int value, bool save = false) : base(value, save)
        {
        }

        protected override bool IsPositive => value > 0;

        protected override KnotTextKeyReference DescriptionKnotKeyReferencePositive =>
            ModDescriptionsContainer.Instance.manaWastingPositive;

        protected override KnotTextKeyReference DescriptionKnotKeyReferenceNegative =>
            ModDescriptionsContainer.Instance.manaWastingNegative;

        protected override Sprite SpritePositive => ModifierSpritesContainer.Instance.manaMod;

        protected override Sprite SpriteNegative => ModifierSpritesContainer.Instance.manaMod;
    }
}