using System.Linq;
using Battle.Modifiers;
using Battle.Units;
using UnityEngine;

namespace Battle.UI.ModsDisplaying
{
    public class ModIconController : MonoBehaviour
    {
        [SerializeField] private Unit unit;
        [SerializeField] private ModIconGrid grid;

        public void Start()
        {
            foreach (Modifier mod in unit.AllModifierLists.SelectMany(modList => modList.ModList))
                DrawMod((IModIconModifier)mod);

            foreach (ModifierList list in unit.AllModifierLists) list.OnModAdded += DrawMod;
        }

        public void OnDestroy()
        {
            foreach (ModifierList modList in unit.AllModifierLists)
                modList.OnModAdded -= DrawMod;
        }

        private void DrawMod(Modifier mod)
        {
            DrawMod((IModIconModifier)mod);
        }

        private void DrawMod(IModIconModifier mod)
        {
            grid.Add(mod);
        }
    }
}