using Battle.Units.StatModifiers;
using Battle.Units.Statuses;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "PoweredKick", menuName = "Spells/PoweredKick")]
    public class PoweredKick : Spell
    {
        [SerializeField] private int damage;
        [SerializeField] private int damageModMoves;
        [SerializeField] private int stunMoves;

        public override string Description =>
            string.Format(descriptionKeyRef.Value, stunMoves, damageModMoves);

        protected override void Action()
        {
            UnitBelong.damage.mods.Add(new DamageMoveMod(damage, damageModMoves));
            UnitBelong.Statuses.Add(new Stun(stunMoves));
        }
    }
}