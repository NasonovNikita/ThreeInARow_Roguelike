using UnityEngine;

namespace Battle.Match3.MatchingCells
{
    public class Attack : MatchingCell
    {
        [SerializeField] private int baseAttackVal;

        protected override void Use()
        {
            TurningUnit.target.TakeDamage(TurningUnit.damage.ApplyDamage(baseAttackVal));
        }

        protected override bool IsSameType(MatchingCell second)
        {
            return second is Attack;
        }
    }
}