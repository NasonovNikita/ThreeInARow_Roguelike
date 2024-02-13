using System.Collections;
using Battle.Modifiers;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "Hypnosis", menuName = "Spells/Hypnosis")]
    public class Hypnosis : Spell
    {
        public override IEnumerator Cast()
        {
            if (CantCastOrCast()) yield break;

            manager.target.AddMod(new Modifier(count, ModType.Blind));

            yield return Wait();
        }

        public override string Description => descriptionKeyRef.Value;
    }
}