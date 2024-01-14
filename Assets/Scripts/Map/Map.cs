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
        public static List<Vertex> vertexes = new();

        public static int currentVertex = -1;

        public Vector3 baseScale;
        public Vector3 chosenScale;
        public float timeScale;

        public Canvas canvas;

        public void Awake()
        {
            AudioManager.instance.StopAll();

            if (currentVertex == vertexes.Count - 1)
            {
                Win();
            }

            if (currentVertex != -1)
            {
                CurrentVertex().transform.localScale = chosenScale;
            }
        
            SavesManager.SaveGame();
            
            InitVertexes();
        
            AudioManager.instance.Play(AudioEnum.Map);
        }

        public void OnDisable()
        {
            HideVertexes();
        }

        private void InitVertexes()
        {
            foreach (Vertex vertex in vertexes)
            {
                vertex.gameObject.SetActive(true);
            }
        }

        public void HideVertexes()
        {
            foreach (var vertex in vertexes.Where(vertex => vertex != null))
            {
                vertex.gameObject.SetActive(false);
            }
        }

        public static void Regenerate()
        {
            foreach (var vertex in vertexes.Where(v => v != null)) Destroy(vertex.gameObject);
            vertexes = MapGenerator.instance.GetMap(Globals.instance.seed);
        }

        public void OnClick(Vertex vertex)
        {
            if (currentVertex == -1)
            {
                if (vertexes.IndexOf(vertex) != 0) return;
            
                currentVertex = vertexes.IndexOf(vertex);
                vertex.ScaleUp(chosenScale, timeScale, OnScale);
            }
            else if (CurrentVertex().BelongsToNext(vertex))
            {
                CurrentVertex().ScaleDown(baseScale, timeScale);
                currentVertex = vertexes.IndexOf(vertex);
                vertex.ScaleUp(chosenScale, timeScale, OnScale);
            }

            void OnScale()
            {
                vertex.OnArrive();
            }
        }

        public static Vertex CurrentVertex()
        {
            return vertexes[currentVertex];
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