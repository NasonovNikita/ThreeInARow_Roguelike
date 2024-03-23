using Battle.Units;
using Battle.Units.Stats;
using Core;
using Other;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Battle
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] private ObjectMover mover;
        [SerializeField] private Vector3 deltaMove;
        public Text text;
        
        public void MoveUp() => Move(1);
        public void MoveDown() => Move(-1);
        public void Stay() => Move(0);

        private void Move(int direction) =>
            StartCoroutine(mover.MoveBy(deltaMove * direction, () => Destroy(gameObject)));
    }
}