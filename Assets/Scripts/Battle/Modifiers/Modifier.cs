using System;
using System.Collections.Generic;
using Other;
using UnityEngine;
using UnityEngine.Serialization;

namespace Battle.Modifiers
{
    /// <summary>
    ///     Supports concatenation, initialization and state checking.<br/>
    ///     <inheritdoc cref="IChangeAble"/>
    /// </summary>
    [Serializable]
    public abstract class Modifier : IChangeAble
    {
        [SerializeField] public bool isSaved;
        [SerializeField] protected bool killed;

        protected Modifier(bool isSaved = false) => this.isSaved = isSaved;

        protected virtual List<IChangeAble> ChangeAblesToInitialize => new();

        protected abstract bool HiddenEndedWork { get; }
        public bool EndedWork => HiddenEndedWork || killed;

        public event Action OnChanged;

        protected T CreateChangeableSubSystem<T>(T changeAble) where T : IChangeAble
        {
            InitChangeAble(changeAble);
            ChangeAblesToInitialize.Add(changeAble);

            return changeAble;
        }

        public void Init()
        {
            foreach (var changeAble in ChangeAblesToInitialize)
                InitChangeAble(changeAble);
        }

        public void Kill()
        {
            killed = true;
            OnChanged?.Invoke();
        }

        private void InitChangeAble(IChangeAble changeAble)
        {
            changeAble.OnChanged += () => OnChanged?.Invoke();
            (changeAble as IInitiated)?.Init();
        }

        #region ConcatAbility

        public bool CanConcat(Modifier other) =>
            isSaved == other.isSaved && !EndedWork && HiddenCanConcat(other);

        public void Concat(Modifier other)
        {
            HiddenConcat(other);
            OnChanged?.Invoke();
        }

        protected abstract bool HiddenCanConcat(Modifier other);

        protected abstract void HiddenConcat(Modifier other);

        #endregion
    }
}