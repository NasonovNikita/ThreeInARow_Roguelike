using System;
using Battle.Spells;
using Core.Singleton;
using Other;
using UnityEngine;

namespace UI.MessageWindows
{
    public class SpellGettingWarningWindow : MonoBehaviour
    {
        private Spell _gettingSpell;
        private Action _onYes;

        public static void Create(Spell spell, Action onYes = null)
        {
            var window =
                Tools.InstantiateUI(PrefabsContainer.Instance.spellGettingWarningWindow);
            window._gettingSpell = spell;
            window._onYes = onYes;
        }

        public void Yes()
        {
            _onYes?.Invoke();
            SpellReplacingWindow.Create(_gettingSpell);
            Close();
        }

        public void No()
        {
            Close();
        }

        private void Close()
        {
            Destroy(gameObject);
        }
    }
}