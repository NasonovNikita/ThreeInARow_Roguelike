using System;
using System.Collections.Generic;
using System.Linq;
using UI;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using Object = UnityEngine.Object;
using UnityRandom = UnityEngine.Random;

namespace Other
{
    public static class Tools
    {
        public static void InstantiateAll<T>(List<T> objects) where T : Object
        {
            for (var i = 0; i < objects.Count; i++)
            {
                if (objects[i] is null) return;

                objects[i] = Object.Instantiate(objects[i]);
            }
        }

        /// <summary>
        ///     Instantiates into <see cref="UICanvas"/>.
        /// </summary>
        public static T InstantiateUI<T>(T obj) where T : Object =>
            Object.Instantiate(obj, UICanvas.Instance.transform);

        public static void InitButton(this Button btn, Action onClick,
            string content)
        {
            btn.onClick.AddListener(() => onClick());
            btn.GetComponentInChildren<Text>().text = content;
        }

        /// <summary>
        ///     Convert float value to percents (rounded).
        /// </summary>
        public static int Percents(float v) => (int)(v * 100);

        /// <summary>
        ///     Gets all items from two-dimensional array and returns as a single list.
        /// </summary>
        public static List<T> MultiDimToOne<T>(T[,] orig)
        {
            var value = new List<T>();
            for (var i = 0; i < orig.GetLength(0); i++)
            for (var j = 0; j < orig.GetLength(1); j++)
                value.Add(orig[i, j]);

            return value;
        }

        /// <summary>
        ///     Returns all combinations of given array of length of <b>count</b>.
        ///     Is used to get all possible variations of arrays consisting of given items.
        /// </summary>
        /// <example>
        ///     Returns [1, 1], [1, 2], [1, 3], [2, 1] ... (or something like that).
        ///     <code>
        /// string res = "";
        /// foreach(T[] arr in Repeat(new int[] {1, 2, 3}, 2)
        /// {
        ///     res += arr.ToString() + ", ";
        /// }
        /// Debug.Log(res);
        /// </code>
        /// </example>
        /// <param name="items">Items that will be repeated.</param>
        /// <param name="count">Count of items in each returned array.</param>
        /// <returns>Iterator for all combinations of items.</returns>
        public static T[][] Repeat<T>(T[] items, int count)
        {
            var sequences = new List<T[]> { Array.Empty<T>() };

            for (var i = 0; i < count; i++)
                sequences = sequences
                    .SelectMany(
                        _ => items,
                        (seq, item) => seq.Concat(new[] { item }).ToArray()
                    ).ToList(); 

            return sequences.ToArray();
        }

        public static string FormatByKeys(this string formattedString,
            IReadOnlyDictionary<string, object> values)
        {
            return values.Aggregate(formattedString,
                (current, pair) =>
                    current.Replace($"{{{pair.Key}}}", pair.Value.ToString()));
        }

        /// <summary>
        ///     Supports Python-like format, where there is no indexes in {} brackets.
        /// </summary>
        /// <example>
        ///     Both variants work:<br/>
        ///     <c>"Hello, {0}!".IndexErrorProtectedFormat("Tom") // "Hello, Tom!"</c>
        ///     <br/>
        ///     <c>"Hello, {}!".IndexErrorProtectedFormat("Tom") // "Hello, Tom!"</c>
        /// </example>
        public static string IndexErrorProtectedFormat(this string original,
            params object[] args)
        {
            var res = "";

            List<string> parts = new();
            List<string> replaceMarks = new();

            var lastPart = "";
            for (var i = 0; i < original.Length; i++)
            {
                var c = original[i];

                if (c == '{')
                {
                    parts.Add(lastPart);
                    lastPart = "";

                    var replaceMark = "{";
                    do
                    {
                        i++;
                        replaceMark += original[i];
                    } while (original[i] != '}');

                    replaceMarks.Add(replaceMark);
                }
                else
                {
                    lastPart += c;
                }
            }

            if (lastPart != "") parts.Add(lastPart);

            Assert.IsTrue(
                (replaceMarks.Contains("{}") &&
                 new HashSet<string>(replaceMarks).Count == 1) ||
                !replaceMarks.Contains("{}"),
                "Indexed and non-indexed replace points can't exist at the same time\n" +
                $"Original string: {original}");

            if (replaceMarks.Contains("{}"))
            {
                var i = 0;
                for (; i < replaceMarks.Count; i++) res += parts[i] + $"{{{i}}}";

                if (parts.Count != replaceMarks.Count) res += parts[i];
            }
            else
            {
                res = original;
            }

            return string.Format(res, args);
        }

        public static class Random
        {
            public static bool RandomChance(int chance) =>
                UnityRandom.Range(1, 101) <= chance;

            public static T RandomChoose<T>(IEnumerable<T> toChoose)
            {
                var list = toChoose.ToList();
                return list[UnityRandom.Range(0, list.Count)];
            }

            public static T RandomChooseWithChances<T>(List<(T, float)> chances)
            {
                var sum = chances.Sum(v => v.Item2);
                var chosenChance = UnityRandom.Range(0, sum);

                var result = default(T);
                foreach (var chance in chances)
                    if (chosenChance > chance.Item2)
                    {
                        chosenChance -= chance.Item2;
                    }
                    else
                    {
                        result = chance.Item1;
                        break;
                    }

                return result;
            }

            public static T RandomChooseWithChances<T>(List<(T, int)> chances)
            {
                var sum = chances.Sum(v => v.Item2);
                var chosenChance = UnityRandom.Range(1, sum + 1);

                var result = default(T);
                foreach (var chance in chances)
                    if (chosenChance > chance.Item2)
                    {
                        chosenChance -= chance.Item2;
                    }
                    else
                    {
                        result = chance.Item1;
                        break;
                    }

                return result;
            }

            /// <summary>
            ///     Initializes <see cref="UnityRandom"/> with new seed.
            /// </summary>
            public static void ResetRandom()
            {
                UnityRandom.InitState((int)DateTime.Now.Ticks);
            }
        }

        public static class Json
        {
            public static string ListToJson<T>(IEnumerable<T> list)
            {
                var elements = list.Select(elem => JsonUtility.ToJson(elem)).ToList();

                return "{[" + string.Join(",", elements) + "]}";
            }

            public static List<T> JsonToListScriptableObjects<T>(string json)
                where T : ScriptableObject
            {
                var elements = ParseList(json);
                var res = new List<T>();
                for (var i = 0; i < elements.Count; i++)
                    res.Add(ScriptableObject.CreateInstance<T>());

                for (var i = 0; i < elements.Count; i++)
                    JsonUtility.FromJsonOverwrite(elements[i], res[i]);

                return res;
            }

            private static List<string> ParseList(string json)
            {
                json = json[2..^2];
                var elements = new List<string>();
                var nextElem = "";
                var openedBracesCnt = 0;
                var i = 0;
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
    }
}