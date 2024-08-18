using System;
using System.Collections.Generic;
using Battle.Modifiers;
using Battle.UI.ModsDisplaying;
using Battle.Units.StatModifiers;
using Core.Singleton;
using UnityEngine;

namespace Battle.Units.Statuses
{
    [Serializable]
    public class Sharp : Status
    {
        [SerializeField] private int addition;

        private List<Unit> _hitEnemies = new();

        public Sharp(int addition, bool save = false) : base(save)
        {
            this.addition = addition;
        }

        public override Sprite Sprite => ModifierSpritesContainer.Instance.sharp;

        public override string Description =>
            IModIconModifier.SimpleFormatDescription(
                ModDescriptionsContainer.Instance.sharp.Value,
                addition);

        public override string SubInfo => addition.ToString();
        public override bool ToDelete => addition == 0;

        public override void Init(Unit unit)
        {
            foreach (var enemy in unit.Enemies)
                enemy.hp.OnValueChanged += _ => ApplyMod(enemy);

            BattleFlowManager.OnCycleEnd += EmptyEnemiesList;

            base.Init(unit);
        }

        private void ApplyMod(Unit enemy)
        {
            if (_hitEnemies.Contains(enemy)) return;

            enemy.hp.onTakingDamageMods.Add(new HpDamageConstMod(addition));
            _hitEnemies.Add(enemy);
        }

        private void EmptyEnemiesList()
        {
            _hitEnemies = new List<Unit>();
        }

        protected override bool HiddenCanConcat(Modifier other)
        {
            return other is Sharp;
        }

        protected override void HiddenConcat(Modifier other)
        {
            addition += ((Sharp)other).addition;
        }
    }
}