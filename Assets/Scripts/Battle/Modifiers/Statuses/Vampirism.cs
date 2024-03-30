using System;
using Battle.Units;
using UnityEngine;

namespace Battle.Modifiers.Statuses
{
    [Serializable]
    public class Vampirism : Status, ISaveAble, IConcatAble
    {
        [SerializeField] private int heal;

        public Vampirism(int healAmount, bool save = false)
        {
            heal = healAmount;
            Save = save;
        }
        
        public override Sprite Sprite => throw new NotImplementedException();
        public override string Tag => throw new NotImplementedException();
        public override string Description => throw new NotImplementedException();

        public override string SubInfo => heal.ToString();
        public override bool ToDelete => heal == 0;

        public override void Init(Unit unit)
        {
            unit.OnMadeHit += Heal;
            
            base.Init(unit);
        }

        private void Heal() => belongingUnit.hp.Heal(heal);

        public bool Save { get; }
        public bool ConcatAbleWith(IConcatAble other) =>
            other is Vampirism vampirism &&
            Save == vampirism.Save;

        public void Concat(IConcatAble other) =>
            heal += ((Vampirism)other).heal;
    }
}