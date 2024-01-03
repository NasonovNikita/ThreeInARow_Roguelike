using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Battle.Items;
using Battle.Spells;
using Battle.Units.Stats;
using UnityEngine;

namespace Battle.Units
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "UnitData/PlayerData")]
    [Serializable]
    public class PlayerData : ScriptableObject
    {
        [SerializeField] public UnitHp unitHp;
        [SerializeField] public Mana mana;
        [SerializeField] public UnitDamage damage;
        [SerializeField] public List<Item> items;
        [SerializeField] public List<Spell> spells;
        [SerializeField] public int manaPerGem;
        [SerializeField] public DmgType chosenElement;
        [SerializeField] public int money;
        
        public static PlayerData NewData(Player player, PlayerData oldData = null)
        {
            PlayerData data = NewData(player.Hp, player.mana, player.unitDamage, player.manaPerGem,
                player.chosenElement, oldData);

            return data;
        }

        [SuppressMessage("ReSharper", "ParameterHidesMember")]
        public static PlayerData NewData(UnitHp unitHp, Mana mana, UnitDamage damage, int manaPerGem,
            DmgType chosenElement, PlayerData oldData = null)
        {
            PlayerData data = CreateInstance<PlayerData>();
            data.unitHp = unitHp;
            data.mana = mana;
            data.damage = damage;
            data.manaPerGem = manaPerGem;
            data.chosenElement = chosenElement;
            if (oldData is null) return data;
            data.spells = oldData.spells;
            data.items = oldData.items;
            data.money = oldData.money;

            return data;
        }
        
        public void Init(Unit unit)
        {
            unit.Hp = unitHp;
            unit.mana = mana;
            unit.manaPerGem = manaPerGem;
            unit.unitDamage = damage;
            unit.spells = new List<Spell>(spells);
            unit.items = new List<Item>(items);
            unit.chosenElement = chosenElement;
        }
    }
}