using UnityEngine;

namespace Map.Nodes
{
    public class Edge : MonoBehaviour
    {
        private LineRenderer line;

        public void Awake()
        {
            line = GetComponent<LineRenderer>();
        }

        public void Draw(Vector3 pos1, Vector3 pos2)
        {
            line.SetPosition(0, pos1);
            line.SetPosition(1, pos2);
        }
    }
}