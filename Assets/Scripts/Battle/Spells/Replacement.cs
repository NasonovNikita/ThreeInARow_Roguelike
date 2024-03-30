using UnityEngine;
using Grid = Battle.Match3.Grid;

namespace Battle.Spells
{
    [CreateAssetMenu(menuName = "Spells/Replacement")]
    public class Replacement : Spell
    {
        [SerializeField] private int count;
        [SerializeField] private Match3.MatchingCells.Mana manaCell;
        
        protected override void Action()
        {
            var grid = FindFirstObjectByType<Grid>();
            for (int i = 0; i < count; i++)
                grid.SetCell(manaCell, Random.Range(0, grid.sizeX), Random.Range(0, grid.sizeY));
        }
    }
}