using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace UI
{
    public abstract class PointerTracker : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private bool actAfterTime;
        [SerializeField] private float waitBeforeShowTime;
        private const float StandardWaitTime = 2f;
        private float TmeBeforeAct => waitBeforeShowTime != 0 ? waitBeforeShowTime : StandardWaitTime;

        
        private bool isInside;
        private float timeEnter;
        private float TimeIn => isInside ? Time.time - timeEnter : 0;

        private bool spentTime;
        
        private Camera mainCamera;
        protected Vector3 MousePosition
        {
            get
            {
                Vector3 position = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                float x = position.x;
                float y = position.y;
                return new Vector3(x, y, 0);
            }
        }

        private void Awake()
        {
            mainCamera = Camera.main;
            SceneManager.sceneLoaded += (_, _) => mainCamera = Camera.main;
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