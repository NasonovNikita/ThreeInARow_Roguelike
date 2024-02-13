using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Battle.Match3;
using Battle.Modifiers;
using UnityEngine;
using Grid = Battle.Match3.Grid;
using Random = UnityEngine.Random;

namespace Battle.Units.AI
{
    public class BaseEnemyAI : MonoBehaviour
    {
        protected Enemy attachedEnemy;
        private List<(int, int, int, int, int)> profits = new ();

        public void Awake()
        {
            attachedEnemy = GetComponent<Enemy>();
        }

        public virtual IEnumerator Act()
        {
            yield return StartCoroutine(UseGrid());
        }

        protected virtual IEnumerator UseGrid()
        {
            Grid grid = FindFirstObjectByType<Grid>();
            var box = grid.BoxCopy();
            for (int i = 0; i < box.GetLength(0); i++)
            {
                for (int j = 0; j < box.GetLength(1); j++)
                {
                    if (i < box.GetLength(0) - 1)
                    {
                        (box[i, j], box[i + 1, j]) = (box[i + 1, j], box[i, j]);
                        profits.Add((CountProfit(box), i, j, i + 1, j));
                        (box[i, j], box[i + 1, j]) = (box[i + 1, j], box[i, j]);
                    }

                    if (j < box.GetLength(1) - 1)
                    {
                        (box[i, j], box[i, j + 1]) = (box[i, j + 1], box[i, j]);
                        profits.Add((CountProfit(box), i, j, i, j + 1));
                        (box[i, j], box[i, j + 1]) = (box[i, j + 1], box[i, j]); 
                    } 
                }
            }
            
            int max = profits.Max(val => val.Item1);
            var chosen = profits[Random.Range(0, profits.Count)];
            if (!attachedEnemy.allMods.Exists(mod => mod.type == ModType.Blind && mod.Use != 0))
            {
                var goodOnes = profits.Where(val => val.Item1 == max).ToList();
                chosen = goodOnes[Random.Range(0, goodOnes.Count)];
            }

            profits = new List<(int, int, int, int, int)>();

            yield return StartCoroutine(grid.Choose(chosen.Item2, chosen.Item3));
            yield return StartCoroutine(grid.Choose(chosen.Item4, chosen.Item5));
        }

        protected virtual int CountProfit(Gem[,] box)
        {
            var profit = attachedEnemy.damage.GetGemsDamage(CountMoveResults(box)).Parts
                .Sum(v => v.Value);
            return profit;
        }

        protected Dictionary<GemType, int> CountMoveResults(Gem[,] box)
        {
            HashSet<Gem> deleted = new ();
            
            for (int i = 0; i < box.GetLength(0); i++)
            {
                for (int j = 0; j < box.GetLength(1); j++)
                {
                    if (Grid.HorizontalRowExists(i, j, box))
                    {
                        deleted.Add(box[i, j]);
                        deleted.Add(box[i, j + 1]);
                        deleted.Add(box[i, j + 2]);
                    }

                    // ReSharper disable once InvertIf
                    if (Grid.VerticalRowExists(i, j, box))
                    {
                        deleted.Add(box[i, j]);
                        deleted.Add(box[i + 1, j]);
                        deleted.Add(box[i + 2, j]);
                    }
                }
            }

            Dictionary<GemType, int> counts = new()
            {
                { GemType.Red, 0 },
                { GemType.Blue, 0 },
                { GemType.Green, 0 },
                { GemType.Yellow, 0 },
                { GemType.Mana, 0 }
            };

            foreach (Gem gem in deleted)
            {
                counts[gem.Type] += 1;
            }

            return counts;
        }
    }
}