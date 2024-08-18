using Core.Singleton;
using Other;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.UI.HUD
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] private ObjectMover mover;
        [SerializeField] private Text text;

        [SerializeField] private float moveLength;

        public static HUD Create(string content, Color color, Transform parentTransform)
        {
            var hud = Instantiate(PrefabsContainer.Instance.hud, parentTransform);

            hud.text.text = content;
            hud.text.color = color;


            return hud;
        }

        public void MoveUp()
        {
            Move(1);
        }

        public void MoveDown()
        {
            Move(-1);
        }

        public void Stay()
        {
            Move(-0.1f);
        }

        private void Move(float direction)
        {
            StartCoroutine(mover.MoveBy(
                MoveVector(direction),
                OnMoveEnd
            ));
        }

        private Vector3 MoveVector(float direction)
        {
            return Vector3.up * (moveLength * direction);
        }

        private void OnMoveEnd()
        {
            Destroy(gameObject);
        }
    }
}