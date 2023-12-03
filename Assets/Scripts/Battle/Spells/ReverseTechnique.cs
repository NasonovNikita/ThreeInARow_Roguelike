using System.Collections.Generic;
using Battle.Modifiers;

namespace Battle.Spells
{
    public class ReverseTechnique : Spell
    {
        public override void Cast()
        {
            if (CantCast()) return;

            attachedUnit.mana -= useCost;
            attachedUnit.stateModifiers = new List<Modifier>();
        }
    }
}