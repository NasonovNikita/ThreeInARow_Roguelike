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

            unitBelong.mana.Waste(useCost);
            LogUsage();
            unitBelong.AddMod(new Modifier(-1, ModType.Irritated, value: value));
            new IrritationEvent((int) value, unitBelong);
        }

        public override string Title => titleKeyRef.Value;

        public override string Description => string.Format(descriptionKeyRef.Value, (int)value);
    }
}