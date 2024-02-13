using System.Collections;
using Battle.Modifiers;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "MagicShield", menuName = "Spells/MagicShield")]
    public class MagicShield : Spell
    {
        public override IEnumerator Cast()
        {
            if (CantCastOrCast()) yield break;
        
            manager.player.AddHpMod(new Modifier(count, ModType.Mul, ModClass.HpDamageBase, value: -value));

            yield return Wait();
        }

        public override string Description => string.Format(descriptionKeyRef.Value, Other.Tools.Percents(value));
    }
}