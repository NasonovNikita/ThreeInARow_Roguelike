using UnityEngine;

namespace Battle.Match3.MatchingCells
{
    public class Shield : MatchingCell
    {
        [SerializeField] private int amount;

        protected override void Use()
        {
            TurningUnit.hp.AddDamageMod(new Modifiers.StatModifiers.Shield(amount));
        }

        public override bool IsSameType(Cell second)
        {
            return second is Shield;
        }
    }
}