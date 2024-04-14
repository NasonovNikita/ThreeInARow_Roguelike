using Battle.Modifiers.StatModifiers;
using Battle.Modifiers.Statuses;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "PoweredKick", menuName = "Spells/PoweredKick")]
    public class PoweredKick : Spell
    {
        [SerializeField] private int damage;
        [SerializeField] private int damageModMoves;
        [SerializeField] private int stunMoves;
        
        protected override void Action()
        {
            unitBelong.damage.mods.Add(new DamageMoveMod(damage, damageModMoves));
            unitBelong.AddStatus(new Stun(stunMoves));
        }

        public override string Description =>
            string.Format(descriptionKeyRef.Value,stunMoves, damageModMoves);
    }
}