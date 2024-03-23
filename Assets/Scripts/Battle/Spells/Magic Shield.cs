using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "MagicShield", menuName = "Spells/MagicShield")]
    public class MagicShield : Spell
    {
        protected override void Action() =>
            unitBelong.AddHpMod(new MoveStatModifier(
                count,
                ModType.Add, // TODO on modType change
                ModClass.HpDamage,
                value: -value));

        public override string Description => string.Format(descriptionKeyRef.Value, Other.Tools.Percents(value));
    }
}