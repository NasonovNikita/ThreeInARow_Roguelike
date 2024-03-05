using Battle.Modifiers;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "NatureShield", menuName = "Spells/NatureShield")]
    public class NatureShield : Spell
    {
        protected override void Action() => unitBelong.AddHpMod(new Modifier(
                count,
                ModType.Add,
                ModClass.HpDamageBase,
                value: -value,
                delay: true));
    }
}