using System.Collections;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "Kick", menuName = "Spells/Kick")]
    public class Kick : Spell
    {
        public override IEnumerator Cast()
        {
            if (CantCastOrCast()) yield break;
        
            //PToEDamageLog.Log(manager.target, manager.player, new Damage(mDmg: (int) value));
            manager.target.TakeDamage(new Damage(mDmg: (int) value));

            yield return Wait();
        }

        public override string Description => string.Format(descriptionKeyRef.Value, value);
    }
}