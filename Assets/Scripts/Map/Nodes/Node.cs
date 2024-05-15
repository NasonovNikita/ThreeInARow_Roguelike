using System;
using System.Collections;
using System.Collections.Generic;
using Map.Nodes.Managers;
using Other;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

namespace Map.Nodes
{
    public abstract class Node: MonoBehaviour, IPointerClickHandler
    {
        public ObjectScaler scaler;
        
        [SerializeField] private Edge prefab;
    
        public List<Node> next;

        private readonly List<Edge> edges = new();

        protected int layer;

        private int randomSeed;

        public static event Action<Node> OnArrive;
        public static event Action<Node> OnClicked; 

        public void OnEnable()
        {
            foreach (Node node in next)
            {
                Edge edge = Instantiate(prefab);
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

        protected static Node Create(Node prefab, int layer, int randomSeed)
        {
            Node node = Instantiate(prefab);
            node.layer = layer;
            node.randomSeed = randomSeed;
            GameObject gameObject = node.gameObject;
            DontDestroyOnLoad(gameObject);
            gameObject.SetActive(false);

            return node;
        }

        public bool BelongsToNext(Node other) => next.Contains(other);

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClicked?.Invoke(this);
            
            if (eventData.button != PointerEventData.InputButton.Left ||
                !NodeController.Instance.TryArrive(this)) return;

            StartCoroutine(Arrive());
        }

        private IEnumerator Arrive()
        {
            yield return StartCoroutine(scaler.ScaleUp());
            OnArrive?.Invoke(this);
            
            Action();
        }

        protected abstract void Action();

        protected void SetNodeRandom() => Random.InitState(randomSeed);

        protected void ResetRandom() => Tools.Random.ResetRandom();
    }
}