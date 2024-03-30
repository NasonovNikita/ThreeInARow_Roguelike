using System;
using Battle.Modifiers.StatModifiers;
using Battle.Units;
using UI.Battle;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Battle.Modifiers.Statuses
{
    [Serializable]
    public class Deal : Status, ISaveAble
    {
        [SerializeField] private int value;
        [SerializeField] private bool usedSpells;

        public Deal(int value, bool save)
        {
            this.value = value;
            Save = save;
        }
        
        public override Sprite Sprite => throw new System.NotImplementedException();

        public override string Tag => throw new System.NotImplementedException();

        public override string Description => throw new System.NotImplementedException();

        public override string SubInfo => IModIconAble.EmptyInfo;

        public override bool ToDelete => usedSpells;

        public void CheckAndAddMod()
        {
            if (!usedSpells) belongingUnit.damage.AddMod(new DamageMod(value, true));
        }

        public override void Init(Unit unit)
        {
            usedSpells = false;
            belongingUnit.OnSpellCasted += () => usedSpells = true;
            Object.FindFirstObjectByType<BattleManager>().onBattleEnd += CheckAndAddMod;
            base.Init(unit);
        }

        public bool Save { get; }
    }
}