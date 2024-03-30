using UnityEngine;

namespace Battle.Match3.MatchingCells
{
    public class Attack : MatchingCell
    {
        [SerializeField] private int baseAttackVal;

        protected override void Use()
        {
            TurningUnit.target.TakeDamage(TurningUnit.damage.ApplyDamage(baseAttackVal));
            TurningUnit.MakeHit();
        }

        public override bool IsSameType(Cell second) =>
            second is Attack;
    }
}