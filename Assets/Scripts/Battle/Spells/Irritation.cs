using Battle.BattleEventHandlers;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "Irritation", menuName = "Spells/Irritation")]
    public class Irritation : Spell
    {
        protected override void Action()
        {
            unitBelong.AddMod(new MoveStatModifier(count, ModType.Irritated, value: value));
            new IrritationEvent((int) value, unitBelong);
        }

        public override string Description => string.Format(descriptionKeyRef.Value, (int)value);
    }
}