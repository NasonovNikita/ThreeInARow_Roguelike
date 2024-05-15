using Battle.Modifiers.Statuses;
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
            unitBelong.target.hp.TakeDamage(dmg);
            unitBelong.Statuses.Add(new Burning(burningMoves));
            unitBelong.InvokeOnMadeHit();
        }
    }
}