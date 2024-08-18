using System;
using System.Collections;
using System.Collections.Generic;
using Audio;
using Battle.Units.AI;
using Battle.Units.Statuses;
using UnityEngine;

namespace Battle.Units
{
    /// <summary>
    ///     Has Player as a <see cref="Unit.target"/>.
    ///     Uses <see cref="Units.AI.Ai"/> to make moves.
    /// </summary>
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

        /// <summary>
        ///     Starts Enemy's turn.
        /// </summary>
        public IEnumerator Turn()
        {
            if (!Statuses.List.Exists(mod => mod is Stun { ToDelete: false }))
                yield return StartCoroutine(ai.Act());
        }
    }
}