using Battle.Units;
using UnityEngine;

namespace Battle.Match3.MatchingCells
{
    public class Attack : MatchingCell
    {
        [SerializeField] private int baseAttackVal;

        protected override void Use()
        {
            Unit unit = TurningUnit;
            
            unit.target.TakeDamage(TurningUnit.damage.ApplyDamage(baseAttackVal));
            unit.InvokeOnMadeHit();
        }

        public override bool IsSameType(Cell second) =>
            second is Attack;
    }
}