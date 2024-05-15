using System;
using System.Collections.Generic;
using Battle.Units;
using Core.Singleton;
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
        
        public override Sprite Sprite => ModifierSpritesContainer.Instance.passiveBomb;
        public override string Description => 
            FormatDescriptionByKeys(ModDescriptionsContainer.Instance.passiveBomb.Value, 
                new Dictionary<string, object> 
                { 
                    {"damage", dmg}, 
                    {"manaBorder", manaBorder} 
                });
        public override string SubInfo => EmptyInfo;
        public override bool ToDelete => dmg <= 0;

        public override void Init(Unit unit)
        {
            BattleFlowManager.OnCycleEnd += CheckAndApply;
            base.Init(unit);
        }

        private void CheckAndApply()
        {
            if (belongingUnit.mana > manaBorder) return;
            
            foreach (Enemy enemy in BattleFlowManager.enemiesWithNulls)
            {
                enemy.hp.TakeDamage(dmg);
            }
        }

        protected override bool CanConcat(Modifier other) => 
            other is PassiveBomb bomb &&
            bomb.manaBorder == manaBorder;

        public override void Concat(Modifier other) => 
            dmg += ((PassiveBomb)other).dmg;
    }
}