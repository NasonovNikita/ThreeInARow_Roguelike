using System;
using Battle.Modifiers;
using Battle.Units.StatModifiers;
using Core.Singleton;
using UI.Battle.ModsDisplaying;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Battle.Units.Statuses
{
    [Serializable]
    public class Deal : Status
    {
        [SerializeField] private int value;
        [SerializeField] private bool usedSpells;

        public Deal(int value, bool save = false) : base(save)
        {
            this.value = value;
        }

        public override Sprite Sprite => ModifierSpritesContainer.Instance.deal;

        public override string Description =>
            IModIconModifier.SimpleFormatDescription(ModDescriptionsContainer.Instance.deal.Value, value);

        public override string SubInfo => IModIconModifier.EmptyInfo;
        public override bool ToDelete => usedSpells;

        public void CheckAndAddMod()
        {
            if (!usedSpells) belongingUnit.damage.mods.Add(new DamageConstMod(value, true));
        }

        public override void Init(Unit unit)
        {
            usedSpells = false;
            belongingUnit.OnSpellCasted += () => usedSpells = true;
            Object.FindFirstObjectByType<BattleFlowManager>().OnBattleEnd += CheckAndAddMod;
            base.Init(unit);
        }

        protected override bool CanConcat(Modifier other)
        {
            return other is Deal;
        }

        public override void Concat(Modifier other)
        {
            value += ((Deal)other).value;
        }
    }
}