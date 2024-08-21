using System;
using System.Collections.Generic;
using Other;
using UnityEngine;

namespace Battle.Modifiers
{
    /// <summary>
    ///     Supports concatenation, initialization and state checking.<br/>
    ///     <inheritdoc cref="IChangeAble"/>
    /// </summary>
    [Serializable]
    public abstract class Modifier : IChangeAble
    {
        [SerializeField] public bool save;

        protected Modifier(bool save = false) => this.save = save;

        protected virtual List<IChangeAble> ChangeAblesToInitialize => new();

        public abstract bool EndedWork { get; }

        public event Action OnChanged;

        protected T CreateChangeableSubSystem<T>(T changeAble)
            where T : IChangeAble
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

        private void InitChangeAble(IChangeAble changeAble)
        {
            changeAble.OnChanged += () => OnChanged?.Invoke();
            (changeAble as IInitiated)?.Init();
        }

        #region ConcatAbility

        public bool CanConcat(Modifier other) =>
            save == other.save && !EndedWork && HiddenCanConcat(other);

        protected abstract bool HiddenCanConcat(Modifier other);

        protected abstract void HiddenConcat(Modifier other);

        public void Concat(Modifier other)
        {
            HiddenConcat(other);
            OnChanged?.Invoke();
        }

        #endregion
    }
}