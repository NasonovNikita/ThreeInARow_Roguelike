using System;
using UnityEngine;

namespace UI.Battle.ModsDisplaying
{
    public abstract class DisplayedModifier
    {
        public event Action OnChanged;

        protected void InvokeOnChanged() => OnChanged?.Invoke();
        
        public abstract Sprite Sprite { get; }
        public abstract string Description { get; }
        public abstract string SubInfo { get; }
        public abstract bool ToDelete { get; }

        protected const string EmptyInfo = "";
    }
}