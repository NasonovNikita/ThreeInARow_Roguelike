using System;
using Battle.Modifiers.StatModifiers;
using Battle.Units;
using UnityEngine;

namespace Battle.Modifiers.Statuses
{
    [Serializable]
    public class Sharp : Status
    {
        [SerializeField] private int addition;

        public Sharp(int addition, bool save = false) : base(save)
        {
            this.addition = addition;
        }
        
        public override Sprite Sprite => throw new NotImplementedException();
        public override string Description => throw new NotImplementedException();
        public override string SubInfo => addition.ToString();
        public override bool ToDelete => addition == 0;

        public override void Init(Unit unit)
        {
            foreach (var enemy in unit.Enemies)
                enemy.hp.OnValueChanged += _ => enemy.hp.onTakingDamageMods.Add(new DamageConstMod(addition));
            
            base.Init(unit);
        }

        protected override bool CanConcat(Modifier other) => 
            other is Sharp;

        public override void Concat(Modifier other) => 
            addition += ((Sharp)other).addition;
    }
}