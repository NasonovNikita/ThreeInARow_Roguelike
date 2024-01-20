using System.Linq;
using Battle.Modifiers;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "ReverseTechnique", menuName = "Spells/ReverseTechnique")]
    public class ReverseTechnique : Spell
    {
        public override void Cast()
        {
            if (CantCast()) return;

            unitBelong.mana.Waste(useCost);
            LogUsage();
            foreach (Modifier mod in unitBelong.allMods.Where(v => !v.isPositive))
            {
                mod.TurnOff();
            }
        }

        public override string Title => "Reverse Technique";

        public override string Description => "Delete all your negative mods";
    }
}