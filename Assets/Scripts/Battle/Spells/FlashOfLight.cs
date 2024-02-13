using System.Collections;
using System.Linq;
using Battle.Modifiers;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "FlashOfLight", menuName = "Spells/FlashOfLight")]
    public class FlashOfLight : Spell
    {
        public override IEnumerator Cast()
        {
            if (CantCastOrCast()) yield break;

            foreach (var enemy in manager.enemies.Where(v => v != null))
            {
                enemy.AddMod(new Modifier(count, ModType.Blind));
            }

            yield return Wait();
        }

        public override string Description => descriptionKeyRef.Value;
    }
}