using Battle.Modifiers.StatModifiers;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "MagicShield", menuName = "Spells/MagicShield")]
    public class MagicShield : Spell
    {
        [SerializeField] private int protectionAmount;

        protected override void Action() => unitBelong.hp.onTakingDamageMods.Add(new HpDamageConstMod(-protectionAmount));

        public override string Description =>
            string.Format(descriptionKeyRef.Value, protectionAmount);
    }
}