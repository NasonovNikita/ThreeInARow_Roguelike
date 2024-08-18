using System;
using System.Collections.Generic;
using Battle.Modifiers;
using Battle.Units.Stats;
using Battle.Units.Statuses;
using UnityEngine;
using UnityEngine.Serialization;
using Item = Battle.Items.Item;
using Cell = Battle.Grid.Cell;
using Spell = Battle.Spells.Spell;

namespace Battle.Units
{
    /// Keeps data about player (stats, inventory, money, etc.)
    /// Is Serializeable and can be saved.
    [CreateAssetMenu(fileName = "PlayerData", menuName = "UnitData/PlayerData")]
    [Serializable]
    public class PlayerData : ScriptableObject
    {
        [FormerlySerializedAs("unitHp")] [SerializeField]
        public Hp hp;

        [SerializeField] public Mana mana;
        [SerializeField] public Damage damage;
        [SerializeField] public List<Item> items;
        [SerializeField] public List<Spell> spells;
        [SerializeField] public ModifierList statuses;
        [SerializeField] public List<Cell> cells;
        [SerializeField] public int money;

        public static PlayerData NewData(Player player, PlayerData oldData)
        {
            return NewData(player.hp, player.mana, player.damage, player.Statuses,
                oldData);
        }

        public static PlayerData NewData(
            Hp hp,
            Mana mana,
            Damage damage,
            ModifierList statuses)
        {
            var data = CreateInstance<PlayerData>();

            data.hp = hp.Save();
            data.mana = mana.Save();
            data.damage = damage.Save();

            statuses.RemoveTempModsAndUnAttach();
            data.statuses = statuses;

            return data;
        }

        public static PlayerData NewData(
            Hp hp,
            Mana mana,
            Damage damage,
            ModifierList statuses,
            PlayerData oldData)
        {
            var data = NewData(hp, mana, damage, statuses);

            data.spells = oldData.spells;
            data.items = oldData.items;
            data.money = oldData.money;
            data.cells = oldData.cells;

            return data;
        }

        public void AddStatus(Status status)
        {
            statuses.Add(status);
        }
    }
}