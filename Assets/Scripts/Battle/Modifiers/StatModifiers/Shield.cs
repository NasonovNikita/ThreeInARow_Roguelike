using System;
using Core.Singleton;
using UnityEngine;

namespace Battle.Modifiers.StatModifiers
{
    [Serializable]
    public class Shield : StatModifier
    {
        [SerializeField] private Counter counter;

        public Shield(int count, bool save = false) : base(save)
        {
            counter = CreateChangeableSubSystem(new Counter(count));
        }
        
        protected override int Modify(int val) => counter.Decrease(val);

        public override Sprite Sprite => ModifierSpritesContainer.Instance.shield;
        public override string Description => 
            SimpleFormatDescription(ModDescriptionsContainer.Instance.shield.Value, counter.Count);
        public override string SubInfo => counter.SubInfo;
        public override bool ToDelete => counter.EndedWork;
        protected override bool CanConcat(Modifier other) => other is Shield;

        public override void Concat(Modifier other) => 
            counter.Concat(((Shield)other).counter);
    }
}