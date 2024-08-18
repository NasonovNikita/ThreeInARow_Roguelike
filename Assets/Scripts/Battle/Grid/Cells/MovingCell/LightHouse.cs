using System.Collections.Generic;
using System.Linq;
using Battle.Modifiers;
using Other;
using UnityEngine;

namespace Battle.Grid.Cells.MovingCell
{
    public class LightHouse : MovingCell
    {
        [SerializeField] private int count;
        [SerializeField] private int xBonus;

        private readonly Dictionary<IModifierAble, int> _givenBonuses = new();

        private List<Cell> _currentNeighbours = new();

        private int XBonus =>
            Grid.Instance.GetCellNeighbours(this).Exists(cell => cell.IsSameType(this))
                ? xBonus
                : xBonus * xBonus;

        public override string Description =>
            descriptionKeyRef.Value.IndexErrorProtectedFormat(count, xBonus);

        public void OnEnable()
        {
            UpdateNeighbours();

            Grid.Instance.OnChanged += UpdateNeighbours;
        }

        private void UpdateNeighbours()
        {
            var neighbours = Grid.Instance.GetCellNeighbours(this);

            var oldNeighbours =
                _currentNeighbours.Where(cell => !neighbours.Contains(cell));
            var newNeighbours =
                neighbours.Where(cell => !_currentNeighbours.Contains(cell));

            GiveBonus(newNeighbours);
            DeleteBonus(oldNeighbours);

            _currentNeighbours = neighbours.ToList();
        }

        private void GiveBonus(IEnumerable<Cell> cells)
        {
            foreach (var cell in cells.OfType<IModifierAble>())
            {
                _givenBonuses.Add(cell, cell.Value * (XBonus - 1));
                cell.Modifiers.Add(new AddIntMod(cell.Value * (XBonus - 1)));
            }
        }

        private void DeleteBonus(IEnumerable<Cell> cells)
        {
            foreach (var cell in cells.OfType<IModifierAble>())
            {
                cell.Modifiers.Add(new AddIntMod(-_givenBonuses[cell]));
                _givenBonuses.Remove(cell);
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
    }
}