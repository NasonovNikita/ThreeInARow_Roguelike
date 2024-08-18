using Battle.Units.Statuses;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(menuName = "Spells/Fireball")]
    public class Fireball : Spell
    {
        [SerializeField] private int dmg;
        [SerializeField] private int burningMoves;

        protected override void Action()
        {
            UnitBelong.target.TakeDamage(dmg);
            UnitBelong.target.Statuses.Add(new Burning(burningMoves));
            UnitBelong.InvokeOnMadeHit();
        }
    }
}