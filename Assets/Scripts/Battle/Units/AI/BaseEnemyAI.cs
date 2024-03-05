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
        private Grid grid;

        public void Awake()
        {
            grid = FindFirstObjectByType<Grid>();
            attachedEnemy = GetComponent<Enemy>();
        }

        public virtual IEnumerator Act()
        {
            yield return StartCoroutine(UseGrid());
        }

        protected virtual IEnumerator UseGrid()
        {
            List<Move> profits = new ();
            
            var box = grid.BoxCopy();
            
            for (int i = 0; i < box.GetLength(0); i++)
            {
                for (int j = 0; j < box.GetLength(1); j++)
                {
                    if (i < box.GetLength(0) - 1)
                    {
                        (box[i, j], box[i + 1, j]) = (box[i + 1, j], box[i, j]);
                        profits.Add(new Move(CountProfit(box), (i, j), (i + 1, j)));
                        (box[i, j], box[i + 1, j]) = (box[i + 1, j], box[i, j]);
                    }

                    if (j < box.GetLength(1) - 1)
                    {
                        (box[i, j], box[i, j + 1]) = (box[i, j + 1], box[i, j]);
                        profits.Add(new Move(CountProfit(box), (i, j), (i, j + 1)));
                        (box[i, j], box[i, j + 1]) = (box[i, j + 1], box[i, j]); 
                    } 
                }
            }
            
            int max = profits.Max(val => val.profit);
            var chosen = profits[Random.Range(0, profits.Count)];
            if (!attachedEnemy.IsBlind)
            {
                var goodMoves = profits.Where(val => val.profit == max).ToList();
                chosen = goodMoves[Random.Range(0, goodMoves.Count)];
            }

            yield return StartCoroutine(grid.Choose(chosen.coord1.Item1, chosen.coord1.Item2));
            yield return StartCoroutine(grid.Choose(chosen.coord2.Item1, chosen.coord2.Item2));
        }

        protected virtual int CountProfit(Gem[,] box)
        {
            var profit = attachedEnemy.damage.GetGemsDamage(CountMoveResults(box)).Parts
                .Sum(v => v.Value);
            return profit;
        }

        protected Dictionary<GemType, int> CountMoveResults(Gem[,] box) =>
            grid.CountGemTypes(grid.GetDestroyedGems(box));

        protected struct Move
        {
            public readonly int profit;

            public readonly (int, int) coord1;
            public readonly (int, int) coord2;

            public Move(int profit, (int, int) coord1, (int, int) coord2)
            {
                this.profit = profit;
                this.coord1 = coord1;
                this.coord2 = coord2;
            }
            
        }
    }
}