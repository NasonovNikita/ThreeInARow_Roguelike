using System;
using Battle.Modifiers;
using Battle.UI.ModsDisplaying;
using Core.Singleton;
using UnityEngine;

namespace Battle.Units.StatModifiers
{
    [Serializable]
    public class Shield : OneTurnCounter, IIntModifier
    {
        public Shield(int count, bool isSaved = false) : base(count, isSaved)
        { }

        int IIntModifier.Modify(int val) => Counter.Decrease(val);

        public override Sprite Sprite => ModSpritesContainer.Instance.shield;

        public override string Description =>
            IModIconModifier.SimpleFormatDescription(
                ModDescriptionsContainer.Instance.shield.Value, Counter.Count);

        public override string SubInfo => Counter.SubInfo;

        protected override bool HiddenCanConcat(Modifier other) => other is Shield;

        protected override void HiddenConcat(Modifier other)
        {
            Counter.ConcatWith(((Shield)other).Counter);
        }
    }
}