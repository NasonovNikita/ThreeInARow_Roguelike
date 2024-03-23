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

        protected override bool IsSameType(MatchingCell second)
        {
            return second is Mana;
        }
    }
}