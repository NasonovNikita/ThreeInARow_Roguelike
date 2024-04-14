using System;
using System.Collections.Generic;
using Battle.Modifiers;
using Battle.Modifiers.Statuses;
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
        [SerializeField] public ModifierList<Status> statuses;
        [SerializeField] public List<Cell> cells;
        [SerializeField] public int manaPerGem;
        [SerializeField] public int money;
        
        public static PlayerData NewData(Player player, PlayerData oldData = null) =>
            NewData(player.hp, player.mana, player.damage, player.manaPerGem, player.Statuses, oldData);

        public static PlayerData NewData(Hp hp, Mana mana, Damage damage, int manaPerGem, ModifierList<Status> statuses, PlayerData oldData = null)
        {
            var data = CreateInstance<PlayerData>();
            
            data.hp = hp.Save();
            data.mana = mana.Save();
            data.damage = damage.Save();
            
            data.manaPerGem = manaPerGem;
            
            data.statuses = statuses.Save();
            
            if (oldData is null) return data;
            
            data.spells = oldData.spells;
            data.items = oldData.items;
            data.money = oldData.money;

            return data;
        }

        public void AddStatus(Status status) => statuses.Add(status);
    }
}