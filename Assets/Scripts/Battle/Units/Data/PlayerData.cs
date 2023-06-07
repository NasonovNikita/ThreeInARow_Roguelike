using System;
using System.Collections.Generic;
using UnityEngine;

namespace Battle.Units.Data
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "UnitData/PlayerData")]
    [Serializable]
    public class PlayerData : UnitData
    {
        [SerializeField]
        public int manaPerGem;
        [SerializeField]
        public int money;


        public PlayerData(int manaPerGem, Stat hp, Stat mana, Stat damage, List<Modifier> statusModifiers, List<Item> items,
            List<Spell> spells, int money) : base(hp, mana, damage, statusModifiers, items, spells)
        {
            this.manaPerGem = manaPerGem;
            this.money = money;
        }

    
        public PlayerData() : base()
        {
            manaPerGem = 20;
            money = 0;
        }
    }
}