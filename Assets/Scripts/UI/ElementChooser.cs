using System.Collections.Generic;
using Battle;
using Battle.Units;
using UI.MessageWindows;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class ElementChooser : MonoBehaviour, IPointerClickHandler
    {
        private const int WastingMana = 30;
        [SerializeField] private DmgType element;

        private static readonly Dictionary<DmgType, string> elementsDescriptions = new Dictionary<DmgType, string>()
        {
            { DmgType.Fire, "Fire deals 20 dmg per turn\n(1 next move)\nEnemy gets *1.25 dmg\n(2 moves including current)"},
            { DmgType.Cold, "Freezing enemies have 50% chance to miss\nand deal 25% less dmg"},
            { DmgType.Poison, "Poison deals 15 dmg per turn\n(1 next move)\nThey lose 1 positive mod (if exist)"},
            { DmgType.Light, "Light heals you for 5 hp per gem"}
        };


        public void Choose()
        {
            if (Player.data.mana < WastingMana || Player.data.chosenElement == element) return;
            Player.data.mana.Waste(WastingMana);
            Player.data.chosenElement = element;
        }


        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                DevDebugWindow.instance.Write(
                    $"Changing element costs {WastingMana} of mana\n{elementsDescriptions[element]}\nEach elements works if you use appropriate gem!"
                    );
            }
        }
    }
}