using System;
using System.Collections.Generic;
using Battle.Spells;
using Battle.Units;
using Core.Singleton;
using Other;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MessageWindows
{
    public class SpellReplacingWindow : MonoBehaviour
    {
        [SerializeField] private List<Button> buttons;
        private Spell _gettingSpell;

        public void Start()
        {
            if (Player.Data.spells.Count != 4)
                throw new Exception(
                    "Player must have 4 spells if this window is created");

            for (var i = 0; i < 4; i++)
            {
                var index = i;
                buttons[i].InitButton(() => ReplaceSpell(index),
                    Player.Data.spells[i].Title);
            }
        }

        private void ReplaceSpell(int index)
        {
            Player.Data.spells[index] = _gettingSpell;
            Destroy(gameObject);
        }

        public static void Create(Spell spell)
        {
            var window =
                Tools.InstantiateUI(PrefabsContainer.Instance.spellReplacingWindow);
            window._gettingSpell = spell;
        }
    }
}