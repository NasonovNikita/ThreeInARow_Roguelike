using System;
using System.Collections.Generic;
using Battle.Modifiers;
using Core.Singleton;
using UI.Battle.ModsDisplaying;
using UnityEngine;

namespace Battle.Units.Statuses
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
            IModIconModifier.FormatDescriptionByKeys(ModDescriptionsContainer.Instance.passiveBomb.Value,
                new Dictionary<string, object>
                {
                    { "damage", dmg },
                    { "manaBorder", manaBorder }
                });

        public override string SubInfo => IModIconModifier.EmptyInfo;
        public override bool ToDelete => dmg <= 0;

        public override void Init(Unit unit)
        {
            BattleFlowManager.Instance.OnEnemiesTurnStart += CheckAndApply;
            base.Init(unit);
        }

        private void CheckAndApply()
        {
            if (belongingUnit.mana < manaBorder) return;

            foreach (Enemy enemy in BattleFlowManager.EnemiesWithoutNulls) enemy.TakeDamage(dmg);
        }

        protected override bool CanConcat(Modifier other)
        {
            return other is PassiveBomb bomb &&
                   bomb.manaBorder == manaBorder;
        }

        public override void Concat(Modifier other)
        {
            dmg += ((PassiveBomb)other).dmg;
        }
    }
}