using System.Collections;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "Singe", menuName = "Spells/Singe")]
    public class Singe : Spell
    {
        public override IEnumerator Cast()
        {
            if (CantCastOrCast()) yield break;
            unitBelong.Target.TakeDamage(new Damage(fDmg: (int)value));
            
            yield return Wait();
        }

        public override string Description => throw new System.NotImplementedException();
    }
}