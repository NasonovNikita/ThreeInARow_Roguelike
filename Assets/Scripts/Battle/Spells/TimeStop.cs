using System.Collections;
using System.Linq;
using Battle.Modifiers;
using Battle.Units;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "TimeStop", menuName = "Spells/TimeStop")]
    public class TimeStop : Spell
    {
        public override IEnumerator Cast()
        {
            if (CantCastOrCast()) yield break;
        
        
            foreach (Enemy enemy in manager.enemies.Where(v => v != null))
            {
                enemy.AddMod(new Modifier(count, ModType.Stun));
            }

            yield return Wait();
        }

        public override string Description => descriptionKeyRef.Value;
    }
}