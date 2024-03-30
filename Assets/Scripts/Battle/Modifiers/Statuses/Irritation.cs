using System;
using System.Linq;
using Battle.Modifiers.StatModifiers;
using Battle.Units;
using UnityEngine;

namespace Battle.Modifiers.Statuses
{
    [Serializable]
    public class Irritation : Status, IConcatAble
    {
        [SerializeField] private int damageAddition;
        [SerializeField] private MoveCounter moveCounter;
        private bool enemyDied;

        public Irritation(int damageAddition, int moves)
        {
            this.damageAddition = damageAddition;
            moveCounter = new MoveCounter(moves);
        }
        
        public override Sprite Sprite => throw new NotImplementedException();
        public override string Tag => throw new NotImplementedException();
        public override string Description => throw new NotImplementedException();

        public override string SubInfo => moveCounter.SubInfo;

        public override bool ToDelete => moveCounter.EndedWork || damageAddition == 0;

        public override void Init(Unit unit)
        {
            foreach (var enemy in unit.Enemies.Where(enemy => enemy != null))
            {
                enemy.hp.onHpChanged += () => CheckEnemy(enemy);
                
            }

            Manager.onTurnEnd += CheckAndApply;
            
            base.Init(unit);
        }

        private void CheckEnemy(Unit enemy)
        {
            if (enemy.hp <= 0) enemyDied = true;
        }

        private void CheckAndApply()
        {
            if (!enemyDied) belongingUnit.damage.AddMod(new DamageMod(damageAddition));
            enemyDied = false;
        }

        public bool ConcatAbleWith(IConcatAble other) =>
            other is Irritation irritation &&
            moveCounter.moves == irritation.moveCounter.moves;

        public void Concat(IConcatAble other) =>
            damageAddition += ((Irritation)other).damageAddition;
    }
}