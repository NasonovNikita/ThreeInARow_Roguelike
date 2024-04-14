using System;
using System.Linq;
using Battle.Modifiers.StatModifiers;
using Battle.Units;
using UnityEngine;

namespace Battle.Modifiers.Statuses
{
    [Serializable]
    public class Irritation : Status
    {
        [SerializeField] private int damageAddition;
        [SerializeField] private MoveCounter moveCounter;
        private bool enemyDied;

        public Irritation(int damageAddition, int moves, bool save = false) : base(save)
        {
            this.damageAddition = damageAddition;
            moveCounter = CreateChangeableSubSystem(new MoveCounter(moves));
        }
        
        public override Sprite Sprite => throw new NotImplementedException();
        public override string Description => throw new NotImplementedException();
        public override string SubInfo => moveCounter.SubInfo;
        public override bool ToDelete => moveCounter.EndedWork || damageAddition == 0;

        public override void Init(Unit unit)
        {
            foreach (var enemy in unit.Enemies.Where(enemy => enemy != null))
            {
                enemy.hp.OnValueChanged += _ => CheckEnemy(enemy);
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
            if (!enemyDied) belongingUnit.damage.mods.Add(new DamageConstMod(damageAddition));
            enemyDied = false;
        }

        protected override bool CanConcat(Modifier other) => 
            other is Irritation;

        public override void Concat(Modifier other) => 
            damageAddition += ((Irritation)other).damageAddition;
    }
}