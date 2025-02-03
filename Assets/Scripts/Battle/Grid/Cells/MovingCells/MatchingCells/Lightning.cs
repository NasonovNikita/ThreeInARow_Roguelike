using System.Collections.Generic;
using System.Linq;
using Battle.Modifiers;
using Battle.Units;
using UnityEngine;

namespace Battle.Grid.Cells.MovingCells.MatchingCells
{
    public class Lightning : RowingCell, IModifierAble
    {
        [SerializeField] private int baseAttackVal;

        public override string Description => descriptionKeyRef.Value;

        public ModifierList Modifiers { get; } = new();

        public int Value => baseAttackVal;
        
        public override bool IsSameType(Cell other) => other is Lightning;

        protected override int CountInRow => 3;

        // AI generated
        public override List<MatchingCell> GetCellsToUse()
        {
            var found = new HashSet<RowingCell>();
            
            var isPartOfHorizontalRow = new bool[Grid.Instance.sizeY][];
            for (var index = 0; index < Grid.Instance.sizeY; index++)
                isPartOfHorizontalRow[index] = new bool[Grid.Instance.sizeX];

            var isPartOfVerticalRow = new bool[Grid.Instance.sizeY][];
            for (var index = 0; index < Grid.Instance.sizeY; index++)
                isPartOfVerticalRow[index] = new bool[Grid.Instance.sizeX];

            // Step 1: Mark cells that are part of a horizontal row
            for (var i = 0; i < Grid.Instance.sizeY; i++)
            for (var j = 0; j <= Grid.Instance.sizeX - CountInRow; j++)
            {
                if (!RowExists(i, j, dj: 1)) continue;
                
                for (var dj = 0; dj < CountInRow; dj++)
                {
                    isPartOfHorizontalRow[i][j + dj] = true;
                }
            }

            // Step 2: Mark cells that are part of a vertical row
            for (var j = 0; j < Grid.Instance.sizeX; j++)
            for (var i = 0; i <= Grid.Instance.sizeY - CountInRow; i++)
            {
                if (!RowExists(i, j, di: 1)) continue;
                
                for (var di = 0; di < CountInRow; di++)
                {
                    isPartOfVerticalRow[i + di][j] = true;
                }
            }

            // Step 3: Identify crosses and add all matching rows
            for (var i = 0; i < Grid.Instance.sizeY; i++)
            for (var j = 0; j < Grid.Instance.sizeX; j++)
            {
                if (!isPartOfHorizontalRow[i][j] || !isPartOfVerticalRow[i][j]) continue;
                
                // Traverse the entire horizontal row to the left
                for (var x = j; x >= 0 && isPartOfHorizontalRow[i][x]; x--)
                {
                    found.Add((RowingCell)Grid.Instance.Box[i, x]);
                }

                // Traverse the entire horizontal row to the right
                for (var x = j + 1; x < Grid.Instance.sizeX && isPartOfHorizontalRow[i][x]; x++)
                {
                    found.Add((RowingCell)Grid.Instance.Box[i, x]);
                }

                // Traverse the entire vertical row upwards
                for (var y = i - 1; y >= 0 && isPartOfVerticalRow[y][j]; y--)
                {
                    found.Add((RowingCell)Grid.Instance.Box[y, j]);
                }

                // Traverse the entire vertical row downwards
                for (var y = i + 1; y < Grid.Instance.sizeY && isPartOfVerticalRow[y][j]; y++)
                {
                    found.Add((RowingCell)Grid.Instance.Box[y, j]);
                }

                // Add the cross cell itself
                found.Add((RowingCell)Grid.Instance.Box[i, j]);
            }

            return found.Cast<MatchingCell>().ToList();
        }

        protected override void Use()
        {
            foreach (var enemy in Player.Instance.Enemies)
            {
                enemy.TakeDamage(
                    Player.Instance.damage.ApplyDamage(
                        IIntModifier.UseModList(Modifiers.List, baseAttackVal)
                        )
                    );
                Player.Instance.InvokeOnMadeHit();
            }
        }
    }
}