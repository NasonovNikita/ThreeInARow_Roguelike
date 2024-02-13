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
    
        public ObjectMover mover;
        public ObjectScaler scaler;

        public bool EndedScale => !scaler.doScale;
        public bool EndedMove => !mover.doMove;

        public void Move(Vector2 endPos)
        {
            mover.StartMovementTo(endPos);
        }

        public void Scale(Vector3 endScale)
        {
            scaler.StartScale(endScale);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left) StartCoroutine(grid.OnClick(this));
        }
    }
}