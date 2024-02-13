using Battle.Units;
using Battle.Units.Stats;
using Core;
using Other;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Battle
{
    public class UnitHUD : MonoBehaviour
    {
        [SerializeField] private Text text;
        [SerializeField] private ObjectMover mover;
        [SerializeField] private Vector2 deltaMove;

        [SerializeField] private Color hpLoseColor;
        [SerializeField] private Color hpGetColor;

        [SerializeField] private Color manaColor;

        [SerializeField] private Color defaultColor;

        public static void CreateStatChangeHud(Unit unit, Stat stat, int value)
        {
            if (unit == null) return;
            UnitHUD hud = CreateBaseHud(unit);
            hud.text.text = value.ToString();
            
            hud.text.color = stat switch
            {
                Hp when value >= 0 => hud.hpGetColor,
                Hp => hud.hpLoseColor,
                Mana => hud.manaColor,
                _ => hud.text.color
            };
            
            hud.Move(value switch
            {
                > 0 => 1,
                0 => 0,
                < 0 => -1
            });
        }

        public static void CreateStringHud(Unit unit, string content, bool isPositiveMessage)
        {
            if (unit == null) return;
            UnitHUD hud = CreateBaseHud(unit);
            hud.text.text = content;

            hud.text.color = hud.defaultColor;
            
            hud.Move(isPositiveMessage ? 1 : -1);
        }

        private static UnitHUD CreateBaseHud(Unit unit)
        {
            UnitHUD hud = Tools.InstantiateUI(PrefabsContainer.instance.unitHUD);
            
            hud.transform.localPosition = unit.transform.localPosition;

            return hud;
        }

        private void Move(int direction)
        {
            mover.StartMovementBy(deltaMove * direction, () => Destroy(gameObject));
        }
    }
}