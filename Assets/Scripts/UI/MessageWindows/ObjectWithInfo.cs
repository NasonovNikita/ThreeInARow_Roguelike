using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.MessageWindows
{
    public class ObjectWithInfo : PointerTracker, IPointerClickHandler
    {
        public string text;

        private Vector3 Shift => DevDebugWindow.instance.WindowSize;

        private void ShowInfo() => DevDebugWindow.instance.Write(text);

        private void CloseInfo()
        {
            DevDebugWindow.instance.Close();
        }

        private void PlaceInfo() => DevDebugWindow.instance.MoveTo(MousePosition, Shift);

        private void InitInfo()
        {
            ShowInfo();
            PlaceInfo();
        }

        protected override void OnTimeSpent() => InitInfo();

        protected override void OnExit() => CloseInfo();

        protected override void WhileInside() => PlaceInfo();

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Right) return;
            
            InitInfo();
        }
    }
}