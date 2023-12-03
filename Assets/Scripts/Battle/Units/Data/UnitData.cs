using System;
using System.Collections.Generic;
using Battle.Items;
using Battle.Modifiers;
using Battle.Spells;
using UnityEngine;

namespace Battle.Units.Data
{
    [Serializable]
    public class UnitData : ScriptableObject
    {
        [SerializeField] public Stat hp;
        [SerializeField] public Stat mana;
        [SerializeField] public Stat fDmg;
        [SerializeField] public Stat cDmg;
        [SerializeField] public Stat pDmg;
        [SerializeField] public Stat lDmg;
        [SerializeField] public Stat phDmg;
        [SerializeField] public List<Modifier> stateModifiers;
        [SerializeField] public List<Item> items;
        [SerializeField] public List<Spell> spells;
        [SerializeField] public int manaPerGem;

        public UnitData(Stat hp, Stat mana, Stat fDmg, Stat cDmg, Stat pDmg, Stat lDmg, Stat phDmg, List<Modifier> stateModifiers, List<Item> items)
        {
            this.hp = hp;
            this.mana = mana;
            this.fDmg = fDmg;
            this.cDmg = cDmg;
            this.pDmg = pDmg;
            this.lDmg = lDmg;
            this.phDmg = phDmg;
            this.stateModifiers = stateModifiers;
            this.items = items;
        }

        public UnitData()
        {
            hp = new Stat(200);
            mana = new Stat(100);
            phDmg = new Stat(20);
            stateModifiers = new List<Modifier>();
            items = new List<Item>();
            spells = new List<Spell>();
        }

        public void Init(Unit unit)
        {
            unit.hp = hp;
            unit.mana = mana;
            unit.fDmg = fDmg;
            unit.cDmg = cDmg;
            unit.pDmg = pDmg;
            unit.lDmg = lDmg;
            unit.phDmg = phDmg;
            unit.stateModifiers = stateModifiers;
            unit.items = items;
            unit.spells = spells;
        }
    }
}