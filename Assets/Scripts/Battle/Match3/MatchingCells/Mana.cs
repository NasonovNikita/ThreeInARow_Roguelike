using UnityEngine;

namespace Battle.Match3.MatchingCells
{
    public class Mana : MatchingCell
    {
        [SerializeField] private int amount;

        protected override void Use()
        {
            TurningUnit.mana.Refill(amount);
        }

        public override bool IsSameType(Cell second)
        {
            return second is Mana;
        }
    }
}