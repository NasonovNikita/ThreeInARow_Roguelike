using System;
using System.Collections.Generic;
using Battle.Spells;
using Battle.Units;
using Core;
using Core.SingletonContainers;
using Other;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MessageWindows
{
    public class SpellReplacingWindow : MonoBehaviour
    {
        private Spell gettingSpell;
        [SerializeField] private List<Button> buttons;

        public void Start()
        {
            if (Player.data.spells.Count != 4)
                throw new Exception("Player must have 4 spells if this window is created");

            for (int i = 0; i < 4; i++)
            {
                var index = i;
                Tools.InitButton(buttons[i], () => ReplaceSpell(index), Player.data.spells[i].Title);
            }
        }

        private void ReplaceSpell(int index)
        {
            Player.data.spells[index] = gettingSpell;
            Destroy(gameObject);
        }

        public static void Create(Spell spell)
        {
            var window = Tools.InstantiateUI(PrefabsContainer.instance.spellReplacingWindow);
            window.gettingSpell = spell;
        }
    }
}