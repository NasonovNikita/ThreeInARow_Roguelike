using System;
using Battle.Modifiers.StatModifiers;
using Battle.Units;
using UnityEngine;

namespace Battle.Modifiers.Statuses
{
    [Serializable]
    public class Sharp : Status, ISaveAble, IConcatAble
    {
        [SerializeField] private int addition;

        public Sharp(int addition, bool save = false)
        {
            this.addition = addition;
            Save = save;
        }
        
        public override Sprite Sprite => throw new NotImplementedException();

        public override string Tag => throw new NotImplementedException();

        public override string Description => throw new NotImplementedException();

        public override string SubInfo => addition.ToString();

        public override bool ToDelete => addition == 0;

        public override void Init(Unit unit)
        {
            foreach (var enemy in unit.Enemies)
                enemy.hp.onHpChanged += () => enemy.hp.AddDamageMod(new DamageMod(addition));
            
            base.Init(unit);
        }

        public bool ConcatAbleWith(IConcatAble other) =>
            other is Sharp sharp &&
            Save == sharp.Save;

        public void Concat(IConcatAble other) =>
            addition += ((Sharp)other).addition;

        public bool Save { get; }
    }
}