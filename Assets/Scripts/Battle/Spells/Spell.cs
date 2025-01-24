using System;
using System.Collections;
using Battle.Units;
using Other;
using UnityEngine;
using UnityEngine.Serialization;

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
        [SerializeField] private int useCost;
        
        public virtual int UseCost => useCost;

        public virtual bool CantCast => UnitBelong.mana < UseCost;

        protected Unit UnitBelong;

        public event Action OnChanged;

        public virtual void Init(Unit unit)
        {
            UnitBelong = unit;
        }

        /// <summary>   The same as <see cref="Cast"/> but also checks if it's Player's turn.   </summary>
        public IEnumerator PlayerCast()
        {
            if (BattleFlowManager.Instance.AllowedToUseSpells)
                yield return UnitBelong.StartCoroutine(Cast());
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

        public override void Get()
        {
            Player.Data.spells.Add(this);
        }

        protected abstract void Action();

        protected virtual IEnumerator Wait()
        {
            yield return new WaitForSeconds(CastTime);
        }

        protected virtual void Waste()
        {
            UnitBelong.mana.Waste(UseCost);
        }

        protected void InvokeOnChanged()
        {
            OnChanged?.Invoke();
        }
    }
}