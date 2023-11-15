using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

namespace Other
{
    public class FieldReference : MonoBehaviour
    {
        public string currentField;

        public void SetValue<T>(T value)
        {
            typeof(Globals).GetField(currentField).SetValue(Globals.instance, value);
        }

        public T GetValue<T>()
        {
            return (T) typeof(Globals).GetField(currentField).GetValue(Globals.instance);
        }
    }
}