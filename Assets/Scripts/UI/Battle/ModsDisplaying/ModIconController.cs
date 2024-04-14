using System.Collections.Generic;
using Battle.Modifiers;
using Battle.Modifiers.StatModifiers;
using Battle.Units;
using UnityEngine;

namespace UI.Battle.ModsDisplaying
{
    public class ModIconController : MonoBehaviour
    {
        [SerializeField] private Unit unit;
        [SerializeField] private ModIconGrid grid;

        public void Start()
        {
            foreach (var modList in StatModifierLists)
            {
                modList.OnModAdded += OnModifierAdded;
            }
        }

        private List<ModifierList<StatModifier>> StatModifierLists => new()
            {
                unit.hp.onTakingDamageMods,
                unit.hp.onHealingMods,
                unit.mana.refillingMods,
                unit.mana.wastingMods,
                unit.damage.mods
            };

        private void OnModifierAdded(Modifier mod) => 
            grid.Add(mod);
    }
}