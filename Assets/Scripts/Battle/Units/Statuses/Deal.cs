using System;
using Battle.Modifiers;
using Battle.UI.ModsDisplaying;
using Battle.Units.StatModifiers;
using Core.Singleton;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Battle.Units.Statuses
{
    [Serializable]
    public class Deal : Status
    {
        [SerializeField] private int value;
        [SerializeField] private bool usedSpells;

        public Deal(int value, bool save = false) : base(save) => this.value = value;

        public override Sprite Sprite => ModifierSpritesContainer.Instance.deal;

        public override string Description =>
            IModIconModifier.SimpleFormatDescription(
                ModDescriptionsContainer.Instance.deal.Value,
                value);

        public override string SubInfo => IModIconModifier.EmptyInfo;
        public override bool ToDelete => usedSpells;

        public void CheckAndAddMod()
        {
            if (!usedSpells)
                BelongingUnit.damage.mods.Add(new DamageConstMod(value, true));
        }

        public override void Init(Unit unit)
        {
            usedSpells = false;
            BelongingUnit.OnSpellCasted += () => usedSpells = true;
            Object.FindFirstObjectByType<BattleFlowManager>().OnBattleEnd +=
                CheckAndAddMod;
            base.Init(unit);
        }

        protected override bool HiddenCanConcat(Modifier other) => other is Deal;

        protected override void HiddenConcat(Modifier other)
        {
            value += ((Deal)other).value;
        }
    }
}