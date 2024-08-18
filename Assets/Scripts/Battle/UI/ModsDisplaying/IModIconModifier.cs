using System.Collections.Generic;
using Other;
using UnityEngine;

namespace Battle.UI.ModsDisplaying
{
    /// <summary>
    ///     Contains <see cref="Sprite"/> and <see cref="Description"/>, etc. to be used in view.
    /// </summary>
    public interface IModIconModifier : IChangeAble
    {
        protected const string EmptyInfo = "";

        public Sprite Sprite { get; }
        public string Description { get; }
        public string SubInfo { get; }
        public bool ToDelete { get; }

        protected static string SimpleFormatDescription(string original,
            params object[] args)
        {
            return original.IndexErrorProtectedFormat(args);
        }

        protected static string FormatDescriptionByKeys(string original,
            Dictionary<string, object> args)
        {
            return original.FormatByKeys(args);
        }
    }
}