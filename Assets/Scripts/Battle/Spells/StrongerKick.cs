using System.Collections;
using Battle.Modifiers;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "StrongerKick", menuName = "Spells/StrongerKick")]
    public class StrongerKick : Spell
    {
        public override IEnumerator Cast()
        {
            if (CantCastOrCast()) yield break;
        
            manager.player.AddDamageMod(new Modifier(count, ModType.Mul, ModClass.DamageBase, value: value));

            yield return Wait();
        }

        public override string Description => string.Format(descriptionKeyRef.Value, Other.Tools.Percents(value));
    }
}