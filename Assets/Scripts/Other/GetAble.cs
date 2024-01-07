using System;
using Battle.Items;
using Battle.Spells;
using Battle.Units;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Other
{
    public class GetAble : ScriptableObject
    {
        [SerializeField] protected Object good;
        [SerializeField] protected GetAbleType type;
        public int frequency;

        public string Title => ((IGetAble)good).Title;
        public string Description => ((IGetAble)good).Description;

    
        [NonSerialized]
        public bool got;
        public void Get()
        {
            got = true;
            
            switch (type)
            {
                case GetAbleType.Item:
                    Player.data.items.Add((Item) good);
                    break;
                case GetAbleType.Spell:
                    Player.data.spells.Add((Spell) good);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            ((IGetAble)good).OnGet();
        }
    }
    
    

    public interface IGetAble
    {
        public string Title { get; }
        public string Description { get; }

        public void OnGet();
    }

    public enum GetAbleType
    {
        Item,
        Spell
    }
}