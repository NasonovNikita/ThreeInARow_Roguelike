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

        private List<ModifierList<StatModifier>> AllStatModifierLists => new()
        {
            unit.hp.onHealingMods,
            unit.hp.onTakingDamageMods,
            unit.mana.refillingMods,
            unit.mana.wastingMods,
            unit.damage.mods
        };

        public void Start()
        {
            foreach (var modList in StatModifierLists)
                modList.OnModAdded += DrawMod;

            foreach (StatModifier mod in AllStatModifierLists.SelectMany(modList => modList.ModList))
                DrawMod(mod);

            foreach (Status mod in unit.Statuses.ModList)
                DrawMod(mod);
        }

        public void OnDestroy()
        {
            foreach (var modList in StatModifierLists) 
                modList.OnModAdded -= DrawMod;
        }

        private List<ModifierList<StatModifier>> StatModifierLists => new()
            {
                unit.hp.onTakingDamageMods,
                unit.hp.onHealingMods,
                unit.mana.refillingMods,
                unit.mana.wastingMods,
                unit.damage.mods
            };

        private void DrawMod(Modifier mod) => 
            grid.Add(mod);
    }
}