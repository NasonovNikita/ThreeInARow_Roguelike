using Battle;
using Battle.Units;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Battle
{
    public class Picker : MonoBehaviour, IPointerClickHandler
    {
        public Enemy enemy;
        
        private PickerManager pickerManager;

        public void Awake()
        {
            pickerManager = FindFirstObjectByType<PickerManager>();
            enemy.OnDied += () => pickerManager.OnPickerDestroyed(this);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            pickerManager.Pick(this);
        }
    }
}