using System;
using UnityEngine;

namespace Battle.Modifiers.StatModifiers
{
    [Serializable]
    public class DamageMod : SimpleStatModifier
    {
        public DamageMod(int value, bool save = false) : base(value, save) {}
        
        public override Sprite Sprite => throw new NotImplementedException();

        public override string Tag => throw new NotImplementedException();
        public override string Description => throw new NotImplementedException();
        protected override bool ConcatAbleWith(BaseStatModifier other) => 
            other is DamageMod;
    }
}
