using Core.Singleton;
using Knot.Localization;
using UnityEngine;

namespace Battle.Units.StatModifiers
{
    public abstract class DamageMod : ValuedStatModifier
    {
        protected DamageMod(int value, bool isSaved = false) : base(value, isSaved)
        {
        }

        protected override bool IsPositive => value > 0;

        protected override KnotTextKeyReference DescriptionKnotKeyReferencePositive =>
            ModDescriptionsContainer.Instance.damagePositive;

        protected override KnotTextKeyReference DescriptionKnotKeyReferenceNegative =>
            ModDescriptionsContainer.Instance.damageNegative;

        protected override Sprite SpriteNegative =>
            ModSpritesContainer.Instance.damage;

        protected override Sprite SpritePositive =>
            ModSpritesContainer.Instance.damage;
    }
}