using System;
using System.Collections.Generic;
using Other;
using UnityEngine;

namespace UI.Battle.ModsDisplaying
{
    public abstract class DisplayedModifier
    {
        public event Action OnChanged;

        protected static string SimpleFormatDescription(string original, params object[] args) =>
            original.IndexErrorProtectedFormat(args);

        protected static string FormatDescriptionByKeys(string original, Dictionary<string, object> args) =>
            original.FormatByKeys(args);

        protected void InvokeOnChanged() => OnChanged?.Invoke();
        
        public abstract Sprite Sprite { get; }
        public abstract string Description { get; }
        public abstract string SubInfo { get; }
        public abstract bool ToDelete { get; }

        protected const string EmptyInfo = "";
    }
}