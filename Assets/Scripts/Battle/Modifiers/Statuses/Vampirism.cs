using System;
using Battle.Units;
using UnityEngine;

namespace Battle.Modifiers.Statuses
{
    [Serializable]
    public class Vampirism : Status
    {
        [SerializeField] private int heal;

        public Vampirism(int healAmount, bool save = false) : base(save) => 
            heal = healAmount;

        public override Sprite Sprite => throw new NotImplementedException();
        public override string Description => throw new NotImplementedException();
        public override string SubInfo => heal.ToString();
        public override bool ToDelete => heal == 0;

        public override void Init(Unit unit)
        {
            unit.OnMadeHit += Heal;
            
            base.Init(unit);
        }

        private void Heal() => belongingUnit.hp.Heal(heal);
        
        protected override bool CanConcat(Modifier other) => 
            other is Vampirism;

        public override void Concat(Modifier other) => 
            heal += ((Vampirism)other).heal;
    }
}