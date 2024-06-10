using System;
using System.Collections.Generic;
using Battle.Modifiers;
using Battle.Spells;
using Battle.Units.Stats;
using Battle.Units.Statuses;
using Core.Singleton;
using UnityEngine;
using Tools = Other.Tools;

namespace Battle.Units
{
    public abstract class Unit : MonoBehaviour
    {
        public Hp hp;
        public Mana mana;
        public Damage damage;

        public List<Spell> spells;

        public Unit target;

        protected ModifierList statuses = new();
        public abstract List<Unit> Enemies { get; }
        public ModifierList Statuses => statuses;

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

            foreach (Spell spell in spells) spell.Init(this);

            statuses.OnModAdded += modifier => ((Status)modifier).Init(this);
        }

        public event Action OnSpellCasted;
        public event Action OnMadeHit;
        public event Action OnDied;

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
        } // ReSharper disable Unity.PerformanceAnalysis
        public void Die()
        {
            OffScreenPoint.Instance.Hide(gameObject);
            OnDied?.Invoke();
        }
    }
}