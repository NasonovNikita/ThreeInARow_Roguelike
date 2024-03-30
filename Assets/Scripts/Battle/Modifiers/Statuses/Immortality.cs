using System;
using Battle.Units;
using UI.Battle;
using UnityEngine;

namespace Battle.Modifiers.Statuses
{
    [Serializable]
    public class Immortality : Status, ISaveAble
    {
        [SerializeField] private int chance;
        [SerializeField] private int leftHp;

        public Immortality(int chance, int leftHp, bool save)
        {
            this.chance = chance;
            this.leftHp = leftHp;
            Save = save;
        }
        
        public override Sprite Sprite => throw new System.NotImplementedException();
        public override string Tag => throw new System.NotImplementedException();
        public override string Description => throw new System.NotImplementedException();
        public override string SubInfo => IModIconAble.EmptyInfo;
        public override bool ToDelete => false;

        public override void Init(Unit unit)
        {
            unit.hp.onHpChanged += CheckAndApply;
            
            base.Init(unit);
        }

        private void CheckAndApply()
        {
            if (belongingUnit.hp <= 0 && Other.Tools.Random.RandomChance(chance))
            {
                belongingUnit.hp.Heal(leftHp);
            }
        }

        public bool Save { get; }
    }
}