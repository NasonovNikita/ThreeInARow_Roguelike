using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Battle.Items;
using Battle.Modifiers;
using Battle.Spells;
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
        [SerializeField] public UnitDamage damage;
        [SerializeField] public List<Item> items;
        [SerializeField] public List<Spell> spells;
        [SerializeField] public List<Modifier> allMods;
        [SerializeField] public int manaPerGem;
        [SerializeField] public DmgType chosenElement;
        [SerializeField] public int money;
        public bool loaded;
        
        public static PlayerData NewData(Player player, PlayerData oldData = null)
        {
            PlayerData data = NewData(player.hp, player.mana, player.unitDamage, player.manaPerGem,
                player.chosenElement, oldData);

            return data;
        }

        [SuppressMessage("ReSharper", "ParameterHidesMember")]
        public static PlayerData NewData(Hp hp, Mana mana, UnitDamage damage, int manaPerGem,
            DmgType chosenElement, PlayerData oldData = null)
        {
            PlayerData data = CreateInstance<PlayerData>();
            data.hp = hp;
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
            unit.hp = hp;
            unit.mana = mana;
            unit.manaPerGem = manaPerGem;
            unit.unitDamage = damage;
            unit.allMods = new List<Modifier>(allMods);
            unit.spells = new List<Spell>(spells);
            unit.items = new List<Item>(items);
            unit.chosenElement = chosenElement;
        }
        
        public void AddHpMod(Modifier mod)
        {
            hp.AddMod(mod);
            allMods.Add(mod);
        }

        public void AddDamageMod(Modifier mod)
        {
            damage.AddMod(mod);
            allMods.Add(mod);
        }

        public void AddManaMod(Modifier mod)
        {
            mana.AddMod(mod);
            allMods.Add(mod);
        }
        
    }
}