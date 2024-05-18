using System;
using System.Collections.Generic;
using System.Linq;
using UI;
using UnityEngine;
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

            public static T RandomChoose<T>(IEnumerable<T> toChoose)
            {
                var list = toChoose.ToList();
                return list[UnityRandom.Range(0, list.Count)];
            }

            public static T RandomChooseWithChances<T>(List<(T, float)> chances)
            {
                float sum = chances.Sum(v => v.Item2);
                float chosenChance = UnityRandom.Range(0, sum);

                var result = default(T);
                foreach ((T, float) chance in chances)
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

                var result = default(T);
                foreach ((T, int) chance in chances)
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

            public static void ResetRandom() => 
                UnityRandom.InitState((int) DateTime.Now.Ticks);
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
                    switch (json[i])
                    {
                        case '{':
                            nextElem += "{";
                            openedBracesCnt += 1;
                            break;
                        case '}':
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

        public static T InstantiateUI<T>(T obj) where T : Object => 
            Object.Instantiate(obj, UICanvas.Instance.transform);

        public static void InitButton(this Button btn, UnityEngine.Events.UnityAction onClick, string content)
        {
            btn.onClick.AddListener(onClick);
            btn.GetComponentInChildren<Text>().text = content;
        }

        public static int Percents(float v) => (int)(v * 100);
        
        
        public static string FormatByKeys(this string formattedString, IReadOnlyDictionary<string, object> values) =>
            values.Aggregate(formattedString,
                (current, pair) => current.Replace(pair.Key, (string)pair.Value));

        public static string IndexErrorProtectedFormat(this string original, params object[] args)
        {
            string res = "";

            List<string> parts = new();
            List<string> replaceMarks = new();

            string lastPart = "";
            for (int i = 0; i < original.Length; i++)
            {
                char c = original[i];
                
                if (c == '{')
                {
                    parts.Add(lastPart);
                    lastPart = "";
                    
                    string replaceMark = "{";
                    do
                    {
                        i++;
                        replaceMark += original[i];
                    } while (original[i] != '}');
                    
                    replaceMarks.Add(replaceMark);
                }
                else lastPart += c;
            }
            
            if (lastPart != "") parts.Add(lastPart);

            UnityEngine.Assertions.Assert.IsTrue(
                replaceMarks.Contains("{}") && new HashSet<string>(replaceMarks).Count == 1 ||
                !replaceMarks.Contains("{}"),
                "Indexed and non-indexed replace points can't exist at the same time\n" +
                $"Original string: {original}");

            if (replaceMarks.Contains("{}"))
            {
                int i = 0;
                for (; i < replaceMarks.Count; i++)
                {
                    res += parts[i] + $"{{{i}}}";
                }

                if (parts.Count != replaceMarks.Count) res += parts[i];
            }
            else res = original;

            return string.Format(res, args);
        }
    }
}