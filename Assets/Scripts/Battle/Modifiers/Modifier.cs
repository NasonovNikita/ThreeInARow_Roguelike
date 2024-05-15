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
        protected virtual List<IChangeAble> ChangeAblesToInitialize => new();

        protected Modifier(bool save = false)
        {
            this.save = save;
        }

        public static void RemoveNotSavingMods<T>(List<T> list) where T : Modifier
        {
            foreach (T mod in list.ToList().Where(mod => !mod.save)) list.Remove(mod);
        }

        public static List<T> LoadList<T>(List<T> list) where T : Modifier
        {
            foreach (T mod in list) mod.Init();

            return list;
        }
        

        public static void AddToList<T>(List<T> list, T other) where T : Modifier
        {
            T second = list.FirstOrDefault(obj =>
                obj.LowCanConcat(other));
            
            if (second is not null) second.LowConcat(other);
            else list.Add(other);
        }

        #region ConcatAbility
        
        public bool LowCanConcat(Modifier other) => 
            save == other.save && !ToDelete && CanConcat(other);

        protected abstract bool CanConcat(Modifier other);

        public abstract void Concat(Modifier other);

        private void LowConcat(Modifier other)
        {
            Concat(other);
            InvokeOnChanged();
        }
        
        #endregion

        protected T CreateChangeableSubSystem<T>(T changeAble) where T : IChangeAble
        {
            changeAble.OnChanged += InvokeOnChanged;
            
            return changeAble;
        }

        public void Init()
        {
            foreach (IChangeAble changeAble in ChangeAblesToInitialize)
            {
                changeAble.OnChanged += InvokeOnChanged;
            }
            foreach (IInit init in ChangeAblesToInitialize.Where(changeAble => changeAble != null).Select(changeAble => changeAble as IInit)) 
                init?.Init();
        }
    }
}