using Battle.Units;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Battle.UI
{
    public class Picker : MonoBehaviour, IPointerClickHandler
    {
        public Enemy enemy;

        private PickerManager _pickerManager;

        public void Awake()
        {
            _pickerManager = FindFirstObjectByType<PickerManager>();
            enemy.OnDied += () => _pickerManager.OnPickerDestroyed(this);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _pickerManager.Pick(this);
        }
    }
}