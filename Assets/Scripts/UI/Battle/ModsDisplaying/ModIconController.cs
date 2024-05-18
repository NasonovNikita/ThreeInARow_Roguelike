using System.Collections.Generic;
using System.Linq;
using Battle.Modifiers;
using Battle.Modifiers.StatModifiers;
using Battle.Modifiers.Statuses;
using Battle.Units;
using UnityEngine;

namespace UI.Battle.ModsDisplaying
{
    public class ModIconController : MonoBehaviour
    {
        [SerializeField] private Unit unit;
        [SerializeField] private ModIconGrid grid;

        private List<ModifierList> AllModifierLists => new()
        {
            unit.hp.onHealingMods,
            unit.hp.onTakingDamageMods,
            unit.mana.refillingMods,
            unit.mana.wastingMods,
            unit.damage.mods,
            unit.Statuses
        };

        public void Start()
        {
            foreach (Modifier mod in AllModifierLists.SelectMany(modList => modList.ModList))
                DrawMod(mod);

            foreach (ModifierList list in AllModifierLists)
            {
                list.OnModAdded += DrawMod;
            }
        }

        public void OnDestroy()
        {
            foreach (ModifierList modList in AllModifierLists) 
                modList.OnModAdded -= DrawMod;
        }

        private void DrawMod(Modifier mod) => 
            grid.Add(mod);
    }
}