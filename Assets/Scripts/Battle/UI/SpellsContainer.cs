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
            var spellButtons = GetComponentsInChildren<Button>();
            for (var i = 0; i < Player.Instance.spells.Count && i < 4; i++)
            {
                InitButton(spellButtons[i], i);
                var i1 = i;
                Player.Instance.spells[i].OnChanged += () => InitButton(spellButtons[i1], i1);
            }
        }

        private void InitButton(Button btn, int index)
        {
            var spell = Player.Instance.spells[index];
            var objectWithInfo =
                btn.GetComponent<InfoObject>(); // btn MUST have this component
            objectWithInfo.text = spell.Description;
            objectWithInfo.actAfterTime = true;
            btn.InitButton(() => StartCoroutine(spell.PlayerCast()),
                spell.Title + " " + spell.UseCost);
        }
    }
}