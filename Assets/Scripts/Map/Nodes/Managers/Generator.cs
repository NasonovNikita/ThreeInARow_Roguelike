using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Battle.Units;
using Core;
using Other;
using Shop;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Map.Nodes.Managers
{
    public class Generator : MonoBehaviour
    {
        public int difficulty;

        [SerializeField] private int depth;

        [SerializeField] private int minWidth;
        [SerializeField] private int maxWidth;

        [SerializeField] private GameObject up;
        [SerializeField] private GameObject down;
        [SerializeField] private GameObject left;
        [SerializeField] private GameObject right;

        [SerializeField] private int goodsCountInShop;

        [SerializeField] private int treasureFrequency;

        private readonly Vector3 dX = new(0.01f, 0, 0);
        private readonly Vector3 dY = new(0, 0.01f, 0);

        private readonly List<(NodeType, int)> vertexesByChance = new()
        {
            (NodeType.Battle, 13),
            (NodeType.Shop, 6)
            // (NodeType.Treasure, 3) TODO decide if needed
        };

        private List<Good> allGoods;

        private List<EnemyGroup> enemyGroups = new();
        public static Generator Instance { get; private set; }

        private int RandomSeed => Random.Range(0, 100000);

        public void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
        }

        public void Update()
        {
            difficulty = (int)Globals.Instance.difficulty;
        }

        public List<Node> GetMap(int seed)
        {
            Random.InitState(seed);

            enemyGroups = Resources.LoadAll<EnemyGroup>("EnemyGroups").ToList();

            allGoods = Resources.LoadAll<Good>("Goods").ToList();

            var layers = Generate();

            var bounds = BindLayers(layers);


            Tools.Random.ResetRandom();

            return InitMap(layers, bounds);
        }

        private List<List<Node>> Generate()
        {
            List<List<Node>> layers = new() { new List<Node> { GenBattle(0) } };

            for (var i = 1; i < depth - 1; i++)
                layers.Add((i + 1) % treasureFrequency == 0
                    ? new List<Node>(GenTreasureLayer(i))
                    : GenLayer(i));

            layers.Add(new List<Node> { GenBoss(depth) });

            PlaceNodes(layers);

            return layers;
        }

        private List<Node> InitMap(List<List<Node>> layers, List<List<(int, int)>> bonds)
        {
            List<Node> resultVertexes = new();

            List<List<Node>> layeredNodes = new();

            for (var i = 0; i < layers.Count; i++)
            {
                layeredNodes.Add(new List<Node>());
                for (var j = 0; j < layers[i].Count; j++)
                {
                    Node node = layers[i][j];
                    layeredNodes[i].Add(node);
                    resultVertexes.Add(node);
                }
            }

            for (var i = 0; i < bonds.Count; i++)
            {
                var oldLayer = layeredNodes[i];
                var newLayer = layeredNodes[i + 1];
                foreach ((int, int) bounds in bonds[i])
                    oldLayer[bounds.Item1].next.Add(newLayer[bounds.Item2]);
            }

            return resultVertexes;
        }

        private void PlaceNodes(IReadOnlyList<List<Node>> layers)
        {
            Vector3 yStep = (up.transform.position - down.transform.position) / (layers.Count - 1);
            var width = layers.Max(layer => layer.Count);
            Vector3 xStep = (left.transform.position - right.transform.position) / width;
            for (var i = 0; i < layers.Count; i++)
            for (var j = 0; j < layers[i].Count; j++)
                PlaceLayer(layers[i], i, xStep, yStep);
        }

        private void PlaceLayer(IReadOnlyList<Node> layer, int i, Vector3 xStep, Vector3 yStep)
        {
            var k = layer.Count;
            layer[0].transform.position =
                down.transform.position + i * yStep + ((float)k - 1) / 2 * -xStep;
            for (var j = 1; j < k; j++)
                layer[j].transform.position =
                    layer[j - 1].transform.position + xStep + Random.Range(-25, 26) * dX +
                    Random.Range(-25, 26) * dY;
        }

        private List<Node> GenLayer(int layer)
        {
            List<Node> resultLayer = new();

            var width = Random.Range(minWidth, maxWidth + 1);

            for (var i = 0; i < width; i++) resultLayer.Add(GenVertex(layer));

            return resultLayer;
        }

        private IEnumerable<TreasureNode> GenTreasureLayer(int layer)
        {
            List<TreasureNode> resultLayer = new();

            var width = Random.Range(minWidth, maxWidth + 1);

            for (var i = 0; i < width; i++) resultLayer.Add(GenTreasure(layer));

            return resultLayer;
        }

        private List<List<(int, int)>> BindLayers(List<List<Node>> layers)
        {
            List<List<(int, int)>> bounds = new();
            for (var i = 0; i < depth - 1; i++) bounds.Add(Bind2Layers(layers[i], layers[i + 1]));

            return bounds;
        }

        private List<(int, int)> Bind2Layers(ICollection oldLayer, ICollection newLayer)
        {
            HashSet<int> boundVertexes = new();

            List<(int, int)> bounds = new();

            while (boundVertexes.Count != oldLayer.Count + newLayer.Count)
            {
                var a = Random.Range(0, oldLayer.Count);
                var b = Random.Range(0, newLayer.Count);

                if (CrossExists(bounds, a, b) ||
                    bounds.Exists(pair => pair.Item1 == a && pair.Item2 == b)) continue;
                bounds.Add((a, b));
                boundVertexes.AddRange(new[] { a, -b - 1 });
            }

            return bounds;
        }

        private Node GenVertex(int layer)
        {
            NodeType type = Tools.Random.RandomChooseWithChances(vertexesByChance);

            return type switch
            {
                NodeType.Battle => GenBattle(layer),
                NodeType.Shop => GenShop(layer),
                NodeType.Treasure => GenTreasure(layer),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private BattleNode GenBattle(int layer)
        {
            return BattleNode.Create(layer, RandomSeed, false);
        }

        public EnemyGroup ChooseBattleEnemyGroup(int layer)
        {
            var allowedGroups = enemyGroups.Where(v => !v.isBoss).ToList();
            var battleDifficulty = difficulty + layer * difficulty / 3 + Random.Range(-5, 6);
            var chosenDifficulty = allowedGroups.Select(v => v.Difficulty).Aggregate((min, next) =>
                Math.Abs(min - battleDifficulty) < Math.Abs(next - battleDifficulty) ? min : next);

            return Tools.Random.RandomChoose(
                allowedGroups.Where(v => v.Difficulty == chosenDifficulty).ToList()
            );
        }

        private BattleNode GenBoss(int layer)
        {
            var vertex = BattleNode.Create(layer, RandomSeed, true);

            return vertex;
        }

        public EnemyGroup ChooseBoss(int layer)
        {
            var allowedGroups = enemyGroups.Where(v => v.isBoss).ToList();
            return ChooseGroup(allowedGroups, layer);
        }

        private EnemyGroup ChooseGroup(IReadOnlyCollection<EnemyGroup> groups, int layer)
        {
            var battleDifficulty = difficulty + layer * difficulty / 3 + Random.Range(-5, 6);
            var chosenDifficulty = groups.Select(v => v.Difficulty).Aggregate((min, next) =>
                Math.Abs(min - battleDifficulty) < Math.Abs(next - battleDifficulty) ? min : next);

            return Tools.Random.RandomChoose(
                groups.Where(v => v.Difficulty == chosenDifficulty).ToList()
            );
        }

        private ShopNode GenShop(int layer)
        {
            return ShopNode.Create(layer, RandomSeed);
        }

        private TreasureNode GenTreasure(int layer)
        {
            return TreasureNode.Create(layer, RandomSeed);
        }

        public List<Good> ChooseGoods(int layer)
        {
            List<Good> currentGoods = new();

            if (allGoods.Count(good => !PlayerHasLootItem(good.target)) <= goodsCountInShop)
                return allGoods.Where(good => !PlayerHasLootItem(good.target)).ToList();

            while (new HashSet<Good>(currentGoods).Count < goodsCountInShop)
            {
                currentGoods = new List<Good>();
                for (var i = 0; i < goodsCountInShop; i++) currentGoods.Add(ChooseGood(layer));
            }

            return currentGoods;
        }

        private Good ChooseGood(int layer)
        {
            var chances = allGoods.Where(good => !PlayerHasLootItem(good.target))
                .Select(good => (good, LayerPow(good.target.Frequency, layer))).ToList();

            return Tools.Random.RandomChooseWithChances(chances);
        }

        private bool PlayerHasLootItem(LootItem item)
        {
            return Player.data.spells.Contains(item) || Player.data.items.Contains(item);
        }

        public LootItem ChooseTreasure(int layer)
        {
            var treasures = Resources.LoadAll<Good>("Goods")
                .Where(treasure => !PlayerHasLootItem(treasure.target))
                .Select(good => (tr: good, LayerPow(good.target.Frequency,
                    layer)))
                .ToList();

            return Tools.Random.RandomChooseWithChances(treasures).target;
        }

        private float LayerPow(int freq, int layer)
        {
            return (float)Math.Pow(freq, layer != 0 ? 1 / (float)layer : 1);
        }

        private bool CrossExists(IEnumerable<(int, int)> bounds, int c, int d)
        {
            var res = false;

            foreach ((int, int) unused in bounds
                         .Where(bound => (bound.Item1 - c) * (bound.Item2 - d) < 0))
                res = true;

            return res;
        }
    }
}