using System.Collections.Generic;
using System.Linq;
using Battle.Grid.Modifiers;
using Other;
using UnityEngine;

namespace Battle.Grid.Cells.MovingCell
{
    public class LightHouse : MovingCell
    {
        [SerializeField] private int count;
        [SerializeField] private int xBonus;

        private int XBonus =>
            Grid.Instance.GetCellNeighbours(this).Exists(cell => cell.IsSameType(this))
                ? xBonus
                : xBonus * xBonus;

        private List<Cell> currentNeighbours = new();

        private readonly Dictionary<IModifierAble, int> givenBonuses = new();

        public void OnEnable()
        {
            UpdateNeighbours();

            Grid.Instance.OnChanged += UpdateNeighbours;
        }

        private void UpdateNeighbours()
        {
            var neighbours = Grid.Instance.GetCellNeighbours(this);

            var oldNeighbours = currentNeighbours.Where(cell => !neighbours.Contains(cell));
            var newNeighbours = neighbours.Where(cell => !currentNeighbours.Contains(cell));

            GiveBonus(newNeighbours);
            DeleteBonus(oldNeighbours);

            currentNeighbours = neighbours.ToList();
        }

        private void GiveBonus(IEnumerable<Cell> cells)
        {
            foreach (IModifierAble cell in cells.OfType<IModifierAble>())
            {
                givenBonuses.Add(cell, cell.Value * (XBonus - 1));
                cell.Modifiers.Add(new AddIntMod(cell.Value * (XBonus - 1)));
            }
        }

        private void DeleteBonus(IEnumerable<Cell> cells)
        {
            foreach (IModifierAble cell in cells.OfType<IModifierAble>())
            {
                cell.Modifiers.Add(new AddIntMod(-givenBonuses[cell]));
                givenBonuses.Remove(cell);
            }
        }

        public override bool BoxIsStable(Cell[,] box)
        {
            return Tools.MultiDimToOne(box).Count(IsSameType) == count;
        }

        public override bool IsSameType(Cell other)
        {
            return other is LightHouse;
        }

        public override string Description => 
            descriptionKeyRef.Value.IndexErrorProtectedFormat(count, xBonus);

    }
}