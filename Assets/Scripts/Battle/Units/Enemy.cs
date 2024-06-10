using System;
using System.Collections;
using System.Collections.Generic;
using Audio;
using Battle.Units.AI;
using Battle.Units.Statuses;
using UnityEngine;

namespace Battle.Units
{
    [Serializable]
    public class Enemy : Unit
    {
        [SerializeField] private Ai ai;

        public override List<Unit> Enemies => new() { target };

        public override void Awake()
        {
            hp.OnValueChanged += _ => AudioManager.Instance.Play(AudioEnum.EnemyHit);

            base.Awake();
        }

        public IEnumerator Turn()
        {
            if (!statuses.ModList.Exists(mod => mod is Stun { ToDelete: false }))
                yield return StartCoroutine(ai.Act());
        }
    }
}