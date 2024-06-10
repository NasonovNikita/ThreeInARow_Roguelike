using System;
using Battle.Modifiers;
using Core.Singleton;
using UI.Battle.ModsDisplaying;
using UnityEngine;

namespace Battle.Units.StatModifiers
{
    [Serializable]
    public class Shield : Modifier, IIntModifier, IModIconModifier
    {
        [SerializeField] private Counter counter;

        public override bool EndedWork => ToDelete;

        public Shield(int count, bool save = false) : base(save)
        {
            counter = CreateChangeableSubSystem(new Counter(count));
        }

        public Sprite Sprite => ModifierSpritesContainer.Instance.shield;

        public string Description =>
            IModIconModifier.SimpleFormatDescription(ModDescriptionsContainer.Instance.shield.Value, counter.Count);

        public string SubInfo => counter.SubInfo;
        public bool ToDelete => counter.EndedWork;

        int IIntModifier.Modify(int val)
        {
            return counter.Decrease(val);
        }

        protected override bool CanConcat(Modifier other)
        {
            return other is Shield;
        }

        public override void Concat(Modifier other)
        {
            counter.Concat(((Shield)other).counter);
        }
    }
}