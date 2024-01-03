using System;
using Battle.Units;
using Shop;
using UnityEngine;

namespace Battle.Items
{
    [Serializable]
    public abstract class Item : ScriptableObject, IGood
    {
        [SerializeField] private string title;
        [SerializeField] private string description;
        
        public abstract void Use(Unit unitBelong);
        
        
        public string Title => title;
        public string Description => description;
        public virtual void OnBuy() {}
    }
}