using System.Collections.Generic;
using System.Linq;
using Battle.Units.StatModifiers;
using Other;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Battle.Grid.Cells
{
    public class PanicButton : Cell, IPointerClickHandler
    {
        [SerializeField] private int cooldownMoves;
        [SerializeField] private int startingCooldown;
        [SerializeField] private int shieldCount;
        private MoveCounter _counter;

        public override string Description => descriptionKeyRef.Value.FormatByKeys(
            new Dictionary<string, object>
            {
                {"count", shieldCount},
                {"cooldown", cooldownMoves},
                {"startCooldown", startingCooldown}
            });

        public void Awake()
        {
            _counter = new MoveCounter(startingCooldown);
        }

        public override bool BoxIsStable(Cell[,] box) =>
            Tools.MultiDimToOne(box).Count(cell => cell is PanicButton) == 1;

        public override bool IsSameType(Cell other) => other is PanicButton;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (_counter.Moves > 0) return;

            BattleFlowManager.Instance.CurrentlyTurningUnit.hp.onTakingDamageMods.Add(
                new Shield(shieldCount));

            _counter = new MoveCounter(cooldownMoves);
        }
    }
}