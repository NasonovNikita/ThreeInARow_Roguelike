using System.Collections.Generic;
using System.Linq;
using Battle.Modifiers;

namespace Battle.Spells
{
    public class ReverseTechnique : Spell
    {
        public override void Cast()
        {
            if (CantCast()) return;

            attachedUnit.mana.Waste(useCost);
            foreach (Modifier mod in attachedUnit.allMods.Where(v => !v.IsPositive))
            {
                mod.TurnOff();
            }
        }
    }
}