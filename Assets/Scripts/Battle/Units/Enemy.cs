using System;
using System.Collections;
using Audio;
using UnityEngine;

namespace Battle.Units
{
    [Serializable]
    public class Enemy : Unit
    {
        [SerializeField] private AI.Ai ai;

        public delegate void OnGettingHitDelegate(Enemy enemy);

        public static event OnGettingHitDelegate OnGettingHit;
        
        public override void TakeDamage(int dmg)
        {
            base.TakeDamage(dmg);
            OnGettingHit?.Invoke(this);
            AudioManager.instance.Play(AudioEnum.EnemyHit);
        }

        public IEnumerator Turn()
        {
            while (HasMoves)
            {
                yield return StartCoroutine(ai.Act());
                WasteMove();
                if (HasMoves) continue;
                RefillMoves();
                yield break;
            }
        }

        protected override void NoHp()
        {
            manager.OnEnemyDeath();
            Destroy(gameObject);
        }
    }
}