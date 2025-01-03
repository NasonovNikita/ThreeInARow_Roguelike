using System;
using System.Collections.Generic;
using Battle.Modifiers;
using Battle.UI.ModsDisplaying;
using Core.Singleton;
using UnityEngine;

namespace Battle.Units.Statuses
{
    [Serializable]
    public class PassiveBomb : Status
    {
        [SerializeField] private int dmg;
        [SerializeField] private int manaBorder;

        public PassiveBomb(int damage, int manaBorder, bool isSaved = false) : base(isSaved)
        {
            dmg = damage;
            this.manaBorder = manaBorder;
        }

        public override Sprite Sprite => ModSpritesContainer.Instance.passiveBomb;

        public override string Description =>
            IModIconModifier.FormatDescriptionByKeys(
                ModDescriptionsContainer.Instance.passiveBomb.Value,
                new Dictionary<string, object>
                {
                    { "damage", dmg },
                    { "manaBorder", manaBorder }
                });

        public override string SubInfo => IModIconModifier.EmptyInfo;
        protected override bool HiddenEndedWork => dmg <= 0;

        public override void Init(Unit unit)
        {
            BattleFlowManager.Instance.OnEnemiesTurnStart += CheckAndApply;
            base.Init(unit);
        }

        private void CheckAndApply()
        {
            if (BelongingUnit.mana < manaBorder) return;

            foreach (var enemy in BattleFlowManager.EnemiesAlive)
                enemy.TakeDamage(dmg);
        }

        protected override bool HiddenCanConcat(Modifier other) =>
            other is PassiveBomb bomb &&
            bomb.manaBorder == manaBorder;

        protected override void HiddenConcat(Modifier other)
        {
            dmg += ((PassiveBomb)other).dmg;
        }
    }
}