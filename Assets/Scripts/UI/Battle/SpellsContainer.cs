using Battle.Units;
using Other;
using UI.MessageWindows;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Battle
{
    public class SpellsContainer : MonoBehaviour
    {
        public void Start()
        {
            Player player = FindFirstObjectByType<Player>();
            var spellButtons = GetComponentsInChildren<Button>();
            for (int i = 0; i < player.spells.Count && i < 4; i++)
            {
                Button btn = spellButtons[i];
                var spell = player.spells[i];
                btn.GetComponent<ObjectWithInfo>().text = spell.Description; // btn must have this component
                Tools.InitButton(btn, spell.Cast, spell.Title + " " + spell.useCost);
            }
        }
    }
}