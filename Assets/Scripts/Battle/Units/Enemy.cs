using System;
using System.Collections;
using Audio;
using Battle.Units.AI;
using UnityEngine;

namespace Battle.Units
{
    [Serializable]
    public class Enemy : Unit
    {
        [SerializeField]
        private BaseEnemyAI ai;

        public override Unit Target => manager.player;
        
        public new void TurnOn()
        {
            base.TurnOn();
        }
        public override void TakeDamage(Damage dmg)
        {
            base.TakeDamage(dmg);
            AudioManager.instance.Play(AudioEnum.EnemyHit);
        }

        public override IEnumerator Act()
        {
            if (Stunned() || manager.State == BattleState.End) yield break;
            yield return StartCoroutine(ai.Act());
            yield return StartCoroutine(base.Act());
        }

        protected override void NoHp()
        {
            if (this == null) return;
            StartCoroutine(manager.KillEnemy(this));
        }
    }
}