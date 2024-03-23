using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "PoweredKick", menuName = "Spells/PoweredKick")]
    public class PoweredKick : Spell
    {
        protected override void Action()
        {
            unitBelong.AddMod(new MoveStatModifier(count, ModType.Stun));
            unitBelong.AddDamageMod(new MoveStatModifier(count + 1, ModType.Add,  // TODO on modType change
                ModClass.Damage, value: value));
        }

        public override string Description => string.Format(descriptionKeyRef.Value, count, 1 + value);
    }
}