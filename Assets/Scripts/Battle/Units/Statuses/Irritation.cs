using System;
using System.Linq;
using Battle.Modifiers;
using Battle.Units.StatModifiers;
using Core.Singleton;
using UI.Battle.ModsDisplaying;
using UnityEngine;

namespace Battle.Units.Statuses
{
    [Serializable]
    public class Irritation : Status
    {
        [SerializeField] private int damageAddition;
        [SerializeField] private MoveCounter moveCounter;
        private bool enemyDied;

        public Irritation(int damageAddition, int moves, bool save = false) : base(save)
        {
            this.damageAddition = damageAddition;
            moveCounter = CreateChangeableSubSystem(new MoveCounter(moves));
        }

        public override Sprite Sprite => ModifierSpritesContainer.Instance.irritation;

        public override string Description =>
            IModIconModifier.SimpleFormatDescription(ModDescriptionsContainer.Instance.irritation.Value, damageAddition);

        public override string SubInfo => moveCounter.SubInfo;
        public override bool ToDelete => moveCounter.EndedWork || damageAddition == 0;

        public override void Init(Unit unit)
        {
            foreach (Unit enemy in unit.Enemies.Where(enemy => enemy != null))
                enemy.hp.OnValueChanged += _ => CheckEnemy(enemy);

            BattleFlowManager.OnCycleEnd += CheckAndApply;

            base.Init(unit);
        }

        private void CheckEnemy(Unit enemy)
        {
            if (enemy.Dead) enemyDied = true;
        }

        private void CheckAndApply()
        {
            if (!enemyDied) belongingUnit.damage.mods.Add(new DamageConstMod(damageAddition));
            enemyDied = false;
        }

        protected override bool CanConcat(Modifier other)
        {
            return other is Irritation;
        }

        public override void Concat(Modifier other)
        {
            damageAddition += ((Irritation)other).damageAddition;
        }
    }
}