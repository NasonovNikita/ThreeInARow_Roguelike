using Battle.Units;
using UI.Battle;
using UnityEngine;

namespace Battle.Items
{
    [CreateAssetMenu(fileName = "LongScythe", menuName = "Items/LongBraid")]
    public class LongScythe : Item
    {
        public override void Use(Unit unitBelong)
        {
            BattleTargetPicker.SetAllRawsAvailable();
        }

        public override string Title => "LongScythe";

        public override string Description => "You can hit enemies from any raw";
    }
}