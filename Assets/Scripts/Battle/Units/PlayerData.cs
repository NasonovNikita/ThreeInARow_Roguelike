using System;
using System.Collections.Generic;
using Battle.Units.Modifiers;
using Item =  Battle.Items.Item;
using Cell = Battle.Match3.Cell;
using Spell = Battle.Spells.Spell;
using Battle.Units.Stats;
using UnityEngine;
using UnityEngine.Serialization;

namespace Battle.Units
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "UnitData/PlayerData")]
    [Serializable]
    public class PlayerData : ScriptableObject
    {
        [FormerlySerializedAs("unitHp")] [SerializeField] public Hp hp;
        [SerializeField] public Mana mana;
        [SerializeField] public Damage damage;
        [SerializeField] public List<Item> items;
        [SerializeField] public List<Spell> spells;
        [SerializeField] public List<Modifier> statuses; //TODO check if not serialized in actual json
        [SerializeField] public List<Cell> cells;
        [SerializeField] public int manaPerGem;
        [SerializeField] public int money;
        
        public static PlayerData NewData(Player player, PlayerData oldData = null) => NewData(player.hp, player.mana, player.damage, player.manaPerGem, oldData);

        public static PlayerData NewData(Hp hp, Mana mana, Damage damage, int manaPerGem, PlayerData oldData = null)
        {
            PlayerData data = CreateInstance<PlayerData>();
            data.hp = hp.Save();
            data.mana = mana.Save();
            data.damage = damage.Save();
            data.manaPerGem = manaPerGem;
            if (oldData is null) return data;
            data.statuses = oldData.statuses;
            data.spells = oldData.spells;
            data.items = oldData.items;
            data.money = oldData.money;

            return data;
        }
    }
}