using Core.Singleton;
using Knot.Localization;
using UnityEngine;

namespace Battle.Units.StatModifiers
{
    public abstract class ManaWastingMod : ValuedStatModifier
    {
        protected ManaWastingMod(int value, bool isSaved = false) : base(value, isSaved)
        {
        }

        protected override bool IsPositive => value > 0;

        protected override KnotTextKeyReference DescriptionKnotKeyReferencePositive =>
            ModDescriptionsContainer.Instance.manaWastingPositive;

        protected override KnotTextKeyReference DescriptionKnotKeyReferenceNegative =>
            ModDescriptionsContainer.Instance.manaWastingNegative;

        protected override Sprite SpritePositive =>
            ModSpritesContainer.Instance.manaMod;

        protected override Sprite SpriteNegative =>
            ModSpritesContainer.Instance.manaMod;
    }
}