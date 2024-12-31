using System;
using Battle.Modifiers;
using Battle.UI.ModsDisplaying;
using Core.Singleton;
using Other;
using UnityEngine;

namespace Battle.Units.Statuses
{
    [Serializable]
    public class Immortality : Status
    {
        [SerializeField] private int chance;

        public Immortality(int chance, bool isSaved = false) : base(isSaved) =>
            this.chance = chance;

        public override Sprite Sprite => ModSpritesContainer.Instance.immortality;

        public override string Description =>
            IModIconModifier.SimpleFormatDescription(ModDescriptionsContainer.Instance
                .immortality
                .Value);

        public override string SubInfo => IModIconModifier.EmptyInfo;
        protected override bool HiddenEndedWork => chance <= 0;

        public override void Init(Unit unit)
        {
            Debug.unityLogger.Log("a");
            unit.hp.OnValueChanged += _ => CheckAndApply();

            base.Init(unit);
        }

        private void CheckAndApply()
        {
            if (BelongingUnit.hp <= 0 && Tools.Random.RandomChance(chance))
                BelongingUnit.hp.Heal(1);
        }

        protected override bool HiddenCanConcat(Modifier other) => other is Immortality;

        protected override void HiddenConcat(Modifier other)
        {
            chance += ((Immortality)other).chance;
            chance = Math.Max(chance, 100);
        }
    }
}