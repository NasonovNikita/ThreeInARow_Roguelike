using System;
using UnityEngine;

namespace Other
{
    [Serializable]
    public abstract class LootItem : ScriptableObject
    {
        [SerializeField] protected Rarity rarity;
        public Sprite img;
        
        public int Frequency =>
            rarity switch
            {
                Rarity.Common => 20,
                Rarity.Rare => 14,
                Rarity.Epic => 9,
                Rarity.Legendary => 5,
                Rarity.Secret => 0,
                _ => throw new ArgumentOutOfRangeException()
            };

        public abstract string Title { get; }
        public abstract string Description { get; }
        
        public virtual void OnGet() {}
        public virtual void Get()
        {
            OnGet();
        }
    }

    public enum Rarity
    {
        Common,
        Rare,
        Epic,
        Legendary,
        Secret
    }
}