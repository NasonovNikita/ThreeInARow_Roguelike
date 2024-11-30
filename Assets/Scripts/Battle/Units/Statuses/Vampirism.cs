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

        public Vampirism(int healAmount, bool isSaved = false) : base(isSaved) =>
            heal = healAmount;

        public override Sprite Sprite => ModSpritesContainer.Instance.vampirism;

        public override string Description =>
            IModIconModifier.SimpleFormatDescription(
                ModDescriptionsContainer.Instance.vampirism.Value, heal);

        public override string SubInfo => heal.ToString();
        protected override bool HiddenEndedWork => heal == 0;

        public override void Init(Unit unit)
        {
            unit.OnMadeHit += Heal;

            base.Init(unit);
        }

        private void Heal()
        {
            BelongingUnit.hp.Heal(heal);
        }

        protected override bool HiddenCanConcat(Modifier other) => other is Vampirism;

        protected override void HiddenConcat(Modifier other)
        {
            heal += ((Vampirism)other).heal;
        }
    }
}