using System.Collections.Generic;
using System.Linq;
using Audio;
using Core;
using Core.Saves;
using Map.Vertexes;
using UnityEngine;
using UnityEngine.UI;

namespace Map
{
    public class Map : MonoBehaviour
    {
        private static List<Vertex> _vertexes = new();

        public static int currentVertex = -1;

        public Canvas canvas;

        public void Awake()
        {
            AudioManager.instance.StopAll();

            if (currentVertex == _vertexes.Count - 1)
            {
                Win();
            }
        
            GameSave.Save();
            
            InitVertexes();
        
            AudioManager.instance.Play(AudioEnum.Map);
        }

        public void OnDisable() => HideVertexes();

        public bool AllowedToArrive(Vertex vertex) => 
            currentVertex == -1 && _vertexes[0] == vertex ||
            currentVertex != -1 && _vertexes[currentVertex] == vertex;

        public void SetCurrentVertex(Vertex vertex)
        {
            currentVertex = _vertexes.FindIndex(v => v == vertex);
        }

        private void InitVertexes()
        {
            foreach (var vertex in _vertexes)
            {
                vertex.gameObject.SetActive(true);
            }
        }

        private void HideVertexes()
        {
            foreach (var vertex in _vertexes.Where(vertex => vertex != null))
            {
                vertex.gameObject.SetActive(false);
            }
        }

        public static void Regenerate()
        {
            foreach (var vertex in _vertexes.Where(v => v != null)) Destroy(vertex.gameObject);
            _vertexes = MapGenerator.instance.GetMap(Globals.instance.seed);
        }

        private void Win()
        {
            GameObject menu = Instantiate(PrefabsContainer.instance.winMessage, canvas.transform, false);
            var buttons = menu.GetComponentsInChildren<Button>();
            buttons[0].onClick.AddListener(GameManager.instance.NewGame);
            buttons[1].onClick.AddListener(GameManager.instance.MainMenu);
            menu.gameObject.SetActive(true);
        }
    }
}