using System.Collections.Generic;
using UnityEngine;

namespace Other
{
    public static class Tools
    {
        public static void InstantiateAll<T>(IList<T> a) where T : Object
        {
            for (int i = 0; i < a.Count; i++)
            {
                a[i] = Object.Instantiate(a[i]);
            }
        }
    }
}