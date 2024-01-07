using System.Collections.Generic;
using System.Linq;
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

        public static T RandomChooseWithChances<T>(List<(T, int)> chances)
        {
            int sum = chances.Sum(v => v.Item2);
            int chosenChance = Random.Range(1, sum + 1);

            T result = default(T);
            foreach (var chance in chances)
            {
                if (chosenChance > chance.Item2) chosenChance -= chance.Item2;
                else 
                {
                    result = chance.Item1;
                    break;
                }
            }

            return result;
        }
    }
}