using System;
using System.Collections.Generic;
using UnityEngine;

namespace Battle.Modifiers
{
    [Serializable]
    public class ModifierList<T> where T : Modifier
    {
        public event Action<T> OnModAdded;

        public List<T> ModList => modList;
        
        [SerializeField] [SerializeReference] private List<T> modList = new();

        public ModifierList() {}
        
        public ModifierList(ModifierList<T> other) => modList = new List<T>(other.modList);

        public void Add(T elem)
        {
            Modifier.AddToList(modList, elem);
            if (modList.Contains(elem)) OnModAdded?.Invoke(elem);
        }

        public ModifierList<T> Save()
        {
            modList = Modifier.SaveList(modList);

            return this;
        }
    }
}