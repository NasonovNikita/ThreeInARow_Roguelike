using UnityEngine;

namespace Battle.Modifiers.StatModifiers
{
    public class HealingMod : SimpleStatModifier
    {
        public HealingMod(int value, bool save = false) : base(value, save) {}

        public override Sprite Sprite => throw new System.NotImplementedException();

        public override string Tag => throw new System.NotImplementedException();

        public override string Description => throw new System.NotImplementedException();
        
        protected override bool ConcatAbleWith(BaseStatModifier other) =>
            other is HealingMod;
    }
}