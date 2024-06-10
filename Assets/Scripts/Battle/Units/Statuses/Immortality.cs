using System;
using Battle.Modifiers;
using Core.Singleton;
using Other;
using UI.Battle.ModsDisplaying;
using UnityEngine;

namespace Battle.Units.Statuses
{
    [Serializable]
    public class Immortality : Status
    {
        [SerializeField] private int chance;

        public Immortality(int chance, bool save = false) : base(save)
        {
            this.chance = chance;
        }

        public override Sprite Sprite => ModifierSpritesContainer.Instance.immortality;

        public override string Description =>
            IModIconModifier.SimpleFormatDescription(ModDescriptionsContainer.Instance.immortality.Value);

        public override string SubInfo => IModIconModifier.EmptyInfo;
        public override bool ToDelete => chance <= 0;

        public override void Init(Unit unit)
        {
            Debug.unityLogger.Log("a");
            unit.hp.OnValueChanged += _ => CheckAndApply();

            base.Init(unit);
        }

        private void CheckAndApply()
        {
            if (belongingUnit.hp <= 0 && Tools.Random.RandomChance(chance))
                belongingUnit.hp.Heal(1);
        }

        protected override bool CanConcat(Modifier other)
        {
            return other is Immortality;
        }

        public override void Concat(Modifier other)
        {
            chance += ((Immortality)other).chance;
            chance = Math.Max(chance, 100);
        }
    }
}