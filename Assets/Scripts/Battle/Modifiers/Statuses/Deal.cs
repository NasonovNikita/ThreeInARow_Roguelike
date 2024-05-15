using System;
using Battle.Modifiers.StatModifiers;
using Battle.Units;
using Core.Singleton;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Battle.Modifiers.Statuses
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
        public override string Description => SimpleFormatDescription(ModDescriptionsContainer.Instance.deal.Value, value);
        public override string SubInfo => EmptyInfo;
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
        
        protected override bool CanConcat(Modifier other) => other is Deal;

        public override void Concat(Modifier other) => 
            value += ((Deal)other).value;
    }
}