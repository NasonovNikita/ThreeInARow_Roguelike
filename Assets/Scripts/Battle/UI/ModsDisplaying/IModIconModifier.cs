using System.Collections.Generic;
using Other;
using UnityEngine;

namespace Battle.UI.ModsDisplaying
{
    public interface IModIconModifier : IChangeAble
    {
        protected static string SimpleFormatDescription(string original, params object[] args)
        {
            return original.IndexErrorProtectedFormat(args);
        }

        protected static string FormatDescriptionByKeys(string original,
            Dictionary<string, object> args)
        {
            return original.FormatByKeys(args);
        }

        public Sprite Sprite { get; }
        public string Description { get; }
        public string SubInfo { get; }
        public bool ToDelete { get; }

        protected const string EmptyInfo = "";
    }
}