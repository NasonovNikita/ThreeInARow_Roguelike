using System;
using System.Collections.Generic;
using System.Linq;
using Other;
using UI.Battle.ModsDisplaying;
using UnityEngine;

namespace Battle.Modifiers
{
    [Serializable]
    public abstract class Modifier : DisplayedModifier
    {
        [SerializeField] private bool save;

        protected Modifier(bool save = false) => this.save = save;

        public static List<T> SaveList<T>(IEnumerable<T> list) where T : Modifier=> 
            list?.Where(obj => obj.save).ToList();


        public bool LowCanConcat(Modifier other) => 
            save == other.save && CanConcat(other);

        protected abstract bool CanConcat(Modifier other);

        public abstract void Concat(Modifier other);

        private void LowConcat(Modifier other)
        {
            Concat(other);
            InvokeOnChanged();
        }

        public static void AddToList<T>(List<T> list, T other) where T : Modifier
        {
            var second = list.FirstOrDefault(obj =>
                obj.LowCanConcat(other));
            
            if (second is not null) second.LowConcat(other);
            else list.Add(other);
        }

        protected T CreateChangeableSubSystem<T>(T changeAble) where T : IChangeAble
        {
            changeAble.OnChanged += InvokeOnChanged;
            return changeAble;
        }
    }
}