using System;
using Battle.Spells;
using Core;
using Core.SingletonContainers;
using Other;
using UnityEngine;

namespace UI.MessageWindows
{
    public class SpellGettingWarningWindow : MonoBehaviour
    {
        private Spell gettingSpell;
        private Action onYes;
        
        public static void Create(Spell spell, Action onYes = null)
        {
            var window = Tools.InstantiateUI(PrefabsContainer.instance.spellGettingWarningWindow);
            window.gettingSpell = spell;
            window.onYes = onYes;
        }
        
        public void Yes()
        {
            onYes?.Invoke();
            SpellReplacingWindow.Create(gettingSpell);
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