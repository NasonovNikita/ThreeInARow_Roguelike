using UnityEngine;

namespace Battle.Match3.MatchingCells
{
    public class Shield : MatchingCell
    {
        [SerializeField] private int amount;

        protected override void Use()
        {
            TurningUnit.hp.AddDamageMod(new Units.Modifiers.StatModifiers.Shield(amount));
        }

        protected override bool IsSameType(MatchingCell second)
        {
            return second is Shield;
        }
    }
}