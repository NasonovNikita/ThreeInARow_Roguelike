using System;
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
    /// <summary>
    ///     Generates a <see cref="Node"/> tree for <see cref="Map"/>.
    /// </summary>
    /// <seealso cref="GetMap"/>
    public class Generator : MonoBehaviour
    {
        [SerializeField] private int depth;

        [SerializeField] private int minWidth;
        [SerializeField] private int maxWidth;

        [SerializeField] private GameObject up;
        [SerializeField] private GameObject down;
        [SerializeField] private GameObject left;
        [SerializeField] private GameObject right;

        [SerializeField] private int goodsCountInShop;

        [SerializeField] private int treasureFrequency;

        private readonly Vector3 _dX = new(0.01f, 0, 0);
        private readonly Vector3 _dY = new(0, 0.01f, 0);

        private readonly List<(NodeType, int)> _vertexesByChance = new()
        {
            (NodeType.Battle, 13),
            (NodeType.Shop, 6)
        };

        private List<Good> _allGoods;

        private List<EnemyGroup> _enemyGroups = new();
        private static int Difficulty => (int)Globals.Instance.difficulty;
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

        /// <summary>
        ///     <see cref="Generate">Generates</see>,
        ///     <see cref="PlaceNodes">places</see> and
        ///     <see cref="ConnectVertexes">connects</see>
        ///     <see cref="Node">nodes</see>.
        /// </summary>
        /// <param name="seed">
        ///     Randomizer is initialized with this seed.
        ///     You get the same map with the same seed.
        /// </param>
        /// <returns>Disabled nodes, ready to be used.</returns>
        /// <seealso cref="NodeController">NodeController which uses those nodes</seealso>
        public List<Node> GetMap(int seed)
        {
            Random.InitState(seed);

            _enemyGroups = Resources.LoadAll<EnemyGroup>("EnemyGroups").ToList();

            _allGoods = Resources.LoadAll<Good>("Goods").ToList();

            var layers = Generate();

            PlaceNodes(layers);

            var connectedNodes = ConnectVertexes(layers, CreateBonds(layers));
            Tools.Random.ResetRandom();

            return connectedNodes;
        }

        /// <summary>
        ///     Generates layers of nodes one by one.
        /// </summary>
        /// <returns>Layers of generated nodes</returns>
        /// <remarks>
        ///     First layer is <see cref="BattleNode"/>,
        ///     once in <see cref="treasureFrequency"/> it is <see cref="TreasureNode"/>s layer,
        ///     last layer is Boss Battle.
        /// </remarks>
        private List<List<Node>> Generate()
        {
            List<List<Node>> layers = new() { new List<Node> { GenBattle(0) } };

            for (var i = 1; i < depth - 1; i++)
                layers.Add((i + 1) % treasureFrequency == 0
                    ? new List<Node>(GenTreasureLayer(i))
                    : GenLayer(i));

            layers.Add(new List<Node> { GenBoss(depth) });

            return layers;
        }

        /// <summary>
        ///     Connects nodes by given bonds.
        /// </summary>
        /// <param name="layers">Layers of nodes to be connected.</param>
        /// <param name="bonds">
        ///     Bonds for layers.
        /// </param>
        /// <returns>Connected nodes.</returns>
        /// <remarks>
        ///     Each list in <paramref name="bonds"/> is
        ///     <b>
        ///         list of <typeparamref name="(int, int)"/>
        ///         showing which pairs of nodes must be connected
        ///     </b>
        ///     (indexes).
        ///     (1, 3) would mean that node from one layer with index <b>1</b> would be connected
        ///     with node from another layer with index <b>3</b>.<br/>
        ///     E.g. <b>second</b> list states connections between <b>second</b> and <b>third</b> layers.<br/>
        /// </remarks>
        /// <seealso cref="CreateBonds"/>
        /// <seealso cref="Node.next">Node.next where the connections are applied.</seealso>
        private List<Node> ConnectVertexes(List<List<Node>> layers,
            List<List<(int, int)>> bonds)
        {
            List<Node> resultVertexes = new();

            List<List<Node>> layeredNodes = new();

            for (var i = 0; i < layers.Count; i++)
            {
                layeredNodes.Add(new List<Node>());
                for (var j = 0; j < layers[i].Count; j++)
                {
                    var node = layers[i][j];
                    layeredNodes[i].Add(node);
                    resultVertexes.Add(node);
                }
            }

            for (var i = 0; i < bonds.Count; i++)
            {
                var oldLayer = layeredNodes[i];
                var newLayer = layeredNodes[i + 1];
                foreach (var bounds in bonds[i])
                    oldLayer[bounds.Item1].next.Add(newLayer[bounds.Item2]);
            }

            return resultVertexes;
        }

        /// <summary>
        ///     Places nodes in given in <paramref name="layers"/> order.
        /// </summary>
        private void PlaceNodes(List<List<Node>> layers)
        {
            var yStep = (up.transform.position - down.transform.position) /
                        (layers.Count - 1);
            var width = layers.Max(layer => layer.Count);
            var xStep = (left.transform.position - right.transform.position) / width;
            for (var i = 0; i < layers.Count; i++)
            for (var j = 0; j < layers[i].Count; j++)
                PlaceLayer(layers[i], i, xStep, yStep);
        }

        private void PlaceLayer(List<Node> layer, int i, Vector3 xStep,
            Vector3 yStep)
        {
            var k = layer.Count;
            layer[0].transform.position =
                down.transform.position + i * yStep + ((float)k - 1) / 2 * -xStep;
            for (var j = 1; j < k; j++)
                layer[j].transform.position =
                    layer[j - 1].transform.position + xStep +
                    Random.Range(-25, 26) * _dX +
                    Random.Range(-25, 26) * _dY;
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

        private List<List<(int, int)>> CreateBonds(List<List<Node>> layers)
        {
            List<List<(int, int)>> bounds = new();
            for (var i = 0; i < depth - 1; i++)
                bounds.Add(Bind2Layers(layers[i], layers[i + 1]));

            return bounds;
        }

        private List<(int, int)> Bind2Layers(List<Node> oldLayer, List<Node> newLayer)
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
            var type = Tools.Random.RandomChooseWithChances(_vertexesByChance);

            return type switch
            {
                NodeType.Battle => GenBattle(layer),
                NodeType.Shop => GenShop(layer),
                NodeType.Treasure => GenTreasure(layer),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private BattleNode GenBattle(int layer) =>
            BattleNode.Create(layer, RandomSeed, false);

        public EnemyGroup ChooseBattleEnemyGroup(int layer)
        {
            var allowedGroups = _enemyGroups.Where(v => !v.isBoss).ToList();
            var battleDifficulty =
                Difficulty + layer * Difficulty / 3 + Random.Range(-5, 6);
            var chosenDifficulty = allowedGroups.Select(v => v.Difficulty).Aggregate(
                (min, next) =>
                    Math.Abs(min - battleDifficulty) < Math.Abs(next - battleDifficulty)
                        ? min
                        : next);

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
            var allowedGroups = _enemyGroups.Where(v => v.isBoss).ToList();
            return ChooseGroup(allowedGroups, layer);
        }

        private EnemyGroup ChooseGroup(IReadOnlyCollection<EnemyGroup> groups, int layer)
        {
            var battleDifficulty =
                Difficulty + layer * Difficulty / 3 + Random.Range(-5, 6);
            var chosenDifficulty = groups.Select(v => v.Difficulty).Aggregate(
                (min, next) =>
                    Math.Abs(min - battleDifficulty) < Math.Abs(next - battleDifficulty)
                        ? min
                        : next);

            return Tools.Random.RandomChoose(
                groups.Where(v => v.Difficulty == chosenDifficulty).ToList()
            );
        }

        private ShopNode GenShop(int layer) => ShopNode.Create(layer, RandomSeed);

        private TreasureNode GenTreasure(int layer) =>
            TreasureNode.Create(layer, RandomSeed);

        public List<Good> ChooseGoods(int layer)
        {
            List<Good> currentGoods = new();

            if (_allGoods.Count(good => !PlayerHasLootItem(good.target)) <=
                goodsCountInShop)
                return _allGoods.Where(good => !PlayerHasLootItem(good.target)).ToList();

            while (new HashSet<Good>(currentGoods).Count < goodsCountInShop)
            {
                currentGoods = new List<Good>();
                for (var i = 0; i < goodsCountInShop; i++)
                    currentGoods.Add(ChooseGood(layer));
            }

            return currentGoods;
        }

        private Good ChooseGood(int layer)
        {
            var chances = _allGoods.Where(good => !PlayerHasLootItem(good.target))
                .Select(good => (good, LayerPow(good.target.Frequency, layer))).ToList();

            return Tools.Random.RandomChooseWithChances(chances);
        }

        private bool PlayerHasLootItem(LootItem item) =>
            Player.Data.spells.Contains(item) || Player.Data.items.Contains(item);

        public LootItem ChooseTreasure(int layer)
        {
            var treasures = Resources.LoadAll<Good>("Goods")
                .Where(treasure => !PlayerHasLootItem(treasure.target))
                .Select(good => (tr: good, LayerPow(good.target.Frequency,
                    layer)))
                .ToList();

            return Tools.Random.RandomChooseWithChances(treasures).target;
        }

        private float LayerPow(int freq, int layer) =>
            (float)Math.Pow(freq, layer != 0 ? 1 / (float)layer : 1);

        private bool CrossExists(IEnumerable<(int, int)> bounds, int c, int d)
        {
            var res = false;

            foreach (var unused in bounds
                         .Where(bound => (bound.Item1 - c) * (bound.Item2 - d) < 0))
                res = true;

            return res;
        }
    }
}