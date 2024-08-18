using System;
using Battle.Modifiers;
using Battle.UI.ModsDisplaying;
using Core.Singleton;
using Other;
using UnityEngine;

namespace Battle.Units.StatModifiers
{
    [Serializable]
    public class Shield : Modifier, IIntModifier, IModIconModifier
    {
        [SerializeField] private Counter counter;

        public Shield(int count, bool save = false) : base(save)
        {
            counter = CreateChangeableSubSystem(new Counter(count));
        }

        public override bool EndedWork => ToDelete;

        int IIntModifier.Modify(int val)
        {
            return counter.Decrease(val);
        }

        public Sprite Sprite => ModifierSpritesContainer.Instance.shield;

        public string Description =>
            IModIconModifier.SimpleFormatDescription(
                ModDescriptionsContainer.Instance.shield.Value,
                counter.Count);

        public string SubInfo => counter.SubInfo;
        public bool ToDelete => counter.EndedWork;

        protected override bool HiddenCanConcat(Modifier other)
        {
            return other is Shield;
        }

        protected override void HiddenConcat(Modifier other)
        {
            counter.ConcatWith(((Shield)other).counter);
        }
    }
}