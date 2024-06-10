using System.Collections.Generic;
using Other;
using UnityEngine;

namespace UI.Battle.ModsDisplaying
{
    public interface IModIconModifier : IChangeAble
    {
        protected static string SimpleFormatDescription(string original, params object[] args) =>
            original.IndexErrorProtectedFormat(args);

        protected static string FormatDescriptionByKeys(string original, Dictionary<string, object> args) =>
            original.FormatByKeys(args);
        
        public Sprite Sprite { get; }
        public string Description { get; }
        public string SubInfo { get; }
        public bool ToDelete { get; }

        protected const string EmptyInfo = "";
    }
}