using Battle.BattleEventHandlers;
using Battle.Modifiers;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "Irritation", menuName = "Spells/Irritation")]
    public class Irritation : Spell
    {
        public override void Cast()
        {
            if (CantCast()) return;

            attachedUnit.mana -= useCost;
            attachedUnit.stateModifiers.Add(new Modifier(count, ModType.Irritated, value: value));
            new IrritationEvent(value, attachedUnit);
        }
    }
}