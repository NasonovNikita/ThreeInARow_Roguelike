using System.Collections;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "ArrowOfLight", menuName = "Spells/ArrowOfLight")]
    public class ArrowOfLight : Spell
    {
        public override IEnumerator Cast()
        {
            if (CantCastOrCast()) yield break;
            //PToEDamageLog.Log(manager.target, manager.player, new Damage(lDmg: (int) value));
            manager.target.TakeDamage(new Damage(lDmg: (int) value));
            
            
            yield return Wait();
        }

        public override string Description => string.Format(descriptionKeyRef.Value, (int) value);
    }
}