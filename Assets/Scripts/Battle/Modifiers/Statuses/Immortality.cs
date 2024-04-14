using System;
using Battle.Units;
using Other;
using UnityEngine;

namespace Battle.Modifiers.Statuses
{
    [Serializable]
    public class Immortality : Status
    {
        [SerializeField] private int chance;

        public Immortality(int chance, bool save = false) : base(save) => this.chance = chance;

        public override Sprite Sprite => throw new NotImplementedException();
        public override string Description => throw new NotImplementedException();
        public override string SubInfo => EmptyInfo;
        public override bool ToDelete => chance <= 0;

        public override void Init(Unit unit)
        {
            unit.hp.OnValueChanged += _ => CheckAndApply();
            
            base.Init(unit);
        }

        private void CheckAndApply()
        {
            if (belongingUnit.hp <= 0 && Tools.Random.RandomChance(chance)) 
                belongingUnit.hp.Heal(1);
        }

        protected override bool CanConcat(Modifier other) => 
            other is Immortality;

        public override void Concat(Modifier other)
        {
            chance += ((Immortality)other).chance;
            chance = Math.Max(chance, 100);
        }
    }
}