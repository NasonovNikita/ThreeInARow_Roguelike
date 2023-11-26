using System;
using Audio;
using Battle.Modifiers;
using Battle.Units.AI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Battle.Units.Enemies
{
    [Serializable]
    [RequireComponent(typeof(EnemyAI))]
    public class Enemy : Unit
    {
        private EnemyAI _ai;
        public new void TurnOn()
        {
            base.TurnOn();
        
            _ai = GetComponent<EnemyAI>();
        }
        public override void DoDamage(Damage dmg)
        {
            base.DoDamage(dmg);
            AudioManager.instance.Play(AudioEnum.EnemyHit);
        }

        public override void Act()
        {
            if (stateModifiers.Exists(val => val.type == ModType.Burning && val.Use() != 0)
                && 0 <= Random.Range(0, 101) && Random.Range(0, 101) <= 10)
            {
                int index = manager.enemies.IndexOf(this);
                if (index == 0)
                {
                    manager.enemies[1].StartBurning(1);
                }
                else if (index == manager.enemies.Count - 1)
                {
                    manager.enemies[^1].StartBurning(1);
                }
                else
                {
                    manager.enemies[index - 1].StartBurning(1);
                    manager.enemies[index + 1].StartBurning(1);
                }
            }
            if (Stunned() || manager.State == BattleState.End) return;
            _ai.Act();
        }

        protected override void NoHp()
        {
            DeathLog.Log(this);
            StartCoroutine(manager.KillEnemy(this));
        }
    }
}