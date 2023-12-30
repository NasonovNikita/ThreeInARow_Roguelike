using Other;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Battle.Match3
{
    public class Gem : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private GemType type;
        public GemType Type => type;

        public Grid grid;
    
        private ObjectMover _mover;
        private ObjectScaler _scaler;

        private void Awake()
        {
            _mover = GetComponent<ObjectMover>();
            _mover.doMove = false;
        
            _scaler = GetComponent<ObjectScaler>();
            _scaler.doScale = false;
        }

        public void Move(Vector2 endPos, float time)
        {
            _mover.StartMovement(endPos, time);
        }

        public void Scale(Vector3 endScale, float time)
        {
            _scaler.StartScale(endScale, time);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            StartCoroutine(grid.OnClick(this));
        }
    }
}