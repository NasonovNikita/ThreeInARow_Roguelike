using Battle.Modifiers.StatModifiers;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "NatureShield", menuName = "Spells/NatureShield")]
    public class NatureShield : Spell
    {
        [SerializeField] private int protectionAmount;
        [SerializeField] private int moves;

        protected override void Action() =>
            unitBelong.hp.onTakingDamageMods.Add(new DamageMoveMod(protectionAmount, moves));
    }
}