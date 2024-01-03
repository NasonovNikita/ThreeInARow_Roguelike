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
                if (a[i] is null) return;
                
                a[i] = Object.Instantiate(a[i]);
            }
        }

        public static bool RandomChance(int chance)
        {
            return Random.Range(1, 101) <= chance;
        }

        public static T RandomChoose<T>(List<T> toChoose)
        {
            return toChoose[Random.Range(0, toChoose.Count)];
        }
    }
}