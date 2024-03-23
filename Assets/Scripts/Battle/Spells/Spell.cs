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

        protected Unit unitBelong;

        protected BattleManager manager;

        private const float CastTime = 0.5f; // TEMP

        public virtual void Init(Unit unit)
        {
            unitBelong = unit;
            manager = FindFirstObjectByType<BattleManager>();
        }

        public void PlayerCast()
        {
            if (manager.CurrentlyTurningUnit is Player)
                unitBelong.StartCoroutine(Cast());
        }

        public IEnumerator Cast()
        {
            if (CantCast) yield break;
            
            SpellUsageLog.Log(unitBelong, useCost);
            
            Action();

            Waste();
            yield return Wait();
        }

        protected virtual bool CantCast => unitBelong.mana < useCost;

        protected abstract void Action();

        protected virtual IEnumerator Wait()
        {
            yield return new WaitForSeconds(CastTime);
        }

        protected virtual void Waste() => unitBelong.mana.Waste(useCost);

        public override void Get() => Player.data.spells.Add(this);
    }
}