using System;
using Battle.Modifiers;
using Battle.UI.ModsDisplaying;
using Core.Singleton;
using UnityEngine;

namespace Battle.Units.Statuses
{
    [Serializable]
    public class Vampirism : Status
    {
        [SerializeField] private int heal;

        public Vampirism(int healAmount, bool save = false) : base(save)
        {
            heal = healAmount;
        }

        public override Sprite Sprite => ModifierSpritesContainer.Instance.vampirism;

        public override string Description =>
            IModIconModifier.SimpleFormatDescription(
                ModDescriptionsContainer.Instance.vampirism.Value, heal);

        public override string SubInfo => heal.ToString();
        public override bool ToDelete => heal == 0;

        public override void Init(Unit unit)
        {
            unit.OnMadeHit += Heal;

            base.Init(unit);
        }

        private void Heal()
        {
            belongingUnit.hp.Heal(heal);
        }

        protected override bool CanConcat(Modifier other)
        {
            return other is Vampirism;
        }

        public override void Concat(Modifier other)
        {
            heal += ((Vampirism)other).heal;
        }
    }
}