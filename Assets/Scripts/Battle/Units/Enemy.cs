using System;
using Audio;
using Battle.Units.AI;
using UnityEngine;

namespace Battle.Units
{
    [Serializable]
    [RequireComponent(typeof(EnemyAI))]
    public class Enemy : Unit
    {
        private EnemyAI ai;

        public override Unit Target => manager.player;
        
        public new void TurnOn()
        {
            base.TurnOn();
        
            ai = GetComponent<EnemyAI>();
        }
        public override void DoDamage(Damage dmg)
        {
            base.DoDamage(dmg);
            AudioManager.instance.Play(AudioEnum.EnemyHit);
        }

        public void Act()
        {
            if (Stunned() || manager.State == BattleState.End) return;
            ai.Act();
        }

        protected override void NoHp()
        {
            if (this != null) StartCoroutine(manager.KillEnemy(this));
        }
    }
}