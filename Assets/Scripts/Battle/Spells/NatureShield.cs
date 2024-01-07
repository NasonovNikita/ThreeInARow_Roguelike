using Battle.Modifiers;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "NatureShield", menuName = "Spells/NatureShield")]
    public class NatureShield : Spell
    {
        public override void Cast()
        {
            if (CantCast()) return;

            unitBelong.mana.Waste(useCost);
            LogUsage();
            unitBelong.AddHpMod(new Modifier(count, ModType.Add, ModClass.DamageBase, value: -value));
        }
    }
}