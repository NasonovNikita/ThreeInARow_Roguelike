using System.Linq;
using Battle.Modifiers;
using Battle.Units;
using UnityEngine;

namespace Battle.UI.ModsDisplaying
{
    /// <summary>
    ///     Seeks for new unit's Modifiers and draws them in <see cref="ModIconGrid"/>.
    /// </summary>
    public class ModIconController : MonoBehaviour
    {
        [SerializeField] private Unit unit;
        [SerializeField] private ModIconGrid grid;

        public void Start()
        {
            foreach (var mod in unit.AllModifierLists.SelectMany(modList => modList.List))
                DrawMod((IModIconModifier)mod);

            foreach (var list in unit.AllModifierLists) list.OnModAdded += DrawMod;
        }

        public void OnDestroy()
        {
            foreach (var modList in unit.AllModifierLists)
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