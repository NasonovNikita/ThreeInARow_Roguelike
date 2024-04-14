using Core.SingletonContainers;
using Knot.Localization;
using UnityEngine;

namespace Battle.Modifiers.StatModifiers
{
    public abstract class DamageMod : ValuedStatModifier
    {
        protected DamageMod(int value, bool save = false) : base(value, save) {}

        public override Sprite Sprite => SpritesContainer.instance.damageMod;

        protected override KnotTextKeyReference DescriptionKnotKeyReferencePositive => 
            ModDescriptionsContainer.Instance.damageModDescription;
    }
}