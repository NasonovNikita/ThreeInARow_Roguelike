using UI.Battle;
using UnityEngine;

namespace Battle.Items
{
    [CreateAssetMenu(fileName = "LongScythe", menuName = "Items/LongBraid")]
    public class LongScythe : Item
    {
        public override void OnGet()
        {
            PickerManager.SetAllAvailable();
        }
    }
}