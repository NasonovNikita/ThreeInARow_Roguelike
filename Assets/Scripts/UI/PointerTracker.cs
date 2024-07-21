using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public abstract class PointerTracker : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private const float StandardWaitTime = 2f;
        [SerializeField] public bool actAfterTime;
        [SerializeField] private float waitBeforeShowTime;


        private bool isInside;

        private bool spentTime;
        private float timeEnter;

        private float TmeBeforeAct =>
            waitBeforeShowTime != 0 ? waitBeforeShowTime : StandardWaitTime;

        private float TimeIn => isInside ? Time.time - timeEnter : 0;

        private Camera MainCamera => Camera.main;

        protected Vector3 MousePosition
        {
            get
            {
                Vector3 position = MainCamera.ScreenToWorldPoint(Input.mousePosition);
                var x = position.x;
                var y = position.y;
                return new Vector3(x, y, 0);
            }
        }

        public void Update()
        {
            if (actAfterTime && !spentTime && TimeIn > TmeBeforeAct)
            {
                OnTimeSpent();
                spentTime = true;
            }

            if (isInside) WhileInside();
        }

        public void OnPointerEnter(PointerEventData ignored)
        {
            timeEnter = Time.time;
            isInside = true;

            OnEnter();
        }

        public void OnPointerExit(PointerEventData ignored)
        {
            isInside = false;
            spentTime = false;

            OnExit();
        }

        protected virtual void OnEnter()
        {
        }

        protected virtual void OnTimeSpent()
        {
        }

        protected virtual void WhileInside()
        {
        }

        protected virtual void OnExit()
        {
        }
    }
}