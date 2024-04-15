using Core.SingletonContainers;
using Knot.Localization;
using UnityEngine;

namespace Battle.Modifiers.StatModifiers
{
    public abstract class DamageMod : ValuedStatModifier
    {
        protected DamageMod(int value, bool save = false) : base(value, save) {}

        protected override bool IsPositive => value > 0;

        protected override KnotTextKeyReference DescriptionKnotKeyReferencePositive => 
            ModDescriptionsContainer.Instance.damageModDescriptionPositive;
        protected override KnotTextKeyReference DescriptionKnotKeyReferenceNegative =>
            ModDescriptionsContainer.Instance.damageModDescriptionNegative;

        protected override Sprite SpriteNegative => SpritesContainer.Instance.damageMod;
        protected override Sprite SpritePositive => SpritesContainer.Instance.damageMod;
    }
}