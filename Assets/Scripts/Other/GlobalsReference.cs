using Core;
using UnityEngine;

namespace Other
{
    /// <summary>
    ///     Ridiculous shi-. Never do this.<br/>Don't look how it works.
    /// </summary>
    public class GlobalsReference : MonoBehaviour
    {
        public string currentField;

        public void SetValue<T>(T value)
        {
            typeof(Globals).GetField(currentField).SetValue(Globals.Instance, value);
        }

        public T GetValue<T>()
        {
            return (T)typeof(Globals).GetField(currentField).GetValue(Globals.Instance);
        }
    }
}