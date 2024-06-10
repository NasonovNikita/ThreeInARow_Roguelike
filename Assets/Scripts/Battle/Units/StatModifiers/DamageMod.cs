using Core.Singleton;
using Knot.Localization;
using UnityEngine;

namespace Battle.Units.StatModifiers
{
    public abstract class DamageMod : ValuedStatModifier
    {
        protected DamageMod(int value, bool save = false) : base(value, save)
        {
        }

        protected override bool IsPositive => value > 0;

        protected override KnotTextKeyReference DescriptionKnotKeyReferencePositive =>
            ModDescriptionsContainer.Instance.damagePositive;

        protected override KnotTextKeyReference DescriptionKnotKeyReferenceNegative =>
            ModDescriptionsContainer.Instance.damageNegative;

        protected override Sprite SpriteNegative => ModifierSpritesContainer.Instance.damage;
        protected override Sprite SpritePositive => ModifierSpritesContainer.Instance.damage;
    }
}