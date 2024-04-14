using System;
using Core.SingletonContainers;
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

        public override Sprite Sprite => SpritesContainer.instance.shield;
        public override string Description => 
            string.Format(ModDescriptionsContainer.Instance.shieldDescription.Value, counter.Count);
        public override string SubInfo => counter.SubInfo;
        public override bool ToDelete => counter.Count == 0;
        protected override bool CanConcat(Modifier other) => other is Shield;

        public override void Concat(Modifier other) => 
            counter.Concat(((Shield)other).counter);
    }
}