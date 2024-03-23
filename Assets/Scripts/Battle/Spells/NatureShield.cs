using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "NatureShield", menuName = "Spells/NatureShield")]
    public class NatureShield : Spell
    {
        protected override void Action() => unitBelong.AddHpMod(new MoveStatModifier(
                count,
                ModType.Add,
                ModClass.HpDamage,
                value: -value,
                delay: true));
    }
}