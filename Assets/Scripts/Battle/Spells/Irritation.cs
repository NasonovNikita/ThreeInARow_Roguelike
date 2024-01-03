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

            attachedUnit.mana.Waste(useCost);
            LogUsage();
            attachedUnit.AddMod(new Modifier(-1, ModType.Irritated, value: value));
            new IrritationEvent(value, attachedUnit);
        }
    }
}