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
                InfoObject objectWithInfo = btn.GetComponent<InfoObject>();
                objectWithInfo.text = spell.Description;
                objectWithInfo.actAfterTime = true;// btn must have this component
                Tools.InitButton(btn, () => StartCoroutine(spell.Cast()), spell.Title + " " + spell.useCost);
            }
        }
    }
}