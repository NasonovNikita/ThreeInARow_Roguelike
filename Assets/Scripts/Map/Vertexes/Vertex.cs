using System.Collections.Generic;
using Other;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Map.Vertexes
{
    public class Vertex: MonoBehaviour, IPointerClickHandler
    {
        private Edge _prefab;
    
        public List<Vertex> next;

        private MapManager _map;

        public void Awake()
        {
            _prefab = Resources.Load<Edge>("Prefabs/Map/Edge");
            _map = FindFirstObjectByType<MapManager>();
        }

        public void Start()
        {
            foreach (Vertex vertex in next)
            {
                Edge edge = Instantiate(_prefab);
                edge.Draw(transform.position, vertex.transform.position);
            }
        }

        public bool BelongsToNext(Vertex vertex)
        {
            return next.Contains(vertex);
        }

        protected virtual void OnArrive()
        {
        
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _map.OnClick(this);
        }
    
        // ReSharper disable Unity.PerformanceAnalysis
        public void ScaleUp(Vector3 endScale, float time)
        {
            ObjectScaler scaler = GetComponent<ObjectScaler>();
            scaler.StartScale(endScale, time, OnArrive);
        }
    
        public void ScaleDown(Vector3 endScale, float time)
        {
            ObjectScaler scaler = GetComponent<ObjectScaler>();
            scaler.StartScale(endScale, time);
        }
    }
}