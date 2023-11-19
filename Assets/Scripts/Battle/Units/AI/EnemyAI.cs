using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Grid = Battle.Match3.Grid;

namespace Battle.Units.AI
{
    public class EnemyAI : MonoBehaviour
    {
        private Enemy _attachedEnemy;
        private Player _player;
        private Grid _grid;
        private List<(int, int, int, int, int)> profits = new ();

        public void Awake()
        {
            _attachedEnemy = GetComponent<Enemy>();
            _player = FindFirstObjectByType<Player>();
        }

        private IEnumerator<WaitUntil> Attack()
        {
            UseSpells();

            _grid = FindFirstObjectByType<Grid>();
            UseGrid();

            yield return new WaitUntil(() => _grid.state == GridState.Blocked);
            
            Damage dmg = new Damage(
                (int) _attachedEnemy.fDmg * _grid.destroyed[GemType.Red], 
                (int) _attachedEnemy.cDmg * _grid.destroyed[GemType.Blue],
                (int) _attachedEnemy.pDmg * _grid.destroyed[GemType.Green],
                (int) _attachedEnemy.lDmg * _grid.destroyed[GemType.Yellow],
                (int) _attachedEnemy.phDmg * _grid.destroyed.Sum(t => t.Key != GemType.Mana ? t.Value : 0)
                );

            _attachedEnemy.mana += _grid.destroyed[GemType.Mana] * _attachedEnemy.manaPerGem;
            _grid.ClearDestroyed();
            if (dmg.IsZero()) yield break;
            
            _player.DoDamage(dmg);
            EToPDamageLog.Log(_attachedEnemy, _player, dmg);
        }

        private void UseGrid()
        {
            var box = _grid.BoxCopy();

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
            profits = new List<(int, int, int, int, int)>();
            
            StartCoroutine(_grid.EnemyChoose(chosen.Item2, chosen.Item3));
            StartCoroutine(_grid.EnemyChoose(chosen.Item4, chosen.Item5));
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

            int dmg =
                (int)_attachedEnemy.fDmg * counts[GemType.Red] +
                (int)_attachedEnemy.cDmg * counts[GemType.Blue] +
                (int)_attachedEnemy.pDmg * counts[GemType.Green] +
                (int)_attachedEnemy.lDmg * counts[GemType.Yellow] +
                (int)_attachedEnemy.phDmg * deleted.Count(val => val.Type != GemType.Mana);
            return dmg;
        }

        private void UseSpells()
        {
            if (_attachedEnemy.spells.Count == 0) return;
            
            var possibleSpells = _attachedEnemy.spells.Where(spell => _attachedEnemy.mana >= spell.manaCost).ToList();

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