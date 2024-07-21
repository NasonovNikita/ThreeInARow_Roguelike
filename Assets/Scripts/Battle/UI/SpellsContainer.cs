using Battle.Spells;
using Battle.Units;
using Other;
using UI.MessageWindows;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.UI
{
    public class SpellsContainer : MonoBehaviour
    {
        public void Start()
        {
            var player = FindFirstObjectByType<Player>();
            var spellButtons = GetComponentsInChildren<Button>();
            for (var i = 0; i < player.spells.Count && i < 4; i++)
            {
                Button button = spellButtons[i];
                Spell spell = player.spells[i];
                var objectWithInfo = button.GetComponent<InfoObject>();
                objectWithInfo.text = spell.Description;
                objectWithInfo.actAfterTime = true; // btn must have this component
                button.InitButton(() => StartCoroutine(spell.Cast()),
                    spell.Title + " " + spell.useCost);
            }
        }
    }
}