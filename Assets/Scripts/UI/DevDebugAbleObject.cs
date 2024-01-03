using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class DevDebugAbleObject : MonoBehaviour, IPointerClickHandler
    {
        public string text;

        private void OnPress()
        {
            DevDebugWindow.instance.Write(text);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right) OnPress();
        }
    }
}