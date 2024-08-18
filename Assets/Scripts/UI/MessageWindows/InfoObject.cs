using Knot.Localization;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.MessageWindows
{
    /// <summary>
    ///     Shows info text when pointer is on this object for some time
    ///     or after right click.
    /// </summary>
    public class InfoObject : PointerTracker, IPointerClickHandler
    {
        public string text;

        public KnotTextKeyReference keyReference;
        private string Text => text != "" ? text : keyReference.Value;

        private Vector3 Shift => InfoWindow.Instance.WindowSize * 0.6f;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Right) return;

            InitInfo();
        }

        private void ShowInfo()
        {
            InfoWindow.Instance.Write(Text);
        }

        private void CloseInfo()
        {
            InfoWindow.Instance.Close();
        }

        private void PlaceInfo()
        {
            InfoWindow.Instance.MoveTo(MousePosition, Shift);
        }

        private void InitInfo()
        {
            ShowInfo();
            PlaceInfo();
        }

        protected override void OnTimeSpent()
        {
            InitInfo();
        }

        protected override void OnExit()
        {
            CloseInfo();
        }

        protected override void WhileInside()
        {
            PlaceInfo();
        }
    }
}