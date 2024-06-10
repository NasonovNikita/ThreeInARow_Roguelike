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
        private const float CastTime = 0.5f; // TEMP
        [SerializeField] public int useCost;

        protected BattleFlowManager battleFlowManager;

        protected Unit unitBelong;

        public virtual bool CantCast => unitBelong.mana < useCost;

        public virtual void Init(Unit unit)
        {
            unitBelong = unit;
            battleFlowManager = FindFirstObjectByType<BattleFlowManager>();
        }

        public void PlayerCast()
        {
            if (battleFlowManager.CurrentlyTurningUnit is Player)
                unitBelong.StartCoroutine(Cast());
        }

        public IEnumerator Cast()
        {
            if (CantCast) yield break;

            Action();

            Waste();
            unitBelong.UseSpell();

            yield return Wait();
        }

        protected abstract void Action();

        protected virtual IEnumerator Wait()
        {
            yield return new WaitForSeconds(CastTime);
        }

        protected virtual void Waste()
        {
            unitBelong.mana.Waste(useCost);
        }

        public override void Get()
        {
            Player.data.spells.Add(this);
        }
    }
}