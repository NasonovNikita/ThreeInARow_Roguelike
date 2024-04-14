using System;
using Battle.Modifiers.StatModifiers;
using Battle.Units;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Battle.Modifiers.Statuses
{
    [Serializable]
    public class Deal : Status
    {
        [SerializeField] private int value;
        [SerializeField] private bool usedSpells;

        public Deal(int value, bool save) : base(save)
        {
            this.value = value;
        }
        
        public override Sprite Sprite => throw new NotImplementedException();
        public override string Description => throw new NotImplementedException();
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
            Object.FindFirstObjectByType<BattleManager>().onBattleEnd += CheckAndAddMod;
            base.Init(unit);
        }
        
        protected override bool CanConcat(Modifier other) => other is Deal;

        public override void Concat(Modifier other) => 
            value += ((Deal)other).value;
    }
}