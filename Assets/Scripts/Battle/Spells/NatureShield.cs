using Battle.Modifiers;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "NatureShield", menuName = "Spells/NatureShield")]
    public class NatureShield : Spell
    {
        public override void Cast()
        {
            if (CantCastOrCast()) return;

            unitBelong.AddHpMod(
                new Modifier(count, ModType.Add, ModClass.HpDamageBase, value: -value, delay: true));
        }

        public override string Title => throw new System.NotImplementedException();

        public override string Description => throw new System.NotImplementedException();
    }
}