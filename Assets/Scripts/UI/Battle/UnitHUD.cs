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

        public static void Create(Unit unit, Stat stat, int value)
        {
            if (unit == null) return;
            UnitHUD hud = Tools.InstantiateUI(PrefabsContainer.instance.unitHUD);
            
            hud.transform.localPosition = unit.transform.localPosition;
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

        private void Move(int k)
        {
            mover.StartMovementBy(deltaMove * k, () => Destroy(gameObject));
        }
    }
}