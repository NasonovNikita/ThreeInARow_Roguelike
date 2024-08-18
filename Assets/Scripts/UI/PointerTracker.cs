using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    /// <summary>
    ///     Tracks if pointer is on an object and does some action after time.
    /// </summary>
    public abstract class PointerTracker : MonoBehaviour, IPointerEnterHandler,
        IPointerExitHandler
    {
        private const float StandardWaitTime = 2f;
        [SerializeField] public bool actAfterTime;
        [SerializeField] private float waitBeforeShowTime;


        private bool _isInside;

        private bool _spentTime;
        private float _timeEnter;

        private float TmeBeforeAct =>
            waitBeforeShowTime != 0 ? waitBeforeShowTime : StandardWaitTime;

        private float TimeIn => _isInside ? Time.time - _timeEnter : 0;

        private Camera MainCamera => Camera.main;

        protected Vector3 MousePosition
        {
            get
            {
                var position = MainCamera.ScreenToWorldPoint(Input.mousePosition);
                var x = position.x;
                var y = position.y;
                return new Vector3(x, y, 0);
            }
        }

        public void Update()
        {
            if (actAfterTime && !_spentTime && TimeIn > TmeBeforeAct)
            {
                OnTimeSpent();
                _spentTime = true;
            }

            if (_isInside) WhileInside();
        }

        public void OnPointerEnter(PointerEventData ignored)
        {
            _timeEnter = Time.time;
            _isInside = true;

            OnEnter();
        }

        public void OnPointerExit(PointerEventData ignored)
        {
            _isInside = false;
            _spentTime = false;

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