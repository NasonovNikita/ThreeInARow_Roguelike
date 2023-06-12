using System;
using System.Collections.Generic;
using Battle.Units;
using UnityEngine;
using UnityEngine.Assertions.Must;

namespace Battle
{
    public abstract class Unit : MonoBehaviour
    {
        public Stat hp;
        public Stat mana;
        public Stat damage;

        public List<Modifier> stateModifiers = new();

        public List<Item> items;

        public List<Spell> spells;

        protected BattleManager manager;

        public UnitType type;

        protected void TurnOn()
        {
            manager = FindFirstObjectByType<BattleManager>();
            hp.Init();
            mana.Init();
            damage.Init();

            foreach (Item item in items)
            {
                item.Init(this);
            }

            foreach (Spell spell in spells)
            {
                spell.Init(this);
            }
        }
    

        public virtual void DoDamage(int value)
        {
            hp -= value;

            if (hp == 0)
            {
                NoHp();
            }
        }
        public void Delete()
        {
            Destroy(gameObject);
        }

        public bool Stunned()
        {
            return stateModifiers.Exists(state => state.Type == ModType.Stun && state.Value != 0);
        }

        public Stat StatByType(StatType statTypeType)
        {
            
            return statTypeType switch
            {
                StatType.Hp => hp,
                StatType.Mana => mana,
                StatType.Damage => damage,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public abstract void Act();

        protected abstract void NoHp();
    }
}