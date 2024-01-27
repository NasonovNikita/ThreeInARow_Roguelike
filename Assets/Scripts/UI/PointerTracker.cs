using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public abstract class PointerTracker : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] public bool actAfterTime;
        [SerializeField] private float waitBeforeShowTime;
        private const float StandardWaitTime = 2f;
        private float TmeBeforeAct => waitBeforeShowTime != 0 ? waitBeforeShowTime : StandardWaitTime;

        
        private bool isInside;
        private float timeEnter;
        private float TimeIn => isInside ? Time.time - timeEnter : 0;

        private bool spentTime;
        
        private Camera MainCamera => Camera.main;
        protected Vector3 MousePosition
        {
            get
            {
                Vector3 position = MainCamera.ScreenToWorldPoint(Input.mousePosition);
                float x = position.x;
                float y = position.y;
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

        protected virtual void OnEnter() {}

        protected virtual void OnTimeSpent() {}
        protected virtual void WhileInside() {}
        protected virtual void OnExit() {}
    }
}