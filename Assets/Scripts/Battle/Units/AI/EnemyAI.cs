using System;
using System.Collections.Generic;
using System.Linq;
using Battle.Modifiers;
using Battle.Spells;
using Battle.Units.Enemies;
using Battle.Units.Stats;
using UnityEngine;
using Grid = Battle.Match3.Grid;
using Random = UnityEngine.Random;

namespace Battle.Units.AI
{
    public class EnemyAI : MonoBehaviour
    {
        private Enemy attachedEnemy;
        private Player player;
        private Grid grid;
        private List<(int, int, int, int, int)> profits = new ();

        public void Awake()
        {
            attachedEnemy = GetComponent<Enemy>();
            player = FindFirstObjectByType<Player>();
        }

        private IEnumerator<WaitUntil> Attack()
        {
            UseSpells();

            grid = FindFirstObjectByType<Grid>();
            UseGrid();

            yield return new WaitUntil(() => grid.state == GridState.Blocked);

            Damage dmg = attachedEnemy.unitDamage.GetGemsDamage(grid.destroyed);
            
            attachedEnemy.UseElementOnDestroyed(grid.destroyed, attachedEnemy.Target);
            
            grid.ClearDestroyed();
            if (dmg.IsZero()) yield break;
            
            player.DoDamage(dmg);
            EToPDamageLog.Log(attachedEnemy, player, dmg);
        }

        private void UseGrid()
        {
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
            var goodOnes = profits.Where(val => val.Item1 == max).ToList();
            var chosen = goodOnes[Random.Range(0, goodOnes.Count)];

            if (attachedEnemy.allMods.Exists(mod => mod.type == ModType.Blind && mod.Use() != 0))
            {
                chosen = profits[Random.Range(0, profits.Count)];
            }
            profits = new List<(int, int, int, int, int)>();


            StartCoroutine(grid.Choose(chosen.Item2, chosen.Item3));
            StartCoroutine(grid.Choose(chosen.Item4, chosen.Item5));
            
        }

        private int CountProfit(Gem[,] box)
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

            int dmg = attachedEnemy.unitDamage.GetGemsDamage(counts).Get().Sum(v => v.Value);
            return dmg;
        }

        private void UseSpells()
        {
            if (attachedEnemy.spells.Count == 0) return;
            
            var possibleSpells = attachedEnemy.spells.Where(spell => attachedEnemy.mana >= spell.useCost).ToList();

            if (possibleSpells.Count == 0) return;
            Spell chosenSpell = possibleSpells[Random.Range(0, possibleSpells.Count)];
            chosenSpell.Cast();
        }

        public void Act()
        {
            StartCoroutine(Attack());
        }
    }
}