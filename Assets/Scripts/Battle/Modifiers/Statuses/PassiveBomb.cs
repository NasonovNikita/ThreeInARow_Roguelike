using System;
using Battle.Units;
using UI.Battle;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Battle.Modifiers.Statuses
{
    [Serializable]
    public class PassiveBomb : Status, IConcatAble, ISaveAble
    {
        [SerializeField] private int dmg;
        [SerializeField] private int manaBorder;

        public PassiveBomb(int damage, int manaBorder, bool save)
        {
            dmg = damage;
            this.manaBorder = manaBorder;
            Save = save;
        }
        
        public override Sprite Sprite => throw new NotImplementedException();

        public override string Tag => throw new NotImplementedException();

        public override string Description => throw new NotImplementedException();

        public override string SubInfo => IModIconAble.EmptyInfo;

        public override bool ToDelete => dmg == 0;

        public override void Init(Unit unit)
        {
            Manager.onTurnEnd += CheckAndApply;
            base.Init(unit);
        }

        private void CheckAndApply()
        {
            if (belongingUnit.mana > manaBorder) return;
            
            foreach (var enemy in Manager.Enemies)
            {
                enemy.TakeDamage(dmg);
            }
        }

        public bool ConcatAbleWith(IConcatAble other) =>
            other is PassiveBomb passiveBomb &&
            manaBorder == passiveBomb.manaBorder &&
            Save == passiveBomb.Save;

        public void Concat(IConcatAble other) => dmg += ((PassiveBomb)other).dmg;

        public bool Save { get; }
    }
}