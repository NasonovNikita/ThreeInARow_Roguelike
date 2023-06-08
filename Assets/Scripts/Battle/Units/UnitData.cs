using System;
using System.Collections.Generic;
using UnityEngine;

namespace Battle.Units
{
    [Serializable]
    public class UnitData : ScriptableObject
    {
        [SerializeField] public Stat hp;
        [SerializeField] public Stat mana;
        [SerializeField] public Stat damage;
        [SerializeField] public List<Modifier> statusModifiers;
        [SerializeField] public List<Item> items;
        [SerializeField] public List<Spell> spells;


        public UnitData(Stat hp, Stat mana, Stat damage, List<Modifier> statusModifiers, List<Item> items,
            List<Spell> spells)
        {
            this.hp = hp;
            this.mana = mana;
            this.damage = damage;
            this.statusModifiers = statusModifiers;
            this.items = items;
            this.spells = spells;
        }

        public UnitData()
        {
            hp = new Stat(200);
            mana = new Stat(100);
            damage = new Stat(20);
            statusModifiers = new List<Modifier>();
            items = new List<Item>();
            spells = new List<Spell>();
        }

        public void Init(Unit unit)
        {
            unit.hp = hp;
            unit.mana = mana;
            unit.damage = damage;
            unit.statusModifiers = statusModifiers;
            unit.items = items;
            unit.spells = spells;
        }
    }
}