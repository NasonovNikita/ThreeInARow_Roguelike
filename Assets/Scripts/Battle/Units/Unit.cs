using System;
using System.Collections.Generic;
using Battle.Modifiers;
using Battle.Spells;
using Battle.Units.Stats;
using Core.Singleton;
using UnityEngine;
using Tools = Other.Tools;

namespace Battle.Units
{
    /// <summary>
    ///     Contains <see cref="Stat">stats</see> (hp, mana, damage), <see cref="spells"/>,
    ///     <see cref="Statuses"/>, <see cref="target"/> to deal damage to
    /// </summary>
    public abstract class Unit : MonoBehaviour
    {
        public Hp hp;
        public Mana mana;
        public Damage damage;

        public List<Spell> spells;

        public Unit target;

        [NonSerialized] public ModifierList Statuses = new();

        /// <summary>
        ///     Can be used to deal damage to all enemies of this unit at once.
        /// </summary>
        public abstract List<Unit> Enemies { get; }

        /// <summary>
        ///     Easy way to get all ModifierLists of this unit.
        /// </summary>
        public List<ModifierList> AllModifierLists => new()
        {
            hp.onHealingMods,
            hp.onTakingDamageMods,
            mana.refillingMods,
            mana.wastingMods,
            damage.mods,
            Statuses
        };

        public bool Dead { get; private set; }

        public virtual void Awake()
        {
            Tools.InstantiateAll(spells);

            foreach (var spell in spells) spell.Init(this);

            foreach (var modList in AllModifierLists)
            {
                // Every mod is initialized after adding
                modList.OnModAdded += modifier => ((UnitModifier)modifier).Init(this);
            }
        }

        public event Action OnSpellCasted;
        public event Action OnMadeHit;
        public event Action OnDied;

        /// <summary>
        ///     Invokes <see cref="OnSpellCasted"/>.
        /// </summary>
        public void UseSpell()
        {
            OnSpellCasted?.Invoke();
        }

        public void InvokeOnMadeHit()
        {
            OnMadeHit?.Invoke();
        }

        public void TakeDamage(int val)
        {
            hp.TakeDamage(val);

            CheckHp();
        }

        private void CheckHp()
        {
            if (Dead || hp > 0) return;

            Dead = true;
            Die();
        }
        
        // ReSharper disable Unity.PerformanceAnalysis
        public void Die()
        {
            OffScreenPoint.Instance.Hide(gameObject);
            OnDied?.Invoke();
        }
    }
}