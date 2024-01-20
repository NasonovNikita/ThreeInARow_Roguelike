using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Object = UnityEngine.Object;
using UnityRandom = UnityEngine.Random;

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

        public static class Random
        {
            public static bool RandomChance(int chance)
            {
                return UnityRandom.Range(1, 101) <= chance;
            }

            public static T RandomChoose<T>(List<T> toChoose)
            {
                return toChoose[UnityRandom.Range(0, toChoose.Count)];
            }

            public static T RandomChooseWithChances<T>(List<(T, float)> chances)
            {
                float sum = chances.Sum(v => v.Item2);
                float chosenChance = UnityRandom.Range(0, sum);

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
            
            public static T RandomChooseWithChances<T>(List<(T, int)> chances)
            {
                int sum = chances.Sum(v => v.Item2);
                int chosenChance = UnityRandom.Range(1, sum + 1);

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

            public static void ResetRandom()
            {
                UnityRandom.InitState((int) DateTime.Now.Ticks);
            }
        }

        public static T InstantiateUI<T>(T obj) where T : Object
        {
            return Object.Instantiate(obj, Object.FindFirstObjectByType<Canvas>().transform);
        }

        public static void InitButton(Button btn, UnityAction onClick, string content)
        {
            btn.onClick.AddListener(onClick);
            btn.GetComponentInChildren<Text>().text = content;
        }

        public static class Json
        {
            public static string ListToJson<T>(IEnumerable<T> list)
            {
                var elements = list.Select(elem => JsonUtility.ToJson(elem)).ToList();
                
                return "{[" + string.Join(",", elements) + "]}";
            }

            public static List<T> JsonToList<T>(string json) where T : ScriptableObject
            {
                var elements = ParseList(json);
                var res = new List<T>();
                for (int i = 0; i < elements.Count; i++)
                {
                    res.Add(ScriptableObject.CreateInstance<T>());
                }
                
                for (int i = 0; i < elements.Count; i++)
                {
                    JsonUtility.FromJsonOverwrite(elements[i], res[i]);
                }

                return res;
            }

            private static List<string> ParseList(string json)
            {
                json = json[2..^2];
                var elements = new List<string>();
                string nextElem = "";
                int openedBracesCnt = 0;
                int i = 0;
                while (i < json.Length)
                {
                    switch (json[i].ToString())
                    {
                        case "{":
                            nextElem += "{";
                            openedBracesCnt += 1;
                            break;
                        case "}":
                            nextElem += "}";
                            openedBracesCnt -= 1;
                            break;
                        default:
                            nextElem += json[i];
                            break;
                    }

                    i++;
                    if (openedBracesCnt != 0) continue;
                    elements.Add(nextElem);
                    nextElem = "";
                    i++;
                }
                return elements;
            }
        }

        public static int Percents(float v) => (int)(v * 100);
    }
}