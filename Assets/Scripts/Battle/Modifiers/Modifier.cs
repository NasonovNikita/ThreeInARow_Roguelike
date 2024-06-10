using System;
using System.Collections.Generic;
using System.Linq;
using Other;
using UnityEngine;

namespace Battle.Modifiers
{
    [Serializable]
    public abstract class Modifier : IChangeAble
    { 
        [SerializeField] private bool save;
    
        protected Modifier(bool save = false)
        {
            this.save = save;
        }
    
        protected virtual List<IChangeAble> ChangeAblesToInitialize => new();
    
        public static void RemoveNotSavingMods<T>(List<T> list) where T : Modifier
        {
            foreach (T mod in list.ToList().Where(mod => !mod.save)) list.Remove(mod);
        }
    
        public static List<T> LoadList<T>(List<T> list) where T : Modifier
        {
            foreach (T mod in list) mod.Init();
    
            return list;
        }
    
    
        public static void AddToList(List<Modifier> list, Modifier other)
        {
            Modifier second = list.FirstOrDefault(obj =>
                obj.LowCanConcat(other));
    
            if (second is not null) second.LowConcat(other);
            else list.Add(other);
        }
    
        protected T CreateChangeableSubSystem<T>(T changeAble) where T : IChangeAble
        {
            InitChangeAble(changeAble);
            ChangeAblesToInitialize.Add(changeAble);
    
            return changeAble;
        }
    
        public void Init()
        {
            foreach (IChangeAble changeAble in ChangeAblesToInitialize) InitChangeAble(changeAble);
        }
    
        private void InitChangeAble(IChangeAble changeAble)
        {
            changeAble.OnChanged += () => OnChanged?.Invoke();
            (changeAble as IInit)?.Init();
        }
    
        #region ConcatAbility
    
        public bool LowCanConcat(Modifier other)
        {
            return save == other.save && !EndedWork && CanConcat(other);
        }
    
        protected abstract bool CanConcat(Modifier other);
    
        public abstract void Concat(Modifier other);
    
        private void LowConcat(Modifier other)
        {
            Concat(other);
            OnChanged?.Invoke();
        }
    
        #endregion
    
        public abstract bool EndedWork { get; }
        
        public event Action OnChanged;
    }
}