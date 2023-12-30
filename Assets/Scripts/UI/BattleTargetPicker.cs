using Battle;
using Battle.Units;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class BattleTargetPicker : MonoBehaviour, IPointerClickHandler
    {
        private static int pickedEnemyIndex = -1;
        private static BattleManager manager;

        private Enemy enemy;
        
        public static void TurnOn()
        {
            manager = FindFirstObjectByType<BattleManager>();
            Pick(0);
        }

        public void Awake()
        {
            enemy = GetComponentInParent<Enemy>();
        }

        private static void Pick(int index)
        {
            if (pickedEnemyIndex == index) return;
            
            pickedEnemyIndex = index;
            manager.target = manager.enemies[index];
            
            // TODO Drawing pick
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            int index = manager.enemies.FindIndex(v => v == enemy);
            Pick(index);
        }
    }
}