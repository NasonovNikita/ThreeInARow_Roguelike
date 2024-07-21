using System;
using System.Collections.Generic;
using Battle.Modifiers;
using Battle.UI.ModsDisplaying;
using Battle.Units.StatModifiers;
using Core.Singleton;
using UnityEngine;

namespace Battle.Units.Statuses
{
    [Serializable]
    public class Fury : Status
    {
        [SerializeField] private int addition;
        [SerializeField] private int hpBorder;
        [SerializeField] private DamageConstMod constMod;

        public Fury(int addition, int hpBorder, bool save = false) : base(save)
        {
            this.addition = addition;
            this.hpBorder = hpBorder;
        }

        private bool Condition => belongingUnit.hp <= hpBorder;

        public override Sprite Sprite => ModifierSpritesContainer.Instance.fury;

        public override string Description =>
            IModIconModifier.FormatDescriptionByKeys(ModDescriptionsContainer.Instance.fury.Value,
                new Dictionary<string, object>
                {
                    { "hpBorder", hpBorder },
                    { "addition", addition }
                });

        public override string SubInfo => IModIconModifier.EmptyInfo;
        public override bool ToDelete => addition == 0;

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

        public override void Init(Unit unit)
        {
            base.Init(unit);
            unit.hp.OnValueChanged += _ => CheckHpAndApplyMod();
        }

        protected override bool CanConcat(Modifier other)
        {
            return other is Fury fury &&
                   fury.hpBorder == hpBorder;
        }

        public override void Concat(Modifier other)
        {
            var otherAddition = ((Fury)other).addition;
            if (Condition && constMod is not null)
                constMod.Concat(new DamageConstMod(otherAddition));
            addition += otherAddition;
        }
    }
}