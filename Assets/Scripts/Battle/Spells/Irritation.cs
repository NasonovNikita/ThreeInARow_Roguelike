using Battle.BattleEventHandlers;
using Battle.Modifiers;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "Irritation", menuName = "Spells/Irritation")]
    public class Irritation : Spell
    {
        protected override void Action()
        {
            unitBelong.AddMod(new Modifier(count, ModType.Irritated, value: value));
            new IrritationEvent((int) value, unitBelong);
        }

        public override string Description => string.Format(descriptionKeyRef.Value, (int)value);
    }
}