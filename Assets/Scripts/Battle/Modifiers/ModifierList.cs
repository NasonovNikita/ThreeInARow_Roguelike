using System;
using System.Collections.Generic;
using UnityEngine;

namespace Battle.Modifiers
{
    [Serializable]
    public class ModifierList
    {
        [SerializeField] [SerializeReference] private List<Modifier> modList = new();

        public ModifierList()
        {
        }

        public ModifierList(ModifierList other)
        {
            modList = new List<Modifier>(other.modList);
        }

        public List<Modifier> ModList => modList;
        public event Action<Modifier> OnModAdded;

        public void Add(Modifier elem)
        {
            Modifier.AddToList(modList, elem);
            if (modList.Contains(elem)) OnModAdded?.Invoke(elem);
        }

        public void SaveMods()
        {
            Modifier.RemoveNotSavingMods(modList);
            OnModAdded = null;
        }

        public void InitMods()
        {
            foreach (Modifier mod in modList) mod.Init();
        }
    }
}