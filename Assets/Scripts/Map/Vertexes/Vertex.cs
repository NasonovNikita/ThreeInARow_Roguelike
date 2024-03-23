using System;
using System.Collections.Generic;
using Other;
using UI.MessageWindows;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

namespace Map.Vertexes
{
    public abstract class Vertex: MonoBehaviour, IPointerClickHandler
    {
        public ObjectScaler scaler;
        
        [SerializeField] private Edge prefab;
    
        public List<Vertex> next;

        private Map map;

        protected MapGenerator generator;

        private readonly List<Edge> edges = new();

        protected int layer;

        private int randomSeed;

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

        protected static Vertex Create(Vertex prefab, int layer, int randomSeed)
        {
            Vertex vertex = Instantiate(prefab);
            vertex.layer = layer;
            vertex.randomSeed = randomSeed;
            vertex.generator = FindFirstObjectByType<MapGenerator>();
            GameObject gameObject = vertex.gameObject;
            DontDestroyOnLoad(gameObject);
            gameObject.SetActive(false);

            return vertex;
        }

        protected void SetRandom()
        {
            Random.InitState(randomSeed);
        }

        protected void ResetRandom()
        {
            Tools.Random.ResetRandom();
        }

        public abstract void OnArrive();

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left) return;
            
            
        }
    }
}