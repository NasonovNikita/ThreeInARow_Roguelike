using System;
using System.Collections;
using System.Collections.Generic;
using Map.Nodes.Managers;
using Other;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Map.Nodes
{
    /// <summary>
    ///     You can <i>arrive to node</i> which means you enter some room.
    ///     At this time the node is scaled to indicate it as <i>chosen</i>.
    /// </summary>
    public abstract class Node : MonoBehaviour, IPointerClickHandler
    {
        [FormerlySerializedAs("prefab")] [SerializeField]
        private Edge edgePrefab;

        [SerializeField] private ObjectScaler scaler;

        /// <summary>
        ///     Nodes where you can <see cref="Arrive">arrive</see> to from this one.
        /// </summary>
        public List<Node> next;

        private readonly List<Edge> _edges = new();

        protected int Layer;
        protected int Seed;

        public void OnEnable()
        {
            foreach (var node in next)
            {
                var edge = Instantiate(edgePrefab);
                edge.Draw(transform.position, node.transform.position);
                _edges.Add(edge);
            }
        }

        public void OnDisable()
        {
            for (var i = 0; i < _edges.Count; i++)
            {
                var edge = _edges[i];
                if (edge == null) return;
                _edges.Remove(edge);
                Destroy(edge.gameObject);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClicked?.Invoke(this);

            if (eventData.button != PointerEventData.InputButton.Left ||
                !NodeController.Instance.TryArrive(this)) return;

            StartCoroutine(Arrive());
        }

        public static event Action<Node> OnArrive;
        public static event Action<Node> OnClicked;

        protected static Node Create(Node prefab, int layer, int randomSeed)
        {
            var node = Instantiate(prefab);
            node.Layer = layer;
            node.Seed = randomSeed;
            var gameObject = node.gameObject;
            DontDestroyOnLoad(gameObject);
            gameObject.SetActive(false);

            return node;
        }

        public void Choose()
        {
            StartCoroutine(scaler.ScaleUp());
        }

        public void UnChoose()
        {
            StartCoroutine(scaler.Unscale());
        }

        public bool BelongsToNext(Node other) => next.Contains(other);

        private IEnumerator Arrive()
        {
            yield return StartCoroutine(scaler.ScaleUp());
            OnArrive?.Invoke(this);

            Action();
        }

        protected abstract void Action();
    }
}