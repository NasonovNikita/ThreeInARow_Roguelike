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
        [SerializeReference] private Object good;
        [SerializeField] private GoodType type;
    
        public int price;
        public int frequency;
        public string Title => ((IGood)good).Title;
        public string Description => ((IGood)good).Description;
    
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
            ((IGood)good).OnBuy();
        }

    }

    public interface IGood
    {
        public string Title { get; }
        public string Description { get; }

        public void OnBuy();
    }

    public enum GoodType
    {
        Item,
        Spell
    }
}