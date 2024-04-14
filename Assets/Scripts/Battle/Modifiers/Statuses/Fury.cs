using System;
using Battle.Modifiers.StatModifiers;
using Battle.Units;
using UnityEngine;
using UnityEngine.Serialization;

namespace Battle.Modifiers.Statuses
{
    [Serializable]
    public class Fury : Status
    {
        [SerializeField] private int addition;
        [SerializeField] private int hpBorder;
        [FormerlySerializedAs("mod")] [SerializeField] private DamageConstMod constMod;

        public Fury(int addition, int hpBorder, bool save = false) : base(save)
        {
            this.addition = addition;
            this.hpBorder = hpBorder;
        }

        private void CheckHpAndApplyMod()
        {
            switch (Condition)
            {
                case false when constMod is not null:
                    belongingUnit.damage.mods.Add(new DamageConstMod(-addition));
                    constMod = null;
                    break;
                case true when constMod is null:
                    constMod = new DamageConstMod(addition);
                    belongingUnit.damage.mods.Add(constMod);
                    break;
            }
        }

        private bool Condition => belongingUnit.hp <= hpBorder;

        public override void Init(Unit unit)
        {
            base.Init(unit);
            unit.hp.OnValueChanged += _ => CheckHpAndApplyMod();
        }

        public override Sprite Sprite => throw new NotImplementedException();
        public override string Description => throw new NotImplementedException();
        public override string SubInfo => throw new NotImplementedException();
        public override bool ToDelete => addition == 0;
        
        protected override bool CanConcat(Modifier other) => 
            other is Fury fury &&
            fury.hpBorder == hpBorder;

        public override void Concat(Modifier other)
        {
            var otherAddition = ((Fury)other).addition;
            if (Condition && constMod is not null)
            {
                constMod.Concat(new DamageConstMod(otherAddition));
            }
            addition += otherAddition;
        }
    }
}