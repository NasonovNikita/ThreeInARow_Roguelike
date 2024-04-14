using System;
using Battle.Units;
using UnityEngine;

namespace Battle.Modifiers.Statuses
{
    [Serializable]
    public class PassiveBomb : Status
    {
        [SerializeField] private int dmg;
        [SerializeField] private int manaBorder;

        public PassiveBomb(int damage, int manaBorder, bool save = false) : base(save)
        {
            dmg = damage;
            this.manaBorder = manaBorder;
        }
        
        public override Sprite Sprite => throw new NotImplementedException();
        public override string Description => throw new NotImplementedException();
        public override string SubInfo => EmptyInfo;
        public override bool ToDelete => dmg <= 0;

        public override void Init(Unit unit)
        {
            Manager.onTurnEnd += CheckAndApply;
            base.Init(unit);
        }

        private void CheckAndApply()
        {
            if (belongingUnit.mana > manaBorder) return;
            
            foreach (var enemy in Manager.Enemies)
            {
                enemy.TakeDamage(dmg);
            }
        }

        protected override bool CanConcat(Modifier other) => 
            other is PassiveBomb bomb &&
            bomb.manaBorder == manaBorder;

        public override void Concat(Modifier other) => 
            dmg += ((PassiveBomb)other).dmg;
    }
}