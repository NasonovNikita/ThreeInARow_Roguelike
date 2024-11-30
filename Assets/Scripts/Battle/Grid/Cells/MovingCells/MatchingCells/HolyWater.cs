using System.Linq;

namespace Battle.Grid.Cells.MovingCells.MatchingCells
{
    public class HolyWater : Match3Cell
    {
        public override string Description => descriptionKeyRef.Value;
        public override bool IsSameType(Cell other) => other is HolyWater;

        protected override void Use()
        {
            foreach (var mod in BattleFlowManager.Instance.CurrentlyTurningUnit
                         .AllModifierLists.SelectMany(modifierList => modifierList.List).Where(mod => !mod.isSaved))
            {
                mod.Kill();
            }
        }
    }
}