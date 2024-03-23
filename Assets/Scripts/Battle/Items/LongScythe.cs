using Battle.Units;
using UI.Battle;
using UnityEngine;

namespace Battle.Items
{
    [CreateAssetMenu(fileName = "LongScythe", menuName = "Items/LongBraid")]
    public class LongScythe : Item
    {
        public override string Title => titleKeyRef.Value;

        public override string Description => descriptionKeyRef.Value;

        public override void Get()
        {
            PickerManager.SetAllAvailable();
            base.Get();
        }
    }
}