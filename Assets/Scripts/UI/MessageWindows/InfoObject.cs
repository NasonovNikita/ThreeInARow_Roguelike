using Knot.Localization;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.MessageWindows
{
    public class InfoObject : PointerTracker, IPointerClickHandler
    {
        public string text;
        
        public KnotTextKeyReference keyReference;
        protected virtual string Text => text != "" ? text : keyReference.Value;

        private Vector3 Shift => InfoWindow.instance.WindowSize * 0.6f;

        private void ShowInfo() => InfoWindow.instance.Write(Text);

        private void CloseInfo()
        {
            InfoWindow.instance.Close();
        }

        private void PlaceInfo() => InfoWindow.instance.MoveTo(MousePosition, Shift);

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