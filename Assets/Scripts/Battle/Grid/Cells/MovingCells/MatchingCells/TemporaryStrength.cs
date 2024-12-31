using System.Collections.Generic;
using Battle.Units;
using Battle.Units.StatModifiers;
using Other;
using UnityEngine;

namespace Battle.Grid.Cells.MovingCells.MatchingCells
{
    public class TemporaryStrength : Match3Cell
    {
        [SerializeField] private int shieldCount;
        [SerializeField] private int debuffAmount;
        
        public override string Description => descriptionKeyRef.Value.FormatByKeys(
            new Dictionary<string, object>
            {
                {"shieldCount", shieldCount},
                {"debuff", debuffAmount}
            });
        
        public override bool IsSameType(Cell other) => other is TemporaryStrength;

        protected override void Use()
        {
            var shield = new Units.StatModifiers.Shield(shieldCount);
            BattleFlowManager.Instance.CurrentlyTurningUnit.hp.onTakingDamageMods
                .Add(shield);
            
            // var counter = new MoveCounter(1);
            //
            // counter.OnMove += () =>
            //     BattleFlowManager.Instance.CurrentlyTurningUnit.hp.onTakingDamageMods.Add(
            //         new HpDamageMoveMod(debuffAmount, 1));

            BattleFlowManager.Instance.OnCycleEnd += GiveDebuff;
            return;

            void GiveDebuff()
            {
                Player.Instance.hp.onTakingDamageMods.Add(new HpDamageMoveMod(debuffAmount, 1));

                BattleFlowManager.Instance.OnCycleEnd -= GiveDebuff;
            }
        }
    }
}