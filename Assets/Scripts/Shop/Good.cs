using System;
using Battle.Items;
using Battle.Spells;
using Battle.Units;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Shop
{
    [CreateAssetMenu(fileName = "Good", menuName = "Good")]
    public class Good : ScriptableObject
    {
        [SerializeField] private Object good;
        [SerializeField] private GoodType type;
    
        public int price;
        public int frequency;
    
        [NonSerialized]
        public bool bought;

        public void Buy()
        {
            if (Player.data.money < price) return;
            bought = true;
            Player.data.money -= price;
            switch (type)
            {
                case GoodType.Item:
                    Player.data.items.Add((Item) good);
                    break;
                case GoodType.Spell:
                    Player.data.spells.Add((Spell) good);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public string GetName()
        {
            return type switch
            {
                GoodType.Item => ((Item)good).title,
                GoodType.Spell => ((Spell)good).title,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }

    public enum GoodType
    {
        Item,
        Spell
    }
}