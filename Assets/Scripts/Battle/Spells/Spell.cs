using System;
using System.Collections;
using Battle.Units;
using Other;
using UnityEngine;

namespace Battle.Spells
{
    /// <summary>
    ///     Base class for all spells.<br/>
    ///     Initialize with <see cref="Unit"/> and then use <see cref="Cast"/>.
    /// </summary>
    /// <remarks>For <see cref="Player"/> use <see cref="PlayerCast"/>!</remarks>
    [Serializable]
    public abstract class Spell : LootItem
    {
        private const float CastTime = 0.5f; // TEMP
        [SerializeField] public int useCost;

        protected BattleFlowManager BattleFlowManager;

        protected Unit UnitBelong;

        public virtual bool CantCast => UnitBelong.mana < useCost;

        public virtual void Init(Unit unit)
        {
            UnitBelong = unit;
            BattleFlowManager = FindFirstObjectByType<BattleFlowManager>();
        }

        /// <summary>   The same as <see cref="Cast"/> but also checks if it's Player's turn.   </summary>
        public void PlayerCast()
        {
            if (BattleFlowManager.CurrentlyTurningUnit is Player)
                UnitBelong.StartCoroutine(Cast());
        }

        /// <summary>
        ///     Checks if Unit can cast the spell, uses <b>Action</b>,
        ///     <b>Wastes</b> mana (or whatever), Invokes events and plays an animation (if exists).
        /// </summary>
        /// <returns>
        ///     IEnumerator with time delays.
        ///     Is Supposed to be used with <c>StartCoroutine(Cast())</c>
        /// </returns>
        public IEnumerator Cast()
        {
            if (CantCast) yield break;

            Action();

            Waste();
            UnitBelong.UseSpell();

            yield return Wait();
        }

        protected abstract void Action();

        protected virtual IEnumerator Wait()
        {
            yield return new WaitForSeconds(CastTime);
        }

        protected virtual void Waste()
        {
            UnitBelong.mana.Waste(useCost);
        }

        public override void Get()
        {
            Player.Data.spells.Add(this);
        }
    }
}