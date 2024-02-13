using System.Collections;
using Battle.Modifiers;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "NatureShield", menuName = "Spells/NatureShield")]
    public class NatureShield : Spell
    {
        public override IEnumerator Cast()
        {
            if (CantCastOrCast()) yield break;
            unitBelong.AddHpMod(
                new Modifier(count, ModType.Add, ModClass.HpDamageBase, value: -value, delay: true));


            yield return Wait();
        }

        public override string Description => throw new System.NotImplementedException();
    }
}