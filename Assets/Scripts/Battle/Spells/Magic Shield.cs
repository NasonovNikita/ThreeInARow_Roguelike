using Battle.Modifiers;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "MagicShield", menuName = "Spells/MagicShield")]
    public class MagicShield : Spell
    {
        protected override void Action() =>
            manager.player.AddHpMod(new Modifier(
                count,
                ModType.Mul,
                ModClass.HpDamageBase,
                value: -value));

        public override string Description => string.Format(descriptionKeyRef.Value, Other.Tools.Percents(value));
    }
}