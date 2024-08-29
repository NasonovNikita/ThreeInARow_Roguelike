using System;
using System.Linq;
using Battle.Modifiers;
using Battle.UI.ModsDisplaying;
using Battle.Units.StatModifiers;
using Core.Singleton;
using UnityEngine;

namespace Battle.Units.Statuses
{
    [Serializable]
    public class Irritation : Status
    {
        [SerializeField] private int damageAddition;
        [SerializeField] private MoveCounter moveCounter;
        private bool _enemyDied;

        public Irritation(int damageAddition, int moves, bool save = false) : base(save)
        {
            this.damageAddition = damageAddition;
            moveCounter = CreateChangeableSubSystem(new MoveCounter(moves));
        }

        public override Sprite Sprite => ModSpritesContainer.Instance.irritation;

        public override string Description =>
            IModIconModifier.SimpleFormatDescription(
                ModDescriptionsContainer.Instance.irritation.Value, damageAddition);

        public override string SubInfo => moveCounter.SubInfo;

        protected override bool HiddenEndedWork =>
            moveCounter.EndedWork || damageAddition == 0;

        public override void Init(Unit unit)
        {
            foreach (var enemy in unit.Enemies.Where(enemy => enemy != null))
                enemy.hp.OnValueChanged += _ => CheckEnemy(enemy);

            BattleFlowManager.OnCycleEnd += CheckAndApply;

            base.Init(unit);
        }

        private void CheckEnemy(Unit enemy)
        {
            if (enemy.Dead) _enemyDied = true;
        }

        private void CheckAndApply()
        {
            if (!_enemyDied)
                BelongingUnit.damage.mods.Add(new DamageConstMod(damageAddition));
            _enemyDied = false;
        }

        protected override bool HiddenCanConcat(Modifier other) => other is Irritation;

        protected override void HiddenConcat(Modifier other)
        {
            damageAddition += ((Irritation)other).damageAddition;
        }
    }
}