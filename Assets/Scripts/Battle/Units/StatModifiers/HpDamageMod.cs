using Core.Singleton;
using Knot.Localization;
using UnityEngine;

namespace Battle.Units.StatModifiers
{
    public abstract class HpDamageMod : ValuedStatModifier
    {
        protected HpDamageMod(int value, bool save = false) : base(value, save)
        {
        }

        protected override bool IsPositive => value <= 0;

        protected override KnotTextKeyReference DescriptionKnotKeyReferencePositive =>
            ModDescriptionsContainer.Instance.hpDamagePositive;

        protected override KnotTextKeyReference DescriptionKnotKeyReferenceNegative =>
            ModDescriptionsContainer.Instance.hpDamageNegative;

        protected override Sprite SpritePositive =>
            ModSpritesContainer.Instance.damage;

        protected override Sprite SpriteNegative =>
            ModSpritesContainer.Instance.damage;
    }
}