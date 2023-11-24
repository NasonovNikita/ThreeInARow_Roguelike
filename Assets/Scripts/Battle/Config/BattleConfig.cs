
using Battle.Units;
using UI;
using UnityEngine;
using Grid = Battle.Match3.Grid;

namespace Battle.Config
{
    [CreateAssetMenu(fileName = "BattleConfig", menuName = "Battle Config")]
    public class BattleConfig : ScriptableObject
    {
        [SerializeField]
        private GameObject spells;
        [SerializeField]
        private Player player;
        [SerializeField]
        private EnemyPlacer enemies;
        [SerializeField]
        private Grid grid;
        public BattleConfigs mark;

        public void Apply()
        {
            Canvas canvas = FindFirstObjectByType<Canvas>();

            Player playerToMove = canvas.GetComponentInChildren<Player>();
            var playerTransform = player.transform;
            var plTransform = playerToMove.transform;
            plTransform.localPosition = playerTransform.localPosition;
            plTransform.localScale = playerTransform.localScale;

            Destroy(FindFirstObjectByType<SpellsContainer>().gameObject);
            Instantiate(spells, canvas.transform, false);
            
            Destroy(FindFirstObjectByType<EnemyPlacer>().gameObject);
            EnemyPlacer placer = Instantiate(enemies, canvas.transform, false);
            placer.Place();

            Grid oldGrid = FindFirstObjectByType<Grid>();
            Destroy(oldGrid.gameObject);
            Instantiate(grid);

            BattleManager manager = FindFirstObjectByType<BattleManager>();
            manager.ApplyConfig();
        }
    }

    public enum BattleConfigs
    {
        BaseConfig,
        BigGridConfig
    }
}