using System.Collections;
using Battle.BattleEventHandlers;
using Battle.Modifiers;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "Irritation", menuName = "Spells/Irritation")]
    public class Irritation : Spell
    {
        public override IEnumerator Cast()
        {
            if (CantCastOrCast()) yield break;

            unitBelong.AddMod(new Modifier(count, ModType.Irritated, value: value));
            new IrritationEvent((int) value, unitBelong);

            yield return Wait();
        }

        public override string Description => string.Format(descriptionKeyRef.Value, (int)value);
    }
}