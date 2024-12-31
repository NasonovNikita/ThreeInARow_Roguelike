using System;
using Battle.Modifiers;
using Battle.UI.ModsDisplaying;
using Core.Singleton;
using UnityEngine;

namespace Battle.Units.StatModifiers
{
    [Serializable]
    public class Frozen : OneTurnCounter, IIntModifier
    {
        public Frozen(int count, bool isSaved = false) : base(count, isSaved)
        { }

        int IIntModifier.Modify(int val) => Counter.Decrease(val);

        public override Sprite Sprite => ModSpritesContainer.Instance.frozen;

        public override string Description =>
            IModIconModifier.SimpleFormatDescription(
                ModDescriptionsContainer.Instance.frozen.Value, Counter.Count);

        public override string SubInfo => Counter.SubInfo;

        protected override bool HiddenCanConcat(Modifier other) =>
            other is Frozen;

        protected override void HiddenConcat(Modifier other) => 
            Counter.ConcatWith(((Frozen)other).Counter);
    }
}