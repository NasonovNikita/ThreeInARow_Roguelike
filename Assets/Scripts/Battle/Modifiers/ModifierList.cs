using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Battle.Modifiers
{
    /// <summary>
    ///     A special list for <see cref="Modifier">Modifiers</see>. It can save, initialize and add new mods.
    ///     Usually all objects with modified behaviour have their own <see cref="ModifierList"/>.
    /// </summary>
    [Serializable]
    public class ModifierList
    {
        [SerializeField] [SerializeReference] private List<Modifier> list = new();

        public ModifierList(ModifierList other = null)
        {
            if (other is not null)
                list = new List<Modifier>(other.list);
        }

        public List<Modifier> List => new(list);

        public event Action<Modifier> OnModAdded;

        /// <summary>
        ///     Adds a new modifier to list. If it is possible to <see cref="Modifier.Concat">concat</see> a mod with another one
        ///     it will.
        /// </summary>
        /// <remarks>Invokes OnModAdded event only if mod was added instead of concatenation.</remarks>
        public void Add(Modifier elem)
        {
            SmartAdd(elem);
            if (list.Contains(elem)) OnModAdded?.Invoke(elem);
        }

        private void SmartAdd(Modifier other)
        {
            var second = list.FirstOrDefault(obj =>
                obj.CanConcat(other));

            if (second is not null)
                second.Concat(other); // If found mod, can concat with, concat
            else list.Add(other); // Else just add
        }

        public void InitMods()
        {
            foreach (var mod in list) mod.Init();
        }


        /// <summary>   Shortcut for using both <see cref="RemoveTempMods"/> and <see cref="UnAttachEvents"/>.    </summary>
        public void RemoveTempModsAndUnAttach()
        {
            RemoveTempMods();
            UnAttachEvents();
        }

        public void RemoveTempMods()
        {
            foreach (var mod in list.ToList().Where(mod => !mod.save))
                list.Remove(mod);
        }

        public void UnAttachEvents()
        {
            OnModAdded = null;
        }
    }
}