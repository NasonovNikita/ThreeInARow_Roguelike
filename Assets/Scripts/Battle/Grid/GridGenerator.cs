using System;
using System.Collections.Generic;
using System.Linq;
using Battle.Units;
using Other;
using UnityEngine;

namespace Battle.Grid
{
    public class GridGenerator : MonoBehaviour
    {
        [SerializeField] private float refillTime;
        public static GridGenerator Instance { get; private set; }

        private static Cell RandomCell =>
            CellPool.Instance.Acquire(Tools.Random.RandomChoose(Player.Data.cells));

        private static bool BoxIsStable =>
            Player.Data.cells.Aggregate(true,
                (prev, cell) => prev && cell.BoxIsStable(Grid.Instance.box));

        public void Start()
        {
            Instance = this;
            Generate();
        }

        private void Generate()
        {
            Grid.Instance.CreateEmptyBox();

            do
            {
                for (var i = 0; i < Grid.Instance.sizeY; i++)
                for (var j = 0; j < Grid.Instance.sizeX; j++)
                    Grid.Instance.SetCell(RandomCell, i, j);
            } while (!BoxIsStable);

            Grid.Instance.InitGrid();
        }

        public void Refill()
        {
            const int maxTries = 1000;
            var tries = 0;

            do
            {
                tries++;
                for (var i = 0; i < Grid.Instance.sizeY; i++)
                for (var j = 0; j < Grid.Instance.sizeX; j++)
                    if (!Grid.Instance.box[i, j].isActiveAndEnabled)
                        Grid.Instance.SetCell(RandomCell, i, j);

                if (tries <= maxTries) continue;

                throw new OperationCanceledException("Couldn't refill the grid. Too many attempts");
            } while (!BoxIsStable);

            Grid.Instance.InitGrid();
        }

        public void ReplaceCellsByCoordinates(List<(int, int)> coordinates)
        {
            const int maxTries = 10000;
            var tries = 0;

            var successVariants = new List<Cell[]>();

            foreach (var variant in Tools.Repeat(Player.Data.cells.ToArray(),
                         coordinates.Count))
            {
                tries++;
                for (var i = 0; i < coordinates.Count; i++)
                    Grid.Instance.SetCell(CellPool.Instance.Acquire(variant[i]),
                        coordinates[i].Item1, coordinates[i].Item2);

                if (BoxIsStable) successVariants.Add(variant);
                if (tries < maxTries) continue;

                Debug.unityLogger.LogWarning("Grid", "maximum refill tries reached");
                break;
            }

            if (successVariants.Count == 0)
                throw new OperationCanceledException("Grid refill fail");


            var chosenVariant = Tools.Random.RandomChoose(successVariants);

            for (var i = 0; i < coordinates.Count; i++)
                Grid.Instance.SetCell(CellPool.Instance.Acquire(chosenVariant[i]),
                    coordinates[i].Item1, coordinates[i].Item2);

            Grid.Instance.InitGrid();
        }
    }
}