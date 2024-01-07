using System;
using System.Collections.Generic;
using Other;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Map.Vertexes
{
    public abstract class Vertex: MonoBehaviour, IPointerClickHandler
    {
        private Edge prefab;
    
        public List<Vertex> next;

        private Map map;

        private readonly List<Edge> edges = new();

        public void Awake()
        {
            prefab = Resources.Load<Edge>("Prefabs/Map/Edge");
        }

        public void OnEnable()
        {
            map = FindFirstObjectByType<Map>();
            foreach (Vertex vertex in next)
            {
                Edge edge = Instantiate(prefab);
                edge.Draw(transform.position, vertex.transform.position);
                edges.Add(edge);
            }
        }

        public void OnDisable()
        {
            for (var i = 0; i < edges.Count; i++)
            {
                var edge = edges[i];
                if (edge == null) return;
                edges.Remove(edge);
                Destroy(edge.gameObject);
            }
        }

        protected static Vertex Create(Vertex prefab)
        {
            Vertex vertex = Instantiate(prefab);
            GameObject gameObject = vertex.gameObject;
            DontDestroyOnLoad(gameObject);
            gameObject.SetActive(false);

            return vertex;
        }

        public bool BelongsToNext(Vertex vertex)
        {
            return next.Contains(vertex);
        }

        public abstract void OnArrive();

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left) map.OnClick(this);
        }
    
        // ReSharper disable Unity.PerformanceAnalysis
        public void ScaleUp(Vector3 endScale, float time, Action onEnd = null)
        {
            ObjectScaler scaler = GetComponent<ObjectScaler>();
            scaler.StartScale(endScale, time, onEnd);
        }
    
        public void ScaleDown(Vector3 endScale, float time)
        {
            ObjectScaler scaler = GetComponent<ObjectScaler>();
            scaler.StartScale(endScale, time);
        }
    }
}