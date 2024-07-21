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
    public abstract class Node : MonoBehaviour, IPointerClickHandler
    {
        [FormerlySerializedAs("prefab")] [SerializeField]
        private Edge edgePrefab;

        [SerializeField] private ObjectScaler scaler;

        public List<Node> next;

        private readonly List<Edge> edges = new();

        protected int layer;
        protected int seed;

        public void OnEnable()
        {
            foreach (Node node in next)
            {
                Edge edge = Instantiate(edgePrefab);
                edge.Draw(transform.position, node.transform.position);
                edges.Add(edge);
            }
        }

        public void OnDisable()
        {
            for (var i = 0; i < edges.Count; i++)
            {
                Edge edge = edges[i];
                if (edge == null) return;
                edges.Remove(edge);
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
            Node node = Instantiate(prefab);
            node.layer = layer;
            node.seed = randomSeed;
            GameObject gameObject = node.gameObject;
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
            StartCoroutine(scaler.UnScale());
        }

        public bool BelongsToNext(Node other)
        {
            return next.Contains(other);
        }

        private IEnumerator Arrive()
        {
            yield return StartCoroutine(scaler.ScaleUp());
            OnArrive?.Invoke(this);

            Action();
        }

        protected abstract void Action();
    }
}