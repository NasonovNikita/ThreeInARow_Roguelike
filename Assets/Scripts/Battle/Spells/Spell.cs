using System;
using System.Collections;
using Battle.Units;
using Other;
using UnityEngine;

namespace Battle.Spells
{
    [Serializable]
    public abstract class Spell : LootItem
    {
        [SerializeField] public int useCost;

        [SerializeField] protected float value;

        [SerializeField] protected int count;

        protected Unit unitBelong;

        protected BattleManager manager;

        public const float CastTime = 0.5f; // TEMP

        public override string Title => titleKeyRef.Value;

        public override string Description => descriptionKeyRef.Value;

        public virtual void Init(Unit unit)
        {
            unitBelong = unit;
            manager = FindFirstObjectByType<BattleManager>();
        }

        public IEnumerator Cast()
        {
            if (CantCast) yield break;
            
            
            SpellUsageLog.Log(unitBelong, useCost);
            
            Action();

            Waste();
            yield return Wait();
        }
        
        protected bool NotAllowedTurn => unitBelong switch
        {
            Player => manager.State != BattleState.Turn,
            Enemy => manager.State != BattleState.EnemiesAct,
            _ => throw new ArgumentOutOfRangeException()
        };

        protected virtual bool CantCast => NotAllowedTurn || unitBelong.mana < useCost;

        protected abstract void Action();

        protected virtual IEnumerator Wait()
        {
            yield return unitBelong.StartCoroutine(BaseWait());
            
            IEnumerator BaseWait()
            {
                yield return new WaitForSeconds(CastTime);
            }
        }

        protected virtual void Waste()
        {
            unitBelong.WasteMana(useCost);
        }

        public override void Get()
        {
            Player.data.spells.Add(this);
            base.Get();
        }
    }
}