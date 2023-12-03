using System;
using Battle.Units;
using UnityEngine;

namespace Battle.Items
{
    [Serializable]
    public abstract class Item : ScriptableObject
    {
        [SerializeField] public string title;
        
        public abstract void Use(Unit unitBelong);
    }
}