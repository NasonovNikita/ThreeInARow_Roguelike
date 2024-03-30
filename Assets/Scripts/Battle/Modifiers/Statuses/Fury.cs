using System;
using Battle.Modifiers.StatModifiers;
using Battle.Units;
using UnityEngine;

namespace Battle.Modifiers.Statuses
{
    [Serializable]
    public class Fury : Status, ISaveAble
    {
        [SerializeField] private int addition;
        [SerializeField] private int hpBorder;
        [SerializeField] private DamageMod mod;

        public Fury(int addition, int hpBorder, bool save = false)
        {
            this.addition = addition;
            this.hpBorder = hpBorder;
            Save = save;
        }

        private void CheckHpAndApplyMod()
        {
            switch (Condition)
            {
                case false when mod is not null:
                    belongingUnit.damage.AddMod(new DamageMod(-addition));
                    mod = null;
                    break;
                case true when mod is null:
                    mod = new DamageMod(addition);
                    belongingUnit.damage.AddMod(mod);
                    break;
            }
        }

        private bool Condition => belongingUnit.hp <= hpBorder;

        public override void Init(Unit unit)
        {
            base.Init(unit);
            unit.hp.onHpChanged += CheckHpAndApplyMod;
        }

        public override Sprite Sprite => throw new NotImplementedException();

        public override string Tag => throw new NotImplementedException();

        public override string Description => throw new NotImplementedException();

        public override string SubInfo => throw new NotImplementedException();

        public override bool ToDelete => false;

        public bool Save { get; }
    }
}