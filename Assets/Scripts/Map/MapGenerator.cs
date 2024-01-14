using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using Battle.Units;
using Core;
using Map.Vertexes;
using Other;
using Shop;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Map
{
    public class MapGenerator : MonoBehaviour
    {
        public static MapGenerator instance;
        
        public int difficulty;
        
        [SerializeField] private int depth;

        [SerializeField] private int minWidth;
        [SerializeField] private int maxWidth;

        [SerializeField] private GameObject up;
        [SerializeField] private GameObject down;
        [SerializeField] private GameObject left;
        [SerializeField] private GameObject right;

        private readonly Vector3 dX = new (0.01f, 0, 0);
        private readonly Vector3 dY = new (0, 0.01f, 0);

        private List<EnemyGroup> enemyGroups = new();
    
        private readonly List<(VertexType, int)> vertexesByChance = new ()
        {
            (VertexType.Battle, 13),
            (VertexType.Shop, 9),
            //(VertexType.Treasure, 3)
        };

        [SerializeField] private int treasureFrequency;

        private List<Good> allGoods;
        
        public void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        
            DontDestroyOnLoad(gameObject);
        }

        public void Update()
        {
            difficulty = (int) Globals.instance.difficulty;
        }

        public List<Vertex> GetMap(int seed)
        {
            Random.InitState(seed);

            enemyGroups = Resources.LoadAll<EnemyGroup>("Presets/EnemyGroups").ToList();

            allGoods = Resources.LoadAll<Good>("Goods").ToList();

            var layers =  Generate();
            
            var bounds = BindLayers(layers);

        
            Random.InitState((int) DateTime.Now.Ticks);
            
            return InitMap(layers, bounds);
        }

        private List<List<Vertex>> Generate()
        {
            List<List<Vertex>> layers = new() { new List<Vertex> { GenBattle(0)} };

            for (int i = 1; i < depth - 1; i++)
            {
                layers.Add((i + 1) % treasureFrequency == 0 ? new List<Vertex>(GenTreasureLayer(i)) : GenLayer(i));
            }

            layers.Add(new List<Vertex> { GenBoss(depth) });
        
            PlaceVertexes(layers);

            return layers;
        }
        
        private List<Vertex> InitMap(List<List<Vertex>> layers, List<List<(int, int)>> bonds)
        {
            List<Vertex> resultVertexes = new();
            
            List<List<Vertex>> layeredVertexes = new();
            
            for (int i = 0; i < layers.Count; i++)
            {
                layeredVertexes.Add(new List<Vertex>());
                for (int j = 0; j < layers[i].Count; j++)
                {
                    var vertex = layers[i][j];
                    layeredVertexes[i].Add(vertex);
                    resultVertexes.Add(vertex);
                }
            }

            for (int i = 0; i < bonds.Count; i++)
            {
                var oldLayer = layeredVertexes[i];
                var newLayer = layeredVertexes[i + 1];
                foreach (var bounds in bonds[i])
                {
                    oldLayer[bounds.Item1].next.Add(newLayer[bounds.Item2]);
                }
            }

            return resultVertexes;
        }

        private void PlaceVertexes(IReadOnlyList<List<Vertex>> layers)
        {
            Vector3 yStep = (up.transform.position - down.transform.position) / (layers.Count - 1);
            int width = layers.Max(layer => layer.Count);
            Vector3 xStep = (left.transform.position - right.transform.position) / width;
            for (int i = 0; i < layers.Count; i++)
            {
                for (int j = 0; j < layers[i].Count; j++)
                {
                    PlaceLayer(layers[i], i, xStep, yStep);
                }
            }
        }

        private void PlaceLayer(IReadOnlyList<Vertex> layer, int i, Vector3 xStep, Vector3 yStep)
        {
            int k = layer.Count;
            layer[0].transform.position = down.transform.position + i * yStep + ((float) k - 1) / 2 * -xStep;
            for (int j = 1; j < k; j++)
            {
                layer[j].transform.position = layer[j - 1].transform.position + xStep + Random.Range(-25, 26) * dX +
                                    Random.Range(-25, 26) * dY;
            }
        }

        private List<Vertex> GenLayer(int layer)
        {
            List<Vertex> resultLayer = new();

            int width = Random.Range(minWidth, maxWidth + 1);

            for (int i = 0; i < width; i++)
            {
                resultLayer.Add(GenVertex(layer));
            }

            return resultLayer;
        }

        private List<TreasureVertex> GenTreasureLayer(int layer)
        {
            List<TreasureVertex> resultLayer = new();

            int width = Random.Range(minWidth, maxWidth + 1);

            for (int i = 0; i < width; i++)
            {
                resultLayer.Add(GenTreasure(layer));
            }

            return resultLayer;
        }

        private List<List<(int, int)>> BindLayers(List<List<Vertex>> layers)
        {
            List<List<(int, int)>> bounds = new();
            for (int i = 0; i < depth - 1; i++)
            {
                bounds.Add(Bind2Layers(layers[i], layers[i + 1]));
            }

            return bounds;
        }

        private static List<(int, int)> Bind2Layers(ICollection oldLayer, ICollection newLayer)
        {
            HashSet<int> boundVertexes = new();

            List<(int, int)> bounds = new();

            while (boundVertexes.Count != oldLayer.Count + newLayer.Count)
            {
                int a = Random.Range(0, oldLayer.Count);
                int b = Random.Range(0, newLayer.Count);

                if (CrossExists(bounds, a, b) ||
                    bounds.Exists(pair => pair.Item1 == a && pair.Item2 == b)) continue;
                bounds.Add((a, b));
                boundVertexes.AddRange(new[] { a, -b - 1 });
            }

            return bounds;
        }

        private BattleVertex GenBattle(int layer)
        {
            var vertex = BattleVertex.Create();
            var allowedGroups = enemyGroups.Where(v => !v.isBoss).ToList();

            int battleDifficulty = difficulty + layer * difficulty / 3 + Random.Range(-5, 6);
            int chosenDifficulty = allowedGroups.Select(v => v.Difficulty).Aggregate((min, next) =>
                Math.Abs(min - battleDifficulty) < Math.Abs(next - battleDifficulty) ? min : next);

            vertex.group = Tools.Random.RandomChoose(
                allowedGroups.Where(v => v.Difficulty == chosenDifficulty).ToList()
                );

            return vertex;
        }

        private ShopVertex GenShop(int layer)
        {
            var vertex = ShopVertex.Create();

            List<Good> currentGoods = new();

            for (int i = 0; i < 4; i++)
            {
                currentGoods.Add(ChooseGood(layer));
            }

            vertex.goods = currentGoods;

            return vertex;
        }

        private TreasureVertex GenTreasure(int layer)
        {
            TreasureVertex vertex = TreasureVertex.Create();

            GetAble treasure = ChooseTreasure(layer);

            vertex.treasure = treasure;
            
            return vertex;
        }

        private BattleVertex GenBoss(int layer)
        {
            var vertex = BattleVertex.Create();
            var allowedGroups = enemyGroups.Where(v => v.isBoss).ToList();

            int battleDifficulty = difficulty + layer * difficulty / 3 + Random.Range(-5, 6);
            int chosenDifficulty = allowedGroups.Select(v => v.Difficulty).Aggregate((min, next) =>
                Math.Abs(min - battleDifficulty) < Math.Abs(next - battleDifficulty) ? min : next);

            vertex.group = Tools.Random.RandomChoose(
                allowedGroups.Where(v => v.Difficulty == chosenDifficulty).ToList()
            );

            return vertex;
        }

        private Good ChooseGood(int layer)
        {
            List<(Good, int)> chances = allGoods
                .Select(good => (good, (int) Math.Pow(good.target.frequency, -layer))).ToList();

            return Tools.Random.RandomChooseWithChances(chances);
        }

        private GetAble ChooseTreasure(int layer)
        {
            var treasures = Resources.LoadAll<Good>("Goods")
                .Select(good => (tr: good, (int) Math.Pow(good.target.frequency, -layer))).ToList();

            return Tools.Random.RandomChooseWithChances(treasures).target;
        }

        private Vertex GenVertex(int layer)
        {
            VertexType type = Tools.Random.RandomChooseWithChances(vertexesByChance);

            return type switch
            {
                VertexType.Battle => GenBattle(layer),
                VertexType.Shop => GenShop(layer),
                VertexType.Treasure => GenTreasure(layer),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private static bool CrossExists(List<(int, int)> bounds, int c, int d)
        {
            bool res = false;

            foreach (var unused in bounds
                         .Where(bound => (bound.Item1 - c) * (bound.Item2 - d) < 0))
            {
                res = true;
            }

            return res;
        }
    }
}